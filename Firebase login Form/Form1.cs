using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Firebase_login_Form
{
    public partial class Form1 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        { 
            AuthSecret= "xfNsmA32P269GuCHQ94GuxVhoJjou0m6HUp9yELI",
            BasePath= "https://regform-6ee6d-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
            }
            
        }
        public static string usernamepass;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Please put username and password.");
                }
                else
                {
                    FirebaseResponse response = client.Get(@"Users/");
                    register ResUser = response.ResultAs<register>();
                    register CurUser = new register
                    {
                        username = textBox1.Text,
                        password=textBox2.Text
                    };
                    if(register.IsEqual(ResUser,CurUser))
                    {
                        frmHome frm = new frmHome();
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }   
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Your ID is didn't match. Please signup again!");
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong. Please restart the system.");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            if(client!=null)
            {

            }
            else
            {
                MessageBox.Show("Connection error. Try again!");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmRegister frm = new frmRegister();
            this.Hide();
            frm.ShowDialog();
        }
    }

    public class register
    {
        public string username { get; internal set; }
        public string password { get; internal set; }
        public string id { get; set; }

        private static string error = "There was some errorr";
        public static bool IsEqual(register user1,register user2)
        {
            if(user1==null || user2==null)
            {
                return false;
            }
            if(user1.username!=user2.username)
            {
                error = "Username doesnt exist!";
                return false;
            }
            if (user1.password != user2.password)
            {
                error = "Username and password doesn't match";
                return false;
            }
            return true;
        }
    }
}
