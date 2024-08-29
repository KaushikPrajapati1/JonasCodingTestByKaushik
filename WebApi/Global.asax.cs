using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;



namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ILoggerFactory LoggerFactory { get; private set; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Logger.Info("Application started");
       
        }
    }
}
