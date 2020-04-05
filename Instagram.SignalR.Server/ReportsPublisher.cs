using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Fiver.Asp.SignalR.Server
{
    public class ReportsPublisher : Hub
    {
        public Task PublishReport(string reportName)
        {
            return Clients.All.InvokeAsync("OnReportPublished", reportName);
        }

        public Task SetLike(string reportName)
        {
            return Clients.All.InvokeAsync("OnSetLike", reportName);
        }

        public Task SetLikeColor(string reportName)
        {
            return Clients.All.InvokeAsync("OnSetLikeColor", reportName);
        }
    }
}
