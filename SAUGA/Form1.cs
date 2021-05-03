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
            //kai paspaudzia registruotis- paima is text box'u reiksmes ir iraso i users.txt
            Console.WriteLine(this.reguser);
            using (System.IO.StreamWriter sw = new StreamWriter("C:\\Users\\inga3\\source\\repos\\SAUGA\\users.txt", true))
            {
                sw.WriteLine(reguser.Text + "," + regpass.Text);
        }
            //create a file where all of the users passwords will be held
            FileStream fs = File.Create(@"C:\\Users\\inga3\\source\\repos\\SAUGA\\" + reguser.Text + ".txt");


            //tada reload
            this.Controls.Clear();
            this.InitializeComponent();


        }

        private void signBtn_Click(object sender, EventArgs e)
        {
            //kai paspaudzia sign up- pasiziuri ar user.txt yra toks stringas kaip signup text'boxuose
            //see if line contains

            if (File.ReadLines("C:\\Users\\inga3\\source\\repos\\SAUGA\\users.txt").Any(line => line.Contains(usertxt.Text) && line.Contains( passtxt.Text)))
            {
                //if yes
                Console.WriteLine("user exists");
                //go to users main page
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
