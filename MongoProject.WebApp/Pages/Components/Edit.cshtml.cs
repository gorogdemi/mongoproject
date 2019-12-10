using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;

namespace MongoProject.WebApp.Pages.Components
{
    public class EditModel : PageModel
    {
        private readonly IRepository _repository;

        public Component Component { get; set; }

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

            Component = await _repository.FindComponentAsync(id);

            return Component == null ? NotFound() : (IActionResult)Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var componentToUpdate = await _repository.FindComponentAsync(id);

            if (componentToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                componentToUpdate,
                "component",
                c => c.Name, c => c.Type, c => c.Size, c => c.Quantity))
            {
                await _repository.UpdateComponentAsync(componentToUpdate);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}