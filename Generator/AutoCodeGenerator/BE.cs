using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCodeGenerator
{
    public class Enums
    {
        public enum CodeType
        {
            API,
            BAL,
            DAL,
            BO
            
        }
    }
    public class BE
    {
      
        private DataSet _packages;
        private DataSet _storedProcedures;
        private DataSet _standardAbbreviations;
        private DataSet _storedProcedureComments;
        private string _owner;
        private string _package;
        private string _storedProcedure;
        private string _codeOutput;
        private string _connectionString;
        private Enums.CodeType _codeType;
        private string _interfaceName;

        public BE()
        {
        }

        public BE(string ownerParameter, string packageParameter)
        {
            _owner = ownerParameter;
            _package = packageParameter;
            //_interfaceName = ConfigurationManager.AppSettings("interface")
        }

        public Enums.CodeType CodeTypeToGenerate
        {
            get { return _codeType; }
            set { _codeType = value; }
        }

        public DataSet Packages
        {
            get { return _packages; }
            set { _packages = value; }
        }

        public DataSet StoredProcedures
        {
            get { return _storedProcedures; }
            set { _storedProcedures = value; }
        }

        public string InterfaceName
        {
            get { return _interfaceName; }
            set { _interfaceName = value; }
        }

        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public string Package
        {
            get { return _package; }
            set { _package = value; }
        }

        public string StoredProcedure
        {
            get { return _storedProcedure; }
            set { _storedProcedure = value; }
        }

        public string CodeOutput
        {
            get { return _codeOutput; }
            set { _codeOutput = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public DataSet StandardAbbreviations
        {
            get { return _standardAbbreviations; }
            set { _standardAbbreviations = value; }
        }


        public DataSet StoredProcedureComments
        {
            get { return _storedProcedureComments; }
            set { _storedProcedureComments = value; }
        }



    }


}
