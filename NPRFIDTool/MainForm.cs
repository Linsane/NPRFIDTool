using System;
using System.Drawing;
using System.Windows.Forms;
using NPRFIDTool.NPKit;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

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
        private NPTimingManager timingManager;
        private NPBackendService services;

        NPRFIDReaderInfo inStoreReader;
        NPRFIDReaderInfo checkReader;

        private RadioButton[] inStoreRadioList;
        private CheckBox[] inStoreCheckBoxList;
        private RadioButton[] checkRadioList;
        private CheckBox[] checkCheckBoxList;

        private delegate void MainThreadMethod();

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

            #region 初始化各种组件
            services = new NPBackendService("", "RFID0012");
            readerManager = new NPRFIDReaderManager();
            NPWebSocket.errorHandler += (err) => {
                MessageBox.Show("websocket 连接失败");
                resetAppStatus();
            };
            // websocket通知开始读入库端口
            NPWebSocket.startInStoreHandler += (wse) =>
            {

            };
            // websocket通知结束读入库端口
            NPWebSocket.stopInStoreHandler += (wse) =>
            {
                Console.WriteLine("websocket通知入库结束，主动请求Remain表数据");
                services.getStockInit((remainData)=>
                {
                    if (remainData == null) return;
                    // 处理返回数据，存储到数据库中的Remain表
                    dbManager.appendDataToDataBase(TableType.TableTypeRemain,remainData);
                });
            };
            NPWebSocket.connectStopHandler += (wse) =>
            {
                MessageBox.Show("websocket 连接断开");
                resetAppStatus();
            };
            readerManager.failHandler += (ex) =>
            {
                MessageBox.Show("连接读写器失败:" + ex.ToString());
                resetAppStatus();
            };
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

        // 点击启动/停止按钮
        private void controlButton_Click(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            if (button.Text == "停止") // 处理停止逻辑
            {
                resetAppStatus();
                return;
            }

            // 处理开始逻辑
            if (!validateCurrentConfiguration())
            {
                MessageBox.Show("完善配置后请先点击更新配置");
                return;
            }

            resetAppStatus();
            controlButton.Text = "停止";
            controlButton.Enabled = false;
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
                resetAppStatus();
                return;
            }
            dbManager.clearDataBase(TableType.TableTypeCheck);
            #endregion

            #region RFID硬件信息
            // 入库
            inStoreReader = new NPRFIDReaderInfo(PortType.PortTypeInStore, configManager.inStoreIP, configManager.inStoreAntNums, configManager.inStorePorts);
            // 盘点
            checkReader = new NPRFIDReaderInfo(PortType.PortTypeCheck, configManager.checkIP, configManager.checkAntNums, configManager.checkPorts);
            #endregion

            #region WebSocket连接
            NPWebSocket.connect();
            #endregion

            // Timing 控制
            if (timingManager == null)
            {
                timingManager = new NPTimingManager(configManager.readPortTime, configManager.readPortCycle, configManager.analyzeCycle * 60);
                timingManager.readPortTimesUpHandler += (src, ee) =>
                {
                    // 停止读盘点接口
                    Console.WriteLine("停止盘点");
                    readerManager.endReading(checkReader);
                    // 将盘点数据写入数据库
                    dbManager.appendDataToDataBase(TableType.TableTypeCheck, readerManager.checkedDict);
                };
                timingManager.scanCycleStartHandler += (src, ee) =>
                {
                    // 开始读盘点接口
                    Console.WriteLine("开始盘点");
                    readerManager.beginReading(checkReader);
                };
                timingManager.analyzeCycleStartHandler += (src, ee) =>
                {
                    // 开始分析差异数据，上报分析结果
                    Console.WriteLine("分析盘点结果");
                    JObject remainData = dbManager.queryDataBase(TableType.TableTypeRemain);
                    JArray diffArray = readerManager.getDiffTagsArray(remainData);
                    services.reportCheckDiff(diffArray);
                };
            }
            timingManager.startCycles();
            // 启动自动触发一次盘点
            readerManager.beginReading(checkReader);
            timingManager.readPortTimer.Enabled = true;

            controlButton.Enabled = true;
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

        private void resetControlButton()
        {
            controlButton.Text = "启动";
            controlButton.Enabled = true;
        }

        // 重设程序状态
        private void resetAppStatus()
        {
            if (controlButton.InvokeRequired)
            {
                this.BeginInvoke(new MainThreadMethod(resetControlButton), null);
            }
            else
            {
                resetControlButton();
            }
            if (dbManager != null) dbManager.disconnectDataBase();
            if (timingManager != null) timingManager.stopCycles();
            NPWebSocket.disconnect();
            readerManager.endReading(inStoreReader);
            readerManager.endReading(checkReader);
        }


        #region 控件输入校验
        private void urlTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textBox = sender as TextBox;
            showEmptyWarningIfNeeded(textBox, e);
            string Pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&$%\$#\=~])*$";
            Regex r = new Regex(Pattern);
            Match m = r.Match(textBox.Text);
            if (!m.Success)
            {
                errorProvider1.SetError(textBox, "请输入有效的URL地址");
            }
            else
            {
                errorProvider1.SetError(textBox, null);
            }
        }

        private void dbInfo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textBox = sender as TextBox;
            showEmptyWarningIfNeeded(textBox, e);
            // 校验数据库地址有效性

            if (textBox == dbAddressTextBox)
            {
                string num = "(25[0-5]|2[0-4]//d|[0-1]//d{2}|[1-9]?//d)";
                string IPPattern = "^" + num + "//." + num + "//." + num + "//." + num + "$";
                string Pattern = @"^(localhost|"+ IPPattern+ @")(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&$%\$#\=~])*$";
                Regex r = new Regex(Pattern);
                Match m = r.Match(textBox.Text);
                if (!m.Success)
                {
                    errorProvider1.SetError(textBox, "请输入有效的数据库地址");
                }
                else
                {
                    errorProvider1.SetError(textBox, null);
                }
            }
        }

        private void IPTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!isValidateIP(textBox.Text))
            {
                errorProvider1.SetError(textBox, "请输入有效的IP地址");
            }
            else
            {
                errorProvider1.SetError(textBox, null);
            }
        }

        private bool isValidateIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }



        private void showEmptyWarningIfNeeded(TextBox txtBox, System.ComponentModel.CancelEventArgs e)
        {
            if (txtBox == null) return;
            e.Cancel = (txtBox.Text == string.Empty);
            if (string.IsNullOrEmpty(txtBox.Text))
            {
                errorProvider1.SetError(txtBox, "不能为空");
            }
            else
            {
                errorProvider1.SetError(txtBox, null);
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            readerManager.beginReading(inStoreReader);
            Console.WriteLine("开始入库");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            readerManager.endReading(inStoreReader);
            Console.WriteLine("结束入库");
        }


    }
}
