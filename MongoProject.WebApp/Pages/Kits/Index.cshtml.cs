using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;
using MongoProject.WebApp.Utilities;

namespace MongoProject.WebApp.Pages.Kits
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public string NameSort { get; set; }

        public string PriceSort { get; set; }

        public string QuantitySort { get; set; }

        public string CurrentFilter { get; set; }

        public string CurrentSort { get; set; }

        public PaginatedList<Kit> Kits { get; private set; }

        public IndexModel(IRepository repository)
        {
            _repository = repository;
            Kits = new PaginatedList<Kit>();
        }

        public async Task OnGet(string currentFilter, string sortOrder, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = string.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            PriceSort = sortOrder == "Date" ? "PriceDesc" : "Price";
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
            Kits = await _repository.GetAllKitsPaginatedAsync(CurrentFilter, CurrentSort, pageIndex ?? 1, pageSize);
        }
    }
}