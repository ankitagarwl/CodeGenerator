using   PA.DPW.PACSES.BaseClasses;
using   PA.DPW.PACSES.Utilities;
using   System.Configuration;
using   System.Text;
using	System;
using   System.Data;

public class RiderDO
{
public static string GetStringValue(BaseDataProvider dataProvider, DbCommand command, string parameterName)
{
	if (Convert.IsDBNull(dataProvider.GetParameterValue(command, parameterName))) {
		return string.Empty;
	}
	return dataProvider.GetParameterValue(command, parameterName).ToString();
}

public static System.DateTime GetDateValue(BaseDataProvider dataProvider, DbCommand command, string parameterName)
{
	if (Convert.IsDBNull(dataProvider.GetParameterValue(command, parameterName))) {
		return Convert.ToDateTime(System.DBNull.Value);
	}
	return Convert.ToDateTime(dataProvider.GetParameterValue(command, parameterName));
}

public static decimal GetDecimalValue(BaseDataProvider dataProvider, DbCommand command, string parameterName)
{
	if (Convert.IsDBNull(dataProvider.GetParameterValue(command, parameterName))) {
		return Convert.ToDecimal(System.DBNull.Value);
	}
	return Convert.ToDecimal(dataProvider.GetParameterValue(command, parameterName));
}

public static int GetIntegerValue(BaseDataProvider dataProvider, DbCommand command, string parameterName)
{
	if (Convert.IsDBNull(dataProvider.GetParameterValue(command, parameterName))) {
		return Convert.ToInt32(System.DBNull.Value);
	}
	return Convert.ToInt32(dataProvider.GetParameterValue(command, parameterName));
}
}

