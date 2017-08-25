using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeGenerator_web.Controllers
{
    public class DefaultController : ApiController
    {
        #region Ride History
        /// <summary>
        /// RIde History
        /// </summary>
        ///  <param name="rideridmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RideHistory([FromBody] RiderIDmodel rideridmodel)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Result objResult = null;
            UserBAL objUserBAL = new UserBAL();
            List<RideHistory> list = new List<RideHistory>();
            HttpClient client = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            string str_guid = Guid.NewGuid().ToString();
            try
            {


                #region API_Entered_log
                response1 = client.PostAsJsonAsync(ConfigurationSettings.AppSettings["LoggerURL"],
                        new LoggerDetails()
                        {
                            ContollerName = GetType().Name,
                            InOutType = "In",
                            guid = str_guid,
                            level = "Info",
                            message = "RideHistory API Entered"
                        }).Result;
                #endregion


                #region BAL_Entered_log
                response1 = client.PostAsJsonAsync(ConfigurationSettings.AppSettings["LoggerURL"],
                            new LoggerDetails()
                            {
                                ContollerName = GetType().Name,
                                InOutType = "In",
                                guid = str_guid,
                                level = "Debug",
                                message = "RideHistory BAL started"
                            }).Result;
                #endregion
                list = objUserBAL.RideHistory(rideridmodel.riderid);
                #region BAL_finished_log
                response1 = client.PostAsJsonAsync(ConfigurationSettings.AppSettings["LoggerURL"],
                            new LoggerDetails()
                            {
                                ContollerName = GetType().Name,
                                InOutType = "Out",
                                guid = str_guid,
                                level = "Debug",
                                message = "RideHistory BAL finished"
                            }).Result;
                #endregion


                if (list != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, list);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Data Empty!");
                }
                return response;
            }
            catch (Exception ex)
            {
                #region exception_handling
                response1 = client.PostAsJsonAsync(ConfigurationSettings.AppSettings["LoggerURL"],
                            new LoggerDetails()
                            {
                                ContollerName = GetType().Name,
                                InOutType = "",
                                guid = str_guid,
                                level = "Error",
                                message = "API finished",
                                ex = ex

                            }).Result;
                #endregion
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
        }
        #endregion
    }
}
