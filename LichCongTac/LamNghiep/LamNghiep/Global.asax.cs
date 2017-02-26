using LamNghiep.Controllers;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace LamNghiep
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Dictionary<string, Dictionary<string, object>> shortLiveCache
                                            = new Dictionary<string, Dictionary<string, object>>();
        protected void Application_Start()
        {
            string l4Net = Server.MapPath("~/log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(l4Net));

            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((MvcApplication)sender).Context;
            var ex = this.Server.GetLastError();
            Controller controller = new ErrorController();
            var routeData = new RouteData();
            var action = "ErrorPage";
            HttpException httpEx = new HttpException();

            httpContext.ClearError();
            httpContext.Response.Clear();
            if (ex.GetType() == typeof(HttpException))
            {
                httpEx = ex as HttpException;

                switch (httpEx.GetHttpCode())
                {
                    case 404:
                        action = "NotFound";
                        break;

                    case 401:
                        action = "SqlException";
                        break;
                }
            }

           
            string logContent = string.Format(@"{5}#############   ERROR: {0}    #################{5}StatusCode = '{1}', Action = '{2}'{5}Error message: {3}{5}Error details: {4} {5}Inner exception message: {6}",
                System.DateTime.Now.ToString(),
                (ex is HttpException ? httpEx.GetHttpCode() : 500),
                action,
                ex.Message,
                ex.StackTrace,
                Environment.NewLine,
                ex.InnerException == null ? string.Empty : ex.InnerException.Message);
            log.Debug(logContent);

            //httpContext.Response.StatusCode = ex is HttpException ? httpEx.GetHttpCode() : 500;
            //httpContext.Response.TrySkipIisCustomErrors = true;

            string Message = ex.Message;
            string ErrorCode = httpContext.Response.StatusCode.ToString();

            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;
            routeData.Values["message"] = Message;
            routeData.Values["referer"] = httpContext.Request.Url.LocalPath;

            bool isAjaxCall = string.Equals("XMLHttpRequest", this.Context.Request.Headers["x-requested-with"], StringComparison.OrdinalIgnoreCase);
            this.Context.ClearError();

            if (isAjaxCall)
            {
                //this.Context.Response.BufferOutput = true;
                //this.Context.Response.ContentType = "application/json";
                //this.Context.Response.StatusCode = 200;
                //this.Context.Response.Write(
                //    new JavaScriptSerializer().Serialize(
                //        new
                //        {
                //            CommonError = "CommonError",
                //            url =
                //                System.Configuration.ConfigurationManager.AppSettings["MobileSubFolder"] + "/Common/Error/" + action + "?referer=" + routeData.Values["referer"] + "&message="
                //                + routeData.Values["message"]
                //        }));
            }
            else
            {
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }

            controller.Dispose();
        }
    }
}
