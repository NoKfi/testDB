using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Relese
{
    public partial class Form1 : Form
    {
        SqlCon conn = new SqlCon();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                textBox2.UseSystemPasswordChar = true; 
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String login = textBox1.Text;
            String pass = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            SqlCommand command = new SqlCommand("SELECT * FROM login WHERE login = @login AND password = @password", conn.getConnection());

            command.Parameters.AddWithValue("@login", SqlDbType.VarChar).Value = login;
            command.Parameters.AddWithValue("@password", SqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                Form1.ActiveForm.Hide();
                this.Hide();
                FormReder frmMen = new FormReder();
                frmMen.Show();
            }
            else
            {
                MessageBox.Show("Error");
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Выход", "Вы точно хотите выйти?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }
    }
}
