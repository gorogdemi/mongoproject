using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Kits
{
    public class EditModel : PageModel
    {
        private readonly IRepository _repository;

        [BindProperty]
        public Kit Kit { get; set; }

        public EditModel(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Kit = await _repository.FindKitAsync(id);

            return Kit == null ? NotFound() : (IActionResult)Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var kitToUpdate = await _repository.FindKitAsync(id);

            if (kitToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(kitToUpdate, "kit", c => c.Id, c => c.Name, c => c.Price, c => c.Components))
            {
                await _repository.UpdateKitAsync(kitToUpdate);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}