using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Components
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository _repository;

        public DetailsModel(IRepository repository)
        {
            _repository = repository;
        }

        public Component Component { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Component = await _repository.FindComponentAsync(id);

            return Component == null ? NotFound() : (IActionResult)Page();
        }
    }
}