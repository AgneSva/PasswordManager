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
    public partial class Form3 : Form
    {
        public string nameUpdate;
        public string passwordUpdate;
        public string urlUpdate;
        public string commUpdate;
        public string username;
        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");

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
            newpasstxt.PasswordChar = '*';

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

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {

            //encrypt newly updated password -DES 
            string NewCryptedPass = Encrypt(newpasstxt.Text);
            Console.WriteLine("\nEncrypt Result: {0}", NewCryptedPass);

            string oldline= nameUpdate + "," + passwordUpdate + "," + urlUpdate + "," + commUpdate;
            string newline = nameUpdate + "," + NewCryptedPass + "," + urlUpdate + "," + commUpdate;
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
