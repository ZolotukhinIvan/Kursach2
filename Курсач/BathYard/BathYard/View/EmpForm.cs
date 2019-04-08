using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BathYard
{
    public partial class EmpForm : Form
    {
        public Employee Employee { get; set; }

        public EmpForm(Employee emp, int newEmployee)
        {
            InitializeComponent();
            Employee = emp;
            errorTB.Text = string.Empty;
            if (newEmployee == 0)
            {
                loginTB.Text = Employee.Login;
                passTB.Text = Employee.Password;
                fioTB.Text = Employee.FullName;
                postTB.Text = Employee.Position;
                wageTB.Text = Convert.ToString(Employee.Wage);
                ageNUM.Value = Convert.ToDecimal(Employee.Age);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush lineBrush = new LinearGradientBrush(
                new Point(0, 0), new Point(0, 500),
                Color.LightCyan, Color.Aqua);
            g.FillRectangle(lineBrush, ClientRectangle);
        }

        private void yesBTN_Click(object sender, EventArgs e)
        {
            Validation();
            if (errorTB.Text == string.Empty)
            {
                Employee.Login = loginTB.Text;
                Employee.Password = passTB.Text;
                Employee.FullName = fioTB.Text;
                Employee.Position = postTB.Text;
                Employee.Wage = Convert.ToDouble(wageTB.Text);
                Employee.Age = Convert.ToInt32(ageNUM.Value);
                DialogResult = DialogResult.Yes;
            }
        }
        public void Validation()
        {
            errorTB.Text = string.Empty;
            if (loginTB.Text == string.Empty ||
                passTB.Text == string.Empty ||
                fioTB.Text == string.Empty ||
                postTB.Text == string.Empty ||
                wageTB.Text == string.Empty)
            {
                errorTB.Text = "Заполните все данные!";
                return;
            }
            if (passTB.TextLength < 3)
            {
                errorTB.Text = "Пароль не может быть меньше 3 символов!";
                return;
            }
            double wage = 0;
            try
            {
                wage = Convert.ToDouble(wageTB.Text);
            }
            catch(Exception ex)
            {
                errorTB.Text = "Неправильный формат зарплаты!";
                return;
            }
            if(wage <= 0)
            {
                errorTB.Text = "Увеличьте зарплату!";
                return;
            }
        }

        private void cancelBTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

    }
}
