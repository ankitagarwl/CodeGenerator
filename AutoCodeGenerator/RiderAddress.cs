using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoCodeGenerator
{
    public class RiderAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip5 { get; set; }
        public string Zip4 { get; set; }

    }

    [XmlRoot("AddressValidateResponse")]
    public class AddressValidateResponse
    {
        [XmlElement("Address")]
        public List<Add> Address { get; set; }
    }

    public class Add
    {
        [XmlElement("Address2")]
        public string Address2 { get; set; }

        [XmlElement("City")]
        public string City { get; set; }

        [XmlElement("State")]
        public string State { get; set; }

        [XmlElement("Zip5")]
        public string Zip5 { get; set; }

        [XmlElement("Zip4")]
        public string Zip4 { get; set; }

        



    }

}
