using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SAUGA
{
    public partial class form : Form
    {
        public string username;
        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
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

        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }


        private void createbtn_Click_1(object sender, EventArgs e)
        {


            //encrypt password from newpass textbox with des algorithm
            string cryptedPass = Encrypt(newpass.Text);
            Console.WriteLine("\nEncrypt Result: {0}", cryptedPass);


            ////opens and writes into users personal file with their passwords- new password
            using (System.IO.StreamWriter sw = new StreamWriter(@"C:\\Users\\inga3\\source\\repos\\SAUGA\\" + username + ".txt", true))
            {
                sw.WriteLine(newname.Text + "," + cryptedPass + "," + newurl.Text + "," + newcomm.Text);
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

            //decrypt before copying
            string decryptedPass = Decrypt(selectedpass);

            System.Windows.Forms.Clipboard.SetText(decryptedPass);
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

        private void updatebtn_Click(object sender, EventArgs e)
        {

            //which password to update??
            string selectedname = listView1.SelectedItems[0].SubItems[0].Text;
            string selectedpass = listView1.SelectedItems[0].SubItems[1].Text;
            string selectedurl = listView1.SelectedItems[0].SubItems[2].Text;
            string selectedcomm = listView1.SelectedItems[0].SubItems[3].Text;

            Console.WriteLine(selectedname);
            Console.WriteLine(selectedpass);

       
            // this.Hide();
            Form3 f3 = new Form3(username,selectedname, selectedpass,selectedurl,selectedcomm);
            f3.Show();
         
        }

        private void showbtn_Click(object sender, EventArgs e)
        {
            //selected password is decrypted
           string selectedpass = listView1.SelectedItems[0].SubItems[1].Text;
  
           string decryptedPass= Decrypt(selectedpass);
           listView1.SelectedItems[0].SubItems[1].Text = decryptedPass;

        }
    }
}
