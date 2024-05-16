using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=okul.mdb");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();

            OleDbCommand student_command = new OleDbCommand();
            student_command.Connection = connection;
            if (textBox1.Text !="")
            {
                student_command.CommandText = "select * from tbl_öğrenci where öğrenci_no=" + textBox1.Text;
            }
            else if (textBox2.Text !="")
            {
                student_command.CommandText = "select * from tbl_öğrenci where adısoyadı='" + textBox2.Text + "'";
            }
            else
            {
                MessageBox.Show("Ögrenci no veya adını giriniz");
            }

            OleDbDataReader student_read = student_command.ExecuteReader();
            student_read.Read();
            textBox1.Text = student_read[0].ToString();
            textBox2.Text = student_read[1].ToString();

            OleDbCommand grade_Command = new OleDbCommand("select * from tbl_not where öğrenci_no=" + textBox1.Text, connection);
            OleDbDataReader grade_Read = grade_Command.ExecuteReader();

            while (grade_Read.Read())
            {
                int lecutre_id = Convert.ToInt32(grade_Read["ders_id"]);
                string visa = grade_Read["vize"].ToString();
                string final = grade_Read["final"].ToString();
                string letter = grade_Read["harf"].ToString();

                OleDbCommand lecture_command = new OleDbCommand("select ders_adı from tbl_ders where ders_id=" + lecutre_id, connection);
                OleDbDataReader lecture_read = lecture_command.ExecuteReader();
                lecture_read.Read();
                string lecture_name = lecture_read[0].ToString();

                listBox1.Items.Add(lecture_name +" " +lecutre_id + " " +visa + " "+ final +" "+ letter);
            }


            connection.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            connection.Open();
            OleDbCommand combo_command = new OleDbCommand("select ders_adı from tbl_ders", connection);
            OleDbDataReader combo_read = combo_command.ExecuteReader();

            while (combo_read.Read())
            {
                comboBox1.Items.Add(combo_read[0]);
            }
            connection.Close();
        }
    }
}
