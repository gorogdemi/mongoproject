using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;

namespace MongoProject.WebApp.Pages.Components
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public List<Component> Components { get; private set; }

        public IndexModel(IRepository repository)
        {
            _repository = repository;
            Components = new List<Component>();
        }

        public async Task OnGet()
        {
            Components = await _repository.GetAllComponentsAsync();
        }
    }
}
