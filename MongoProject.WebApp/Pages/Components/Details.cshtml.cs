﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Components
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IRepository _repository;

        public DetailsModel(IRepository repository)
        {
            _repository = repository;
        }

        public Component Component { get; set; }

        public async Task<IActionResult> OnGetAsync(string id, ComponentType? type)
        {
            if (id == null || type == null)
            {
                return NotFound();
            }

            Component = await _repository.FindComponentAsync(id, type.Value);

            return Component == null ? NotFound() : (IActionResult)Page();
        }
    }
}