using Group3_Project.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Group3_Project
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private bool checkTextBox()
        {
            if (txtUser.Text.Trim().Length == 0)
            {
                MessageBox.Show("Username is blank!");
                return false;
            }
            if (txtPass.Text.Trim().Length == 0)
            {
                MessageBox.Show("Password is blank!");
                return false;
            }           
            if (txtInputCaptcha.Text.Trim().Length == 0)
            {
                MessageBox.Show("Captcha is blank!");
                txtCaptcha.Text = randomCaptcha();
                return false;
            }
            if(txtInputCaptcha.Text != txtCaptcha.Text)
            {
                MessageBox.Show("Captcha is wrong!");
                txtCaptcha.Text = randomCaptcha();
                return false;
            }
            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!checkTextBox())
            {
                return;
            }
            string pass = GetMD5(txtPass.Text);
            Account a = Database.Login(txtUser.Text, pass);
            if (a == null)
            {
                MessageBox.Show("Username or Password is wrong!");
                txtCaptcha.Text = randomCaptcha();
            }
            else
            {
                this.Hide();
                frmManage manage = new frmManage(Convert.ToString(a.Id));
                manage.ShowDialog();
                this.Close();
            }
        }
        private String GetMD5(string txt)
        {
            String str = "";
            Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer)
            {
                str += b.ToString("X2");
            }
            return str;
        }
        public static byte[] encryptData(String data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(String data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
        public string randomCaptcha()
        {
            Random rnd = new Random();
            int number = rnd.Next(10000, 99999);
            string text = md5(number.ToString());
            text = text.ToUpper();
            text = text.Substring(0, 6);
            return text;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtCaptcha.Text = randomCaptcha();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            txtCaptcha.Text = randomCaptcha();
        }
    }
}
