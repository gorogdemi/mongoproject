using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Kits
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly IRepository _repository;

        [BindProperty]
        public Kit Kit { get; set; }

        public AddModel(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var kit = new Kit();

            if (await TryUpdateModelAsync(kit, "kit", c => c.Id, c => c.Name, c => c.Price, c => c.Components))
            {
                await _repository.AddKitAsync(kit);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}