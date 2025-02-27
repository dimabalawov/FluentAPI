using FluentAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FluentAPI
{
    public partial class Form3 : Form
    {
        public Models.Employee changedEmployee { get; private set; }
        public Form3(Models.Employee employee)
        {
            InitializeComponent();
            changedEmployee = employee;
            textBox1.Text = employee.FirstName;
            textBox2.Text = employee.LastName;
            textBox3.Text = employee.Position;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            changedEmployee.FirstName = textBox1.Text;
            changedEmployee.LastName = textBox2.Text;
            changedEmployee.Position = textBox3.Text;
            using (var db = new AppDbContext())
            {
                db.Employees.Update(changedEmployee);  
                db.SaveChanges();
            }
            try
            {
                // Используем StreamWriter для добавления строки в файл
                using (StreamWriter writer = new StreamWriter("logs.txt", append: true))
                {
                    writer.WriteLine($"Данные сотрудника {changedEmployee.FirstName} {changedEmployee.LastName} изменены!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при добавлении лога: " + ex.Message);
            }
            MessageBox.Show("Данные изменены!");
            this.Close();
        }
    }
}
