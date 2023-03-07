using Coupon.API.Dtos;
using Coupon.API.Dtos.LoyaltyInfo;
using Coupon.API.Infrastructure.Models;
using Coupon.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Coupon.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyInfoRepository _loyaltyInfoRepository;
        private readonly IMapper<LoyaltyInfoDto, LoyaltyInfo> _mapper;

        public LoyaltyController(ILoyaltyInfoRepository loyaltyRepository, IMapper<LoyaltyInfoDto, LoyaltyInfo> mapper)
        {
            _loyaltyInfoRepository = loyaltyRepository;
            _mapper = mapper;
        }

        [HttpGet("{buyerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetLoyaltyPointsByBuyerAsync(int buyerId)
        {
            var points = await _loyaltyInfoRepository.GetPointsAvailableAsync(buyerId);
            if (points == null)
            {
                return NotFound();
            }
            return points;
        }
    }
}
