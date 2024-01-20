using AutoMapper;
using FDBlog.Entity.Dtos.Articles;
using FDBlog.Entity.Dtos.Categories;
using FDBlog.Entity.Entities;
using FDBlog.Service.Extensions;
using FDBlog.Service.Services.Abstractions;
using FDBlog.Service.Services.Concrete;
using FDBlog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace FDBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<Category> _validator;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;

        public CategoryController(ICategoryService categoryService, IValidator<Category> validator, IMapper mapper, IToastNotification toast)
        {
            _categoryService = categoryService;
            _validator = validator;
            _mapper = mapper;
            _toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> DeletedCategory()
        {
            var categories = await _categoryService.GetAllCategoriesDeleted();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            var map = _mapper.Map<Category>(categoryAddDto);
            var result = await _validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await _categoryService.CreateCategoryAsync(categoryAddDto);
                _toast.AddSuccessToastMessage(Message.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = "Helal sana :)" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }

            result.AddToModelState(this.ModelState);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddWithAjax([FromBody] CategoryAddDto categoryAddDto)
        {
            var map = _mapper.Map<Category>(categoryAddDto);
            var result = await _validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await _categoryService.CreateCategoryAsync(categoryAddDto);
                _toast.AddSuccessToastMessage(Message.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = "Helal sana :)" });
                return Json(Message.Category.Add(categoryAddDto.Name));
            }
            else
            {
                _toast.AddErrorToastMessage(result.Errors.First().ErrorMessage, new ToastrOptions { Title = "Olamaz olamaz kesinlikle olamaz..." });
                return Json (result.Errors.First().ErrorMessage);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int categoryId)
        {
            var category=await _categoryService.GetCategoryById(categoryId);
            var map=_mapper.Map<Category,CategoryUpdateDto>(category);
            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var map = _mapper.Map<Category>(categoryUpdateDto);
            var result = await _validator.ValidateAsync(map);

            if(result.IsValid)
            {
                var name = await _categoryService.UpdateCategoryAsync(categoryUpdateDto);
                _toast.AddSuccessToastMessage(Message.Category.Update(name), new ToastrOptions { Title = "Helal sana :)" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }
            result.AddToModelState(this.ModelState);
            return View();
        }
        public async Task<IActionResult> Delete(int categoryId)
        {
            var name = await _categoryService.SafeDeleteCategoryAsync(categoryId);
            _toast.AddWarningToastMessage(Message.Category.Delete(name), new ToastrOptions { Title = "İşlem Başarılı." });

            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }  
        public async Task<IActionResult> UndoDelete(int categoryId)
        {
            var name = await _categoryService.UndoDeleteCategoryAsync(categoryId);
            _toast.AddWarningToastMessage(Message.Category.UndoDelete(name), new ToastrOptions { Title = "İşlem Başarılı." });

            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

    }
}
