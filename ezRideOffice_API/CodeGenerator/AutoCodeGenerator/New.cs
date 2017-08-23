using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezRide
{
    public class Enums
    {
        public enum CodeType
        {
            BusinessEntity,
            DataAccess,
            BusinessObject,
            BusinessObjectCached,
            IServiceCallCode,
            ServiceCallCode
        }
    }
    public class New
    {
        public DataSet _packages { get; set; }
        public DataSet _storedProcedures { get; set; }
        public DataSet _standardAbbreviations { get; set; }
        public DataSet _storedProcedureComments { get; set; }
        public string _owner { get; set; }
        public string _package { get; set; }
        public string _storedProcedure { get; set; }
        public string _codeOutput { get; set; }
        public string _connectionString { get; set; }
        public Enums.CodeType _codeType { get; set; }
        public string _interfaceName { get; set; }
    }


}
