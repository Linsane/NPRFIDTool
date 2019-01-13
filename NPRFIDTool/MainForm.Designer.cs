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
            this.localConfigGroup = new System.Windows.Forms.GroupBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.urlLabel = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.systemConfigGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemConfigContainer)).BeginInit();
            this.systemConfigContainer.Panel1.SuspendLayout();
            this.systemConfigContainer.Panel2.SuspendLayout();
            this.systemConfigContainer.SuspendLayout();
            this.serverConfigGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.urlContainer)).BeginInit();
            this.urlContainer.SuspendLayout();
            this.localConfigGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.systemConfigGroup.Size = new System.Drawing.Size(800, 220);
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
            this.systemConfigContainer.Size = new System.Drawing.Size(794, 195);
            this.systemConfigContainer.SplitterDistance = 385;
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
            this.serverConfigGroup.Size = new System.Drawing.Size(385, 195);
            this.serverConfigGroup.TabIndex = 0;
            this.serverConfigGroup.TabStop = false;
            this.serverConfigGroup.Text = "服务器配置";
            // 
            // urlContainer
            // 
            this.urlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlContainer.IsSplitterFixed = true;
            this.urlContainer.Location = new System.Drawing.Point(3, 64);
            this.urlContainer.MaximumSize = new System.Drawing.Size(0, 50);
            this.urlContainer.Name = "urlContainer";
            this.urlContainer.Size = new System.Drawing.Size(379, 50);
            this.urlContainer.SplitterDistance = 111;
            this.urlContainer.TabIndex = 1;
            this.urlContainer.TabStop = false;
            // 
            // localConfigGroup
            // 
            this.localConfigGroup.Controls.Add(this.splitContainer4);
            this.localConfigGroup.Controls.Add(this.splitContainer3);
            this.localConfigGroup.Controls.Add(this.splitContainer2);
            this.localConfigGroup.Controls.Add(this.splitContainer1);
            this.localConfigGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.localConfigGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localConfigGroup.Location = new System.Drawing.Point(0, 0);
            this.localConfigGroup.Name = "localConfigGroup";
            this.localConfigGroup.Size = new System.Drawing.Size(408, 195);
            this.localConfigGroup.TabIndex = 0;
            this.localConfigGroup.TabStop = false;
            this.localConfigGroup.Text = "本地配置";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Location = new System.Drawing.Point(0, 156);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Size = new System.Drawing.Size(408, 30);
            this.splitContainer4.SplitterDistance = 123;
            this.splitContainer4.TabIndex = 3;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Location = new System.Drawing.Point(0, 117);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Size = new System.Drawing.Size(408, 30);
            this.splitContainer3.SplitterDistance = 124;
            this.splitContainer3.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(0, 77);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Size = new System.Drawing.Size(408, 30);
            this.splitContainer2.SplitterDistance = 124;
            this.splitContainer2.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.urlLabel);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(0, 20, 8, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.urlTextBox);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10, 15, 10, 0);
            this.splitContainer1.Size = new System.Drawing.Size(408, 30);
            this.splitContainer1.SplitterDistance = 124;
            this.splitContainer1.TabIndex = 0;
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.urlLabel.Location = new System.Drawing.Point(90, 20);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(26, 12);
            this.urlLabel.TabIndex = 0;
            this.urlLabel.Text = "URL";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlTextBox.Location = new System.Drawing.Point(10, 15);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(260, 21);
            this.urlTextBox.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(270, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "startInStore";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(440, 354);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "stopInstore";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(270, 403);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 26);
            this.button3.TabIndex = 3;
            this.button3.Text = "startCheck";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(440, 403);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(115, 26);
            this.button4.TabIndex = 4;
            this.button4.Text = "stopCheck";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
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
            ((System.ComponentModel.ISupportInitialize)(this.urlContainer)).EndInit();
            this.urlContainer.ResumeLayout(false);
            this.localConfigGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox systemConfigGroup;
        private System.Windows.Forms.SplitContainer systemConfigContainer;
        private System.Windows.Forms.GroupBox serverConfigGroup;
        private System.Windows.Forms.GroupBox localConfigGroup;
        private System.Windows.Forms.SplitContainer urlContainer;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

