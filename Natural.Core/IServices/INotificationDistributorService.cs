using System;
using Natural.Core.Models;

namespace Natural.Core.IServices
{
	public interface INotificationDistributorService
	{
         Task<IEnumerable<Notification>> GetNotification();
        Task<ProductResponse> CreateNotification(Notification notification);
        Task<bool> DeleteNotification(string id);
        Task<Notification> GetNotificationById(string id);
        Task<string> GetExectuvieById(string id);
        Task<Notification> GetNotification_DistributorById(string id);
        Task<ProductResponse> Updatenotification(Notification updatedata,string id);
        Task<List<Notification>> SearchNotification(Dsrview search);
    }
}

