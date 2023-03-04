namespace Coupon.API.Dtos
{
    using Coupon.API.Infrastructure.Models;

    public class CouponMapper : IMapper<CouponDto, Coupon>
    {
        public CouponDto Translate(Coupon entity)
        {
            return new CouponDto
            {
                Code = entity.Code,
                Discount = entity.Discount
            };
        }
    }
}
