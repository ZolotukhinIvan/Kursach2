using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BathYard
{
    [DataContract]
    public class Customer : IModel
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public int CountVisits { get; set; }
        [DataMember]
        public double MoneySpent { get; set; }
        [DataMember]
        public bool Vip { get; set; }
        [DataMember]
        public int Age { get; set; }

        public Customer()
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
