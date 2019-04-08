using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BathYard
{
    public class AuthorizeServiceJs : IAutorizeService
    {
        public Employee Authorize(string login, string password)
        {
            LogerServiceJs<Employee> loger = new LogerServiceJs<Employee>(LogerServiceJs<Employee>.EmployeeFile);
            List<Employee> employees = loger.Take();
            Employee emp = employees.SingleOrDefault(e => e.Login == login && e.Password == password);
            if (emp != null)
                return emp;
            else
                return null;
        }
    }
}
