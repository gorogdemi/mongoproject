using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;
using MongoProject.WebApp.Utilities;

namespace MongoProject.WebApp.Pages.Components
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public string NameSort { get; set; }

        public string TypeSort { get; set; }

        public string QuantitySort { get; set; }

        public string CurrentFilter { get; set; }

        public string CurrentSort { get; set; }

        public PaginatedList<Component> Components { get; private set; }

        public IndexModel(IRepository repository)
        {
            _repository = repository;
            Components = new PaginatedList<Component>();
        }

        public async Task OnGet(string currentFilter, string sortOrder, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = string.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            TypeSort = sortOrder == "Date" ? "TypeDesc" : "Type";
            QuantitySort = sortOrder == "Quantity" ? "QuantityDesc" : "Quantity";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            int pageSize = 5;
            Components = await _repository.GetAllComponentsPaginatedAsync(CurrentFilter, CurrentSort, pageIndex ?? 1, pageSize);
        }
    }
}
