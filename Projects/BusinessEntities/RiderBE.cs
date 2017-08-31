using System.Runtime.Serialization;
using System;
using System.Data;
namespace RiderBE
{
[DataContract()]
public class RiderBE
{ 
AnsiString _riderId;
Object _recordset;
[DataMember()]
public AnsiString RiderId { 
    get { return  _riderId; }
    set { _riderId = value; }
}
[DataMember()]
public Object Recordset { 
    get { return  _recordset; }
    set { _recordset = value; }
}
}
}

