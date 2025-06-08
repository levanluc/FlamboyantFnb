using FlamboyantFnb.Attributes;
using FlamboyantFnb.Domain.Context;
using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces.Repository;
using FlamboyantFnb.Domain.Interfaces.Services;
using FlamboyantFnb.Domain.RequestModel;
using FlamboyantFnb.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlamboyantFnb.Controllers
{
    [Authenticate]
    public class FnbTableController : BaseController
    {
        private readonly IFnbTableRepository _fnbTableRepository;
        private readonly ILogger<FnbTableController> _logger;
        private readonly FnbExecutionContext _executionContext;

        public FnbTableController(ILogger<FnbTableController> logger, IConfiguration configuration, IFnbTableRepository fnbTableRepository, FnbExecutionContext executionContext) : base(configuration)
        {
            _logger = logger;
            _fnbTableRepository = fnbTableRepository;
            _executionContext = executionContext;
        }

        [HttpGet]
        public async Task<BaseResponse<List<FnbTable>>> Get(FnbTableFilterReq req)
        {
            var takeRecordNumber = req.Take > 0 ? req.Take.Value : 10;
            var iQuery = _fnbTableRepository.GetAll();
            if (!string.IsNullOrEmpty(req.Name))
            {
                iQuery = iQuery.Where(x => x.Name.ToLower().Contains(req.Name.ToLower().Trim()));
            }

            if (req.Status.HasValue)
            {
                iQuery = iQuery.Where(x => x.Status == req.Status.Value);
            }
            if (req.Skip > 0)
            {
                iQuery = iQuery.Skip(req.Skip.Value);
            }
            iQuery = iQuery.Take(takeRecordNumber);
            var products = await iQuery.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<BaseResponse<FnbTable>> Post(FnbTableAddReq req)
        {
            var table = new FnbTable
            {
                Name = req.Name,
                SeatCount = req.SeatCount,
                Location = req.Location,
                Status = 0, // Assuming 1 is the default status for a new table 
                MerchantId = _executionContext.MerchantId
            };
            var addedCategory = await _fnbTableRepository.AddAsync(table, true, CancellationToken.None);
            return Ok(addedCategory);
        }

        [HttpPut]
        public async Task<BaseResponse<FnbTable>> Put(FnbTableUpdateReq req)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<FnbTable>
                {
                    Code = 400,
                    Message = "Invalid request data.",
                    Data = new FnbTable()
                };
            }

            var table = await _fnbTableRepository.GetByIdAsync(req.Id);
            if (table == null || table.MerchantId != _executionContext.MerchantId)
            {
                return new BaseResponse<FnbTable>
                {
                    Code = 404,
                    Message = "Table not found.",
                    Data = new FnbTable()
                };
            }

            table.Name = req.Name;
            table.SeatCount = req.SeatCount;
            table.Location = req.Location;

            var updatedTable = await _fnbTableRepository.UpdateAsync(table, true, CancellationToken.None);
            return Ok(updatedTable);
        }

    }
}
