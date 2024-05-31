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
          var Allnotification= await  _HttpCleintWrapper.GetAsync<IEnumerable<Notification>>("/Notification");
            return Allnotification;

        }

        public async Task<List<Notification>> SearchNotification(Dsrview search)
        {
            var notificationSearch = await _HttpCleintWrapper.PostAsync<List<Notification>>("/Notification/Search", search);
            return notificationSearch;

        }


        public async Task<Notification> GetNotification_DistributorById(string id)
        {
            var Allnotification = await _HttpCleintWrapper.GetByIdAsync<Notification>("/Notification/", id);
            return Allnotification;
        }

        public async Task<Notification> GetNotificationById(string id)
        {
            var Allnotification = await _HttpCleintWrapper.GetByIdAsync<Notification>("/Notification/details/", id);
            return Allnotification;

        }

        //public async Task<string> GetExectuvieById(string id)
        //{
            
        //    var Allnotification = await _HttpCleintWrapper.GetIdAsync("/Notification/Executive", id);
        //    return Allnotification;
 
        //}


        public Task<ProductResponse> CreateNotification(Notification notification)
        {

            var result = _HttpCleintWrapper.PostAsync<ProductResponse>("/Notification/", notification);
            return result;
        }


        public async Task<bool> DeleteNotification(string id)
        {
            try
            {
                
                var isDeleted = await _HttpCleintWrapper.DeleteAsync("/Notification", id);

                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete distributor. Error: {ex.Message}");
            }


        }


        public async Task<ProductResponse> Updatenotification(Notification updatedata, string id)
        {

            var updateresult = await _HttpCleintWrapper.PutAsync<ProductResponse>("/Notification", id, updatedata);
            return updateresult;

        }

    }
}

