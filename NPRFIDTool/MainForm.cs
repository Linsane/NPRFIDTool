using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPRFIDTool.NPKit;
using System.Collections;

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


        private void MainForm_Load(object sender, EventArgs e)
        {
            manager = new NPRFIDReaderManager(); 
            // 检查配置

            // 启动读取周期

            // 数据库
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
    }
}
