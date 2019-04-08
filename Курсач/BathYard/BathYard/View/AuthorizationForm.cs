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
    public partial class AuthrizationForm : Form
    {
        IAutorizeService autorizeService;

        public AuthrizationForm(IAutorizeService autorizeService)
        {
            InitializeComponent();
            this.autorizeService = autorizeService;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush lineBrush = new LinearGradientBrush(
                new Point(0, 0), new Point(0, 500),
                Color.LightCyan, Color.Aqua);
            g.FillRectangle(lineBrush, ClientRectangle);
        }

        private void exitBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void enterBTN_Click(object sender, EventArgs e)
        {
            Employee employee = autorizeService.Authorize(loginTB.Text, passTB.Text);
            if (employee == null)
            {
                MessageBox.Show("Неправильный ввод!");
                return;
            }
            HeadForm headForm = new HeadForm(employee, new RepositoryJs());
            Visible = !Visible;
            headForm.ShowDialog();
            Visible = !Visible;
        }
    }
}
