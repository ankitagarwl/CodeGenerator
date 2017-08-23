using System.Web.Http;
using EZRide.StateClass;
using System.Net.Http;
using EZRide.BAL;
using System;
using System.Net;
using System.Collections.Generic;
using System.Web.Http.Cors;
using System.Data;
using System.Configuration;

namespace EZRide.Controllers
{

    /// <summary>
    /// All APIs 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class RiderController : ApiController
    {

       
    }
}
