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
        public MainForm()
        {
            InitializeComponent();
        }

        private NPConfigManager configManager;
        private RadioButton[] inStoreRadioList;
        private CheckBox[] inStoreCheckBoxList;
        private RadioButton[] checkRadioList;
        private CheckBox[] checkCheckBoxList;

        #region 界面改动
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

        private void portsCountGroupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void portsCountGroupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            #region 控件管理
            inStoreRadioList = new RadioButton[]
            {
                inStoreRadio1,inStoreRadio2,inStoreRadio3,inStoreRadio4,inStoreRadio5,inStoreRadio6
            };
            inStoreCheckBoxList = new CheckBox[]
            {
                inStoreCheckBox1,inStoreCheckBox2,inStoreCheckBox3,inStoreCheckBox4,inStoreCheckBox5,inStoreCheckBox6,inStoreCheckBox7,inStoreCheckBox8,inStoreCheckBox9,inStoreCheckBox10,inStoreCheckBox11,inStoreCheckBox12,inStoreCheckBox13,inStoreCheckBox14,inStoreCheckBox15,inStoreCheckBox16
            };
            checkRadioList = new RadioButton[]
{
                checkRadio1,checkRadio2,checkRadio3,checkRadio4,checkRadio5,checkRadio6
};
            checkCheckBoxList = new CheckBox[]
            {
                checkCheckBox1,checkCheckBox2,checkCheckBox3,checkCheckBox4,checkCheckBox5,checkCheckBox6,checkCheckBox7,checkCheckBox8,checkCheckBox9,checkCheckBox10,checkCheckBox11,checkCheckBox12,checkCheckBox13,checkCheckBox14,checkCheckBox15,checkCheckBox16
            };
            #endregion

            #region 配置加载
            configManager = new NPConfigManager();
            loadUpLocalConfiguration(configManager);
            #endregion

            // 启动读取周期

            // 数据库

            NPBackendService.WebSocketConnect();

            //NPBackendService service = new NPBackendService("192.168.100.188", "RFID0012");
            //RspHandler handler = new RspHandler(getStockInitHandler);
            //service.getStockInit(handler);
            //NPConfigManager manager = new NPConfigManager();
            /*
            manager.configURL = "www.baidu.com";
            manager.dbConfig.dbAddress = "localhost:3306";
            manager.dbConfig.dbName = "mysql";
            manager.dbConfig.username = "root";
            manager.dbConfig.password = "123456";
            manager.inStoreIP = "192.168.100.188";
            manager.inStoreAntNums = 4;
            JArray instorePorts = new JArray();
            instorePorts.Add(1);
            manager.inStorePorts = instorePorts;
            manager.checkIP = "192.168.100.188";
            manager.checkAntNums = 4;
            JArray checkPorts = new JArray();
            checkPorts.Add(2);
            checkPorts.Add(3);
            manager.checkPorts = checkPorts;
            manager.readPortTime = 5;
            manager.readPortCycle = 5;
            manager.analyzeCycle = 10;
            manager.markDownConfiguration();
            */
        }

        private void loadUpLocalConfiguration(NPConfigManager manager)
        {
            urlTextBox.Text = manager.configURL == null ? "" : manager.configURL;
            dbAddressTextBox.Text = manager.dbConfig.dbAddress == null ? "" : manager.dbConfig.dbAddress;
            dbNameTextBox.Text = manager.dbConfig.dbName == null ? "" : manager.dbConfig.username;
            dbPasswordTextBox.Text = manager.dbConfig.password == null ? "" : manager.dbConfig.password;
            inStoreIP.Text = manager.inStoreIP == null ? "" : manager.inStoreIP;
            if(manager.inStoreAntNums > 0)
            {
                foreach (RadioButton rb in inStoreRadioList)
                {
                    if (rb.TabIndex + 1 == manager.inStoreAntNums)
                    {
                        rb.Checked = true;
                    }
                }
            }
            if(manager.inStorePorts.Count > 0)
            {
                foreach (CheckBox cb in inStoreCheckBoxList)
                {
                    if (manager.inStorePorts.Contains(cb.TabIndex + 1))
                    {
                        cb.Checked = true;
                    }
                }
            }
        }
    }
}
