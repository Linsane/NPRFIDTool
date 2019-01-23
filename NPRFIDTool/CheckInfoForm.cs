using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using NPRFIDTool.NPKit;
using System.Collections;
using NPCustomWinFormControl;

namespace NPRFIDTool
{
    public partial class CheckInfoForm : Form
    {
        public delegate void ConfirmHandler(ArrayList checkReadderInfos);
        public CheckInfoForm()
        {
            InitializeComponent();
            this.checkReaderInfos = new ArrayList();
        }

        private ArrayList checkReaderInfos;
        private ArrayList checkControls;
        private const int width = 430;
        private const int height = 180;
        private const int gap = 10;
        public ConfirmHandler confirmHandler;

        public void setUpCheckReaderInfos(ArrayList infos)
        {
            this.checkReaderInfos = new ArrayList(infos);
            drawItems();
        }

        private void drawItems()
        {
            if (checkControls == null)
            {
                checkControls = new ArrayList();
            }
            else
            {
                this.Controls.Clear();
                checkControls.Clear();
            }

            int index = 0;

            foreach (NPRFIDReaderInfo info in checkReaderInfos)
            {
                NPCheckInfoControl checkControl = new NPCheckInfoControl();
                checkControl.setTitle("盘点器"+ (index+1));
                checkControl.setAddress(info.readerIP);
                checkControl.setPortNum(info.readerAntNum);
                checkControl.setUsedPort(info.usedPorts);
                int pointX = 0;
                int pointY = 0;
                if (index % 2 == 0)
                {
                    pointX = gap;
                }
                else
                {
                    pointX = gap * 2 + width;
                }
                pointY = gap * (index / 2 + 1) + height * (index / 2);
                checkControl.Location = new System.Drawing.Point(pointX, pointY);
                checkControl.Size = new System.Drawing.Size(width, height);
                checkControl.index = index;
                checkControl.deleteHandler = (itemIndex) =>
                {
                    checkReaderInfos.RemoveAt(itemIndex);
                    drawItems();
                };
                this.Controls.Add(checkControl);
                this.checkControls.Add(checkControl);
                index++;
            }
            updateButtonsLocation();
        }

        private void updateButtonsLocation()
        {
            int cancelX = this.cancelButton.Location.X;
            int comfirmX = this.comfirmButton.Location.X;
            int addX = this.addButton.Location.X;
            int row = (this.checkReaderInfos.Count + 1) / 2;
            int buttonY = (height + gap) * row + gap;
            this.space.Location = new System.Drawing.Point(0, buttonY + 25);
            this.cancelButton.Location = new System.Drawing.Point(cancelX, buttonY);
            this.comfirmButton.Location = new System.Drawing.Point(comfirmX, buttonY);
            this.addButton.Location = new System.Drawing.Point(addX, buttonY);
            this.Controls.Add(this.space);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.comfirmButton);
            this.Controls.Add(this.addButton);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comfirmButton_Click(object sender, EventArgs e)
        {
            foreach(NPCheckInfoControl control in this.checkControls)
            {
                NPRFIDReaderInfo info = (NPRFIDReaderInfo)this.checkReaderInfos[control.index];
                info.readerIP = control.getIpAddress();
                info.readerAntNum = control.getPortNum();
                info.usedPorts = control.getUsedPort();
                this.checkReaderInfos[control.index] = info;

            }
            if (confirmHandler != null)
            {
                confirmHandler(this.checkReaderInfos);
            }
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            NPRFIDReaderInfo info = new NPRFIDReaderInfo(PortType.PortTypeCheck, "", 0, new JArray(),0);
            this.checkReaderInfos.Add(info);
            this.drawItems();
            this.ScrollControlIntoView(this.space);
        }
    }
}
