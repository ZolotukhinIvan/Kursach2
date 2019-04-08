using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace BathYard
{
    public class LogerServiceJs<T> : ILogerService<T>
    {
        public const string EmployeeFile = "emp.json";
        public const string CustomerFile = "cust.json";
        public const string HallFile = "hall.json";
        public const string RentFile = "rent.json";

        public string FileName { get; set; }

        public void Save(List<T> objects)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<T>));
            FileStream fs = new FileStream(FileName, FileMode.Create);
            if (objects.Count != 0)
            {
                js.WriteObject(fs, objects);
            }
            else
            {
                js.WriteObject(fs, null);
            }
            fs.Dispose();
        }

        public LogerServiceJs(string fileName)
        {
            FileName = fileName;
        }

        public List<T> Take()
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<T>));
            if (!File.Exists(FileName))
            {
                File.WriteAllText(FileName, "null");
            }
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                List<T> jsObjects = (List<T>)js.ReadObject(fs);
                if (jsObjects != null)
                    return jsObjects;
                else
                    return new List<T>();
            }
        }
    }
}
