using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProduct(ProductModel mdl);
        Task<List<GetProduct>> GetAllProduct();
        Task<GetProduct> GetproductDetailsById(string ID);
        Task<GetProduct> GetproductById(string ID);
        Task<bool> DeleteImage(string ProductId);
        Task<bool> DeleteProduct(string ProductId);
        Task<ProductResponse> UpdateProduct(string ProductId, ProductModel mdl);

    }
}
