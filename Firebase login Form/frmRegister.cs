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
    public partial class frmRegister : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "xfNsmA32P269GuCHQ94GuxVhoJjou0m6HUp9yELI",
            BasePath = "https://regform-6ee6d-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        public frmRegister()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please put all data needed.");
            }
            else
            {
                var register = new register
                {
                    id = textBox3.Text,
                    username = textBox1.Text,
                    password = textBox2.Text
                };

                FirebaseResponse response = client.Set("Users/" + textBox3.Text, register);
                register res = response.ResultAs<register>();
                MessageBox.Show("Register account successfully");
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                this.Dispose();
                Form1 frm = new Form1();
                frm.ShowDialog();
                    }
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {

            }
            else
            {
                MessageBox.Show("Connection error. Try again!");
            }
        }

        public void id_Leave(object sender,EventArgs e)
        {
            FirebaseResponse response = client.Get("Users/");
            Dictionary<string, register> getSameId = response.ResultAs<Dictionary<string, register>>();

            foreach(var sameID in getSameId)
            {
                string getsame = sameID.Value.id;
                if(textBox1.Text==getsame)
                {
                    MessageBox.Show("ID is already taken");
                    textBox1.Text = String.Empty;
                    break;
                }
            }
        }
        
    }
}
