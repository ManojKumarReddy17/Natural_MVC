//using System;
//using System.Security.Cryptography;
//using Natural.Core.IServices;
//using Natural.Core.Models;

//namespace Naturals.Service.Service
//{
//	public class DistributorNotificationService : IDistributorNotificationService
//    {
		

//        private readonly IHttpClientWrapper _HttpCleintWrapper;

//        public DistributorNotificationService(IHttpClientWrapper httpCleintWrapper)
//        {
//            _HttpCleintWrapper = httpCleintWrapper;
//        }

//        //public Task<List<DistributorNotification>> CreateNotification(DistributorNotification notification)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        public async Task<List<DistributorNotification>> GetNotification()

//        {

//           var notifications =  await _HttpCleintWrapper.GetAsync<List<DistributorNotification>>("/DistributorNotification/");
//            return notifications;
//        }


//        public async  Task<DistributorNotification> GetNotificationById(int id)
//        {
//            string Id = id.ToString();
//            var notifications = await _HttpCleintWrapper.GetByIdAsync<DistributorNotification>("/DistributorNotification", Id);
//            return notifications;

//        }



//        public Task<ProductResponse> CreateNotification(List<DistributorNotification>notification)
//        {

//            //var create = notification.Select(c => new DistributorNotification
//            //{

//            //    Product = c.Product,
//            //    Quantity = c.Quantity,
//            //    Price = c.Price,
//            //    Dsr = dsr.Id

//            //}).ToList();
//        var result =    _HttpCleintWrapper.PostAsync<ProductResponse>("/DistributorNotification/", notification);


//            return result;
//        }


//      public async Task<bool> DeleteNotification(int id)
//        {
//            try
//            {
//                string Id = id.ToString();
//                var isDeleted = await _HttpCleintWrapper.DeleteAsync("/DistributorNotification", Id);

//                return isDeleted;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Failed to delete distributor. Error: {ex.Message}");
//            }


//        }
//    }
//}

