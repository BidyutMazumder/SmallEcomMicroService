using Discount.API.Ripositories.Abstraction;
using Microsoft.AspNetCore.Mvc;
using CoreApiResponse;
using System.Net;
using Discount.API.Models;

namespace Discount.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private readonly ICuponRepository _cuponRepository;

        public DiscountController(ICuponRepository cuponRepository)
        {
            this._cuponRepository = cuponRepository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDisCount(string productId)
        {
            try
            {
                var coupon = await _cuponRepository.GetDiscount(productId);

                return CustomResult(coupon); 
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);   
            }

        }
        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateDisCount([FromBody] Coupon coupon)
        {
            try
            {
                var isSeved = await _cuponRepository.CreateDiscount(coupon);

                if (isSeved)
                {
                    return CustomResult("Coupon Saved successfully",coupon, HttpStatusCode.Created);
                }
                else
                {
                    return CustomResult("Coupon Saved Failed", coupon, HttpStatusCode.BadRequest);
                }
                
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDisCount([FromBody] Coupon coupon)
        {
            try
            {
                var isUpdate = await _cuponRepository.UpdateDiscount(coupon);

                if (isUpdate)
                {
                    return CustomResult("Coupon Update successfully", HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Coupon Update Failed", HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }


        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDisCount(string couponId)
        {
            try
            {
                var isDelete = await _cuponRepository.DeleteDiscount(couponId);

                if (isDelete)
                {
                    return CustomResult("Coupon Delete successfully", HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Coupon Delete Failed", HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }
    }
}
