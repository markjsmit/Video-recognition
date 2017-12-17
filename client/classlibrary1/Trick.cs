using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Trick
    {
        [DataMember]
        public string Naam { get; set; }


        public Trick(string naam)
        {
            // TODO: Complete member initialization
            this.Naam = naam;
        }
    }
}
