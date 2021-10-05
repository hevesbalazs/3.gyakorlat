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
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();            
            lblFullName.Text = Resource1.FullName; //label2
            btnAdd.Text = Resource1.Add; //button1
            btnWriteFile.Text = Resource1.WriteFile; //button2
            //listUsers
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = txtFullName.Text
            };
            users.Add(u);
        }

        private void btnWriteFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "Names.txt";
            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                var currentUser = new User();
                StreamWriter writer = new StreamWriter(saveFile.OpenFile());
                foreach (User item in users)
                {
                    currentUser = item;
                    writer.WriteLine(currentUser.ID.ToString()+" "+currentUser.FullName);

                };
                writer.Dispose();
                writer.Close();

            }
        }
    }
}
