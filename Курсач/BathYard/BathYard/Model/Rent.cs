using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BathYard
{
    [DataContract]
    public class Rent : IModel
    {
        public const int StartWorkTime = 9;
        public const int FinishWorkTime = 21;

        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public int Hours { get; set; }
        [DataMember]
        public int People { get; set; }
        [DataMember]
        public DateTime TimeStart { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public long CustomerID { get; set; }
        [DataMember]
        public long HallID { get; set; }
        public Customer Customer { get; set; }
        public Hall Hall { get; set; }

        public Rent()
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
