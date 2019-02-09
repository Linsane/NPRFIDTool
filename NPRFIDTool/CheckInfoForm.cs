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

namespace NPRFIDTool
{
    public partial class CheckInfoForm : Form
    {
        public CheckInfoForm()
        {
            InitializeComponent();
        }

        private NPRFIDReaderInfo[] checkReaderInfos;

        public void setUpCheckReaderInfos(NPRFIDReaderInfo[] infos)
        {
            this.checkReaderInfos = infos;
            int index = 0;
            int gap = 10;
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
                    pointX = gap * 2 + 430;
                }
                pointY = gap * (index / 2 + 1) + 180 * (index / 2);
                checkControl.Location = new System.Drawing.Point(pointX, pointY);
                checkControl.Size = new System.Drawing.Size(430, 180);
                this.Controls.Add(checkControl);
                index++;
            }
        }
    }
}
