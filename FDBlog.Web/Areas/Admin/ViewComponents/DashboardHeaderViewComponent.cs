﻿using AutoMapper;
using FDBlog.Entity.Dtos.Users;
using FDBlog.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FDBlog.Web.Areas.Admin.ViewComponents
{
    public class DashboardHeaderViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public DashboardHeaderViewComponent(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var map = _mapper.Map<UserDto>(loggedInUser);

            var role = string.Join("", await _userManager.GetRolesAsync(loggedInUser));
            map.Role = role;
            return View(map);
        }
    }
}
