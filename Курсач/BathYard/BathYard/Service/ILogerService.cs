using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BathYard
{
    public interface ILogerService<T>
    {
        void Save(List<T> objects);
        List<T> Take();
    }
}
