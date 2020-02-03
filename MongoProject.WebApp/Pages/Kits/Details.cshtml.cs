using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Kits
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IRepository _repository;

        public DetailsModel(IRepository repository)
        {
            _repository = repository;
        }

        public Kit Kit { get; set; }

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
            if (id == null)
            {
                return NotFound();
            }

            var kit = await _repository.FindKitAsync(id);

            if (kit == null)
            {
                return NotFound();
            }

            await _repository.CheckOutAsync(kit);

            return RedirectToPage("./Index");
        }
    }
}