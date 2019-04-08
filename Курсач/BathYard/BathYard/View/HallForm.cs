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
    public partial class HallForm : Form
    {
        public Hall Hall { get; set; }

        public HallForm(Hall hall, int newHall)
        {
            InitializeComponent();
            Hall = hall;
            errorTB.Text = string.Empty;
            if(newHall == 0)
            {
                nameTB.Text = Hall.Name;
                priceTB.Text = Convert.ToString(Hall.Price);
                capacityNUM.Value = Convert.ToDecimal(Hall.Capacity);
                vipRB.Checked = Hall.OnlyVip;
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
                Hall.Name = nameTB.Text;
                Hall.Price = Convert.ToDouble(priceTB.Text);
                Hall.Capacity = Convert.ToInt32(capacityNUM.Value);
                Hall.OnlyVip = vipRB.Checked;
                DialogResult = DialogResult.Yes;
            }
        }

        public void Validation()
        {
            errorTB.Text = string.Empty;
            if (nameTB.Text == string.Empty ||
                priceTB.Text == string.Empty)
            {
                errorTB.Text = "Заполните все данные!";
                return;
            }
            double price = 0;
            try
            {
                price = Convert.ToDouble(priceTB.Text);
            }
            catch (Exception ex)
            {
                errorTB.Text = "Неправильный формат цены!";
                return;
            }
            if (price <= 0)
            {
                errorTB.Text = "Увеличьте цену!";
                return;
            }
        }

        private void cancelBTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
