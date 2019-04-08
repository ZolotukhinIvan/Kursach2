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
    public partial class HeadForm : Form
    {
        public IRepository repository;
        public Employee user = null;
        public string currentModel;
        public string Customer = "Customer";
        public string Employee = "Employee";
        public string Hall = "Hall";
        public string Rent = "Rent";
        public string Admin = "Админ";

        public long currentId = -1;

        public HeadForm(Employee user, IRepository repository)
        {
            InitializeComponent();
            timer1.Interval = 100;
            timer1.Tick += (o, e) =>
            {
                timeNowTB.Text = DateTime.Now.ToLongTimeString();
                dateNowTB.Text = DateTime.Now.ToShortDateString();
            };
            timer1.Start();
            this.user = user;
            this.repository = repository;
            this.repository.Take();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush lineBrush = new LinearGradientBrush(
                new Point(0, 0), new Point(0, 500),
                Color.LightCyan, Color.Aqua);
            g.FillRectangle(lineBrush, ClientRectangle);
        }

        private void objectsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentModel == Employee)
            {
                Employee employee = repository.Employees.FirstOrDefault(em => em.FullName == (string)objectsCB.SelectedItem);
                if (employee == null)
                    return;
                currentId = employee.ID;
                GetInfoEmployee(employee);
                return;
            }
            if (currentModel == Customer)
            {
                Customer customer = repository.Customers.FirstOrDefault(c => c.FullName == (string)objectsCB.SelectedItem);
                if (customer == null)
                    return;
                currentId = customer.ID;
                GetInfoCustomer(customer);
                return;
            }
            if (currentModel == Hall)
            {
                Hall hall = repository.Halls.FirstOrDefault(h => h.Name == (string)objectsCB.SelectedItem);
                if (hall == null)
                    return;
                currentId = hall.ID;
                GetInfoHall(hall);
                return;
            }
            if (currentModel == Rent)
            {
                Rent rent = repository.Rents.FirstOrDefault(r => r.TimeStart.ToString() == (string)objectsCB.SelectedItem);
                if (rent == null)
                    return;
                currentId = rent.ID;
                GetInfoRent(rent);
                return;
            }
        }

        private void addBTN_Click(object sender, EventArgs e)
        {
            if (user.Position != Admin)
            {
                MessageBox.Show("Вы можете только просматривать!");
                return;
            }
            if (currentModel == Employee)
            {
                EmpForm emp = new EmpForm(new Employee(), 1);
                if(emp.ShowDialog() == DialogResult.Yes)
                {
                    repository.Employees.Add(emp.Employee);
                    objectsCB.Items.Add(emp.Employee.FullName);
                    repository.Save();
                }
                return;
            }
            if (currentModel == Customer)
            {
                CustForm cust = new CustForm(new Customer(), 1);
                if (cust.ShowDialog() == DialogResult.Yes)
                {
                    repository.Customers.Add(cust.Customer);
                    objectsCB.Items.Add(cust.Customer.FullName);
                    repository.Save();
                }
                return;
            }
            if (currentModel == Hall)
            {
                HallForm hall = new HallForm(new Hall(), 1);
                if (hall.ShowDialog() == DialogResult.Yes)
                {
                    repository.Halls.Add(hall.Hall);
                    objectsCB.Items.Add(hall.Hall.Name);
                    repository.Save();
                }
                return;
            }
            if (currentModel == Rent)
            {
                RentForm rent = new RentForm(new Rent(), 1, repository.Customers, repository.Halls, repository.Rents);
                if (rent.ShowDialog() == DialogResult.Yes)
                {
                    repository.Rents.Add(rent.Rent);
                    objectsCB.Items.Add(rent.Rent.TimeStart.ToString());
                    repository.Save();
                }
                return;
            }
            MessageBox.Show("Выберите модель!");
        }

        private void editBTN_Click(object sender, EventArgs e)
        {
            if (user.Position != Admin)
            {
                MessageBox.Show("Вы можете только просматривать!");
                return;
            }
            if (currentModel == Employee && currentId != -1)
            {
                Employee employee = repository.Employees.FirstOrDefault(em => em.ID == currentId);
                if (employee == null)
                    return;
                EmpForm emp = new EmpForm(employee, 0);
                if (emp.ShowDialog() == DialogResult.Yes)
                {
                    objectsCB.SelectedItem = employee.FullName;
                    repository.Save();
                }
                return;
            }
            if (currentModel == Customer && currentId != -1)
            {
                Customer customer = repository.Customers.FirstOrDefault(c => c.ID == currentId);
                if (customer == null)
                    return;
                CustForm cust = new CustForm(customer, 0);
                if (cust.ShowDialog() == DialogResult.Yes)
                {
                    objectsCB.SelectedItem = customer.FullName;
                    repository.Save();
                }
                return;
            }
            if (currentModel == Hall && currentId != -1)
            {
                Hall hall = repository.Halls.FirstOrDefault(h => h.ID == currentId);
                if (hall == null)
                    return;
                HallForm hal = new HallForm(hall, 0);
                if (hal.ShowDialog() == DialogResult.Yes)
                {
                    objectsCB.SelectedItem = hall.Name;
                    repository.Save();
                }
                return;
            }
            if (currentModel == Rent && currentId != -1)
            {
                Rent rent = repository.Rents.FirstOrDefault(r => r.ID == currentId);
                if (rent == null)
                    return;
                RentForm ren = new RentForm(rent, 0, repository.Customers, repository.Halls, repository.Rents);
                if (ren.ShowDialog() == DialogResult.Yes)
                {
                    objectsCB.SelectedItem = rent.TimeStart.ToString();
                    repository.Save();
                }
                return;
            }
            MessageBox.Show("Выберите модель!");
        }

        private void removeBTN_Click(object sender, EventArgs e)
        {
            if (user.Position != Admin)
            {
                MessageBox.Show("Вы можете только просматривать!");
                return;
            }
            if (currentModel == Employee && currentId != -1)
            {
                Employee employee = repository.Employees.FirstOrDefault(em => em.ID == currentId);
                if (employee == null)
                    return;
                objectsCB.Items.Remove(employee.FullName);
                repository.Employees.Remove(employee);
                repository.Save();
                objectsCB.SelectedItem = null;
                objectsCB.Text = string.Empty;
                currentId = -1;
                ClearInfo();
                return;
            }
            if (currentModel == Customer && currentId != -1)
            {
                Customer customer = repository.Customers.FirstOrDefault(c => c.ID == currentId);
                if (customer == null)
                    return;
                objectsCB.Items.Remove(customer.FullName);
                repository.Customers.Remove(customer);
                repository.Save();
                objectsCB.SelectedItem = null;
                objectsCB.Text = string.Empty;
                currentId = -1;
                ClearInfo();
                return;
            }
            if (currentModel == Hall && currentId != -1)
            {
                Hall hall = repository.Halls.FirstOrDefault(h => h.ID == currentId);
                if (hall == null)
                    return;
                objectsCB.Items.Remove(hall.Name);
                repository.Halls.Remove(hall);
                repository.Save();
                objectsCB.SelectedItem = null;
                objectsCB.Text = string.Empty;
                currentId = -1;
                ClearInfo();
                return;
            }
            if (currentModel == Rent && currentId != -1)
            {
                Rent rent = repository.Rents.FirstOrDefault(r => r.ID == currentId);
                if (rent == null)
                    return;
                objectsCB.Items.Remove(rent.TimeStart.ToString());
                repository.Rents.Remove(rent);
                repository.Save();
                objectsCB.SelectedItem = null;
                objectsCB.Text = string.Empty;
                currentId = -1;
                ClearInfo();
                return;
            }
            MessageBox.Show("Выберите модель!");
        }

        // __________________________________________________

        private void empBTN_Click(object sender, EventArgs e)
        {
            currentModel = Employee;
            objectsCB.SelectedItem = null;
            objectsCB.Items.Clear();
            objectsLB.Text = "Сотрудники";
            currentId = -1;
            foreach(Employee employee in repository.Employees)
            {
                objectsCB.Items.Add(employee.FullName);
            }
            attributeLB1.Text = "Логин:";
            attributeLB2.Text = "ФИО:";
            attributeLB3.Text = "Зрплата:";
            attributeLB4.Text = "Пост:";
            attributeLB5.Text = "Возраст:";
            ClearInfo();
        }

        private void hallBTN_Click(object sender, EventArgs e)
        {
            currentModel = Hall;
            objectsCB.SelectedItem = null;
            objectsCB.Items.Clear();
            objectsLB.Text = "Залы";
            currentId = -1;
            foreach (Hall hall in repository.Halls)
            {
                objectsCB.Items.Add(hall.Name);
            }
            attributeLB1.Text = "Название:";
            attributeLB2.Text = "Вместоимость:";
            attributeLB3.Text = "Цена в час:";
            attributeLB4.Text = "Для кого:";
            attributeLB5.Text = string.Empty;
            ClearInfo();
        }

        private void custBTN_Click(object sender, EventArgs e)
        {
            currentModel = Customer;
            objectsCB.SelectedItem = null;
            objectsCB.Items.Clear();
            objectsLB.Text = "Клиенты";
            currentId = -1;
            foreach (Customer customer in repository.Customers)
            {
                objectsCB.Items.Add(customer.FullName);
            }
            attributeLB1.Text = "ФИО:";
            attributeLB2.Text = "Визитов:";
            attributeLB3.Text = "Потрачено:";
            attributeLB4.Text = "Тип:";
            attributeLB5.Text = "Возраст:";
            ClearInfo();
        }

        private void rentBTN_Click(object sender, EventArgs e)
        {
            currentModel = Rent;
            objectsCB.SelectedItem = null;
            objectsCB.Items.Clear();
            objectsLB.Text = "Аренды";
            currentId = -1;
            foreach (Rent rent in repository.Rents)
            {
                objectsCB.Items.Add(rent.TimeStart.ToString());
            }
            attributeLB1.Text = "Клиент:";
            attributeLB2.Text = "Зал:";
            attributeLB3.Text = "Людей:";
            attributeLB4.Text = "Время:";
            attributeLB5.Text = "Цена:";
            ClearInfo();
        }

        //_____________________________________________________________

        private void ClearInfo()
        {
            dataLB1.Text = string.Empty;
            dataLB2.Text = string.Empty;
            dataLB3.Text = string.Empty;
            dataLB4.Text = string.Empty;
            dataLB5.Text = string.Empty;
        }

        private void GetInfoEmployee(Employee employee)
        {
            dataLB1.Text = employee.Login;
            dataLB2.Text = employee.FullName;
            dataLB3.Text = Convert.ToString(employee.Wage) + " р.";
            dataLB4.Text = employee.Position;
            dataLB5.Text = Convert.ToString(employee.Age);
        }

        private void GetInfoCustomer(Customer customer)
        {
            dataLB1.Text = customer.FullName;
            dataLB2.Text = Convert.ToString(customer.CountVisits);
            dataLB3.Text = Convert.ToString(customer.MoneySpent) + " р.";
            if(customer.Vip)
                dataLB4.Text = "VIP";
            else
                dataLB4.Text = "обычный";
            dataLB5.Text = Convert.ToString(customer.Age);
        }

        private void GetInfoHall(Hall hall)
        {
            dataLB1.Text = hall.Name;
            dataLB2.Text = Convert.ToString(hall.Capacity);
            dataLB3.Text = Convert.ToString(hall.Price) + " р./час";
            if (hall.OnlyVip)
                dataLB4.Text = "только для VIP";
            else
                dataLB4.Text = "для всех";
        }

        private void GetInfoRent(Rent rent)
        {
            dataLB1.Text = rent.Customer.FullName;
            dataLB2.Text = rent.Hall.Name;
            dataLB3.Text = Convert.ToString(rent.People);
            dataLB4.Text = rent.TimeStart.ToShortDateString() +": c "+ rent.TimeStart.Hour + ":00 до " + (rent.TimeStart.Hour+rent.Hours) + ":00";
            dataLB5.Text = Convert.ToString(rent.Price) + " р.";
        }
    }
}
