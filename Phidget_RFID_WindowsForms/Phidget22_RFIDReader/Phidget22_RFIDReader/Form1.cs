using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Phidget22;
using Phidget22.Events;

namespace Phidget22_RFIDReader
{
    public partial class Form1 : Form
    {
        private RFID rfid;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "tag:                    -";
            label2.Text = "Status:                 Not Connected";
            label3.Text = "Serial Number:          -";

            checkBox1.Checked = false;
            checkBox1.Enabled = false;

            rfid = new RFID();

            rfid.Attach += new AttachEventHandler(rfid_Attach);
            rfid.Detach += new DetachEventHandler(rfid_Detach);
            rfid.Error += new ErrorEventHandler(rfid_Error);

            rfid.Tag += new RFIDTagEventHandler(rfid_Tag);
            rfid.TagLost += new RFIDTagLostEventHandler(rfid_TagLost);

            rfid.Open();
        }


        private void rfid_Tag(object sender, RFIDTagEventArgs e)
        {
            label1.Text = "Tag:              " + e.Tag;
        }

        private void rfid_TagLost(object sender, RFIDTagLostEventArgs e)
        {
            label1.Text = "Tag:              -";
        }


        void rfid_Attach(object sender, AttachEventArgs e)
        {
            label1.Text = "Tag:              -";
            label2.Text = "Status:           Connected";
            label3.Text = "Serial Number:    " + rfid.DeviceSerialNumber.ToString();

            checkBox1.Checked = rfid.AntennaEnabled;
            checkBox1.Enabled = true;
        }

        private void rfid_Detach(object sender, DetachEventArgs e)
        {
            label1.Text = "Tag:              -";
            label2.Text = "Status:           Not Connected";
            label3.Text = "Serial Number:    -";

            checkBox1.Checked = false;
            checkBox1.Enabled = false;
        }

        private void rfid_Error(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(e.Description);

            rfid.Close();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            rfid.AntennaEnabled = checkBox1.Checked;
        }

    }
}
