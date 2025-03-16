using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.ControllerBases;


namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }
    }
}
