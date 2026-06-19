using Microsoft.AspNetCore.Mvc;
using DVDLBusinussLayer;
using DVLD_API.DTOs;

namespace DVLD_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        // GET: api/country
        [HttpGet]
        public ActionResult<ResponseDTO> GetAllCountries()
        {
            try
            {
                var countries = clsCountry.GetAllCountries();

                if (countries == null || countries.Count == 0)
                {
                    return Ok(new ResponseDTO
                    {
                        Success = false,
                        Message = "لا توجد دول",
                        Data = new List<CountryDTO>()
                    });
                }

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "تم جلب الدول بنجاح",
                    Data = countries
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO
                {
                    Success = false,
                    Message = $"خطأ في الخادم: {ex.Message}",
                    Data = null
                });
            }
        }

        // GET: api/country/5
        [HttpGet("{id}")]
        public ActionResult<ResponseDTO> GetCountryByID(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف الدولة غير صحيح",
                        Data = null
                    });
                }

                var country = clsCountry.Find(id);

                if (country == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "الدولة غير موجودة",
                        Data = null
                    });
                }

                CountryDTO countryDTO = new CountryDTO
                {
                    CountryID = country.CountryID,
                    CountryName = country.CountryName
                };

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "تم جلب البيانات بنجاح",
                    Data = countryDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO
                {
                    Success = false,
                    Message = $"خطأ في الخادم: {ex.Message}",
                    Data = null
                });
            }
        }

        // GET: api/country/name/{countryName}
        [HttpGet("name/{countryName}")]
        public ActionResult<ResponseDTO> GetCountryByName(string countryName)
        {
            try
            {
                if (string.IsNullOrEmpty(countryName))
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "اسم الدولة مطلوب",
                        Data = null
                    });
                }

                var country = clsCountry.Find(countryName);

                if (country == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "الدولة غير موجودة",
                        Data = null
                    });
                }

                CountryDTO countryDTO = new CountryDTO
                {
                    CountryID = country.CountryID,
                    CountryName = country.CountryName
                };

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "تم جلب البيانات بنجاح",
                    Data = countryDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO
                {
                    Success = false,
                    Message = $"خطأ في الخادم: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}