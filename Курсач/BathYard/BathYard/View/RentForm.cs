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
    public partial class RentForm : Form
    {
        public Rent Rent { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Hall> Halls { get; set; }
        public List<Rent> Rents { get; set; }

        public RentForm(Rent rent, int newRent, List<Customer> emps, List<Hall> halls, List<Rent> rents)
        {
            InitializeComponent();
            Rent = rent;
            Customers = emps;
            Halls = halls;
            Rents = rents;
            errorTB.Text = string.Empty;
            foreach(Customer customer in Customers)
            {
                custCB.Items.Add(customer.FullName);
            }
            foreach(Hall hall in Halls)
            {
                hallCB.Items.Add(hall.Name);
            }
            if(newRent == 0)
            {
                custCB.SelectedItem = Rent.Customer.FullName;
                hallCB.SelectedItem = Rent.Hall.Name;
                peopleNUM.Value = Rent.People;
                durationNUM.Value = Rent.Hours;
                datestartDTP.Value = Rent.TimeStart.Date;
                timestartNUM.Value = Rent.TimeStart.Hour;
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
                Rent.Customer = Customers.FirstOrDefault(c => c.FullName == (string)custCB.SelectedItem);
                Rent.CustomerID = Rent.Customer.ID;
                Rent.Hall = Halls.FirstOrDefault(h => h.Name == (string)hallCB.SelectedItem);
                Rent.HallID = Rent.Hall.ID;
                Rent.People = Convert.ToInt32(peopleNUM.Value);
                Rent.Hours = Convert.ToInt32(durationNUM.Value);
                Rent.TimeStart = datestartDTP.Value.Date;
                Rent.TimeStart = Rent.TimeStart.AddHours(Convert.ToInt32(timestartNUM.Value));
                Rent.Price = Rent.Hall.Price * Rent.Hours;
                Rent.Customer.CountVisits++;
                Rent.Customer.MoneySpent += Rent.Price;
                DialogResult = DialogResult.Yes;
            }
        }

        public void Validation()
        {
            errorTB.Text = string.Empty;
            if (custCB.SelectedItem == null)
            {
                errorTB.Text = "Выберите клиента!";
                return;
            }
            if (hallCB.SelectedItem == null)
            {
                errorTB.Text = "Выберите зал!";
                return;
            }
            DateTime timeRent = datestartDTP.Value.Date;
            timeRent = timeRent.AddHours(Convert.ToDouble(timestartNUM.Value));
            for (int hours = 0; hours < durationNUM.Value; hours++)
            {
                timeRent = timeRent.AddHours(hours);
                foreach (Rent rent in Rents)
                {
                    if (rent.Hall.Name == (string)hallCB.SelectedItem && rent.TimeStart.DayOfYear == timeRent.DayOfYear)
                    {
                        int startTime = rent.TimeStart.Hour;
                        int finishTime = rent.TimeStart.Hour + rent.Hours;
                        if (timeRent.Hour >= startTime && timeRent.Hour < finishTime)
                        {
                            errorTB.Text = "В это время зал занят!";
                            return;
                        }
                    }
                }
            }
        }

        private void cancelBTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void datestartDTP_ValueChanged(object sender, EventArgs e)
        {
            if (datestartDTP.Value.Date < DateTime.Now.Date)
            {
                errorTB.Text = "Укажите не прошедшую дату!";
                datestartDTP.Value = DateTime.Now.Date;
            }
            else
                errorTB.Text = string.Empty;
        }

        private void custCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            hallCB.SelectedItem = null;
            hallCB.Items.Clear();
            datestartDTP.Value = DateTime.Now.Date;
            Customer customer = Customers.FirstOrDefault(c => c.FullName == (string)custCB.SelectedItem);
            foreach (Hall hall in Halls)
            {
                if(!customer.Vip && !hall.OnlyVip)
                    hallCB.Items.Add(hall.Name);
                if (customer.Vip)
                    hallCB.Items.Add(hall.Name);
            }
        }

        private void hallCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hall hall = Halls.FirstOrDefault(h => h.Name == (string)hallCB.SelectedItem);
            peopleNUM.Maximum = hall.Capacity;
        }

        private void timestartNUM_ValueChanged(object sender, EventArgs e)
        {
            durationNUM.Maximum = Rent.FinishWorkTime - timestartNUM.Value;
        }
    }
}
