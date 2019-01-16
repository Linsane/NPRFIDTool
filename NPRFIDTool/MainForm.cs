using System;
using System.Drawing;
using System.Windows.Forms;
using NPRFIDTool.NPKit;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace NPRFIDTool
{
    public partial class MainForm : Form
    {
        private NPDBManager dbManager = null;
        NPRFIDReaderInfo inStoreInfo = null;
        NPRFIDReaderInfo checkInfo = null;
        NPRFIDReaderManager manager = null;

        public MainForm()
        {
            InitializeComponent();
        }


        private void systemConfigGroup_Paint(object sender, PaintEventArgs e)
        {
            string title = "系统环境配置";
            SizeF fontSize = e.Graphics.MeasureString(title, systemConfigGroup.Font);
            e.Graphics.DrawString(title, systemConfigGroup.Font, Brushes.Black, (systemConfigGroup.Width - fontSize.Width) / 2, 1);
        }

        private void portsConfigGroupBox_Paint(object sender, PaintEventArgs e)
        {
            string title = "天线配置";
            SizeF fontSize = e.Graphics.MeasureString(title, systemConfigGroup.Font);
            e.Graphics.DrawString(title, systemConfigGroup.Font, Brushes.Black, (systemConfigGroup.Width - fontSize.Width) / 2, 1);
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            manager = new NPRFIDReaderManager();
            // 检查配置

            // 启动读取周期

            // 数据库

            //NPBackendService.WebSocketConnect();

            NPBackendService service = new NPBackendService("192.168.100.188", "RFID0012");
            //RspHandler handler = new RspHandler(getStockInitHandler);
            //service.getStockInit(handler);
        }

        void getStockInitHandler(JObject obj)
        {
            MessageBox.Show("success" + obj.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (inStoreInfo == null)
            {
                NPRFIDReaderInfo info = new NPRFIDReaderInfo();
                info.readerIP = "192.168.100.188";
                info.readerAntNum = 4;
                info.portType = PortType.PortTypeInStore;
                info.usedPorts = new ArrayList();
                info.usedPorts.Add(1);
                inStoreInfo = info;
            }
            
            try
            {
                manager.startReading(inStoreInfo);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("连接读写器失败:" + ex.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            manager.stopReading(inStoreInfo);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkInfo == null)
            {
                NPRFIDReaderInfo info = new NPRFIDReaderInfo();
                info.readerIP = "192.168.100.188";
                info.readerAntNum = 4;
                info.portType = PortType.PortTypeCheck;
                info.usedPorts = new ArrayList();
                info.usedPorts.Add(2);
                checkInfo = info;
            }

            try
            {
                manager.startReading(checkInfo);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("连接读写器失败:" + ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            manager.stopReading(checkInfo);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
