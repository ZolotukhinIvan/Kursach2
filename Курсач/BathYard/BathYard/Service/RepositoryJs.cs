using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BathYard
{
    public class RepositoryJs : IRepository
    {
        public List<Employee> Employees { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Hall> Halls { get; set; }
        public List<Rent> Rents { get; set; }

        LogerServiceJs<Employee> logerEmployee = new LogerServiceJs<Employee>(LogerServiceJs<Employee>.EmployeeFile);
        LogerServiceJs<Customer> logerCustomer = new LogerServiceJs<Customer>(LogerServiceJs<Customer>.CustomerFile);
        LogerServiceJs<Hall> logerHall = new LogerServiceJs<Hall>(LogerServiceJs<Hall>.HallFile);
        LogerServiceJs<Rent> logerRent = new LogerServiceJs<Rent>(LogerServiceJs<Rent>.RentFile);

        public void Save()
        {
            logerEmployee.Save(Employees);
            logerCustomer.Save(Customers);
            logerHall.Save(Halls);
            logerRent.Save(Rents);
        }

        public void Take()
        {
            Employees = logerEmployee.Take();
            Customers = logerCustomer.Take();
            Halls = logerHall.Take();
            Rents = logerRent.Take();
            if(Employees.Count != 0)
                Employees.Last().SetLastID();
            if (Customers.Count != 0)
                Customers.Last().SetLastID();
            if (Halls.Count != 0)
                Halls.Last().SetLastID();
            if (Rents.Count != 0)
                Rents.Last().SetLastID();
            foreach (Rent rent in Rents)
            {
                rent.Customer = Customers.SingleOrDefault(c => c.ID == rent.CustomerID);
                rent.Hall = Halls.SingleOrDefault(h => h.ID == rent.HallID);
            }
        }
    }
}
