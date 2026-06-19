using Microsoft.AspNetCore.Mvc;
using DVDLBusinussLayer;
using DVLD_API.DTOs;

namespace DVLD_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        // GET: api/person
        [HttpGet]
        public ActionResult<ResponseDTO> GetAllPersons()
        {
            try
            {
                var persons = clsPerson.GetAllPersons();

                if (persons == null || persons.Count == 0)
                {
                    return Ok(new ResponseDTO
                    {
                        Success = false,
                        Message = "لا توجد بيانات",
                        Data = new List<PersonDTO>()
                    });
                }

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "تم جلب البيانات بنجاح",
                    Data = persons
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

        // GET: api/person/5
        [HttpGet("{id}")]
        public ActionResult<ResponseDTO> GetPersonByID(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف الشخص غير صحيح",
                        Data = null
                    });
                }

                var person = clsPerson.FindpID(id);

                if (person == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "الشخص غير موجود",
                        Data = null
                    });
                }

                PersonDTO personDTO = new PersonDTO
                {
                    PersonID = person.PersonID,
                    FirstName = person.FirstName,
                    SecondName = person.SecondName,
                    ThirdName = person.ThirdName,
                    LastName = person.LastName,
                    NationalNo = person.NationalNo,
                    DateOfBirth = person.DateOfBirth,
                    Gendor = person.Gendor,
                    Email = person.Email,
                    Phone = person.Phone,
                    Address = person.Address,
                    NationalityCountryID = person.NationalityCountryID,
                    CountryName = person.CountryInfo?.CountryName ?? ""
                };

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "تم جلب البيانات بنجاح",
                    Data = personDTO
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

        // GET: api/person/national/{nationalNo}
        [HttpGet("national/{nationalNo}")]
        public ActionResult<ResponseDTO> GetPersonByNationalNo(string nationalNo)
        {
            try
            {
                if (string.IsNullOrEmpty(nationalNo))
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "رقم الهوية مطلوب",
                        Data = null
                    });
                }

                var person = clsPerson.Find(nationalNo);

                if (person == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "الشخص غير موجود برقم الهوية هذا",
                        Data = null
                    });
                }

                PersonDTO personDTO = new PersonDTO
                {
                    PersonID = person.PersonID,
                    FirstName = person.FirstName,
                    SecondName = person.SecondName,
                    ThirdName = person.ThirdName,
                    LastName = person.LastName,
                    NationalNo = person.NationalNo,
                    DateOfBirth = person.DateOfBirth,
                    Gendor = person.Gendor,
                    Email = person.Email,
                    Phone = person.Phone,
                    Address = person.Address,
                    NationalityCountryID = person.NationalityCountryID,
                    CountryName = person.CountryInfo?.CountryName ?? ""
                };

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "تم جلب البيانات بنجاح",
                    Data = personDTO
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

        // POST: api/person
        [HttpPost]
        public ActionResult<ResponseDTO> AddPerson([FromBody] PersonDTO personDTO)
        {
            try
            {
                if (personDTO == null)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "البيانات مطلوبة",
                        Data = null
                    });
                }

                if (string.IsNullOrEmpty(personDTO.FirstName) || string.IsNullOrEmpty(personDTO.LastName))
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "الاسم الأول والأخير مطلوبان",
                        Data = null
                    });
                }

                if (string.IsNullOrEmpty(personDTO.NationalNo))
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "رقم الهوية مطلوب",
                        Data = null
                    });
                }

                if (clsPerson.IsPersonExist(personDTO.NationalNo))
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "هذا الشخص موجود بالفعل برقم الهوية هذا",
                        Data = null
                    });
                }

                clsPerson person = new clsPerson
                {
                    FirstName = personDTO.FirstName,
                    SecondName = personDTO.SecondName,
                    ThirdName = personDTO.ThirdName,
                    LastName = personDTO.LastName,
                    NationalNo = personDTO.NationalNo,
                    DateOfBirth = personDTO.DateOfBirth,
                    Gendor = personDTO.Gendor,
                    Email = personDTO.Email,
                    Phone = personDTO.Phone,
                    Address = personDTO.Address,
                    NationalityCountryID = personDTO.NationalityCountryID
                };

                if (person.Save())
                {
                    PersonDTO responseDTO = new PersonDTO
                    {
                        PersonID = person.PersonID,
                        FirstName = person.FirstName,
                        SecondName = person.SecondName,
                        ThirdName = person.ThirdName,
                        LastName = person.LastName,
                        NationalNo = person.NationalNo,
                        DateOfBirth = person.DateOfBirth,
                        Gendor = person.Gendor,
                        Email = person.Email,
                        Phone = person.Phone,
                        Address = person.Address,
                        NationalityCountryID = person.NationalityCountryID
                    };

                    return CreatedAtAction(nameof(GetPersonByID), new { id = person.PersonID }, 
                        new ResponseDTO
                        {
                            Success = true,
                            Message = "تمت إضافة الشخص بنجاح",
                            Data = responseDTO
                        });
                }
                else
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "فشل في إضافة الشخص",
                        Data = null
                    });
                }
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

        // PUT: api/person/5
        [HttpPut("{id}")]
        public ActionResult<ResponseDTO> UpdatePerson(int id, [FromBody] PersonDTO personDTO)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف الشخص غير صحيح",
                        Data = null
                    });
                }

                if (personDTO == null)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "البيانات مطلوبة",
                        Data = null
                    });
                }

                var person = clsPerson.FindpID(id);

                if (person == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "الشخص غير موجود",
                        Data = null
                    });
                }

                person.FirstName = personDTO.FirstName;
                person.SecondName = personDTO.SecondName;
                person.ThirdName = personDTO.ThirdName;
                person.LastName = personDTO.LastName;
                person.NationalNo = personDTO.NationalNo;
                person.DateOfBirth = personDTO.DateOfBirth;
                person.Gendor = personDTO.Gendor;
                person.Email = personDTO.Email;
                person.Phone = personDTO.Phone;
                person.Address = personDTO.Address;
                person.NationalityCountryID = personDTO.NationalityCountryID;

                if (person.Save())
                {
                    PersonDTO responseDTO = new PersonDTO
                    {
                        PersonID = person.PersonID,
                        FirstName = person.FirstName,
                        SecondName = person.SecondName,
                        ThirdName = person.ThirdName,
                        LastName = person.LastName,
                        NationalNo = person.NationalNo,
                        DateOfBirth = person.DateOfBirth,
                        Gendor = person.Gendor,
                        Email = person.Email,
                        Phone = person.Phone,
                        Address = person.Address,
                        NationalityCountryID = person.NationalityCountryID
                    };

                    return Ok(new ResponseDTO
                    {
                        Success = true,
                        Message = "تم تحديث بيانات الشخص بنجاح",
                        Data = responseDTO
                    });
                }
                else
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "فشل في تحديث البيانات",
                        Data = null
                    });
                }
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

        // DELETE: api/person/5
        [HttpDelete("{id}")]
        public ActionResult<ResponseDTO> DeletePerson(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف الشخص غير صحيح",
                        Data = null
                    });
                }

                var person = clsPerson.FindpID(id);

                if (person == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "الشخص غير موجود",
                        Data = null
                    });
                }

                if (clsPerson.Deleteperson(id))
                {
                    return Ok(new ResponseDTO
                    {
                        Success = true,
                        Message = "تم حذف الشخص بنجاح",
                        Data = null
                    });
                }
                else
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "فشل في حذف الشخص",
                        Data = null
                    });
                }
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