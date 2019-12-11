using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Components
{
    public class AddModel : PageModel
    {
        private readonly IRepository _repository;

        public Component Component { get; set; }

        public AddModel(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyComponent = new Component();

            if (await TryUpdateModelAsync(
                emptyComponent,
                "component",
                c => c.Id, c => c.Name, c => c.Type))
            {
                await _repository.AddComponentAsync(emptyComponent);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}