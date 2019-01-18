using System;
using System.Drawing;
using System.Windows.Forms;
using NPRFIDTool.NPKit;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Timers;

namespace NPRFIDTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private NPConfigManager configManager;
        private NPDBManager dbManager;
        private NPRFIDReaderManager readerManager;
        NPRFIDReaderInfo inStoreReader;
        NPRFIDReaderInfo checkReader;

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
        }

        // 加载本地配置到控件
        private void loadUpLocalConfiguration(NPConfigManager manager)
        {
            urlTextBox.Text = manager.configURL == null ? "" : manager.configURL;
            dbAddressTextBox.Text = manager.dbConfig.dbAddress == null ? "" : manager.dbConfig.dbAddress;
            dbNameTextBox.Text = manager.dbConfig.dbName == null ? "" : manager.dbConfig.dbName;
            dbUserNameTextBox.Text = manager.dbConfig.username == null ? "" : manager.dbConfig.username;
            dbPasswordTextBox.Text = manager.dbConfig.password == null ? "" : manager.dbConfig.password;
            inStoreIPTextBox.Text = manager.inStoreIP == null ? "" : manager.inStoreIP;
            if(manager.inStoreAntNums > 0)
            {
                foreach (RadioButton rb in inStoreRadioList)
                {
                    if (int.Parse(rb.Text) == manager.inStoreAntNums)
                    {
                        rb.Checked = true;
                    }
                }
            }
            showPartOfCheckBoxs(PortType.PortTypeInStore, manager.inStoreAntNums);
            if(manager.inStorePorts.Count > 0)
            {
                foreach (CheckBox cb in inStoreCheckBoxList)
                {
                    int[] array = manager.inStorePorts.ToObject<int[]>();
                    if (Array.IndexOf<int>(array,cb.TabIndex+1) != -1)
                    {
                        cb.Checked = true;
                    }
                }
            }
            checkIPTextBox.Text = manager.checkIP == null ? "" : manager.checkIP;
            if (manager.checkAntNums > 0)
            {
                foreach (RadioButton rb in checkRadioList)
                {
                    if (int.Parse(rb.Text) == manager.checkAntNums)
                    {
                        rb.Checked = true;
                    }
                }
            }
            showPartOfCheckBoxs(PortType.PortTypeCheck, manager.checkAntNums);
            if (manager.checkPorts.Count > 0)
            {
                foreach (CheckBox cb in checkCheckBoxList)
                {
                    int[] array = manager.checkPorts.ToObject<int[]>();
                    if (Array.IndexOf<int>(array, cb.TabIndex + 1) != -1)
                    {
                        cb.Checked = true;
                    }
                }
            }
            readTimeTextBox.Text = manager.readPortTime == -1 ? "" : manager.readPortTime.ToString();
            scanCycleTextBox.Text = manager.readPortCycle == -1 ? "" : manager.readPortCycle.ToString();
            analyzeCycleTextBox.Text = manager.analyzeCycle == -1 ? "" : manager.analyzeCycle.ToString();
        }

        // 点击更新配置按钮
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!validateCurrentConfiguration())
            {
                MessageBox.Show("请完善配置信息");
                return;
            }

            #region 更新配置信息
            configManager.configURL = urlTextBox.Text;
            configManager.dbConfig.dbAddress = dbAddressTextBox.Text;
            configManager.dbConfig.dbName = dbNameTextBox.Text;
            configManager.dbConfig.username = dbUserNameTextBox.Text;
            configManager.dbConfig.password = dbPasswordTextBox.Text;
            configManager.inStoreIP = inStoreIPTextBox.Text;
            foreach (RadioButton rb in inStoreRadioList)
            {
                if (rb.Checked)
                {
                    configManager.inStoreAntNums = int.Parse(rb.Text);
                    break;
                }
            }
            JArray inStorePorts = new JArray();
            foreach (CheckBox cb in inStoreCheckBoxList)
            {
                
                if (cb.Checked)
                {
                    inStorePorts.Add(int.Parse(cb.Text));
                }
            }
            configManager.inStorePorts = inStorePorts;
            configManager.checkIP = checkIPTextBox.Text;
            foreach (RadioButton rb in checkRadioList)
            {
                if (rb.Checked)
                {
                    configManager.checkAntNums = int.Parse(rb.Text);
                    break;
                }
            }

            JArray checkPorts = new JArray();
            foreach (CheckBox cb in checkCheckBoxList)
            {
                if (cb.Checked)
                {
                    checkPorts.Add(int.Parse(cb.Text));
                }
            }
            configManager.checkPorts = checkPorts;
            configManager.readPortTime = int.Parse(readTimeTextBox.Text);
            configManager.readPortCycle = int.Parse(scanCycleTextBox.Text);
            configManager.analyzeCycle = int.Parse(analyzeCycleTextBox.Text);

            configManager.markDownConfiguration();
            #endregion

            #region 数据库连接
            if (dbManager != null) dbManager.disconnectDataBase();
            dbManager = new NPDBManager(configManager.dbConfig);
            try
            {
                dbManager.connectDataBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接数据库失败，请填写正确数据库信息 err:" + ex.Message);
                return;
            }
            #endregion

            #region RFID硬件信息
            // 入库
            inStoreReader = new NPRFIDReaderInfo(PortType.PortTypeInStore, configManager.inStoreIP, configManager.inStoreAntNums, configManager.inStorePorts);
            // 盘点
            checkReader = new NPRFIDReaderInfo(PortType.PortTypeCheck, configManager.checkIP, configManager.checkAntNums, configManager.checkPorts);
            #endregion

            #region WebSocket连接
            NPWebSocket.errorHandler += (err) => {
                MessageBox.Show("websocket 连接失败");
                dbManager.disconnectDataBase();
            };
            // websocket通知开始读入库端口
            NPWebSocket.startInStoreHandler += (wse) =>
            {

            };
            // websocket通知结束读入库端口
            NPWebSocket.stopInStoreHandler += (wse) =>
            {

            };
            NPWebSocket.connect();
            #endregion

            // test timer;
            NPTimingManager timingManager = new NPTimingManager(2, 240, 600);
            timingManager.readPortTimesUpHandler += (src, ee) =>
            {
                MessageBox.Show("timeout");
            };
            timingManager.startCycles();
        }

        // 校验配置
        private bool validateCurrentConfiguration()
        {
            if((dbAddressTextBox.Text == "" || dbNameTextBox.Text == "" || dbUserNameTextBox.Text == "" || dbPasswordTextBox.Text == "") && urlTextBox.Text == "")
            {
                return false;
            }
            if(inStoreIPTextBox.Text == "" || checkIPTextBox.Text == "")
            {
                return false;
            }
            if(readTimeTextBox.Text == "" || scanCycleTextBox.Text == "" || analyzeCycleTextBox.Text == "")
            {
                return false;
            }
            bool instoreRadioSelected = false;
            foreach (RadioButton rb in inStoreRadioList)
            {
                if (rb.Checked)
                {
                    instoreRadioSelected = true;
                    break;
                }
            }
            if (!instoreRadioSelected) return false;

            bool instoreCheckBoxSelected = false;
            foreach (CheckBox cb in inStoreCheckBoxList)
            {
                if (cb.Checked)
                {
                    instoreCheckBoxSelected = true;
                    break;
                }
            }
            if (!instoreCheckBoxSelected) return false;

            bool checkRadioSelected = false;
            foreach (RadioButton rb in checkRadioList)
            {
                if (rb.Checked)
                {
                    checkRadioSelected = true;
                    break;
                }
            }
            if (!checkRadioSelected) return false;

            bool checkCheckBoxSelected = false;
            foreach (CheckBox cb in checkCheckBoxList)
            {
                if (cb.Checked)
                {
                    checkCheckBoxSelected = true;
                    break;
                }
            }
            if (!checkCheckBoxSelected) return false;

            return true;
        }

        // 入库端口数选择
        private void inStoreRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            clearCheckBoxs(PortType.PortTypeInStore);
            showPartOfCheckBoxs(PortType.PortTypeInStore, int.Parse(rb.Text));
        }

        // 盘点端口数选择
        private void checkRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            clearCheckBoxs(PortType.PortTypeCheck);
            showPartOfCheckBoxs(PortType.PortTypeCheck, int.Parse(rb.Text));
        }

        // 控制端口选项显示个数
        private void showPartOfCheckBoxs(PortType type , int portNum)
        {
            CheckBox[] checkBoxList = type == PortType.PortTypeInStore ? inStoreCheckBoxList : checkCheckBoxList;

            foreach(CheckBox cb in checkBoxList)
            {
                cb.Visible = cb.TabIndex + 1 <= portNum ? true : false;
            }
        }

        // 清空端口选择状态
        private void clearCheckBoxs(PortType type)
        {
            CheckBox[] checkBoxList = type == PortType.PortTypeInStore ? inStoreCheckBoxList : checkCheckBoxList;
            foreach (CheckBox cb in checkBoxList)
            {
                cb.Checked = false;
            }
        }
    }
}
