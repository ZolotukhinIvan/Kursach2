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
    public partial class CustForm : Form
    {
        public Customer Customer { get; set; }

        public CustForm(Customer cust, int newCustomer)
        {
            InitializeComponent();
            Customer = cust;
            errorTB.Text = string.Empty;
            if (newCustomer == 0)
            {
                fioTB.Text = Customer.FullName;
                ageNUM.Value = Convert.ToDecimal(Customer.Age);
                vipCHB.Checked = Customer.Vip;
            }
            else
            {
                Customer.CountVisits = 0;
                Customer.MoneySpent = 0;
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
                Customer.FullName = fioTB.Text;
                Customer.Age = Convert.ToInt32(ageNUM.Value);
                Customer.Vip = vipCHB.Checked;
                DialogResult = DialogResult.Yes;
            }
        }

        public void Validation()
        {
            errorTB.Text = string.Empty;
            if (fioTB.Text == string.Empty)
            {
                errorTB.Text = "Заполните все данные!";
                return;
            }
        }

        private void cancelBTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
