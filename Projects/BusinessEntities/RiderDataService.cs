using System;
public class RiderDataServiceDataService
:  IRiderDataService{
#region RiderDataService Methods
public RiderBE.RiderBE Rider( RiderBE.RiderBE entity) {
     try {
         entity = RiderBO.Rider(entity);
     }
     catch (Exception ex){
         ExceptionManager.HandleException(ex);
         throw;
     }
     finally {

     }
     return entity;
}
#endregion
}

