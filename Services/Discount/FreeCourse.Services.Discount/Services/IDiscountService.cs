using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<DiscountModel>>> GetAll();
        Task<Response<DiscountModel>> GetById(int id);
        Task<Response<NoContent>> Save(DiscountModel model);
        Task<Response<NoContent>> Update(DiscountModel model);
        Task<Response<NoContent>> Delete(int id);
        Task<Response<DiscountModel>> ChechkIfDiscountIsDefinedByUserId(string code, string userId);

    }
}
