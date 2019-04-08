using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BathYard
{
    [DataContract]
    public class Hall : IModel
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Capacity { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public bool OnlyVip { get; set; }

        public Hall()
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

        public bool FullNow(Rent rent)
        {
            return false;
        }
    }
}
