using FluentAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FluentAPI
{
    public partial class Form2 : Form
    {
        public Employee employee { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
            string.IsNullOrWhiteSpace(textBox2.Text) ||
            string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var db = new AppDbContext())
            {

                Models.Employee newEmployee = new Models.Employee
                {
                    FirstName = textBox1.Text,
                    LastName = textBox2.Text,
                    Position = textBox3.Text
                };
                db.Employees.Add(newEmployee); ;
                db.SaveChanges();
                DialogResult = DialogResult.OK;

            }
            try
            {
                // Используем StreamWriter для добавления строки в файл
                using (StreamWriter writer = new StreamWriter("logs.txt", append: true))
                {
                    writer.WriteLine($"Добавлен сотрудник {employee.FirstName} {employee.LastName}!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при добавлении лога: " + ex.Message);
            }
            DialogResult = DialogResult.OK;
        }
    }
}
