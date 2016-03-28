using Rinsen.WebServer.Http;

namespace NetduinoDisplayUpdater.Models.HttpStatusCodes
{
    public class Forbidden : HttpStatusCode
    {
        public override string ReasonPhrase => "Forbidden";

        public override int StatusCode => 403;
    }
}
