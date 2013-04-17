using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LC;
using System.Threading;
namespace HL.Device.HT5000
{
    public partial class BthForm : Form
    {
        Bluetooth bth = new Bluetooth();
        List<DeviceInfo> deviceList = new List<DeviceInfo>();
        bool discovering = false;
        string pairedDevice;
        public BthForm()
        {
            InitializeComponent();
            this.bth.InquiryResult += new EventHandler<InquiryResultEventArgs>(bth_InquiryResult);
        }

        void bth_InquiryResult(object sender, InquiryResultEventArgs e)
        {
            this.Invoke((Action<DeviceInfo>)delegate(DeviceInfo info)
            {
                foreach (DeviceInfo inf in deviceList)
                {
                    if (inf.Address == info.Address)
                    {
                        deviceList.Remove(inf);
                        break;
                    }
                }
                deviceList.Add(info);


                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.SubItems[1].Text == info.Address)
                    {
                        item.Text = info.Name;
                        return;
                    }
                }

                listView1.Items.Add(new ListViewItem(new string[] { info.Name, info.Address }));

            }, e.Info);
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            deviceList.Clear();
            listView1.Items.Clear();


            Thread t = new Thread((ThreadStart)delegate()
            {
                discovering = true;
                bth.DiscoverDevice();
                discovering = false;

                this.Invoke((Action<bool>)delegate(bool bEnabled)
                {
                    btnSearch.Enabled = bEnabled;
                }, true);


            });
            t.Start();
        }

        private void btnDisconn_Click(object sender, EventArgs e)
        {
            if (bth.IsConnected)
            {
                bth.Disconnect();
                bth.UnPair(pairedDevice);
                pairedDevice = null;

                bthConn.Enabled = true;
                bthDisconn.Enabled = false;
            }
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0 || listView1.SelectedIndices.Count == 0)
            {
                return;
            }

            if (!bth.IsConnected)
            {
                string addr = "";

                string name = (string)listView1.SelectedIndices[0].ToString();
                foreach (DeviceInfo info in deviceList)
                {
                    if (info.Name == name)
                    {
                        addr = info.Address;
                        break;
                    }
                }

                if (bth.Pair(addr, pin.Text))
                {
                    pairedDevice = addr;
                    if (bth.Connect(addr))
                    {
                        bthConn.Enabled = false;
                        bthDisconn.Enabled = true;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (discovering)
            {                
                return;
            }
            this.bth.InquiryResult -= new EventHandler<InquiryResultEventArgs>(bth_InquiryResult);
        }
    }
}