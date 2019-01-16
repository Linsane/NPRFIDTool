namespace NPRFIDTool
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.systemConfigGroup = new System.Windows.Forms.GroupBox();
            this.systemConfigContainer = new System.Windows.Forms.SplitContainer();
            this.serverConfigGroup = new System.Windows.Forms.GroupBox();
            this.urlContainer = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.localConfigGroup = new System.Windows.Forms.GroupBox();
            this.dbConfigPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.portsConfigGroupBox = new System.Windows.Forms.GroupBox();
            this.scanConfigGroupBox = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.networkConfigGroupBox = new System.Windows.Forms.GroupBox();
            this.networkConfigContrainer = new System.Windows.Forms.SplitContainer();
            this.inStoreConfigGroupBox = new System.Windows.Forms.GroupBox();
            this.outStoreConfigGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.systemConfigGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemConfigContainer)).BeginInit();
            this.systemConfigContainer.Panel1.SuspendLayout();
            this.systemConfigContainer.Panel2.SuspendLayout();
            this.systemConfigContainer.SuspendLayout();
            this.serverConfigGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.urlContainer)).BeginInit();
            this.urlContainer.Panel1.SuspendLayout();
            this.urlContainer.Panel2.SuspendLayout();
            this.urlContainer.SuspendLayout();
            this.localConfigGroup.SuspendLayout();
            this.dbConfigPanel.SuspendLayout();
            this.portsConfigGroupBox.SuspendLayout();
            this.scanConfigGroupBox.SuspendLayout();
            this.networkConfigGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.networkConfigContrainer)).BeginInit();
            this.networkConfigContrainer.Panel1.SuspendLayout();
            this.networkConfigContrainer.Panel2.SuspendLayout();
            this.networkConfigContrainer.SuspendLayout();
            this.inStoreConfigGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // systemConfigGroup
            // 
            this.systemConfigGroup.Controls.Add(this.systemConfigContainer);
            this.systemConfigGroup.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.systemConfigGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.systemConfigGroup.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.systemConfigGroup.Location = new System.Drawing.Point(0, 0);
            this.systemConfigGroup.Name = "systemConfigGroup";
            this.systemConfigGroup.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.systemConfigGroup.Size = new System.Drawing.Size(896, 220);
            this.systemConfigGroup.TabIndex = 0;
            this.systemConfigGroup.TabStop = false;
            this.systemConfigGroup.Paint += new System.Windows.Forms.PaintEventHandler(this.systemConfigGroup_Paint);
            // 
            // systemConfigContainer
            // 
            this.systemConfigContainer.CausesValidation = false;
            this.systemConfigContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.systemConfigContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemConfigContainer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.systemConfigContainer.IsSplitterFixed = true;
            this.systemConfigContainer.Location = new System.Drawing.Point(3, 22);
            this.systemConfigContainer.Name = "systemConfigContainer";
            // 
            // systemConfigContainer.Panel1
            // 
            this.systemConfigContainer.Panel1.Controls.Add(this.serverConfigGroup);
            // 
            // systemConfigContainer.Panel2
            // 
            this.systemConfigContainer.Panel2.Controls.Add(this.localConfigGroup);
            this.systemConfigContainer.Size = new System.Drawing.Size(890, 195);
            this.systemConfigContainer.SplitterDistance = 430;
            this.systemConfigContainer.SplitterWidth = 1;
            this.systemConfigContainer.TabIndex = 0;
            this.systemConfigContainer.TabStop = false;
            // 
            // serverConfigGroup
            // 
            this.serverConfigGroup.CausesValidation = false;
            this.serverConfigGroup.Controls.Add(this.urlContainer);
            this.serverConfigGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.serverConfigGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverConfigGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.serverConfigGroup.Location = new System.Drawing.Point(0, 0);
            this.serverConfigGroup.Name = "serverConfigGroup";
            this.serverConfigGroup.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.serverConfigGroup.Size = new System.Drawing.Size(430, 195);
            this.serverConfigGroup.TabIndex = 0;
            this.serverConfigGroup.TabStop = false;
            this.serverConfigGroup.Text = "服务器配置";
            // 
            // urlContainer
            // 
            this.urlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.urlContainer.IsSplitterFixed = true;
            this.urlContainer.Location = new System.Drawing.Point(3, 64);
            this.urlContainer.MaximumSize = new System.Drawing.Size(0, 50);
            this.urlContainer.Name = "urlContainer";
            // 
            // urlContainer.Panel1
            // 
            this.urlContainer.Panel1.Controls.Add(this.label1);
            // 
            // urlContainer.Panel2
            // 
            this.urlContainer.Panel2.Controls.Add(this.textBox1);
            this.urlContainer.Size = new System.Drawing.Size(424, 50);
            this.urlContainer.SplitterDistance = 60;
            this.urlContainer.TabIndex = 1;
            this.urlContainer.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(353, 21);
            this.textBox1.TabIndex = 0;
            // 
            // localConfigGroup
            // 
            this.localConfigGroup.Controls.Add(this.dbConfigPanel);
            this.localConfigGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.localConfigGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localConfigGroup.Location = new System.Drawing.Point(0, 0);
            this.localConfigGroup.Name = "localConfigGroup";
            this.localConfigGroup.Padding = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.localConfigGroup.Size = new System.Drawing.Size(459, 195);
            this.localConfigGroup.TabIndex = 0;
            this.localConfigGroup.TabStop = false;
            this.localConfigGroup.Text = "本地配置";
            // 
            // dbConfigPanel
            // 
            this.dbConfigPanel.ColumnCount = 2;
            this.dbConfigPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.dbConfigPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbConfigPanel.Controls.Add(this.label2, 0, 0);
            this.dbConfigPanel.Controls.Add(this.label3, 0, 1);
            this.dbConfigPanel.Controls.Add(this.label4, 0, 2);
            this.dbConfigPanel.Controls.Add(this.label5, 0, 3);
            this.dbConfigPanel.Controls.Add(this.textBox2, 1, 0);
            this.dbConfigPanel.Controls.Add(this.textBox3, 1, 1);
            this.dbConfigPanel.Controls.Add(this.textBox4, 1, 2);
            this.dbConfigPanel.Controls.Add(this.textBox5, 1, 3);
            this.dbConfigPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbConfigPanel.Location = new System.Drawing.Point(3, 34);
            this.dbConfigPanel.Name = "dbConfigPanel";
            this.dbConfigPanel.RowCount = 4;
            this.dbConfigPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.dbConfigPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.dbConfigPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.dbConfigPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.dbConfigPanel.Size = new System.Drawing.Size(453, 158);
            this.dbConfigPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 39);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据库地址(IP:Port):";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 39);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据库名:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 39);
            this.label4.TabIndex = 4;
            this.label4.Text = "数据库用户名:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 41);
            this.label5.TabIndex = 5;
            this.label5.Text = "数据库密码";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(148, 9);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(302, 21);
            this.textBox2.TabIndex = 6;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(148, 48);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(302, 21);
            this.textBox3.TabIndex = 7;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Location = new System.Drawing.Point(148, 87);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(302, 21);
            this.textBox4.TabIndex = 8;
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.Location = new System.Drawing.Point(148, 127);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(302, 21);
            this.textBox5.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(383, 625);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "更新配置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // portsConfigGroupBox
            // 
            this.portsConfigGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portsConfigGroupBox.Controls.Add(this.scanConfigGroupBox);
            this.portsConfigGroupBox.Controls.Add(this.networkConfigGroupBox);
            this.portsConfigGroupBox.Location = new System.Drawing.Point(3, 238);
            this.portsConfigGroupBox.Name = "portsConfigGroupBox";
            this.portsConfigGroupBox.Size = new System.Drawing.Size(887, 373);
            this.portsConfigGroupBox.TabIndex = 5;
            this.portsConfigGroupBox.TabStop = false;
            this.portsConfigGroupBox.Paint += new System.Windows.Forms.PaintEventHandler(this.portsConfigGroupBox_Paint);
            this.portsConfigGroupBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // scanConfigGroupBox
            // 
            this.scanConfigGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scanConfigGroupBox.Controls.Add(this.label7);
            this.scanConfigGroupBox.Controls.Add(this.label6);
            this.scanConfigGroupBox.Controls.Add(this.label8);
            this.scanConfigGroupBox.Location = new System.Drawing.Point(3, 252);
            this.scanConfigGroupBox.Name = "scanConfigGroupBox";
            this.scanConfigGroupBox.Size = new System.Drawing.Size(878, 115);
            this.scanConfigGroupBox.TabIndex = 1;
            this.scanConfigGroupBox.TabStop = false;
            this.scanConfigGroupBox.Text = "扫描配置";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(398, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "天线端口数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "入库天线IP:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(278, 17);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.label8.Size = new System.Drawing.Size(59, 24);
            this.label8.TabIndex = 2;
            this.label8.Text = "使用端口:";
            // 
            // networkConfigGroupBox
            // 
            this.networkConfigGroupBox.Controls.Add(this.networkConfigContrainer);
            this.networkConfigGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.networkConfigGroupBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.networkConfigGroupBox.Location = new System.Drawing.Point(3, 17);
            this.networkConfigGroupBox.Name = "networkConfigGroupBox";
            this.networkConfigGroupBox.Size = new System.Drawing.Size(881, 353);
            this.networkConfigGroupBox.TabIndex = 0;
            this.networkConfigGroupBox.TabStop = false;
            this.networkConfigGroupBox.Text = "网络配置";
            // 
            // networkConfigContrainer
            // 
            this.networkConfigContrainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.networkConfigContrainer.Location = new System.Drawing.Point(3, 22);
            this.networkConfigContrainer.Name = "networkConfigContrainer";
            // 
            // networkConfigContrainer.Panel1
            // 
            this.networkConfigContrainer.Panel1.Controls.Add(this.inStoreConfigGroupBox);
            // 
            // networkConfigContrainer.Panel2
            // 
            this.networkConfigContrainer.Panel2.Controls.Add(this.outStoreConfigGroupBox);
            this.networkConfigContrainer.Size = new System.Drawing.Size(872, 208);
            this.networkConfigContrainer.SplitterDistance = 425;
            this.networkConfigContrainer.TabIndex = 0;
            // 
            // inStoreConfigGroupBox
            // 
            this.inStoreConfigGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.inStoreConfigGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inStoreConfigGroupBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.inStoreConfigGroupBox.Location = new System.Drawing.Point(0, 0);
            this.inStoreConfigGroupBox.Name = "inStoreConfigGroupBox";
            this.inStoreConfigGroupBox.Size = new System.Drawing.Size(425, 208);
            this.inStoreConfigGroupBox.TabIndex = 0;
            this.inStoreConfigGroupBox.TabStop = false;
            this.inStoreConfigGroupBox.Text = "出入库天线配置";
            // 
            // outStoreConfigGroupBox
            // 
            this.outStoreConfigGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outStoreConfigGroupBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outStoreConfigGroupBox.Location = new System.Drawing.Point(0, 0);
            this.outStoreConfigGroupBox.Name = "outStoreConfigGroupBox";
            this.outStoreConfigGroupBox.Size = new System.Drawing.Size(443, 208);
            this.outStoreConfigGroupBox.TabIndex = 0;
            this.outStoreConfigGroupBox.TabStop = false;
            this.outStoreConfigGroupBox.Text = "仓库天线配置";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.91169F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.0883F));
            this.tableLayoutPanel1.Controls.Add(this.textBox7, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox6, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(419, 188);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 37);
            this.label9.TabIndex = 0;
            this.label9.Text = "入库天线IP:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 37);
            this.label10.TabIndex = 1;
            this.label10.Text = "天线端口数";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 74);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.label11.Size = new System.Drawing.Size(89, 114);
            this.label11.TabIndex = 2;
            this.label11.Text = "使用端口";
            // 
            // textBox6
            // 
            this.textBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox6.Location = new System.Drawing.Point(98, 8);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(318, 21);
            this.textBox6.TabIndex = 3;
            // 
            // textBox7
            // 
            this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox7.Location = new System.Drawing.Point(98, 45);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(318, 21);
            this.textBox7.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 663);
            this.Controls.Add(this.portsConfigGroupBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.systemConfigGroup);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RFID桌面管理器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.systemConfigGroup.ResumeLayout(false);
            this.systemConfigContainer.Panel1.ResumeLayout(false);
            this.systemConfigContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.systemConfigContainer)).EndInit();
            this.systemConfigContainer.ResumeLayout(false);
            this.serverConfigGroup.ResumeLayout(false);
            this.urlContainer.Panel1.ResumeLayout(false);
            this.urlContainer.Panel1.PerformLayout();
            this.urlContainer.Panel2.ResumeLayout(false);
            this.urlContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.urlContainer)).EndInit();
            this.urlContainer.ResumeLayout(false);
            this.localConfigGroup.ResumeLayout(false);
            this.dbConfigPanel.ResumeLayout(false);
            this.dbConfigPanel.PerformLayout();
            this.portsConfigGroupBox.ResumeLayout(false);
            this.scanConfigGroupBox.ResumeLayout(false);
            this.scanConfigGroupBox.PerformLayout();
            this.networkConfigGroupBox.ResumeLayout(false);
            this.networkConfigContrainer.Panel1.ResumeLayout(false);
            this.networkConfigContrainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.networkConfigContrainer)).EndInit();
            this.networkConfigContrainer.ResumeLayout(false);
            this.inStoreConfigGroupBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox systemConfigGroup;
        private System.Windows.Forms.SplitContainer systemConfigContainer;
        private System.Windows.Forms.GroupBox serverConfigGroup;
        private System.Windows.Forms.GroupBox localConfigGroup;
        private System.Windows.Forms.SplitContainer urlContainer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel dbConfigPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.GroupBox portsConfigGroupBox;
        private System.Windows.Forms.GroupBox scanConfigGroupBox;
        private System.Windows.Forms.GroupBox networkConfigGroupBox;
        private System.Windows.Forms.SplitContainer networkConfigContrainer;
        private System.Windows.Forms.GroupBox inStoreConfigGroupBox;
        private System.Windows.Forms.GroupBox outStoreConfigGroupBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox6;
    }
}

