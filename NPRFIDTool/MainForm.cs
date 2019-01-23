using System;
using System.Drawing;
using System.Windows.Forms;
using NPRFIDTool.NPKit;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

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
        private bool hasError;

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
                readerManager.scanType = 0;
                readerManager.beginReading(inStoreReader);
                Console.WriteLine("开始入库");
            };
            // websocket通知开始读入库端口
            NPWebSocket.startOutStoreHandler += (wse) =>
            {
                readerManager.scanType = 1;
                readerManager.beginReading(inStoreReader);
                Console.WriteLine("开始出库");
            };
            // websocket通知结束读出入库端口
            NPWebSocket.stopScanHandler += (wse) =>
            {
                readerManager.scanType = -1;
                readerManager.endReading(inStoreReader);
                Console.WriteLine("停止读出入库端口");
            };
            NPWebSocket.connectStopHandler += (wse) =>
            {
                resetAppStatus();
            };
            readerManager.failHandler += (ex) =>
            {
                MessageBox.Show("连接读写器失败:" + ex.ToString());
                resetAppStatus();
            };
            readerManager.portFailHandler += (ex) =>
            {
                MessageBox.Show(ex);
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
                    else
                    {
                        rb.Checked = false;
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
                    else
                    {
                        cb.Checked = false;
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
                    else
                    {
                        rb.Checked = false;
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
                    else
                    {
                        cb.Checked = false;
                    }
                }
            }
            readTimeTextBox.Text = manager.readPortTime == -1 ? "" : manager.readPortTime.ToString();
            scanCycleTextBox.Text = manager.readPortCycle == -1 ? "" : (manager.readPortCycle / 60.0).ToString();
            analyzeCycleTextBox.Text = manager.analyzeCycle == -1 ? "" : (manager.analyzeCycle / 60.0).ToString();
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
            systemConfigGroup.Enabled = false;
            portsConfigGroupBox.Enabled = false;
            updateButton.Enabled = false;

            #region 启动条件判断
            if (!validateCurrentConfiguration())
            {
                MessageBox.Show("请先完善配置");
                resetAppStatus();
                return;
            }

            if (hasError)
            {
                MessageBox.Show("含有不正确配置，请根据提示修改相关配置");
                resetAppStatus();
                return;
            }

            if (checkIfConfigHasChanged())
            {
                DialogResult dr = MessageBox.Show("配置已修改，是否要更新配置？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    updateButton_Click(null, null);
                }
                else
                {
                    loadUpLocalConfiguration(configManager);
                    resetAppStatus();
                    return;
                }
            }
            #endregion

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
            // 清空数据库
            dbManager.clearDataBase(TableType.TableTypeCheck);
            dbManager.clearDataBase(TableType.TableTypeRemain);
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

            #region Timing控制
            if (timingManager == null)
            {
                timingManager = new NPTimingManager(configManager.readPortTime, configManager.readPortCycle, configManager.analyzeCycle);
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
                    Console.WriteLine("开始盘点，读取盘点数据");
                    readerManager.beginReading(checkReader);
                };
                timingManager.analyzeCycleStartHandler += (src, ee) =>
                {
                    // 开始分析差异数据，上报分析结果
                    Console.WriteLine("分析盘点结果");
                    // 请求remain表数据，根据数据表进行盘点分析
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    services.getStockInit((remainData) =>
                    {
                        if (remainData == null) return;
                        dbManager.refreshTableWithData(TableType.TableTypeRemain, remainData);
                        JArray diffArray = readerManager.getDiffTagsArray(remainData);
                        if (diffArray.Count > 0)
                        {
                            services.reportCheckDiff(diffArray);
                            Console.WriteLine("盘点失败，上报差异结果" + diffArray.ToString());
                        }
                        else
                        {
                            services.reportCheckSuccess(null);
                            Console.WriteLine("盘点成功");
                        }
                    });
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                };
            }
            #endregion
            timingManager.startCycles();
            // 启动自动触发一次盘点
            Console.WriteLine("启动的第一次盘点");
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

            if (hasError)
            {
                MessageBox.Show("含有不正确配置，请根据提示修改相关配置");
                resetAppStatus();
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
            configManager.readPortCycle = (int)(double.Parse(scanCycleTextBox.Text)*60);
            configManager.analyzeCycle = (int)(double.Parse(analyzeCycleTextBox.Text)*60);

            configManager.markDownConfiguration();
            #endregion
        }

        #region 校验配置
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

        private bool checkIfConfigHasChanged()
        {
            if (urlTextBox.Text != configManager.configURL) return true;

            if (dbAddressTextBox.Text != configManager.dbConfig.dbAddress || dbNameTextBox.Text != configManager.dbConfig.dbName || dbUserNameTextBox.Text != configManager.dbConfig.username || dbPasswordTextBox.Text != configManager.dbConfig.password)
            {
                return  true;
            }
            if (inStoreIPTextBox.Text != configManager.inStoreIP || checkIPTextBox.Text != configManager.checkIP)
            {
                return true;
            }
            if (int.Parse(readTimeTextBox.Text) != configManager.readPortTime || (int)(double.Parse(scanCycleTextBox.Text)*60) != configManager.readPortCycle || (int)(double.Parse(analyzeCycleTextBox.Text)*60) != configManager.analyzeCycle)
            {
                return true;
            }

            foreach (RadioButton rb in inStoreRadioList)
            {
                if (rb.Checked && int.Parse(rb.Text) != configManager.inStoreAntNums)
                {
                    return true;
                }
            }
            ArrayList portsList = new ArrayList();
            foreach (CheckBox cb in inStoreCheckBoxList)
            {
                if (cb.Checked)
                {
                    portsList.Add(int.Parse(cb.Text));
                }
            }
            if (portsList.Count != configManager.inStorePorts.Count) return true;
            List<int> instorePorts = configManager.inStorePorts.ToObject<List<int>>();
            foreach (int num in portsList){
                if (!instorePorts.Contains(num))
                {
                    return true;
                }
            }

            foreach (RadioButton rb in checkRadioList)
            {
                if (rb.Checked && int.Parse(rb.Text) != configManager.checkAntNums)
                {
                    return true;
                }
            }

            ArrayList list = new ArrayList();
            foreach (CheckBox cb in checkCheckBoxList)
            {
                if (cb.Checked)
                {
                    list.Add(int.Parse(cb.Text));
                }
            }
            if (list.Count != configManager.checkPorts.Count) return true;
            List<int> checkPorts = configManager.checkPorts.ToObject<List<int>>();
            foreach (int num in list)
            {
                if (!checkPorts.Contains(num))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region 控件值变化处理
        // 入库端口数选择
        private void inStoreRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            clearCheckBoxs(PortType.PortTypeInStore, false);
            if (rb.Checked == false) return;
            showPartOfCheckBoxs(PortType.PortTypeInStore, int.Parse(rb.Text));
            if(inStoreIPTextBox.Text == checkIPTextBox.Text)
            {
                checkRadioList[rb.TabIndex].Checked = true;
            }
        }

        // 盘点端口数选择
        private void checkRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            clearCheckBoxs(PortType.PortTypeCheck, false);
            if (rb.Checked == false) return;
            showPartOfCheckBoxs(PortType.PortTypeCheck, int.Parse(rb.Text));
            if (inStoreIPTextBox.Text == checkIPTextBox.Text)
            {
                inStoreRadioList[rb.TabIndex].Checked = true;
            }
        }

        private void inStoreIPTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (isValidateIP(tb.Text))
            {
                portsCountGroupBox1.Enabled = true;
            }
            else
            {
                portsCountGroupBox1.Enabled = false;

            }
            foreach (RadioButton rb in inStoreRadioList)
            {
                rb.Checked = false;
            }
            clearCheckBoxs(PortType.PortTypeInStore, true);
            if (tb.Text == checkIPTextBox.Text)
            {
                foreach(RadioButton rb in checkRadioList)
                {
                    if (rb.Checked)
                    {
                        inStoreRadioList[rb.TabIndex].Checked = true;
                    }
                }
                foreach(CheckBox cb in checkCheckBoxList)
                {
                    inStoreCheckBoxList[cb.TabIndex].Enabled = !cb.Checked;
                }
            }
        }

        private void checkIPTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (isValidateIP(tb.Text))
            {
                portsCountGroupBox2.Enabled = true;
            }

            else
            {
                portsCountGroupBox2.Enabled = false;
            }
            foreach (RadioButton rb in checkRadioList)
            {
                rb.Checked = false;
            }
            clearCheckBoxs(PortType.PortTypeCheck, true);
            if (tb.Text == inStoreIPTextBox.Text)
            {
                foreach (RadioButton rb in inStoreRadioList)
                {
                    if (rb.Checked)
                    {
                        checkRadioList[rb.TabIndex].Checked = true;
                    }
                }
                foreach (CheckBox cb in inStoreCheckBoxList)
                {
                    checkCheckBoxList[cb.TabIndex].Enabled = !cb.Checked;
                }
            }
        }

        private void inStoreCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (inStoreIPTextBox.Text == checkIPTextBox.Text)
            {
                checkCheckBoxList[cb.TabIndex].Enabled = !cb.Checked;
            }
        }

        private void checkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (inStoreIPTextBox.Text == checkIPTextBox.Text)
            {
                inStoreCheckBoxList[cb.TabIndex].Enabled = !cb.Checked;
            }
        }

        #endregion

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
        private void clearCheckBoxs(PortType type, bool hidden)
        {
            CheckBox[] checkBoxList = type == PortType.PortTypeInStore ? inStoreCheckBoxList : checkCheckBoxList;
            foreach (CheckBox cb in checkBoxList)
            {
                cb.Checked = false;
                if (hidden)
                {
                    cb.Visible = false;
                }
            }
            foreach(CheckBox cb in inStoreCheckBoxList)
            {
                cb.Enabled = true;
            }

            foreach (CheckBox cb in checkCheckBoxList)
            {
                cb.Enabled = true;
            }
        }

        private void resetControlButton()
        {
            controlButton.Text = "启动";
            controlButton.Enabled = true;
            systemConfigGroup.Enabled = true;
            portsConfigGroupBox.Enabled = true;
            updateButton.Enabled = true;
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
            readerManager.clear();
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
                hasError = true;
            }
            else
            {
                errorProvider1.SetError(textBox, null);
                hasError = false;
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
                    hasError = true;
                }
                else
                {
                    errorProvider1.SetError(textBox, null);
                    hasError = false;
                }
            }
        }

        private void IPTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!isValidateIP(textBox.Text))
            {
                errorProvider1.SetError(textBox, "请输入有效的IP地址");
                hasError = true;
            }
            else
            {
                errorProvider1.SetError(textBox, null);
                hasError = false;
            }
        }

        private bool isValidateIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        private void timeTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textBox = sender as TextBox;
            try
            {
                double var1 = double.Parse(textBox.Text);
                errorProvider1.SetError(textBox, null);
                hasError = false;
            }
            catch
            {
                errorProvider1.SetError(textBox, "请输入有效的数字");
                hasError = true;
            }
            if(textBox == scanCycleTextBox || textBox == analyzeCycleTextBox)
            {
                if(analyzeCycleTextBox.Text == null) return;
                double var1 = double.Parse(scanCycleTextBox.Text);
                double var2 = double.Parse(analyzeCycleTextBox.Text);
                if(var1>= var2)
                {
                    errorProvider1.SetError(textBox, "盘点周期时长需大于扫描周期时长");
                    hasError = true;
                }

            }
        }

        private void showEmptyWarningIfNeeded(TextBox txtBox, System.ComponentModel.CancelEventArgs e)
        {
            if (txtBox == null) return;
            e.Cancel = (txtBox.Text == string.Empty);
            if (string.IsNullOrEmpty(txtBox.Text))
            {
                errorProvider1.SetError(txtBox, "不能为空");
                hasError = true;
            }
            else
            {
                errorProvider1.SetError(txtBox, null);
                hasError = false;
            }
        }
        #endregion

    }
}
