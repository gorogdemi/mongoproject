using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Components
{
    public class EditModel : PageModel
    {
        private readonly IRepository _repository;

        [BindProperty]
        public Component Component { get; set; }

        public EditModel(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGetAsync(string id, ComponentType? type)
        {
            if (id == null || type == null)
            {
                return NotFound();
            }

            Component = await _repository.FindComponentAsync(id, type.Value);

            return Component == null ? NotFound() : (IActionResult)Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var componentToUpdate = await _repository.FindComponentAsync(id, Component.Type);

            if (componentToUpdate == null)
            {
                return NotFound();
            }

            var result = Component.Type switch
            {
                ComponentType.BLDCMotor => await TryUpdateModelAsync(componentToUpdate as BLDCMotor, "component", c => c.Name, c => c.RPMV, c => c.Shaftdiameter, c => c.Voltagerating),
                ComponentType.Bolt => await TryUpdateModelAsync(componentToUpdate as Bolt, "component", c => c.Name, c => c.Head, c => c.Length, c => c.Size),
                ComponentType.Microcontroller => await TryUpdateModelAsync(componentToUpdate as Microcontroller, "component", c => c.Name, c => c.Processor),
                ComponentType.Propeller => await TryUpdateModelAsync(componentToUpdate as Propeller, "component", c => c.Name, c => c.NoBlades),
                ComponentType.StepperMotor => await TryUpdateModelAsync(componentToUpdate as StepperMotor, "component", c => c.Name, c => c.Currentrating, c => c.Holdingtorque, c => c.Shaftdiameter, c => c.Size, c => c.SPR, c => c.Voltagerating),
                _ => false
            };

            if (result)
            {
                await _repository.UpdateComponentAsync(componentToUpdate);
                return RedirectToPage("./Details", new { id = componentToUpdate.Id, type = componentToUpdate.Type });
            }

            return Page();
        }
    }
}