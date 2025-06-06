using FlamboyantFnb.Attributes;
using FlamboyantFnb.Domain.Context;
using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces.Repository;
using FlamboyantFnb.Domain.Interfaces.Services;
using FlamboyantFnb.Domain.RequestModel;
using FlamboyantFnb.Domain.Response;
using FlamboyantFnb.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FlamboyantFnb.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly FnbExecutionContext _executionContext;

        public CategoryController(ILogger<CategoryController> logger, IConfiguration configuration, ICategoryRepository categoryRepository, FnbExecutionContext executionContext) : base(configuration)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _executionContext = executionContext;
        }

        [HttpPost]
        public async Task<BaseResponse<Category>> Post(CategoryAddReq req)
        {
            var category = new Category
            {
                Name = req.Name,
                MerchantId = _executionContext.MerchantId
            };
            var addedCategory = await _categoryRepository.AddAsync(category, true, CancellationToken.None);
            return Ok(addedCategory);
        }

    }
}
