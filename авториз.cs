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
using System.Data.Common;

namespace DataBase
{
    public partial class Form2 : Form
    {
        DB conDb = new DB();
        SqlDataAdapter adapter = null;
        SqlCommand command = null;
        DataTable table = null;

        public Form2()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            this.Close();
            Form1 frm1 = new Form1();
            frm1.Show();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
            buttonUpdate.PerformClick();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            conDb.OpenConnection();            
            adapter = new SqlDataAdapter();
            command = new SqlCommand("SELECT * FROM uslugi", conDb.GetConnection());
            adapter.SelectCommand = command;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
            dataGridView1.Columns["id"].Visible = false;
            conDb.CloseConnection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            conDb.OpenConnection();
            adapter = new SqlDataAdapter();
            command = new SqlCommand("INSERT INTO uslugi (name) VALUES (@name)", conDb.Connection());
            command.Parameters.AddWithValue("@name", textBoxName.Text);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            conDb.CloseConnection();
            buttonUpdate.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Удаление записи?", "Вы уверены что хотите удалить запись?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                String commSql = "DELETE FROM uslugi WHERE id =";
                String del = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();

                conDb.OpenConnection();
                adapter = new SqlDataAdapter();
                conDb.OpenConnection();
                command = new SqlCommand(commSql + del + " ", conDb.GetConnection());
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.ExecuteNonQuery();
                conDb.CloseConnection();
            }
            buttonUpdate.PerformClick();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
