using System;
using System.Collections.Generic;
using System.Linq;
using ModuleTech;
using System.Collections;

namespace NPRFIDTool.NPKit
{
    enum ReaderStatus { readerStatusStop, readerStatusReading };
    class WrapReader
    {
        public Reader reader;
        public ArrayList checkPorts;
        public bool isReading;
    }

    class NPRFIDReaderManager
    {
        private System.Timers.Timer cycleTimer = null;
        private System.Timers.Timer readPortTimer = null;
        private bool handleInStorePort = false;
        private Dictionary<string, string> checkedDict = new Dictionary<string, string>();
        private ArrayList inStoreTags = new ArrayList();

        public Dictionary<string, WrapReader> readerDict = new Dictionary<string, WrapReader>();

        public void startReading(NPRFIDReaderInfo readerInfo)
        {
            Reader reader;
            WrapReader wrapReader = null;

            bool readerExist = isReaderExist(readerInfo);
            if (!readerExist)
            {

                reader = createRFIDReader(readerInfo);
                wrapReader = new WrapReader();
                wrapReader.reader = reader;
                wrapReader.checkPorts = new ArrayList();
                wrapReader.isReading = false;
                readerDict.Add(readerInfo.readerIP, wrapReader);
            }
            else
            {
                wrapReader = readerDict[readerInfo.readerIP];
                reader = wrapReader.reader;
                // 判断正在使用的端口，更新正在使用端口信息
                SimpleReadPlan readPlan = (SimpleReadPlan)reader.ParamGet("ReadPlan");
                ArrayList list = new ArrayList(readerInfo.usedPorts);
                int[] usedAnts = readPlan.Antennas;
                foreach (int ant in usedAnts)
                {
                    if (!list.Contains(ant))
                    {
                        list.Add(ant);
                    }
                }
                int[] useants = (int[])list.ToArray(typeof(int));
                reader.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, useants));
            }

            // 如果是盘点Reader，则记录好盘点所用的端口
            ArrayList arrayList = null;
            if (readerInfo.portType == PortType.PortTypeCheck)
            {
                wrapReader.checkPorts = new ArrayList(readerInfo.usedPorts);
            }

            if (!wrapReader.isReading)
            {
                reader.StartReading();
                wrapReader.isReading = true;
            }
            
        }

        public void stopReading(NPRFIDReaderInfo readerInfo)
        {
            // 当需要停的时候，判断下是否还有其它Reader的端口在，有的话移除自己的端口即可，不用停止读取RFID，否则停止停止读取RFID
            WrapReader wrapReader = readerDict[readerInfo.readerIP];
            Reader reader = wrapReader.reader;

            SimpleReadPlan readPlan = (SimpleReadPlan)reader.ParamGet("ReadPlan");
            ArrayList list = new ArrayList(readerInfo.usedPorts);
            int[] usedAnts = readPlan.Antennas;

            if (usedAnts.Length <= readerInfo.usedPorts.Count) // 没有其它端口在读了
            {
                reader.StopReading();
                wrapReader.isReading = false;
            }

            List<int> arr = usedAnts.ToList();
            foreach(int ant in readerInfo.usedPorts)
            {
                arr.Remove(ant);
            }

            reader.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, arr.ToArray()));
        }

        // 根据IP判断是否已经存在对应Reader
        private bool isReaderExist(NPRFIDReaderInfo readerInfo)
        {
            bool exist = false;
            foreach (string key in readerDict.Keys)
            {
                if (key == readerInfo.readerIP)
                {
                    exist = true;
                }
            }
            return exist;
        }

        #region 创建RFID连接

        public Reader createRFIDReader(NPRFIDReaderInfo readerInfo)
        {
            int antnum = readerInfo.readerAntNum;
            Reader reader = Reader.Create(readerInfo.readerIP, ModuleTech.Region.CN, antnum);

            #region 必须要设置的参数

            #region 设置读写器的发射功率
            //获取读写器最大发射功率
            int maxp = (int)reader.ParamGet("RfPowerMax");
            AntPower[] pwrs = new AntPower[antnum];
            for (int i = 0; i < antnum; ++i)
            {
                pwrs[i].AntId = (byte)(i + 1);
                pwrs[i].ReadPower = (ushort)maxp;
                pwrs[i].WritePower = (ushort)maxp;
            }
            //设置读写器发射功率,本例设置为最大发射功率，可根据实际情况调整,
            //一般来说，功率越大则识别距离越远
            reader.ParamSet("AntPowerConf", pwrs);
            #endregion

            #region 设置盘存标签使用的天线
            //如果要使用其它天线可以在数组useants中放置其它多个天线编号，本例中是使用天线1

            //int[] useants = new int[] { 1 };
            int[] useants = (int[])readerInfo.usedPorts.ToArray(typeof(int));
            reader.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, useants));
            #endregion

            #region 设置盘存操作的其它细节选项
            BackReadOption bro = new BackReadOption();
            BackReadOption.FastReadTagMetaData frtmdata = new BackReadOption.FastReadTagMetaData();
            /*是否采用高速模式（目前只有slr11xx和slr12xx系列读写器才支持）,对于
            *一般标签数量不大，速度不快的应用没有必要使用高速模式,本例没有设置
            *使用高速模式
            * */
            bro.IsFastRead = false;
            #endregion

            #region 非高速模式才起作用的选项
            //盘存周期,单位为ms，可根据实际使用的天线个数按照每个天线需要200ms
            //的方式计算得出,如果启用高速模式则此选项没有任何意义，可以设置为
            //任意值，或者干脆不设置
            bro.ReadDuration = (ushort)(200 * useants.Length);
            //盘存周期间的设备不工作时间,单位为ms,一般可设置为0，增加设备不工作
            //时间有利于节电和减少设备发热（针对某些使用电池供电或空间结构不利
            //于散热的情况会有帮助）
            bro.ReadInterval = 0;

            bro.FRTMetadata = frtmdata;
            reader.ParamSet("BackReadOption", bro);
            #endregion

            #endregion

            #region 必须要设置的事件处理函数
            //识别到标签事件的处理函数
            reader.ReadException += OnReaderException;
            //读写器异常发生事件的处理函数
            reader.TagsRead += OnTagsRead;

            return reader;
            #endregion
        }

        // 端口数据处理逻辑
        delegate void DataFromPortDelegate(TagReadData[] tags, Reader reader);
        private void processTagData(TagReadData[] tags, Reader reader)
        {
            ArrayList checkPorts = readerDict[reader.Address].checkPorts;
            foreach (TagReadData tag in tags)
            {   
                if (checkPorts.Contains(tag.Antenna)) // 盘点端口数据
                {
                    Console.WriteLine(tag.EPCString + "盤點端口");
                }
                else // 入库端口数据
                {
                    Console.WriteLine(tag.EPCString + "入庫端口");
                }
            }

        }

        private void OnTagsRead(object sender, Reader.TagsReadEventArgs tagsArgs)
        {
            Reader rdrtmp = (Reader)sender;
            DataFromPortDelegate dataFromPort = new DataFromPortDelegate(processTagData);
            dataFromPort(tagsArgs.Tags, rdrtmp);
        }

        private void OnReaderException(object sender, Reader.ReadExceptionEventArgs expArgs)
        {
            Reader rdrtmp = (Reader)sender;
            //如果需要可在此处记录异常日志
            Console.WriteLine(rdrtmp.Address + "--异常信息:" + expArgs.ReaderException.ToString());
        }

        #endregion

        /*
        // 启动定时扫描周期
        private void startCycleTimer()
        {
            if (cycleTimer == null)
            {
                cycleTimer = new System.Timers.Timer();
                cycleTimer.Enabled = true;
                cycleTimer.AutoReset = false;
                cycleTimer.Interval = 180000; // 可配置参数：定时扫描周期
                cycleTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.startRFIDReading);
            }

            cycleTimer.Start();
        }

        // 启动读取RFID端口周期，周期结束重新启动定时扫描周期
        private void startRFIDReading(object source, ElapsedEventArgs e)
        {
            if (readPortTimer == null)
            {
                readPortTimer = new System.Timers.Timer();
                readPortTimer.Enabled = true;
                readPortTimer.AutoReset = false;
                readPortTimer.Interval = 5000; // 可配置参数：扫描天线周期
                readPortTimer.Elapsed += new System.Timers.ElapsedEventHandler(stopRFIDReading);
            }
            readPortTimer.Start();
            reader.StartReading();
        }
        // 上传入库端口数据

        // 将盘点数据写入数据库
        dbManager.appendDataToDataBase(TableType.TableTypeCheck, checkedDict);
        }

        // 更新checked数据记录
        private void updateCheckedData(TagReadData tag)
        {
            bool isNew = false;
            foreach (string key in checkedDict.Keys)
            {
                if (tag.EPCString == key)
                {
                    isNew = true;
                }
            }
            if (isNew)
            {
                checkedDict.Add(tag.EPCString, tag.Time.ToString());
            }
            else
            {
                checkedDict[tag.EPCString] = tag.Time.ToString();
            }
        }
        */
    }
}
