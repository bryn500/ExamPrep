using System.Diagnostics;
using System.Web.Mvc;

namespace DebugTrace.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //basic trace see /trace.axd
            Trace.TraceInformation("Something happened");
            Trace.TraceError("Oh no! Something happened");

            // custom tracing see test.log
            TraceSource mySource = new TraceSource("mySource");
            mySource.TraceInformation("Hello");
            mySource.TraceEvent(TraceEventType.Error, 0, "!!! Hello !!!");
            mySource.Flush();
            mySource.Close();

            return View();
        }
    }
}