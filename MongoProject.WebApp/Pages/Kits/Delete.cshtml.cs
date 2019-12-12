using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Kits
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IRepository _repository;

        public Kit Kit { get; set; }

        public string ErrorMessage { get; set; }

        public DeleteModel(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGetAsync(string id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Kit = await _repository.FindKitAsync(id);

            if (Kit == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kit = await _repository.FindKitAsync(id);

            if (kit == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.DeleteKitAsync(kit);
                return RedirectToPage("./Index");
            }
            catch
            {
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }
    }
}