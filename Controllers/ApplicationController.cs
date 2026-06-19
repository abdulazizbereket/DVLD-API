using Microsoft.AspNetCore.Mvc;
using DVDLBusinussLayer;
using DVLD_API.DTOs;
using System;

namespace DVLD_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        // GET: api/application/5
        [HttpGet("{id}")]
        public ActionResult<ResponseDTO> GetApplicationByID(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف التطبيق غير صحيح",
                        Data = null
                    });
                }

                var application = clsApplication.GetApplicationByApplicationID(id);

                if (application == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "التطبيق غير موجود",
                        Data = null
                    });
                }

                ApplicationDTO applicationDTO = new ApplicationDTO
                {
                    ApplicationID = application.ApplicationID,
                    PersonID = application.PersonID,
                    ApplicationDate = application.ApplicationDate,
                    ApplicationTypeID = (int)application.ApplicationType,
                    ApplicationStatus = (int)application.ApplicationStatus,
                    LastStatusDate = application.LastStatusDate,
                    PaidFees = application.PaidFees,
                    UserID = application.UserID
                };

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "تم جلب بيانات التطبيق بنجاح",
                    Data = applicationDTO
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

        // POST: api/application
        [HttpPost]
        public ActionResult<ResponseDTO> AddApplication([FromBody] ApplicationDTO applicationDTO)
        {
            try
            {
                if (applicationDTO == null)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "البيانات مطلوبة",
                        Data = null
                    });
                }

                if (applicationDTO.PersonID <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف الشخص مطلوب وصحيح",
                        Data = null
                    });
                }

                if (applicationDTO.UserID <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف المستخدم مطلوب",
                        Data = null
                    });
                }

                clsApplication application = new clsApplication((clsApplication.enApplicationType)applicationDTO.ApplicationTypeID)
                {
                    PersonID = applicationDTO.PersonID,
                    ApplicationDate = applicationDTO.ApplicationDate,
                    ApplicationStatus = clsApplication.enStatus.New,
                    LastStatusDate = DateTime.Now,
                    UserID = applicationDTO.UserID
                };

                if (application.Save())
                {
                    ApplicationDTO responseDTO = new ApplicationDTO
                    {
                        ApplicationID = application.ApplicationID,
                        PersonID = application.PersonID,
                        ApplicationDate = application.ApplicationDate,
                        ApplicationTypeID = (int)application.ApplicationType,
                        ApplicationStatus = (int)application.ApplicationStatus,
                        LastStatusDate = application.LastStatusDate,
                        PaidFees = application.PaidFees,
                        UserID = application.UserID
                    };

                    return CreatedAtAction(nameof(GetApplicationByID), new { id = application.ApplicationID },
                        new ResponseDTO
                        {
                            Success = true,
                            Message = "تمت إضافة التطبيق بنجاح",
                            Data = responseDTO
                        });
                }
                else
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "فشل في إضافة التطبيق",
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

        // PUT: api/application/5
        [HttpPut("{id}")]
        public ActionResult<ResponseDTO> UpdateApplication(int id, [FromBody] ApplicationDTO applicationDTO)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف التطبيق غير صحيح",
                        Data = null
                    });
                }

                if (applicationDTO == null)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "البيانات مطلوبة",
                        Data = null
                    });
                }

                var application = clsApplication.GetApplicationByApplicationID(id);

                if (application == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "التطبيق غير موجود",
                        Data = null
                    });
                }

                application.PersonID = applicationDTO.PersonID;
                application.ApplicationDate = applicationDTO.ApplicationDate;
                application.ApplicationStatus = (clsApplication.enStatus)applicationDTO.ApplicationStatus;
                application.LastStatusDate = applicationDTO.LastStatusDate;
                application.UserID = applicationDTO.UserID;

                if (application.Save())
                {
                    ApplicationDTO responseDTO = new ApplicationDTO
                    {
                        ApplicationID = application.ApplicationID,
                        PersonID = application.PersonID,
                        ApplicationDate = application.ApplicationDate,
                        ApplicationTypeID = (int)application.ApplicationType,
                        ApplicationStatus = (int)application.ApplicationStatus,
                        LastStatusDate = application.LastStatusDate,
                        PaidFees = application.PaidFees,
                        UserID = application.UserID
                    };

                    return Ok(new ResponseDTO
                    {
                        Success = true,
                        Message = "تم تحديث بيانات التطبيق بنجاح",
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

        // DELETE: api/application/5
        [HttpDelete("{id}")]
        public ActionResult<ResponseDTO> DeleteApplication(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف التطبيق غير صحيح",
                        Data = null
                    });
                }

                var application = clsApplication.GetApplicationByApplicationID(id);

                if (application == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "التطبيق غير موجود",
                        Data = null
                    });
                }

                if (application.DeleteApplication(id))
                {
                    return Ok(new ResponseDTO
                    {
                        Success = true,
                        Message = "تم حذف التطبيق بنجاح",
                        Data = null
                    });
                }
                else
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "فشل في حذف التطبيق",
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