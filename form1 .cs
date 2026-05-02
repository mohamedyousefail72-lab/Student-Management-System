using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Linq;
using System.IO;

namespace StudentSystem
{
    public enum Level
    {
        First,
        Second,
        Third,
        Fourth
    }

    public partial class Form1 : Form
    {
        List<Student> students = new List<Student>();
        int currentID = 1;

        public Form1()
        {
            InitializeComponent();
            comboLevel.DataSource = Enum.GetValues(typeof(Level));
        }

        void ShowData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;
        }

        void SaveToFile()
        {
            List<string> lines = new List<string>();

            foreach (var s in students)
            {
                lines.Add($"{s.ID},{s.Name},{s.Age},{s.Level}");
            }

            File.WriteAllLines("students.txt", lines);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.ReadOnly = true;
            
            if (File.Exists("students.txt"))
            {
                var lines = File.ReadAllLines("students.txt");

                foreach (var line in lines)
                {
                    var data = line.Split(',');

                    students.Add(new Student()
                    {
                        ID = int.Parse(data[0]),
                        Name = data[1],
                        Age = int.Parse(data[2]),
                        Level = (Level)Enum.Parse(typeof(Level), data[3])
                    });

                    currentID = students.Max(x => x.ID) + 1;
                }

                ShowData();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtAge.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                comboLevel.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value;
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                Student s = new Student()
                {
                    ID = currentID++,
                    Name = txtName.Text,
                    Age = int.Parse(txtAge.Text),
                    Level = (Level)comboLevel.SelectedItem
                };

                students.Add(s);
                ShowData();
                SaveToFile();

                MessageBox.Show("تمت الإضافة");
            }
            catch
            {
                MessageBox.Show("فيه خطأ في البيانات");
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;

                var student = students.FirstOrDefault(x => x.ID == id);

                if (student != null)
                {
                    student.Name = txtName.Text;
                    student.Age = int.Parse(txtAge.Text);
                    student.Level = (Level)comboLevel.SelectedItem;

                    ShowData();
                    SaveToFile();

                    MessageBox.Show("تم التعديل");
                }
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;

                var student = students.FirstOrDefault(x => x.ID == id);

                if (student != null)
                {
                    students.Remove(student);
                    ShowData();
                    SaveToFile();

                    MessageBox.Show("تم الحذف");
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            groupBox1.Text = " Student Info";
            groupBox1.Visible = false;
            groupBox1.Visible = true;
            groupBox1.Enabled = false;
            
        }
    }
}
