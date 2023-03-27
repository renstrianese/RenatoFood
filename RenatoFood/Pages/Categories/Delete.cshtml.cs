using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using RenatoFood.Data;
using RenatoFood.Model;
using System.Dynamic;

namespace RenatoFood.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toastNotification;

        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db, IToastNotification toastNotification)
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

            var categoryfromDb = _db.Categories.Find(Category.Id);
            if (categoryfromDb != null)
            {
                _db.Categories.Remove(categoryfromDb);
                await _db.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Category Delete Successfuly");
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
