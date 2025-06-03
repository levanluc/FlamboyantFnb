using FlamboyantFnb.Attributes;
using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces.Repository;
using FlamboyantFnb.Domain.Interfaces.Services;
using FlamboyantFnb.Domain.RequestModel;
using FlamboyantFnb.Domain.Response;
using FlamboyantFnb.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlamboyantFnb.Controllers
{
    [Authenticate]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration, IProductRepository productRepository) : base(configuration)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<BaseResponse<List<Product>>> Get(ProductFilterReq req)
        {
            var takeRecordNumber = req.Take > 0 ? req.Take.Value : 10;
            var iQuery = _productRepository.GetAll();
            if (!string.IsNullOrEmpty(req.Code))
            {
                iQuery = iQuery.Where(x => x.Code.Contains(req.Code));
            }
            if (!string.IsNullOrEmpty(req.FullName))
            {
                iQuery = iQuery.Where(x => x.Name.Contains(req.FullName));
            }
            if (req.CategoryId.HasValue)
            {
                iQuery = iQuery.Where(x => x.CategoryId == req.CategoryId.Value);
            }
            if (req.IsActive.HasValue)
            {
                iQuery = iQuery.Where(x => x.IsActive == req.IsActive.Value);
            }
            if (req.Skip > 0)
            {
                iQuery = iQuery.Skip(req.Skip.Value);
            }
            iQuery = iQuery.Take(takeRecordNumber);
            var products = await iQuery.ToListAsync();
            return Ok(products);
        }

    }
}
