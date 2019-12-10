using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;

namespace MongoProject.WebApp.Pages.Kits
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository _repository;

        public DetailsModel(IRepository repository)
        {
            _repository = repository;
        }
    }
}