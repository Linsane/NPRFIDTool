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

namespace NPRFIDTool
{
    public partial class CheckInfoForm : Form
    {
        public CheckInfoForm()
        {
            InitializeComponent();
        }

        private ArrayList checkReaderInfos;
        private ArrayList checkControls;
        private const int width = 430;
        private const int height = 180;
        private const int gap = 10;

        public void setUpCheckReaderInfos(ArrayList infos)
        {
            this.checkReaderInfos = infos;
            if(checkControls == null)
            {
                checkControls = new ArrayList();
            }
            else
            {
                this.Controls.Clear();
                checkControls.Clear();
            }

            int index = 0;
            
            foreach (NPRFIDReaderInfo info in infos)
            {
                NPCustomWinFormControl.NPCheckInfoControl checkControl = new NPCustomWinFormControl.NPCheckInfoControl();
                checkControl.setTitle(info.title);
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
                checkControl.deleteHandler = () =>
                {
                    ArrayList newInfos = new ArrayList(infos);
                    newInfos.Remove(info);
                    setUpCheckReaderInfos(newInfos);
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
            int row = (this.checkReaderInfos.Count + 1) / 2;
            int buttonY = (height + gap) * row + gap;
            this.space.Location = new System.Drawing.Point(0, buttonY + 25);
            this.cancelButton.Location = new System.Drawing.Point(cancelX, buttonY);
            this.comfirmButton.Location = new System.Drawing.Point(comfirmX, buttonY);
            this.Controls.Add(this.space);
            this.Controls.Add(cancelButton);
            this.Controls.Add(this.comfirmButton);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comfirmButton_Click(object sender, EventArgs e)
        {

        }
    }
}
