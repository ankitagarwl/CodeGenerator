using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//using DataAccessLayer;
//using BusinessEntities;
using System.Text;
//using Oracle.DataAccess.Client;
using System.Configuration;


namespace AutoCodeGenerator
{
    public static class BO
    {
        const string addInParameter = "AddInParameter";
        const string addOutParameter = "AddOutParameter";

        const string addParameter = "AddParameter";
        const string executeDataset = "ds = dataProvider.ExecuteDataSet(command)";
        const string executeReader = "Dim reader As IDataReader = dataProvider.ExecuteReader(command)";

        const string executeNonQuery = "Dim returnCode As Integer = dataProvider.ExecuteNonQuery(command)";

        const string executeCsharpReader = "IDataReader reader = dataProvider.ExecuteReader(command)";

        const string executeCsharpNonQuery = "int returnCode = dataProvider.ExecuteNonQuery(command)";

        const string qq = "";//Strings.Chr(34);ankit
        private static string entityName;

        private static string methodName;

        public static DataSet ds = new DataSet();

        #region "Package/Stored Procedure retrieval"

        //public static void GetPackageList(BE dc)
        //{
        //    try
        //    {
        //        DAL.GetPackageList(dc);
        //    }
        //    catch (Exception ex)
        //    {
        //        // ExceptionManager.HandleException(ex)
        //        throw;
        //    }
        //}

        //public static void GetPackageStoredProcedure(BE dc)
        //{
        //    try
        //    {
        //        DAL.GetPackageStoredProcedure(dc);
        //        StringBuilder sb = new StringBuilder();
        //    }
        //    catch (Exception ex)
        //    {
        //        // ExceptionManager.HandleException(ex)
        //        throw;
        //    }
        //}

        //public static void GetPackageStoredProcedureComments(BE dc)
        //{
        //    try
        //    {
        //        DAL.GetPackageStoredProcedureComments(dc);
        //    }
        //    catch (Exception ex)
        //    {
        //        // ExceptionManager.HandleException(ex)
        //        throw;
        //    }
        //}
        #endregion

        #region "Generate DAL"

        //public static void GenerateDALCode(BE dc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    OracleConnection conn = new OracleConnection(dc.ConnectionString);
        //    conn.Open();
        //    OracleCommand cmd = GetOracleCommand(conn, dc);

        //    CommentsBlock(cmd, sb, dc);
        //    WriteSignature(cmd, sb, dc);
        //    WriteMethodBody(cmd, sb, dc);

        //    conn.Close();
        //    dc.CodeOutput = sb.ToString;
        //}

        //public static void GenerateCSharpDALCode(BE dc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    OracleConnection conn = new OracleConnection(dc.ConnectionString);
        //    conn.Open();
        //    OracleCommand cmd = GetOracleCommand(conn, dc);

        //    CommentsCSharpBlock(cmd, sb, dc);
        //    WriteCSharpSignature(cmd, sb, dc);
        //    WriteCSharpMethodBody(cmd, sb, dc);

        //    conn.Close();
        //    dc.CodeOutput = sb.ToString;
        //}

        //private static void CommentsBlock(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string dsParameter = string.Empty;
        //    var _with1 = sb;
        //    _with1.AppendLine("''' <summary>");
        //    _with1.AppendLine("''' Wrapper for " + dc.StoredProcedure);
        //    _with1.Append("''' ");
        //    _with1.AppendLine(GetStoredProcedureDescription(dc));
        //    _with1.AppendLine("''' </summary>");

        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (GetDirection(param) != addOutParameter)
        //        {
        //            _with1.Append("''' <param name=");
        //            _with1.Append(qq + parameterName + qq);
        //            _with1.AppendLine("></param>");
        //        }
        //        else if (!IsObject(param.DbType.ToString))
        //        {
        //            _with1.Append("''' <param name=");
        //            _with1.Append(qq + parameterName + qq);
        //            _with1.AppendLine("></param>");
        //        }
        //        else
        //        {
        //            dsParameter = "''' <param name=" + qq + "ds" + qq + "></param>" + Constants.vbCrLf;
        //        }
        //    }
        //    _with1.Append(dsParameter);
        //    _with1.AppendLine("''' <remarks>");
        //    WriteDimStatements(cmd, sb, dc);
        //    WriteCallingCode(cmd, sb, dc);
        //    _with1.AppendLine("''' </remarks>");
        //}

        //private static void CommentsCSharpBlock(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string dsParameter = string.Empty;
        //    var _with2 = sb;
        //    _with2.AppendLine("/// <summary>");
        //    _with2.AppendLine("/// Wrapper for " + dc.StoredProcedure);
        //    _with2.Append("/// ");
        //    _with2.AppendLine(GetStoredProcedureDescription(dc));
        //    _with2.AppendLine("/// </summary>");

        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (GetDirection(param) != addOutParameter)
        //        {
        //            _with2.Append("/// <param name=");
        //            _with2.Append(qq + parameterName + qq);
        //            _with2.AppendLine("></param>");
        //        }
        //        else if (!IsObject(param.DbType.ToString))
        //        {
        //            _with2.Append("/// <param name=");
        //            _with2.Append(qq + parameterName + qq);
        //            _with2.AppendLine("></param>");
        //        }
        //        else
        //        {
        //            dsParameter = "/// <param name=" + qq + "ds" + qq + "></param>" + Constants.vbCrLf;
        //        }
        //    }
        //    _with2.Append(dsParameter);
        //    _with2.AppendLine("/// <remarks>");
        //    WriteVariableStatements(cmd, sb, dc);
        //    WriteCSharpCallingCode(cmd, sb, dc);
        //    _with2.AppendLine("/// </remarks>");
        //}

        //private static void WriteCallingCode(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.Append("''' DAL." + GetMethodName(dc.StoredProcedure.ToLower) + "(");
        //    string signatureComma = string.Empty;
        //    string dsParameter = string.Empty;
        //    var _with3 = sb;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (!IsObject(param.DbType.ToString))
        //        {
        //            _with3.Append(signatureComma + ReverseParameterName(param.ParameterName.ToLower()));
        //            signatureComma = ", ";
        //        }
        //        else
        //        {
        //            dsParameter = signatureComma + "ds";
        //        }
        //    }
        //    _with3.Append(dsParameter);
        //    _with3.AppendLine(")");
        //}

        //private static void WriteCSharpCallingCode(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.Append("/// DAL." + GetMethodName(dc.StoredProcedure.ToLower) + "(");
        //    string signatureComma = string.Empty;
        //    string dsParameter = string.Empty;
        //    var _with4 = sb;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (!IsObject(param.DbType.ToString))
        //        {
        //            _with4.Append(signatureComma + ReverseParameterName(param.ParameterName.ToLower()));
        //            signatureComma = ", ";
        //        }
        //        else
        //        {
        //            dsParameter = signatureComma + "ds";
        //        }
        //    }
        //    _with4.Append(dsParameter);
        //    _with4.AppendLine(")");
        //}

        //private static void WriteSignature(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.Append("Public Shared Sub " + GetMethodName(dc.StoredProcedure.ToLower) + "(");

        //    string signatureComma = string.Empty;
        //    string dsParameter = string.Empty;
        //    var _with5 = sb;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());

        //        if (GetDirection(param) != addOutParameter)
        //        {
        //            _with5.Append(signatureComma + "ByVal " + ReverseParameterName(param.ParameterName.ToLower()));
        //            _with5.Append(" As ");
        //            _with5.Append(param.DbType.ToString);
        //            signatureComma = ", ";

        //        }
        //        else if (!IsObject(param.DbType.ToString))
        //        {
        //            _with5.Append(signatureComma + "ByRef " + ReverseParameterName(param.ParameterName.ToLower()));
        //            _with5.Append(" As ");
        //            _with5.Append(param.DbType.ToString);
        //            signatureComma = ", ";
        //        }
        //        else
        //        {
        //            dsParameter = "ByVal ds As DataSet";
        //        }
        //    }
        //    if (!dsParameter.Equals(string.Empty))
        //    {
        //        _with5.Append(signatureComma + dsParameter);
        //    }
        //    _with5.AppendLine(")");
        //}

        //private static void WriteCSharpSignature(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.Append("public static void " + GetMethodName(dc.StoredProcedure.ToLower) + "(");

        //    string signatureComma = string.Empty;
        //    string dsParameter = string.Empty;
        //    var _with6 = sb;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());

        //        if (GetDirection(param) != addOutParameter)
        //        {
        //            _with6.Append(signatureComma + param.DbType.ToString + " ");
        //            _with6.Append(ReverseParameterName(param.ParameterName.ToLower()));
        //            signatureComma = ", ";

        //        }
        //        else if (!IsObject(param.DbType.ToString))
        //        {
        //            _with6.Append(signatureComma + param.DbType.ToString + " ");
        //            _with6.Append(ReverseParameterName(param.ParameterName.ToLower()));
        //            signatureComma = ", ";
        //        }
        //        else
        //        {
        //            dsParameter = "DataSet ds";
        //        }
        //    }
        //    if (!dsParameter.Equals(string.Empty))
        //    {
        //        _with6.Append(signatureComma + dsParameter);
        //    }
        //    _with6.AppendLine(")");
        //}


        ////    public static void GetAllReferenceTable(DataSet ds)
        ////{
        ////	DataSourceFactory database = new DataSourceFactory();
        ////	BaseDataProvider dataProvider = null;
        ////	DbCommand command = null;
        ////	try {
        ////		dataProvider = (BaseDataProvider)database.GetProvider(Enumerations.DataSource.PACSES);
        ////		command = dataProvider.GetStoredProcCommand("csv_app_pkg.pkg.usp_get_all_re_table");
        ////		dataProvider.AddOutParameter(command, "p_cur_ref", DbType.Object, 0);
        ////		ds = dataProvider.ExecuteDataSet(command);
        ////	} catch (Exception ex) {
        ////		// ExceptionManager.HandleException(ex)
        ////		throw;
        ////	} finally {
        ////		dataProvider.CloseConnection(command);
        ////		database = null;
        ////		dataProvider = null;
        ////		command = null;
        ////	}
        ////}

        //private static void WriteMethodBody(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.AppendLine("Dim database As New DataSourceFactory");
        //    sb.AppendLine("Dim dataProvider As BaseDataProvider = Nothing");
        //    sb.AppendLine("Dim command As DbCommand = Nothing");
        //    sb.AppendLine("Try");

        //    sb.AppendLine("dataProvider = DirectCast(database.GetProvider(Enumerations.DataSource.PACSES), BaseDataProvider)");
        //    sb.AppendLine("command = dataProvider.GetStoredProcCommand(" + qq + cmd.CommandText.ToLower + qq + ")");

        //    string cmdText = executeNonQuery;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string direction = GetDirection(param);
        //        sb.Append("dataProvider.");
        //        sb.Append(direction);
        //        sb.Append("(command, ");
        //        sb.Append(qq + param.ParameterName.ToLower() + qq);
        //        sb.Append(", DbType.");
        //        sb.Append(param.DbType.ToString);

        //        switch (direction)
        //        {
        //            case addParameter:
        //                sb.Append(", ParameterDirection.InputOutput, String.Empty, DataRowVersion.Default, ");
        //                sb.Append(ReverseParameterName(param.ParameterName.ToLower));
        //                sb.Append(".ToString().ToUpperInvariant()");
        //                break;
        //            case addOutParameter:
        //                sb.Append(", ");
        //                sb.Append(param.Size.ToString());
        //                if (param.Size == 0 & IsObject(param.DbType.ToString))
        //                {
        //                    cmdText = executeDataset;
        //                }
        //                break;
        //            case addInParameter:
        //                sb.Append(", ");
        //                sb.Append(ReverseParameterName(param.ParameterName.ToLower));
        //                break;
        //        }
        //        sb.AppendLine(")");
        //    }
        //    sb.AppendLine(cmdText);

        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        if (GetDirection(param) != addInParameter)
        //        {
        //            if (!IsObject(param.DbType.ToString))
        //            {
        //                sb.Append(ReverseParameterName(param.ParameterName.ToLower()));
        //                // sb.Append(" As " & param.DbType.ToString())
        //                sb.Append(" = dataProvider.GetParameterValue(command, ");
        //                sb.AppendLine(qq + param.ParameterName.ToLower() + qq + ")");
        //            }
        //        }
        //    }

        //    sb.AppendLine("Catch ex As Exception");
        //    sb.AppendLine("' ExceptionManager.HandleException(ex)");
        //    sb.AppendLine("Throw");
        //    sb.AppendLine("Finally");
        //    sb.AppendLine("dataProvider.CloseConnection(command)");
        //    sb.AppendLine("database = Nothing");
        //    sb.AppendLine("dataProvider = Nothing");
        //    sb.AppendLine("command = Nothing");
        //    sb.AppendLine("End Try");
        //    sb.AppendLine("End Sub");
        //}

        //private static void WriteCSharpMethodBody(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.AppendLine("{");
        //    sb.AppendLine("     " + "DataSourceFactory database = new DataSourceFactory();");
        //    sb.AppendLine("     " + "BaseDataProvider dataProvider = null;");
        //    sb.AppendLine("     " + "DbCommand command = null;");
        //    sb.AppendLine("     " + "try {");

        //    sb.AppendLine("             " + "dataProvider = (BaseDataProvider)database.GetProvider(Enumerations.DataSource.PACSES);");
        //    sb.AppendLine("             " + "command = dataProvider.GetStoredProcCommand(" + qq + cmd.CommandText.ToLower + qq + ");");

        //    string cmdText = executeNonQuery;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string direction = GetDirection(param);
        //        sb.Append("             " + "dataProvider.");
        //        sb.Append(direction);
        //        sb.Append("(command, ");
        //        sb.Append(qq + param.ParameterName.ToLower() + qq);
        //        sb.Append(", DbType.");
        //        sb.Append(param.DbType.ToString);

        //        switch (direction)
        //        {
        //            case addParameter:
        //                sb.Append(", ParameterDirection.InputOutput, String.Empty, DataRowVersion.Default, ");
        //                sb.Append(ReverseParameterName(param.ParameterName.ToLower));
        //                sb.Append(".ToString().ToUpperInvariant()");
        //                break;
        //            case addOutParameter:
        //                sb.Append(", ");
        //                sb.Append(param.Size.ToString());
        //                if (param.Size == 0 & IsObject(param.DbType.ToString))
        //                {
        //                    cmdText = executeDataset;
        //                }
        //                break;
        //            case addInParameter:
        //                sb.Append(", ");
        //                sb.Append(ReverseParameterName(param.ParameterName.ToLower));
        //                break;
        //        }
        //        sb.AppendLine(");");

        //    }
        //    sb.AppendLine(cmdText + ";");

        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        if (GetDirection(param) != addInParameter)
        //        {
        //            if (!IsObject(param.DbType.ToString))
        //            {
        //                sb.Append("             " + ReverseParameterName(param.ParameterName.ToLower()));
        //                // sb.Append(" As " & param.DbType.ToString())
        //                sb.Append(" = dataProvider.GetParameterValue(command, ");
        //                sb.AppendLine(qq + param.ParameterName.ToLower() + qq + ");");
        //            }
        //        }
        //    }
        //    sb.AppendLine("     }");
        //    sb.AppendLine("     catch (Exception ex) {");
        //    sb.AppendLine("         // ExceptionManager.HandleException(ex)");
        //    sb.AppendLine("         throw;");
        //    sb.AppendLine("     }");
        //    sb.AppendLine("     finally {");
        //    sb.AppendLine("             dataProvider.CloseConnection(command);");
        //    sb.AppendLine("             database = null;");
        //    sb.AppendLine("             dataProvider = null;");
        //    sb.AppendLine("             command = null;");
        //    sb.AppendLine("     }");
        //    sb.AppendLine("}");
        //}

        //private static void WriteDimStatements(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string dsParameter = string.Empty;
        //    var _with7 = sb;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (!IsObject(param.DbType.ToString))
        //        {
        //            _with7.Append("''' Dim ");
        //            _with7.Append(parameterName);
        //            _with7.Append(" As ");
        //            _with7.AppendLine(param.DbType.ToString);
        //        }
        //        else
        //        {
        //            dsParameter = "''' Dim ds As Dataset = New DataSet()" + Constants.vbCrLf;
        //        }
        //    }
        //    _with7.Append(dsParameter);
        //}

        //private static void WriteVariableStatements(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string dsParameter = string.Empty;
        //    var _with8 = sb;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (!IsObject(param.DbType.ToString))
        //        {
        //            _with8.AppendLine("/// " + param.DbType.ToString + " " + parameterName);


        //        }
        //        else
        //        {
        //            dsParameter = "/// Dataset ds = new DataSet()" + Constants.vbCrLf;
        //        }
        //    }
        //    _with8.Append(dsParameter);
        //}


        //#endregion

        //#region "Service Code Generators"

        //public static void GenerateCode(BE dc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    OracleConnection conn = new OracleConnection(dc.ConnectionString);
        //    conn.Open();
        //    OracleCommand cmd = GetOracleCommand(conn, dc);

        //    methodName = GetMethodName(dc.StoredProcedure.ToLower);
        //    entityName = GetMethodName(dc.Package) + "BE." + methodName + "BE";
        //    switch (dc.CodeTypeToGenerate)
        //    {
        //        case Enums.CodeType.BusinessEntity:
        //            WriteBusinessEntity(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.BusinessObject:
        //            WriteBusinessObject(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.BusinessObjectCached:
        //            WriteBusinessObjectCached(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.IServiceCallCode:
        //            WriteIServiceInterface(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.ServiceCallCode:
        //            WriteServiceMethods(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.DataAccess:
        //            WriteDataAccessCode(cmd, sb, dc);
        //            break;
        //        default:
        //            break;
        //            // forget it 
        //    }

        //    conn.Close();
        //    dc.CodeOutput = sb.ToString;
        //}

        //public static void GenerateCSharpCode(BE dc)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    OracleConnection conn = new OracleConnection(dc.ConnectionString);
        //    conn.Open();
        //    OracleCommand cmd = GetOracleCommand(conn, dc);

        //    methodName = GetMethodName(dc.StoredProcedure.ToLower);
        //    entityName = GetMethodName(dc.Package) + "BE." + methodName + "BE";
        //    switch (dc.CodeTypeToGenerate)
        //    {
        //        case Enums.CodeType.BusinessEntity:
        //            WriteCSharpBusinessEntity(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.BusinessObject:
        //            WriteCSharpBusinessObject(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.BusinessObjectCached:
        //            WritecSharpBusinessObjectCached(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.IServiceCallCode:
        //            WriteCSharpIServiceInterface(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.ServiceCallCode:
        //            WriteCSharpServiceMethods(cmd, sb, dc);
        //            break;
        //        case Enums.CodeType.DataAccess:
        //            WriteCSharpDataAccessCode(cmd, sb, dc);
        //            break;
        //        default:
        //            break;
        //            // forget it 
        //    }

        //    conn.Close();
        //    dc.CodeOutput = sb.ToString;
        //}

        //private static void WriteBusinessEntity(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    var _with9 = sb;
        //    _with9.AppendLine("<DataContract()> _");
        //    _with9.AppendLine("Public Class " + GetMethodName(dc.StoredProcedure.ToLower) + "BE");

        //    bool dataSetNotIncluded = true;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (!IsObject(param.DbType.ToString))
        //        {
        //            _with9.Append("Dim _");
        //            _with9.Append(parameterName);
        //            _with9.Append(" As ");
        //            _with9.AppendLine(param.DbType.ToString);
        //        }
        //        else if (dataSetNotIncluded)
        //        {
        //            dataSetNotIncluded = false;
        //            _with9.AppendLine("Dim _ds As Dataset = New DataSet()");
        //        }
        //    }

        //    dataSetNotIncluded = true;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (!IsObject(param.DbType.ToString))
        //        {
        //            _with9.AppendLine("<DataMember()> _");
        //            _with9.Append("Public Property ");
        //            _with9.Append(Capitalize(parameterName));
        //            _with9.Append("() As ");
        //            _with9.AppendLine(param.DbType.ToString);
        //            _with9.AppendLine("Get");
        //            _with9.AppendLine("Return _" + parameterName);
        //            _with9.AppendLine("End Get");
        //            _with9.AppendLine("Set(byVal value As " + param.DbType.ToString + ")");
        //            _with9.AppendLine("_" + parameterName + " = value ");
        //            _with9.AppendLine("End Set");
        //            _with9.AppendLine("End Property");
        //        }
        //        else if (dataSetNotIncluded)
        //        {
        //            dataSetNotIncluded = false;
        //            _with9.AppendLine("<DataMember()> _");
        //            _with9.AppendLine("Public Property ds() As DataSet");
        //            _with9.AppendLine("Get");
        //            _with9.AppendLine("Return _ds");
        //            _with9.AppendLine("End Get");
        //            _with9.AppendLine("Set(byVal value As DataSet)");
        //            _with9.AppendLine("_ds = value ");
        //            _with9.AppendLine("End Set");
        //            _with9.AppendLine("End Property");
        //        }
        //    }
        //    _with9.AppendLine("End Class");
        //}

        //private static void WriteCSharpBusinessEntity(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    var _with10 = sb;
        //    _with10.AppendLine("[DataContract()]");
        //    _with10.AppendLine("public class " + GetMethodName(dc.StoredProcedure.ToLower) + "BE");
        //    _with10.AppendLine("{ ");
        //    bool dataSetNotIncluded = true;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (!IsObject(param.DbType.ToString))
        //        {
        //            _with10.AppendLine(param.DbType.ToString + " _" + parameterName + ";");

        //        }
        //        else if (dataSetNotIncluded)
        //        {
        //            dataSetNotIncluded = false;
        //            _with10.AppendLine("DataSet _ds = new DataSet();");
        //        }
        //    }

        //    dataSetNotIncluded = true;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = ReverseParameterName(param.ParameterName.ToLower());
        //        if (!IsObject(param.DbType.ToString))
        //        {
        //            _with10.AppendLine("[DataMember()]");
        //            _with10.Append("public ");
        //            _with10.Append(param.DbType.ToString + " ");
        //            _with10.AppendLine(Capitalize(parameterName) + " { ");
        //            _with10.AppendLine("    " + "get { " + "return  _" + parameterName + "; }");
        //            _with10.AppendLine("    " + "set { _" + parameterName + " = value; }");
        //            _with10.AppendLine("}");
        //        }
        //        else if (dataSetNotIncluded)
        //        {
        //            dataSetNotIncluded = false;
        //            _with10.AppendLine("[DataMember()]");
        //            _with10.AppendLine("public DataSet ds { ");
        //            _with10.AppendLine("   " + "get { return _ds; }");
        //            _with10.AppendLine("   " + "set { _ds = value; }");
        //            _with10.AppendLine("}");
        //        }
        //    }
        //    _with10.AppendLine("}");
        //}

        //private static void WriteBusinessObject(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string dataAccessName = GetMethodName(dc.Package) + "DO.";
        //    sb.AppendLine("Public Shared Function " + methodName + "(byVal entity As " + entityName + ") as " + entityName);
        //    sb.AppendLine("entity = " + dataAccessName + methodName + "(entity)");
        //    sb.AppendLine("Return entity");
        //    sb.AppendLine("End Function");
        //}

        //private static void WriteCSharpBusinessObject(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string dataAccessName = GetMethodName(dc.Package) + "DO.";
        //    sb.AppendLine("public static " + entityName + " " + methodName + "( " + entityName + " entity  ) {");
        //    sb.AppendLine("     " + "entity = " + dataAccessName + methodName + "(entity);");
        //    sb.AppendLine("     " + "return entity ;");
        //    sb.AppendLine("}");
        //}

        //private static void WriteBusinessObjectCached(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string cacheImplementation = ConfigurationManager.AppSettings("cacheImplementation");
        //    if (cacheImplementation.Equals("shared", StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        cacheImplementation = ", ConfigurationManager.AppSettings(" + qq + "SharedCacheEnabled" + qq + ")";
        //    }
        //    else
        //    {
        //        cacheImplementation = string.Empty;
        //    }
        //    string dataAccessName = GetMethodName(dc.Package) + "DO.";
        //    string myClassName = GetMethodName(dc.Package) + "BO.";
        //    string configEntry = Strings.Chr(34) + myClassName + methodName + ".CacheDuration" + Strings.Chr(34);

        //    sb.AppendLine("Public Shared Function " + methodName + "(byVal entity As " + entityName + ") as " + entityName);

        //    bool hasOutputParameters = false;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string direction = GetDirection(param);
        //        switch (direction)
        //        {
        //            case addParameter:
        //            case addOutParameter:
        //                hasOutputParameters = true;
        //                break;
        //            default:

        //                break;
        //        }
        //    }

        //    if (hasOutputParameters)
        //    {
        //        // do nothing for now 
        //        // nothing to cache! 
        //    }
        //    else
        //    {
        //        sb.AppendLine("Return " + dataAccessName + methodName + "(entity)");
        //        sb.AppendLine("End Function");
        //        return;
        //    }

        //    sb.AppendLine("Dim cacheDurationEntry As String = ConfigurationManager.AppSettings(" + configEntry + ").ToString");
        //    sb.AppendLine("Dim cacheDuration As Integer = 0");
        //    sb.AppendLine("If Not Int32.TryParse(cacheDurationEntry, cacheDuration) Then");
        //    sb.AppendLine("Return " + dataAccessName + methodName + "(entity)");
        //    sb.AppendLine("ElseIf cacheDuration = 0 Then");
        //    sb.AppendLine("Return " + dataAccessName + methodName + "(entity)");
        //    sb.AppendLine("End If");

        //    sb.AppendLine("Dim cachedEntity As " + entityName + "= Nothing");
        //    sb.Append("Dim cacheKey As StringBuilder = New StringBuilder(");
        //    sb.Append(qq + myClassName + methodName + qq);
        //    sb.AppendLine(")");

        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string direction = GetDirection(param);
        //        switch (direction)
        //        {
        //            case addParameter:
        //            case addInParameter:
        //                string parameterName = Capitalize(ReverseParameterName(param.ParameterName.ToLower()));
        //                sb.AppendLine("cacheKey.Append(" + qq + parameterName + qq + ")");
        //                sb.AppendLine("cacheKey.Append(ConvertToString(entity." + parameterName + "))");
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    sb.AppendLine("cachedEntity = DirectCast(CacheManager.GetCacheItem(cacheKey.ToString" + cacheImplementation + "), " + entityName + ")");
        //    sb.AppendLine("If cachedEntity Is Nothing Then");
        //    sb.AppendLine("entity = " + dataAccessName + methodName + "(entity)");
        //    sb.AppendLine("CacheManager.SetCacheItem(cacheKey.ToString, entity" + cacheImplementation + ", cacheDuration)");
        //    sb.AppendLine("Else");
        //    sb.AppendLine("entity = cachedEntity");
        //    sb.AppendLine("End If");
        //    sb.AppendLine("Return entity");
        //    sb.AppendLine("End Function");
        //}


        //private static void WritecSharpBusinessObjectCached(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string cacheImplementation = ConfigurationManager.AppSettings("cacheImplementation");
        //    if (cacheImplementation.Equals("shared", StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        cacheImplementation = ", ConfigurationManager.AppSettings[" + qq + "SharedCacheEnabled" + qq + "]";
        //    }
        //    else
        //    {
        //        cacheImplementation = string.Empty;
        //    }
        //    string dataAccessName = GetMethodName(dc.Package) + "DO.";
        //    string myClassName = GetMethodName(dc.Package) + "BO.";
        //    string configEntry = Strings.Chr(34) + myClassName + methodName + ".CacheDuration" + Strings.Chr(34);

        //    sb.AppendLine("public static " + entityName + " " + methodName + "( " + entityName + " entity )");
        //    sb.AppendLine("{");
        //    bool hasOutputParameters = false;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string direction = GetDirection(param);
        //        switch (direction)
        //        {
        //            case addParameter:
        //            case addOutParameter:
        //                hasOutputParameters = true;
        //                break;
        //            default:

        //                break;
        //        }
        //    }

        //    if (hasOutputParameters)
        //    {
        //        // do nothing for now 
        //        // nothing to cache! 
        //    }
        //    else
        //    {
        //        sb.AppendLine("     " + "return " + dataAccessName + methodName + "(entity);");
        //        sb.AppendLine("}");
        //        return;
        //    }

        //    sb.AppendLine("     " + "string cacheDurationEntry = ConfigurationManager.AppSettings[" + configEntry + "].ToString();");
        //    sb.AppendLine("     " + "int cacheDuration = 0;");
        //    sb.AppendLine("     " + "if (!Int32.TryParse(cacheDurationEntry, out cacheDuration)) {");
        //    sb.AppendLine("     " + "return " + dataAccessName + methodName + "(entity);");
        //    sb.AppendLine("     " + "}");
        //    sb.AppendLine("     " + "else if (cacheDuration == 0) ");
        //    sb.AppendLine("     " + "return " + dataAccessName + methodName + "(entity);");
        //    sb.AppendLine("     " + entityName + " cachedEntity= null;");
        //    sb.Append("         " + "StringBuilder cacheKey  = new StringBuilder(");
        //    sb.Append("         " + qq + myClassName + methodName + qq);
        //    sb.AppendLine(");");

        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string direction = GetDirection(param);
        //        switch (direction)
        //        {
        //            case addParameter:
        //            case addInParameter:
        //                string parameterName = Capitalize(ReverseParameterName(param.ParameterName.ToLower()));
        //                sb.AppendLine("     " + "cacheKey.Append(" + qq + parameterName + qq + ");");
        //                sb.AppendLine("     " + "cacheKey.Append(ConvertToString(entity." + parameterName + "));");
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    sb.AppendLine("     " + "cachedEntity = (" + entityName + ")CacheManager.GetCacheItem(cacheKey.ToString());");
        //    sb.AppendLine("     " + "if (cachedEntity == null) { ");
        //    sb.AppendLine("     " + "entity = " + dataAccessName + methodName + "(entity);");
        //    sb.AppendLine("     " + "CacheManager.SetCacheItem(cacheKey.ToString(), entity" + cacheImplementation + ", cacheDuration);");
        //    sb.AppendLine("     " + "}");
        //    sb.AppendLine("     " + "else { ");
        //    sb.AppendLine("     " + "entity = cachedEntity;");
        //    sb.AppendLine("     " + "}");
        //    sb.AppendLine("     " + "return entity;");
        //    sb.AppendLine(" " + "}");
        //}

        //private static string DblQ(string var)
        //{
        //    return Strings.Chr(34) + Strings.Trim(var) + Strings.Chr(34);
        //}

        //private static void WriteServiceMethods(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string businessObjectNameName = GetMethodName(dc.Package) + "BO.";
        //    sb.AppendLine("Public Function " + methodName + "(byVal entity As " + entityName + ") as " + entityName + " Implements " + dc.InterfaceName + "." + methodName);
        //    sb.AppendLine("Try");
        //    sb.AppendLine("entity = " + businessObjectNameName + methodName + "(entity)");
        //    sb.AppendLine("Catch ex As Exception");
        //    sb.AppendLine("ExceptionManager.HandleException(ex)");
        //    sb.AppendLine("Throw");
        //    sb.AppendLine("Finally");
        //    sb.AppendLine(string.Empty);
        //    sb.AppendLine("End Try");
        //    sb.AppendLine("Return entity");
        //    sb.AppendLine("End Function");
        //}

        //private static void WriteCSharpServiceMethods(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    string businessObjectNameName = GetMethodName(dc.Package) + "BO.";
        //    sb.AppendLine("public " + entityName + " " + methodName + "( " + entityName + " " + "entity" + ") {");
        //    sb.AppendLine("     " + "try {");
        //    sb.AppendLine("         " + "entity = " + businessObjectNameName + methodName + "(entity);");
        //    sb.AppendLine("     " + "}");
        //    sb.AppendLine("     " + "catch (Exception ex){");
        //    sb.AppendLine("         " + "ExceptionManager.HandleException(ex);");
        //    sb.AppendLine("         " + "throw;");
        //    sb.AppendLine("     " + "}");
        //    sb.AppendLine("     " + "finally {");
        //    sb.AppendLine(string.Empty);
        //    sb.AppendLine("     " + "}");
        //    sb.AppendLine("     " + "return entity;");
        //    sb.AppendLine("}");
        //}

        //private static void WriteIServiceInterface(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.AppendLine("[OperationContract()]");
        //    sb.AppendLine("Function " + methodName + "(byval entity As " + entityName + ") As " + entityName);
        //    sb.AppendLine(string.Empty);
        //}

        //private static void WriteCSharpIServiceInterface(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.AppendLine("[OperationContract()]");
        //    sb.AppendLine(entityName + " " + methodName + "(" + entityName + " entity );");
        //    sb.AppendLine(string.Empty);
        //}


        //private static void WriteDataAccessCode(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.AppendLine("Public Shared Function  " + methodName + "(ByVal entity As " + entityName + ") as " + entityName);
        //    sb.AppendLine("Dim database As New DataSourceFactory");
        //    sb.AppendLine("Dim dataProvider As BaseDataProvider = Nothing");
        //    sb.AppendLine("Dim command As DbCommand = Nothing");
        //    sb.AppendLine("Try");

        //    sb.AppendLine("dataProvider = DirectCast(database.GetProvider(Enumerations.DataSource.PACSES), BaseDataProvider)");
        //    sb.AppendLine("command = dataProvider.GetStoredProcCommand(" + qq + cmd.CommandText.ToLower + qq + ")");

        //    string cmdText = executeNonQuery;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = Capitalize(ReverseParameterName(param.ParameterName.ToLower()));
        //        string direction = GetDirection(param);
        //        sb.Append("dataProvider.");
        //        sb.Append(direction);
        //        sb.Append("(command, ");
        //        sb.Append(qq + param.ParameterName.ToLower() + qq);
        //        sb.Append(", DbType.");
        //        sb.Append(param.DbType.ToString);

        //        switch (direction)
        //        {
        //            case addParameter:
        //                sb.Append(", ParameterDirection.InputOutput, String.Empty, DataRowVersion.Default, entity.");
        //                sb.Append(parameterName);
        //                sb.Append(".ToString().ToUpperInvariant()");
        //                break;
        //            case addOutParameter:
        //                sb.Append(", ");
        //                sb.Append(param.Size.ToString());
        //                if (param.Size == 0 & IsObject(param.DbType.ToString))
        //                {
        //                    cmdText = "entity." + executeDataset;
        //                }
        //                break;
        //            case addInParameter:
        //                sb.Append(", entity.");
        //                sb.Append(parameterName);
        //                break;
        //        }
        //        sb.AppendLine(")");
        //    }
        //    sb.AppendLine(cmdText);

        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        if (GetDirection(param) != addInParameter)
        //        {
        //            if (!IsObject(param.DbType.ToString))
        //            {
        //                string parameterName = Capitalize(ReverseParameterName(param.ParameterName.ToLower()));
        //                sb.Append("entity.");
        //                sb.Append(parameterName);
        //                switch (param.DbType.ToString())
        //                {
        //                    case "String":
        //                        sb.Append(" = GetStringValue(dataProvider, command, ");
        //                        break;
        //                    case "Decimal":
        //                        sb.Append(" = GetDecimalValue(dataProvider, command, ");
        //                        break;
        //                    case "Int32":
        //                        sb.Append(" = GetIntegerValue(dataProvider, command, ");
        //                        break;
        //                    case "Date":
        //                    case "DateTime":
        //                        sb.Append(" = GetDateValue(dataProvider, command, ");
        //                        break;
        //                    default:

        //                        break;
        //                }
        //                // sb.Append(" As " & param.DbType.ToString())

        //                sb.AppendLine(qq + param.ParameterName.ToLower() + qq + ")");
        //            }
        //        }
        //    }

        //    sb.AppendLine("Return entity");
        //    sb.AppendLine("Catch ex As Exception");
        //    sb.AppendLine("' ExceptionManager.HandleException(ex)");
        //    sb.AppendLine("Throw");
        //    sb.AppendLine("Finally");
        //    sb.AppendLine("dataProvider.CloseConnection(command)");
        //    sb.AppendLine("database = Nothing");
        //    sb.AppendLine("dataProvider = Nothing");
        //    sb.AppendLine("command = Nothing");
        //    sb.AppendLine("End Try");

        //    sb.AppendLine("End Function");
        //}

        //private static void WriteCSharpDataAccessCode(OracleCommand cmd, StringBuilder sb, BE dc)
        //{
        //    sb.AppendLine(" " + "public static " + entityName + " " + methodName + "( " + entityName + " entity  ) {");
        //    sb.AppendLine("     " + "DataSourceFactory database = new DataSourceFactory();");
        //    sb.AppendLine("     " + "BaseDataProvider dataProvider = null;");
        //    sb.AppendLine("     " + "DbCommand command = null;");
        //    sb.AppendLine("     " + "try {");

        //    sb.AppendLine("     " + "dataProvider = (BaseDataProvider)database.GetProvider(Enumerations.DataSource.PACSES);");
        //    sb.AppendLine("     " + "command = dataProvider.GetStoredProcCommand(" + qq + cmd.CommandText.ToLower + qq + ");");

        //    string cmdText = executeCsharpNonQuery;
        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        string parameterName = Capitalize(ReverseParameterName(param.ParameterName.ToLower()));
        //        string direction = GetDirection(param);
        //        sb.Append("     " + "dataProvider.");
        //        sb.Append(direction);
        //        sb.Append("     " + "(command, ");
        //        sb.Append("     " + qq + param.ParameterName.ToLower() + qq);
        //        sb.Append("     " + ", DbType.");
        //        sb.Append(param.DbType.ToString);

        //        switch (direction)
        //        {
        //            case addParameter:
        //                sb.Append(", ParameterDirection.InputOutput, String.Empty, DataRowVersion.Default, entity.");
        //                sb.Append(parameterName);
        //                sb.Append(".ToString().ToUpperInvariant()");
        //                break;
        //            case addOutParameter:
        //                sb.Append(", ");
        //                sb.Append(param.Size.ToString());
        //                if (param.Size == 0 & IsObject(param.DbType.ToString))
        //                {
        //                    cmdText = "     " + "entity." + executeDataset;
        //                }
        //                break;
        //            case addInParameter:
        //                sb.Append(", entity.");
        //                sb.Append(parameterName);
        //                break;
        //        }
        //        sb.AppendLine(");");
        //    }
        //    sb.AppendLine("     " + cmdText + ";");

        //    foreach (OracleParameter param in cmd.Parameters)
        //    {
        //        if (GetDirection(param) != addInParameter)
        //        {
        //            if (!IsObject(param.DbType.ToString))
        //            {
        //                string parameterName = Capitalize(ReverseParameterName(param.ParameterName.ToLower()));
        //                sb.Append("entity.");
        //                sb.Append(parameterName);
        //                switch (param.DbType.ToString())
        //                {
        //                    case "String":
        //                        sb.Append(" = GetStringValue(dataProvider, command, ");
        //                        break;
        //                    case "Decimal":
        //                        sb.Append(" = GetDecimalValue(dataProvider, command, ");
        //                        break;
        //                    case "Int32":
        //                        sb.Append(" = GetIntegerValue(dataProvider, command, ");
        //                        break;
        //                    case "Date":
        //                    case "DateTime":
        //                        sb.Append(" = GetDateValue(dataProvider, command, ");
        //                        break;
        //                    default:

        //                        break;
        //                }
        //                // sb.Append(" As " & param.DbType.ToString())

        //                sb.AppendLine("     " + qq + param.ParameterName.ToLower() + qq + ");");
        //            }
        //        }
        //    }
        //    sb.AppendLine("     " + "return entity;");
        //    sb.AppendLine("     " + "}");
        //    sb.AppendLine("     " + "catch (Exception ex) {");
        //    sb.AppendLine("     " + "// ExceptionManager.HandleException(ex);");
        //    sb.AppendLine("     " + "throw;");
        //    sb.AppendLine("     " + "}");
        //    sb.AppendLine("     " + "finally {");
        //    sb.AppendLine("     " + "dataProvider.CloseConnection(command);");
        //    sb.AppendLine("     " + "database = null;");
        //    sb.AppendLine("     " + "dataProvider = null;");
        //    sb.AppendLine("     " + "command = null;");
        //    sb.AppendLine("     " + "}");
        //    sb.AppendLine("  " + "}");
        //}


        #endregion

        #region "Common Functions"
        //public static OracleCommand GetOracleCommand(OracleConnection conn, BE dc)
        //{
        //    ds = dc.StandardAbbreviations;
        //    OracleCommand cmd = conn.CreateCommand();
        //    cmd.CommandText = dc.Owner + "." + dc.Package + "." + dc.StoredProcedure;
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    var _with11 = conn;
        //    try
        //    {
        //        OracleCommandBuilder.DeriveParameters(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        Interaction.MsgBox(ex.ToString);
        //    }
        //    return cmd;
        //}

        //private static object GetStoredProcedureDescription(BE dc)
        //{
        //    bool getDescription = false;
        //    string description = string.Empty;

        //    DataView dv = dc.StoredProcedureComments.Tables(0).DefaultView;

        //    foreach (DataRowView drv in dv)
        //    {
        //        string commentLine = drv("text").ToString().ToUpper();
        //        if (Strings.InStr(commentLine, dc.StoredProcedure.ToUpper) != 0)
        //        {
        //            getDescription = true;
        //        }
        //        if (getDescription)
        //        {
        //            if (Strings.InStr(commentLine, "DESCRIPTION") != 0)
        //            {
        //                description = drv("text").ToString().Remove(0, 2);
        //                getDescription = false;
        //            }
        //        }

        //    }
        //    return description.Trim();
        //}

        //private static string GetDirection(OracleParameter param)
        //{
        //    string direction = param.Direction.ToString().ToLower();

        //    switch (direction)
        //    {
        //        case "input":
        //            return addInParameter;
        //        case "output":
        //            return addOutParameter;
        //        case "inputoutput":
        //            return addParameter;
        //    }

        //    return direction;
        //}

        //private static string ReverseParameterName(string var)
        //{
        //    string[] sep = { "_" };
        //    string[] arr = var.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = arr.GetUpperBound(0); i >= 1; i += -1)
        //    {
        //        sb.Append(GetExpandedWord(arr(i)));
        //    }
        //    var = sb.ToString();
        //    return char.ToLower(var(0)) + var.Substring(1);
        //}

        private static string GetExpandedWord(string abbreviation)
        {
            string word = abbreviation.Trim();
            DataView dv = ds.Tables[1].DefaultView;
            dv.RowFilter = "Abbreviation = '" + abbreviation.ToUpper() + "'";
            if (dv.Count != 0)
            {
                foreach (DataRowView drv in dv)
                {
                    word = drv["Description"].ToString().Trim();
                    string tmp = word.Replace("-", " ");
                    word = tmp.Replace("(", " ");
                    tmp = word.Replace(")", " ");
                    word = tmp.Replace("/", " ");
                    tmp = word.Replace("'", string.Empty);
                    word = tmp;
                }
                return FormatMultipleWords(word.ToLower());
            }
            else
            {
                return Capitalize(word);
            }
        }

        private static string FormatMultipleWords(dynamic var)
        {
            string[] sep = { " " };
            string[] arr = var.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= arr.GetUpperBound(0); i++)
            {
                sb.Append(Capitalize(arr[i]));
            }
            return sb.ToString();
        }

        private static string Capitalize(dynamic var)
        {
            return char.ToUpper(var[0]) + var.Substring(1);
        }

        private static string GetMethodName(string var)
        {
            string[] sep = { "_" };
            string[] arr = var.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= arr.GetUpperBound(0); i++)
            {
                sb.Append(GetExpandedWord(arr[i]));
            }

            return sb.ToString();
        }

        public static string GetClassName(BE dc)
        {
            ds = dc.StandardAbbreviations;
            return GetMethodName(dc.Package);
        }

        //private static bool IsObject(string var)
        //{
        //    if (var.Trim.ToLower == "object")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        #endregion
    }

    //=======================================================
    //Service provided by Telerik (www.telerik.com)
    //Conversion powered by NRefactory.
    //Twitter: @telerik
    //Facebook: facebook.com/telerik
    //=======================================================

}
