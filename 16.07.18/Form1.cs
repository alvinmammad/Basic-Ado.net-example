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

namespace _16._07._18
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'classDataSet.Classes' table. You can move, or remove it, as needed.
            this.classesTableAdapter.Fill(this.classDataSet.Classes);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\user\source\repos\16.07.18\16.07.18\Class.mdf;Integrated Security=True";
            var connect = new SqlConnection(conString);
            connect.Open();
            var className = textBox2.Text;
            var classCount = Convert.ToInt32(textBox3.Text);
            var teacherBox = comboBox1.SelectedItem.ToString();
            var insertQuery = "INSERT INTO Classes(Classname,TeacherID,StudentCount) VALUES('" + className + "',(SELECT id FROM Teachers WHERE  TeacherName='" + teacherBox + "'), '" + classCount + "' )";
            var command = new SqlCommand(insertQuery, connect);
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Class was added");
            }
            connect.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\user\source\repos\16.07.18\16.07.18\Class.mdf;Integrated Security=True";
            var connect = new SqlConnection(conString);
            connect.Open();
            var teacherName = textBox1.Text;
            comboBox1.Items.Add(teacherName);
            var insertQuery = "INSERT INTO Teachers(TeacherName) VALUES('" + teacherName + "')";
            var command = new SqlCommand(insertQuery, connect);
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Teacher was added !");
            }

            var selectquery = "select * from Teachers";

            var select = new SqlDataAdapter(selectquery, connect);
            var ds = new DataSet();
            select.Fill(ds);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                comboBox1.Items.Add(item["TeacherName"]);
            }
            connect.Close();
        }
    }
}
