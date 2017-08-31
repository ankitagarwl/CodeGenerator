using System.ServiceModel;
[ServiceContract()]
public interface IRiderDataService
{
#region IRiderDataService Methods
[OperationContract()]
RiderBE.RiderBE Rider(RiderBE.RiderBE entity );

#endregion
}

