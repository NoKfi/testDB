using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Vet_Base
{
    public partial class CreditForm : Form
    {
        private string creditID;
        
        public CreditForm(string nextId)
        {
            InitializeComponent();
            creditID = nextId;
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            string BD = "Database.mdb";
            string Con_Bd = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" +BD;

            using (OleDbConnection connection = new OleDbConnection(Con_Bd))
            {
                string my_Tabl = "Credit";
                OleDbDataAdapter adapter = new OleDbDataAdapter();

                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM " + my_Tabl + " Where CreditInfoId =" + creditID, connection);
                adapter.SelectCommand = command;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView2.DataSource = dataSet.Tables[0];
                connection.Close();
            }
        
        dataGridView2.Columns["IDCredit"].Visible = false;
        dataGridView2.Columns["CreditNumber"].HeaderText = "Номер кредита";
        dataGridView2.Columns["CreditRab"].HeaderText = "Работник банка";
        dataGridView2.Columns["CreditDataVi"].HeaderText = "Дата выдачи";
        dataGridView2.Columns["CreditDataVo"].HeaderText = "Дата возрата";
        dataGridView2.Columns["CreditSumma"].HeaderText = "Сумма";
        dataGridView2.Columns["CreditProc"].HeaderText = "Проценты";
        dataGridView2.Columns["CreditSrok"].HeaderText = "На сколько выдан";
        dataGridView2.Columns["CreditValut"].HeaderText = "Валюта";
        dataGridView2.Columns["CreditInfoId"].Visible = false;


        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
  
        private void AddButton_Click(object sender, EventArgs e)
        {
            string BankBD = "Database.mdb";
            string Con_Bd = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + BankBD;
            if (textBoxNumber.Text == "" && textBoxNumber.MaxLength != 10 || textBoxRab.Text == "" || textBoxSumma.Text == "" || textBoxProc.Text == "" || textBoxSrok.Text == "" || textBoxValut.Text == "" )
            {
                MessageBox.Show("Вы не ввели данные!");
                return;
            }
            else
            {
                using (OleDbConnection connection = new OleDbConnection(Con_Bd)) // string connecting 
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    connection.Open(); 
					OleDbCommand command = new OleDbCommand("INSERT INTO Credit (CreditNumber, CreditRab, CreditDataVi, CreditDataVo, CreditSumma, CreditProc, CreditSrok, CreditValut, CreditInfoId) " +
                    " VALUES ('" + textBoxNumber.Text + "' , '" + textBoxRab.Text + "' ,  '" + dateVi.Text + "', '" + dateVo.Text + "','" + textBoxSumma.Text + "','" + textBoxProc.Text + "'"+
                    ", '" + textBoxSrok.Text + "' ,'" + textBoxValut.Text + "' , '" + creditID + "')", connection);              
                    adapter.InsertCommand = command;
                    adapter.InsertCommand.ExecuteNonQuery();        
                    connection.Close();
                    connect.PerformClick();
                    if (textBoxNumber.Text != " " || textBoxRab.Text != "" || dateVi.Text != " " || dateVo.Text != ""|| textBoxSumma.Text != "" ||  textBoxProc.Text != "" || textBoxSrok.Text != "" || textBoxValut.Text != "")
                    {
                        textBoxNumber.Text = " ";
                        textBoxRab.Text = " ";
                        textBoxSumma.Text = "";
                        textBoxProc.Text = "";
                        textBoxSrok.Text = "";
                        textBoxValut.Text = "";

                    }
                }
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        

        private void TextBoxBreed_TextChanged(object sender, EventArgs e)
        {

        }

     

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            string caption = "Удалить";
            string message = "Вы уверены, что хотите удалить запись?";
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (result != DialogResult.No)
                {
                    string BankBD = "Database.mdb";
                    string Con_Bd = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + BankBD;


                    if (dataGridView2.Rows.Count >= 1)
                    {
                        string str_Del = dataGridView2[0, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                        using (OleDbConnection connection = new OleDbConnection(Con_Bd))
                        {
                            OleDbDataAdapter adapter = new OleDbDataAdapter();
                            connection.Open();

                            OleDbCommand command = new OleDbCommand("DELETE FROM Credit  WHERE IDCredit = " + str_Del + " ", connection);
                            adapter.DeleteCommand = command;
                            adapter.DeleteCommand.ExecuteNonQuery();
                            connection.Close();
                            connect.PerformClick();

                        }
                    }
                    else
                    {
                        MessageBox.Show("База данных не загружена");
                    }
                }
            }
            catch
            {
                MessageBox.Show("База данных не загружена");
            }
        }

        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                dataGridView2.ReadOnly = true;
            }
            else
            {
                dataGridView2.ReadOnly = false;
                string Vet_BD = "Database.mdb";
                string Con_Bd = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Vet_BD;

                string fullTabl = dataGridView2[0, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                string Colm1 = dataGridView2[1, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                string Colm2 = dataGridView2[2, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                string Colm3 = dataGridView2[3, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                string Colm4 = dataGridView2[4, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                string Colm5 = dataGridView2[5, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                string Colm6 = dataGridView2[6, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                string Colm7 = dataGridView2[7, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                string Colm8 = dataGridView2[8, dataGridView2.CurrentCell.RowIndex].Value.ToString();


                using (OleDbConnection connection = new OleDbConnection(Con_Bd))
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("UPDATE Credit SET  CreditNumber= '" + Colm1 +
                        "', CreditRab= '" + Colm2 +
                        "', CreditDataVi= '" + Colm3 +
                        "', CreditDataVo='" + Colm4 +
                        "', CreditSumma= '" + Colm5 +
                        "', CreditProc= '" + Colm6 +
                        "', CreditSrok= '" + Colm7 +
                        "', CreditValut= '" + Colm8 +
                        "' where IDCredit =" + fullTabl + "", connection);
                    adapter.UpdateCommand = command;
                    adapter.UpdateCommand.ExecuteNonQuery();
                    connection.Close();
                }

            }
            
        }              
     
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                dataGridView2.ReadOnly = true;
            }
            else
            {
                dataGridView2.ReadOnly = false;
            }
            
        }
               
        private void buttonSearch_Click(object sender, EventArgs e)
        {

            string BankBD = "Database.mdb";
            string Con_Bd = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + BankBD;
            using (OleDbConnection connection = new OleDbConnection(Con_Bd))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM Credit  WHERE (CreditNumber Like '%" + textBoxSearch.Text +  "%' )", connection);
                adapter.SelectCommand = command;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView2.DataSource = dataSet.Tables[0];
                connection.Close();
            }
            dataGridView2.Columns["IDCredit"].Visible = false;
            dataGridView2.Columns["CreditNumber"].HeaderText = "Номер кредита";
            dataGridView2.Columns["CreditRab"].HeaderText = "Работник банка";
            dataGridView2.Columns["CreditDataVi"].HeaderText = "Дата выдачи";
            dataGridView2.Columns["CreditDataVo"].HeaderText = "Дата возрата";
            dataGridView2.Columns["CreditSumma"].HeaderText = "Сумма";
            dataGridView2.Columns["CreditProc"].HeaderText = "Проценты";
            dataGridView2.Columns["CreditSrok"].HeaderText = "На сколько выдан";
            dataGridView2.Columns["CreditValut"].HeaderText = "Валюта";
            dataGridView2.Columns["CreditInfoId"].Visible = false;
        }

        


        private void Form1_Shown(object sender, EventArgs e)
        {
            connect.PerformClick();
        }



        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSumma_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            
            CreditForm Clouse = new CreditForm(creditID);
            this.Hide();

            InfoForm nf = new InfoForm();
            nf.Show();
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            string nextId_Pet = dataGridView2.Rows[e.RowIndex].Cells["IDCredit"].Value.ToString();
            ClientForm1 nextForm_Pet = new ClientForm1(nextId_Pet);
            nextForm_Pet.ShowDialog();
        }

        private void buttonClients_Click(object sender, EventArgs e)
        {
            SpisokForm new_Form = new SpisokForm();
            new_Form.Show();
        }

        private void CreditForm_Load(object sender, EventArgs e)
        {
            //buttonSearch.PerformClick();
        }

        private void buttonDan_Click(object sender, EventArgs e)
        {
            DogForm new_Form = new DogForm();
            new_Form.Show();
        }
    }
}