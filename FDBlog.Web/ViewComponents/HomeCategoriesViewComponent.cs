﻿using FDBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FDBlog.Web.ViewComponents
{
    public class HomeCategoriesViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public HomeCategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories= await _categoryService.GetAllCategoriesNonDeletedTake24();
            return View(categories);
        }
    }
}
