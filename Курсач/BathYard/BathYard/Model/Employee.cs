using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BathYard
{
    [DataContract]
    public class Employee : IModel
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public double Wage { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public int Age { get; set; }

        public Employee()
        {
            ID = LastID;
            LastID++;
        }

        public static long LastID = 0;

        public void SetLastID()
        {
            LastID = ID;
            LastID++;
        }
    }
}
