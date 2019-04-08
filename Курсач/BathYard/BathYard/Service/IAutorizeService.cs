using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BathYard
{
    public interface IAutorizeService
    {
        Employee Authorize(string login, string password);
    }
}
