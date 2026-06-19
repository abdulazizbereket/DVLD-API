using Microsoft.AspNetCore.Mvc;
using DVDLBusinussLayer;
using DVLD_API.DTOs;
using System;

namespace DVLD_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<ResponseDTO> GetUserByID(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف المستخدم غير صحيح",
                        Data = null
                    });
                }

                var user = clsUser.FindByUserID(id);

                if (user == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "المستخدم غير موجود",
                        Data = null
                    });
                }

                UserDTO userDTO = new UserDTO
                {
                    UserID = user.UserID,
                    PersonID = user.PersonID,
                    UserName = user.UserName,
                    IsActive = user.IsActive
                };

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "تم جلب بيانات المستخدم بنجاح",
                    Data = userDTO
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

        // GET: api/user/username/{username}
        [HttpGet("username/{username}")]
        public ActionResult<ResponseDTO> GetUserByUserName(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "اسم المستخدم مطلوب",
                        Data = null
                    });
                }

                var user = clsUser.FindByUserName(username);

                if (user == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "المستخدم غير موجود",
                        Data = null
                    });
                }

                UserDTO userDTO = new UserDTO
                {
                    UserID = user.UserID,
                    PersonID = user.PersonID,
                    UserName = user.UserName,
                    IsActive = user.IsActive
                };

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "تم جلب بيانات المستخدم بنجاح",
                    Data = userDTO
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

        // POST: api/user
        [HttpPost]
        public ActionResult<ResponseDTO> AddUser([FromBody] UserDTO userDTO)
        {
            try
            {
                if (userDTO == null)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "البيانات مطلوبة",
                        Data = null
                    });
                }

                if (string.IsNullOrEmpty(userDTO.UserName) || string.IsNullOrEmpty(userDTO.Password))
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "اسم المستخدم وكلمة المرور مطلوبان",
                        Data = null
                    });
                }

                if (userDTO.PersonID <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف الشخص مطلوب",
                        Data = null
                    });
                }

                clsUser user = new clsUser
                {
                    PersonID = userDTO.PersonID,
                    UserName = userDTO.UserName,
                    Password = userDTO.Password,
                    IsActive = userDTO.IsActive
                };

                if (user.Save())
                {
                    UserDTO responseDTO = new UserDTO
                    {
                        UserID = user.UserID,
                        PersonID = user.PersonID,
                        UserName = user.UserName,
                        IsActive = user.IsActive
                    };

                    return CreatedAtAction(nameof(GetUserByID), new { id = user.UserID },
                        new ResponseDTO
                        {
                            Success = true,
                            Message = "تمت إضافة المستخدم بنجاح",
                            Data = responseDTO
                        });
                }
                else
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "فشل في إضافة المستخدم",
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

        // PUT: api/user/5
        [HttpPut("{id}")]
        public ActionResult<ResponseDTO> UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف المستخدم غير صحيح",
                        Data = null
                    });
                }

                if (userDTO == null)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "البيانات مطلوبة",
                        Data = null
                    });
                }

                var user = clsUser.FindByUserID(id);

                if (user == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "المستخدم غير موجود",
                        Data = null
                    });
                }

                user.UserName = userDTO.UserName;
                user.Password = userDTO.Password;
                user.IsActive = userDTO.IsActive;
                user.PersonID = userDTO.PersonID;

                if (user.Save())
                {
                    UserDTO responseDTO = new UserDTO
                    {
                        UserID = user.UserID,
                        PersonID = user.PersonID,
                        UserName = user.UserName,
                        IsActive = user.IsActive
                    };

                    return Ok(new ResponseDTO
                    {
                        Success = true,
                        Message = "تم تحديث بيانات المستخدم بنجاح",
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

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public ActionResult<ResponseDTO> DeleteUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "معرف المستخدم غير صحيح",
                        Data = null
                    });
                }

                var user = clsUser.FindByUserID(id);

                if (user == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "المستخدم غير موجود",
                        Data = null
                    });
                }

                if (clsUser.DeleteUser(id))
                {
                    return Ok(new ResponseDTO
                    {
                        Success = true,
                        Message = "تم حذف المستخدم بنجاح",
                        Data = null
                    });
                }
                else
                {
                    return BadRequest(new ResponseDTO
                    {
                        Success = false,
                        Message = "فشل في حذف المستخدم",
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