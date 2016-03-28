using Rinsen.WebServer.FileAndDirectoryServer;

namespace NetduinoDisplayUpdater.Models
{
    public class SDCardManager : NetduinoSDCard.SDCard, ISDCardManager
    {
        public SDCardManager(string WorkingDirectory)
            : base(WorkingDirectory)
        {

        }
    }
}
