using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Components
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository _repository;

        public Component Component { get; set; }

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

            Component = await _repository.FindComponentAsync(id);

            if (Component == null)
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

            var component = await _repository.FindComponentAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.DeleteComponentAsync(component);
                return RedirectToPage("./Index");
            }
            catch
            {
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }
    }
}