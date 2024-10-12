using Dapper;
using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<DiscountModel>> ChechkIfDiscountIsDefinedByUserId(string code, string userId)
        {
            var discounts = await _connection.QueryAsync<DiscountModel>("select * from discount where code = @Code and userid = @UserId", new { Code = code, UserId = userId });

            var hasDiscount = discounts.FirstOrDefault();
            if (hasDiscount is not null)
                return Response<DiscountModel>.Success(hasDiscount, 204);
            return Response<DiscountModel>.Fail("Discount not found!", 404);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _connection.ExecuteAsync("delete from discount where id = @Id", new { Id = id });
            if (status > 0)
                return Response<NoContent>.Success(204);
            return Response<NoContent>.Fail("Discount not found!", 404);
        }

        public async Task<Response<List<DiscountModel>>> GetAll()
        {
            var discontList = await _connection.QueryAsync<DiscountModel>("Select * from discount");
            return Response<List<DiscountModel>>.Success(discontList.ToList(), 200);
        }

        public async Task<Response<DiscountModel>> GetById(int id)
        {
            var discount = (await _connection.QueryAsync<DiscountModel>("select * from discount where id = @id", new { id })).SingleOrDefault();
            if (discount == null)
                return Response<DiscountModel>.Fail("Discount not found!", 404);
            return Response<DiscountModel>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(DiscountModel model)
        {
            var status = await _connection.ExecuteAsync("INSERT INTO discount (userid, rate, code) VALUES(@UserId, @Rate, @Code)", model);
            if (status > 0)
                return Response<NoContent>.Success(204);
            return Response<NoContent>.Fail("An error occurred while saving the discount.", 500);
        }

        public async Task<Response<NoContent>> Update(DiscountModel model)
        {
            var status = await _connection.ExecuteAsync("update discount set userid = @UserId, code = @Code, rate = @Rate where id = @Id", new { Id = model.Id, Code = model.Code, Rate = model.Rate, UserId = model.UserId });
            if (status > 0)
                return Response<NoContent>.Success(204);
            return Response<NoContent>.Fail("Discount not found!", 404);
        }
    }
}
