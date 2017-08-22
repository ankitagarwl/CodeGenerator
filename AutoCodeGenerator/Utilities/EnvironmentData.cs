using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCodeGenerator
{
    public class EnvironmentData : ConfigurationSection
    {
        public EnvironmentData()
        {
        }

        [ConfigurationProperty("strMSSqlDB")]
        public string strMSSqlDB
    {
            get { return (string)this["strMSSqlDB"]; }
            set { this["strMSSqlDB"] = value; }
        }

        [ConfigurationProperty("USERID")]
        public string USERID
        {
            get { return (string)this["USERID"]; }
            set { this["USERID"] = value; }
        }

        [ConfigurationProperty("PASSWORD")]
        public string PASSWORD
        {
            get { return (string)this["PASSWORD"]; }
            set { this["PASSWORD"] = value; }
        }
 

        [ConfigurationProperty("REPORTLOCATION")]

        public string REPORTLOCATION
        {
            get { return (string)this["REPORTLOCATION"]; }
            set { this["REPORTLOCATION"] = value; }
        }

    }
}
