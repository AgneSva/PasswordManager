using Scrypt;
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
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            regpass.PasswordChar = '*';
            passtxt.PasswordChar = '*';
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {

            ScryptEncoder encoder = new ScryptEncoder();
            string hashsedPassword = encoder.Encode(regpass.Text);

            //bool areEquals = encoder.Compare(PassToEncrypt, hashsedPassword);
            //Console.WriteLine(areEquals);

            //into text file with all users= new users name and hashed password
            using (System.IO.StreamWriter sw = new StreamWriter("C:\\Users\\inga3\\source\\repos\\SAUGA\\users.txt", true))
            {
                sw.WriteLine(reguser.Text + "," + hashsedPassword);
            }
            //create a file where all of the users passwords will be held
            FileStream fs = File.Create(@"C:\\Users\\inga3\\source\\repos\\SAUGA\\" + reguser.Text + ".txt");


            //tada reload
            this.Controls.Clear();
            this.InitializeComponent();


        }

        private void signBtn_Click(object sender, EventArgs e)
        {

            //compare password they try to sign up with, with hashed password in users.txt

            //find the line with users name


            string[] lineValues = new string[2];


            foreach (var line in File.ReadAllLines("C:\\Users\\inga3\\source\\repos\\SAUGA\\users.txt"))
            {

                if (line.Contains(usertxt.Text))
                {
                    lineValues = line.Split(',');

                }

            }
            string name = lineValues[0];
            string hashedpass = lineValues[1];

            ScryptEncoder encoder = new ScryptEncoder();
            bool areEquals = encoder.Compare(passtxt.Text, hashedpass);

            if (areEquals == true)
            {
                Console.WriteLine("user exists");
               // go to users main page
                    this.Hide();
                form f2 = new form(usertxt.Text);
                f2.Show();

            }
            else
            {
                MessageBox.Show("this user doesnt exist");
            }





        }
    }
}
