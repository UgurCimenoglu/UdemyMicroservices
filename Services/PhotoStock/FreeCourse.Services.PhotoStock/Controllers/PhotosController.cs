using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        //Burada methoda parametre olarak cancelationToken geçtik, bunu frontendden methoda parametre olarak geçme gibi bir durum söz konusu değil ,yalnızca ilk parametre gelecek frontendden.
        // CancelationToken ne işe yarar ? Bir methoda istek attıgımızda ne bu isteği iptal etme durumunda cancelationToken devreye girer ve isteği sonlandırır. Örneğin biz ön yüzden bir fotograf yükleme isteği attık,
        // bu isteğin ortalama 1 dakika sürdüğünü varsayalım, biz endponitten response dönmeden tarayıcıyı kapatırsak istek normalde iptal olmaz arkada işlemeye devam eder, işte bu noktada cancelationToken kullanırsak
        //tarayıcıyı kapatttıgımız anda istek de iptal olur.
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo.Length > 0 && photo != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo was empty", 400));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo not found!", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
