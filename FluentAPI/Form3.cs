using FluentAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            MessageBox.Show("Данные изменены!");
            this.Close();
        }
    }
}
