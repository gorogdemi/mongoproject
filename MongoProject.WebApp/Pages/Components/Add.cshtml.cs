using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Components
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly IRepository _repository;

        [BindProperty]
        public Component Component { get; set; }

        public ComponentType ComponentType { get; set; }

        public AddModel(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet(ComponentType? type)
        {
            if (type == null)
            {
                NotFound();
            }

            ComponentType = type.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyComponent = Component.Type switch
            {
                ComponentType.BLDCMotor => new BLDCMotor(),
                ComponentType.Bolt => new Bolt(),
                ComponentType.Microcontroller => new Microcontroller(),
                ComponentType.Propeller => new Propeller(),
                ComponentType.StepperMotor => new StepperMotor(),
                _ => new Component()
            };

            var result = Component.Type switch
            {
                ComponentType.BLDCMotor => await TryUpdateModelAsync(emptyComponent as BLDCMotor, "component", c => c.Id, c => c.Name, c => c.Type, c => c.RPMV, c => c.Shaftdiameter, c => c.Voltagerating, c => c.Quantity),
                ComponentType.Bolt => await TryUpdateModelAsync(emptyComponent as Bolt, "component", c => c.Id, c => c.Name, c => c.Type, c => c.Head, c => c.Length, c => c.Size, c => c.Quantity),
                ComponentType.Microcontroller => await TryUpdateModelAsync(emptyComponent as Microcontroller, "component", c => c.Id, c => c.Name, c => c.Type, c => c.Processor, c => c.Quantity),
                ComponentType.Propeller => await TryUpdateModelAsync(emptyComponent as Propeller, "component", c => c.Id, c => c.Name, c => c.Type, c => c.NoBlades, c => c.Quantity),
                ComponentType.StepperMotor => await TryUpdateModelAsync(emptyComponent as StepperMotor, "component", c => c.Id, c => c.Name, c => c.Type, c => c.Currentrating, c => c.Holdingtorque, c => c.Shaftdiameter, c => c.Size, c => c.SPR, c => c.Voltagerating, c => c.Quantity),
                _ => false
            };

            if (result)
            {
                await _repository.AddComponentAsync(emptyComponent);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}