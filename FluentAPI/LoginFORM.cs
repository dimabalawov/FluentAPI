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
    public partial class LoginFORM : Form
    {
        public bool IsAuthenticated { get; private set; } = false;
        public LoginFORM()
        {
            InitializeComponent();
        }

        private void LoginFORM_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox2.Text == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == textBox1.Text);

                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                    return;
                }

                if (user.Password != textBox2.Text)
                {
                    MessageBox.Show("Неверный пароль!");
                    return;
                }
                IsAuthenticated = true;
                MessageBox.Show("Успешный вход!");
                DialogResult = DialogResult.OK;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox2.Text == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            using (var db = new AppDbContext())
            {

                bool userExists = db.Users.Any(u => u.Login == textBox1.Text);

                if (userExists)
                {
                    MessageBox.Show("Такой пользователь уже есть!");
                    return;
                }
                else
                {
                    Models.User newUser = new Models.User
                    {
                        Login = textBox1.Text,
                        Password = textBox2.Text,
                    };
                    IsAuthenticated = true;
                    db.Users.Add(newUser); ;
                    db.SaveChanges();
                    DialogResult = DialogResult.OK;
                }

            }

        }
    }
}
