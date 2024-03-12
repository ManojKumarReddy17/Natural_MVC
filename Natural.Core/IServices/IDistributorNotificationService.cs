using System;
using System.Collections.Generic;
using Natural.Core.Models;

namespace Natural.Core.IServices
{
	public interface IDistributorNotificationService
	{
        Task<ProductResponse> CreateNotification(List<DistributorNotification> notification);

        Task<List<DistributorNotification>> GetNotification();

        Task<DistributorNotification> GetNotificationById(int id);

        Task<bool> DeleteNotification(int id);



    }
}

