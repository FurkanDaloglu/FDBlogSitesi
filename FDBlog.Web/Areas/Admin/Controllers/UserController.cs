using AutoMapper;
using FDBlog.Dal.UnitOfWorks;
using FDBlog.Entity.Dtos.Articles;
using FDBlog.Entity.Dtos.Users;
using FDBlog.Entity.Entities;
using FDBlog.Entity.Enums;
using FDBlog.Service.Extensions;
using FDBlog.Service.Helpers.Images;
using FDBlog.Service.Services.Abstractions;
using FDBlog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using static FDBlog.Web.ResultMessages.Message;

namespace FDBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        private readonly IValidator<AppUser> _validator;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IToastNotification toastNotification, IValidator<AppUser> validator, IUserService userService)
        {
            _mapper = mapper;
            _toast = toastNotification;
            _validator = validator;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetAllUsersWithRoleAsync();

            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await _userService.GetAllRoleAsync();
            return View(new UserAddDto { Roles = roles });
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            var map = _mapper.Map<AppUser>(userAddDto);
            var validation=await _validator.ValidateAsync(map);
            var roles = await _userService.GetAllRoleAsync();
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUserAsync(userAddDto);
                if(result.Succeeded)
                {
                    _toast.AddSuccessToastMessage(Message.User.Add(userAddDto.Email), new ToastrOptions { Title = "Birini daha gandırdık ve üye yaptık :)" });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });
                }
                else
                {
                    result.AddToIdentityModelState(this.ModelState);
                    validation.AddToModelState(this.ModelState);
                    return View(new UserAddDto { Roles = roles });
                }
            }
            return View(new UserAddDto { Roles = roles});
        }
        [HttpGet]
        public async Task<IActionResult> Update(int userId)
        {
            var user = await _userService.GetAppUserByIdAsync(userId);
            var roles = await _userService.GetAllRoleAsync();
            var map=_mapper.Map<UserUpdateDto>(user);
            map.Roles=roles;
            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var user = await _userService.GetAppUserByIdAsync(userUpdateDto.Id);


            if (user!=null)
            {
                
                var roles = await _userService.GetAllRoleAsync();
                if (ModelState.IsValid)
                {
                    var map = _mapper.Map(userUpdateDto, user);
                    var validation = await _validator.ValidateAsync(map);
                    if(validation.IsValid)
                    {
                        user.UserName = userUpdateDto.Email;
                        user.SecurityStamp = Guid.NewGuid().ToString();
                        var result = await _userService.UpdateUserAsync(userUpdateDto);
                        if (result.Succeeded)
                        {
                            _toast.AddSuccessToastMessage(Message.User.Update(userUpdateDto.Email), new ToastrOptions { Title = "İşlem Başarılı :)" });
                            return RedirectToAction("Index", "User", new { Area = "Admin" });
                        }
                        else
                        {
                            result.AddToIdentityModelState(this.ModelState);
                            return View(new UserUpdateDto { Roles = roles });
                        }
                    }
                    else
                    {
                        validation.AddToModelState(this.ModelState);
                        return View(new UserUpdateDto { Roles = roles });

                    }
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Delete(int userId)
        {
            var result = await _userService.DeleteUserAsync(userId);

            if(result.identityResult.Succeeded)
            {
                _toast.AddSuccessToastMessage(Message.User.Delete(result.email), new ToastrOptions { Title = "Zaten İşe Yaramıyodu İyi oldu :)" });
                return RedirectToAction("Index", "User", new { Area = "Admin" });
            }
            else
            {
                result.identityResult.AddToIdentityModelState(this.ModelState);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var profile=await _userService.GetUserProfileAsync();
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
        { 
            if (ModelState.IsValid)
            {
                var result = await _userService.UserProfileUpdateAsync(userProfileDto);
                if(result)
                {
                    _toast.AddSuccessToastMessage("Profil Güncelleme İşlemi Tamamlandı.", new ToastrOptions { Title = "İşlem Başarılı." });
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
                else
                {
                    _toast.AddErrorToastMessage("Profil Güncelleme İşlemi Tamamlanamadı.", new ToastrOptions { Title = "İşlem Başarısız." });
                    return View();
                }
            } 
            return View();
        }
    }
}
