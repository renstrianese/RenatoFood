using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using RenatoFood.Data;
using RenatoFood.Model;
using System.Dynamic;

namespace RenatoFood.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toastNotification;

        [BindProperty]
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db, IToastNotification toastNotification)
        {
            _db = db;
            _toastNotification = toastNotification;
        }
        
        public void OnGet(int id)
        {
            Category = _db.Categories.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Display Order cannot exactly match the name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                await _db.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Category Edited Successfuly");
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
