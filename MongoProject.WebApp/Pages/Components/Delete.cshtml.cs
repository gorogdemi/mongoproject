﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp.Pages.Components
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IRepository _repository;

        public Component Component { get; set; }

        public string ErrorMessage { get; set; }

        public DeleteModel(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGetAsync(string id, ComponentType? type, bool? saveChangesError = false)
        {
            if (id == null || type == null)
            {
                return NotFound();
            }

            Component = await _repository.FindComponentAsync(id, type.Value);

            if (Component == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, ComponentType? type)
        {
            if (id == null || type == null)
            {
                return NotFound();
            }

            var component = await _repository.FindComponentAsync(id, type.Value);

            if (component == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.DeleteComponentAsync(component);
                return RedirectToPage("./Index");
            }
            catch
            {
                return RedirectToAction("./Delete", new { id, type, saveChangesError = true });
            }
        }
    }
}