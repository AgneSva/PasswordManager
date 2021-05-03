using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SAUGA
{
    public partial class form : Form
    {
        public string username;

        public form(string user)
        {

            InitializeComponent();
            //newpass.PasswordChar = '*';
            username = user;
            logged.Text = user;
            listView1.FullRowSelect = true;

        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            //go to start  page
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }



        private void createbtn_Click_1(object sender, EventArgs e)
        {
            ////opens and writes into users personal file with their passwords- new password
            using (System.IO.StreamWriter sw = new StreamWriter(@"C:\\Users\\inga3\\source\\repos\\SAUGA\\" + username + ".txt", true))
            {
                sw.WriteLine(newname.Text + "," + newpass.Text + "," + newurl.Text + "," + newcomm.Text);
            }

            //clear text boxes
            newname.Clear();
            newpass.Clear();
            newurl.Clear();
            newcomm.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newpass.Text = System.Guid.NewGuid().ToString();

        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            //when clicks search-searches for line with coresponding name,then fill the list view
            // List<string> values = new List<string>();
            listView1.Items.Clear();

            List<string> data = File.ReadAllLines(@"C:\\Users\\inga3\\source\\repos\\SAUGA\\" + username + ".txt").ToList();
            foreach (string d in data)
            {

                if (d.StartsWith(searchtxt.Text))
                {

                    string[] items = d.Split(new char[] { ',' },
                       StringSplitOptions.RemoveEmptyEntries);
                    listView1.Items.Add(new ListViewItem(items));
                }
            }

        }

        private void copybtn_Click(object sender, EventArgs e)
        {
            //read which item was selected from listview and copy it
            string selectedpass = listView1.SelectedItems[0].SubItems[1].Text;
            System.Windows.Forms.Clipboard.SetText(selectedpass);
            MessageBox.Show("Copied!");
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            //gets the name of selected:
            string selectedname = listView1.SelectedItems[0].SubItems[0].Text;

            string selectedpass = listView1.SelectedItems[0].SubItems[1].Text;
            string selectedurl = listView1.SelectedItems[0].SubItems[2].Text;
            string selectedcomm = listView1.SelectedItems[0].SubItems[3].Text;
          
            //delete from file
            File.WriteAllLines(@"C:\\Users\\inga3\\source\\repos\\SAUGA\\" + username + ".txt", File.ReadAllLines(@"C:\\Users\\inga3\\source\\repos\\SAUGA\\" + username + ".txt").Where(line => !line.Contains(selectedname) && !line.Contains(selectedpass)));

            //delete from listview
            foreach (ListViewItem eachItem in listView1.SelectedItems)
            {
                listView1.Items.Remove(eachItem);
                MessageBox.Show("Deleted!");
                listView1.Refresh();

            }
        }
    }
}
