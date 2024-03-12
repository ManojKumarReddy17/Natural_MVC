using System;
using System.Security.Cryptography;
using Natural.Core.IServices;
using Natural.Core.Models;

namespace Naturals.Service.Service
{
	public class NotificationDistributorService : INotificationDistributorService
    {
		

        private readonly IHttpClientWrapper _HttpCleintWrapper;

        public NotificationDistributorService(IHttpClientWrapper httpCleintWrapper)
        {
            _HttpCleintWrapper = httpCleintWrapper;
        }




        public async Task<IEnumerable<Notification>> GetNotification()
        {
          var Allnotification= await  _HttpCleintWrapper.GetAsync<IEnumerable<Notification>>("/NotificationDistributor/");
            return Allnotification;

        }

        public async Task<List<Notification>> SearchNotification(Dsrview search)
        {
            var notificationSearch = await _HttpCleintWrapper.PostAsync<List<Notification>>("/NotificationDistributor/Search", search);
            return notificationSearch;

        }


        public async Task<Notification> GetNotification_DistributorById(string id)
        {
            var Allnotification = await _HttpCleintWrapper.GetByIdAsync<Notification>("/NotificationDistributor", id);
            return Allnotification;
        }

        public async Task<Notification> GetNotificationById(string id)
        {
            var Allnotification = await _HttpCleintWrapper.GetByIdAsync<Notification>("/NotificationDistributor/details", id);
            return Allnotification;

        }

        public async Task<string> GetExectuvieById(string id)
        {
            
            var Allnotification = await _HttpCleintWrapper.GetIdAsync("/NotificationDistributor/Executive", id);
            return Allnotification;
 
        }


        public Task<ProductResponse> CreateNotification(Notification notification)
        {

            var result = _HttpCleintWrapper.PostAsync<ProductResponse>("/NotificationDistributor/", notification);
            return result;
        }


        public async Task<bool> DeleteNotification(string id)
        {
            try
            {
                
                var isDeleted = await _HttpCleintWrapper.DeleteAsync("/NotificationDistributor", id);

                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete distributor. Error: {ex.Message}");
            }


        }


        public async Task<ProductResponse> Updatenotification(Notification updatedata, string id)
        {

            var updateresult = await _HttpCleintWrapper.PutAsync<ProductResponse>("/NotificationDistributor", id, updatedata);
            return updateresult;

        }

    }
}

