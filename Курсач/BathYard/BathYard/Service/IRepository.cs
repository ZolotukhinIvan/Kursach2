using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BathYard
{
    public interface IRepository
    {
        List<Employee> Employees { get; set; }
        List<Customer> Customers { get; set; }
        List<Hall> Halls { get; set; }
        List<Rent> Rents { get; set; }

        void Save();
        void Take();
    }
}
