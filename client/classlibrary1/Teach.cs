using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Teach
    {
           [DataMember]
        public string Trick { get; set; }
           [DataMember]
        public string Video { get; set; }
           [DataMember]
        public int[] Positions { get; set; }
         [DataMember]
           public int Cicles { get; set; }
    }
}
