using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using System.Threading.Tasks;

namespace MongoProject.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public int ComponentPieceCount { get; set; }

        public int ComponentCount { get; set; }

        public int LowQuantityComponentCount { get; set; }

        public int KitCount { get; set; }

        public int LowKitCount { get; set; }

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public async Task OnGetAsync()
        {
            ComponentPieceCount = await _repository.GetComponentPieceCountAsync();
            ComponentCount = await _repository.GetComponentCountAsync();
            LowQuantityComponentCount = await _repository.GetLowQuantityComponentCountAsync();
            KitCount = await _repository.GetKitCountAsync();
            LowKitCount = await _repository.GetNotEnoughForKitCountAsync();
        }
    }
}
