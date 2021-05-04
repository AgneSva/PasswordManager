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
    public partial class Form3 : Form
    {
        public string nameUpdate;
        public string passwordUpdate;
        public string urlUpdate;
        public string commUpdate;
        public string username;

        public Form3(string user,string name, string oldpass,string url, string comm)
        {
            InitializeComponent();
            label3.Text = name;
            label6.Text = url;
            label8.Text = comm;

            nameUpdate = name;
            urlUpdate = url;
            commUpdate = comm;
            passwordUpdate = oldpass;
            username = user;

        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            //paimti nauja slaptazodi 
            string newpass = newpasstxt.Text;
            string oldline= nameUpdate + "," + passwordUpdate + "," + urlUpdate + "," + commUpdate;
            string newline = nameUpdate + "," + newpass + "," + urlUpdate + "," + commUpdate;
            Console.WriteLine(newline);


            string[] lines = File.ReadAllLines(@"C:\\Users\\inga3\\source\\repos\\SAUGA\\" + username + ".txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(nameUpdate) && lines[i].Contains(passwordUpdate) && lines[i].Contains(urlUpdate) && lines[i].Contains(commUpdate))
                {
                    lines[i] = newline;
                }

            }

            File.WriteAllLines(@"C:\\Users\\inga3\\source\\repos\\SAUGA\\" + username + ".txt", lines);


            this.Hide();

        }
    }
}
