using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Fiver.Asp.SignalR.Server
{
    public class ReportsPublisher : Hub
    {
        public Task AddComment(string commentData)
        {
            return Clients.All.InvokeAsync("OnAddComment", commentData);
        }

        public Task SetLike(string likeData)
        {
            return Clients.All.InvokeAsync("OnSetLike", likeData);
        }

        public Task SetLikeColor(string likeColorData)
        {
            return Clients.All.InvokeAsync("OnSetLikeColor", likeColorData);
        }
    }
}
