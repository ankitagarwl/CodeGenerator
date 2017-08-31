using   PA.DPW.PACSES.BaseClasses;
using   PA.DPW.PACSES.Utilities;
using   System.Configuration;
using   System.Text;
using	System;
using   System.Data;
public class RiderBO
{
public static string ConvertToString(object parameterValue)
{
	if (parameterValue == null) {
		return string.Empty;
	}
	return parameterValue.ToString();
}
}

