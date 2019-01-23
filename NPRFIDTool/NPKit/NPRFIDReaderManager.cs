using System;
using System.Collections.Generic;
using System.Linq;
using ModuleTech;
using ModuleTech.Gen2;
using ModuleLibrary;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NPRFIDTool.NPKit
{
    enum ReaderStatus { readerStatusStop, readerStatusReading };
    delegate void CreatReaderFailHandler(Exception ex);
    delegate void ReaderPortFail(string msg);
    class WrapReader
    {
        public Reader reader;
        public JArray checkPorts;
        public bool isReading;
        public int[] validPorts;
    }

    class NPRFIDReaderManager
    {
        public JObject checkedDict = new JObject();
        public JObject inStoreDict = new JObject();

        public Dictionary<string,WrapReader> readerDict = new Dictionary<string, WrapReader>();
        public CreatReaderFailHandler failHandler;
        public ReaderPortFail portFailHandler;

        public int scanType = -1; // 扫描类型 0 表示入库 1表示出库

        public void beginReading(NPRFIDReaderInfo readerInfo)
        {
            Reader reader = null;
            WrapReader wrapReader = null;

            bool readerExist = isReaderExist(readerInfo);
            if (!readerExist)
            {
                try
                {
                    reader = createRFIDReader(readerInfo);
                    wrapReader = new WrapReader();
                    wrapReader.reader = reader;
                    wrapReader.checkPorts = new JArray();
                    wrapReader.isReading = false;
                    wrapReader.validPorts = (int[])reader.ParamGet("ConnectedAntennas");
                    readerDict.Add(readerInfo.readerIP, wrapReader);
                    
                }
                catch (Exception ex)
                {
                    failHandler(ex);
                }
            }
            else
            {
                wrapReader = readerDict[readerInfo.readerIP];
                reader = wrapReader.reader;
                // 判断正在使用的端口，更新正在使用端口信息
                SimpleReadPlan readPlan = (SimpleReadPlan)reader.ParamGet("ReadPlan");
                List<int> list = readerInfo.usedPorts.ToObject<List<int>>();
                int[] usedAnts = readPlan.Antennas;
                foreach (int ant in usedAnts)
                {
                    if (!list.Contains(ant))
                    {
                        list.Add(ant);
                    }
                }
                int[] useants = list.ToArray();
                reader.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, useants));
            }

            if (reader == null) return;

            // 判断端口是否可用

            int[] usedPorts = readerInfo.usedPorts.ToObject<int[]>();
            // 如果是盘点Reader，则记录好盘点所用的端口
            ArrayList arrayList = null;
            if (readerInfo.portType == PortType.PortTypeCheck)
            {
                foreach (int port in usedPorts)
                {
                    if (Array.IndexOf(wrapReader.validPorts, port) == -1)
                    {
                        portFailHandler("盘点天线选择有误");
                        return;
                    }
                }
                wrapReader.checkPorts = new JArray(readerInfo.usedPorts);
            }

            if(readerInfo.portType == PortType.PortTypeInStore)
            {
                foreach (int port in usedPorts)
                {
                    if (Array.IndexOf(wrapReader.validPorts, port) == -1)
                    {
                        portFailHandler("入库天线选择有误");
                        return;
                    }
                }
                inStoreDict.RemoveAll();
            }

            if (!wrapReader.isReading)
            {
                reader.StartReading();
                wrapReader.isReading = true;
            }
            
        }

        public void endReading(NPRFIDReaderInfo readerInfo)
        {
            if (readerDict == null || readerDict.Keys.Count <= 0) return;
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
            foreach (var item in readerDict)
            {
                if (readerInfo.readerIP == item.Key)
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
            int[] usedPorts = readerInfo.usedPorts.ToObject<int[]>();
            for (int i = 0; i < antnum; ++i)
            {
                pwrs[i].AntId = (byte)(i + 1);
                if (readerInfo.portType == PortType.PortTypeCheck && Array.IndexOf(usedPorts,i+1) != -1)
                {
                    pwrs[i].ReadPower = (ushort)maxp;
                    pwrs[i].WritePower = (ushort)maxp;
                }
                else
                {
                    pwrs[i].ReadPower = readerInfo.portPower;
                    pwrs[i].WritePower = readerInfo.portPower;
                }
               
            }
            //设置读写器发射功率,本例设置为最大发射功率，可根据实际情况调整,
            //一般来说，功率越大则识别距离越远
            reader.ParamSet("AntPowerConf", pwrs);
            #endregion

            #region 设置盘存标签使用的天线
            //如果要使用其它天线可以在数组useants中放置其它多个天线编号，本例中是使用天线1

            //int[] useants = new int[] { 1 };
            int[] useants = readerInfo.usedPorts.ToObject<int[]>();
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
            WrapReader wrapReader = readerDict[reader.Address];
            List<int> checkPorts = wrapReader.checkPorts.ToObject<List<int>>(); ;
            JArray sendNeededTags = new JArray();
            foreach (TagReadData tag in tags)
            {   
                if (checkPorts.Contains(tag.Antenna)) // 盘点端口数据
                {
                    // 判断是否已经存在这个数据，没有记录checkDict
                    updateCheckedData(tag);
                }
                else // 入库端口数据
                {
                    // 判断是否已经存在这个数据，没有记录到inStoreTags并上报
                    if (!isTagExist(tag.EPCString))
                    {
                        inStoreDict.Add(tag.EPCString, DateTime.Now.ToString());
                        sendNeededTags.Add(tag.EPCString);
                    }
                }
            }
            if (sendNeededTags.Count > 0 && scanType != -1)
            {
                NPWebSocket.sendTagData(sendNeededTags, scanType);
                string msg = "上报" + (scanType == 1 ? "出库" : "入库") + "数据:";
                foreach(string tag in sendNeededTags)
                {
                    Console.WriteLine(msg + tag);
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

        // 判断当前是否已记录并处理这个tag
        private bool isTagExist(string tag)
        {
            bool exist = false;
            foreach (var item in inStoreDict)
            {
                if (item.Key == tag)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        // 更新checked数据记录
        private void updateCheckedData(TagReadData tag)
        {
            bool isNew = true;
            foreach (var item in checkedDict)
            {
                if (tag.EPCString == item.Key)
                {
                    isNew = false;
                }
            }
            if (isNew)
            {
                checkedDict.Add(tag.EPCString, tag.Time.ToString());
                Console.WriteLine("记录盘点端口数据:" + tag.EPCString);
            }
            else
            {
                checkedDict[tag.EPCString] = tag.Time.ToString();
            }
        }

        // 返回盘点结果
        public JArray getDiffTagsArray(JObject remainData)
        {

            JArray diffArray = new JArray();
            foreach(var item in remainData)
            {
                if (!checkedDict.ContainsKey(item.Key))
                {
                    diffArray.Add(item.Key);
                }
            }
            return diffArray;
        }

        public void clear()
        {
            checkedDict.RemoveAll();
            inStoreDict.RemoveAll();
        }
    }
}
