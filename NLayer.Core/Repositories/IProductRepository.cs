using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IProductRepository :IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory(); // Generic Repositorydeki Temel Crud Operasyonları ve bu var.Repository Layerda somutu da var.
    }
}
