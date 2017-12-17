using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Bestand
    {
        [DataMember]
        public string Naam { get; set; }
        [DataMember]
        public byte[] Content { get; set; }

        public Bestand(string naam, byte[] content)
        {
            // TODO: Complete member initialization
            this.Naam = naam;
            this.Content = content;
        }
    }
}
