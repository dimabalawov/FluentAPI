using FluentAPI.Models;
using System.Diagnostics.PerformanceData;

namespace FluentAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadEmployees()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var employees = db.Employees.ToList();
                    listView1.Items.Clear();

                    foreach (var emp in employees)
                    {
                        var item = new ListViewItem(emp.FirstName);
                        item.SubItems.Add(emp.LastName);
                        item.SubItems.Add(emp.Position);
                        item.Tag = emp.Id;
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 addForm = new Form2();

            if (addForm.ShowDialog() == DialogResult.OK)
            {
                using (AppDbContext db = new AppDbContext())
                {
                    db.Employees.Add(addForm.employee);
                    db.SaveChanges();
                }

                LoadEmployees();
            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedItem = listView1.SelectedItems[0];
                int employeeId = (int)selectedItem.Tag;

                using (var db = new AppDbContext())
                {
                    var employee = db.Employees.Find(employeeId);
                    if (employee != null)
                    {
                        db.Employees.Remove(employee);
                        db.SaveChanges();
                        MessageBox.Show("Сотрудник удален!");

                        LoadEmployees();
                    }
                    else
                    {
                        MessageBox.Show("Сотрудник не найден!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника");
            }
        }
        private void FilterEmployees(string searchTerm)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                bool matchesSearch = item.SubItems[0].Text.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                     item.SubItems[1].Text.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                     item.SubItems[2].Text.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                item.Selected = !matchesSearch;

                item.ForeColor = matchesSearch ? SystemColors.ControlText : SystemColors.Window;
                item.Selected = false;
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.Trim();
            FilterEmployees(searchTerm);
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedItem = listView1.SelectedItems[0];
                int employeeId = (int)selectedItem.Tag;

                using (var db = new AppDbContext())
                {
                    var employee = db.Employees.Find(employeeId);
                    if (employee != null)
                    {
                        Form3 editForm = new Form3(employee);
                        editForm.ShowDialog();
                        LoadEmployees();
                    }
                    else
                    {
                        MessageBox.Show("Сотрудник не найден!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника");
            }
        }
    }
}
