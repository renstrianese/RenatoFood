using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using RenatoFood.Data;
using RenatoFood.Model;

namespace RenatoFood.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toastNotification;

        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db, IToastNotification toastNotification)
        {
            _db = db;
            _toastNotification = toastNotification;
        }     
        
        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Display Order cannot exactly match the name");
            }

            if (ModelState.IsValid)
            {
                await _db.Categories.AddAsync(Category);
                await _db.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Category Created Successfuly");
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
