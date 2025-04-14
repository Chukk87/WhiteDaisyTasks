using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhiteDaisyLibrary.Services.Interfaces;

namespace WhiteDaisyTasksUi.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? NameInputPath { get; set; }

        [BindProperty]
        public string? NameOutputPath { get; set; }

        [BindProperty]
        public string? TabInputPath { get; set; }

        [BindProperty]
        public string? TabOutputPath { get; set; }

        [BindProperty]
        public string? StatusMessage { get; set; }

        private readonly INameBuilder _nameBuilder;
        private readonly ITabToXmlConverter _tabToXmlConverter;

        public IndexModel(INameBuilder nameBuilder, ITabToXmlConverter tabToXmlConverter)
        {
            _nameBuilder = nameBuilder;
            _tabToXmlConverter = tabToXmlConverter;
        }

        public async Task<IActionResult> OnPostConvertNamesAsync()
        {
            if (!string.IsNullOrWhiteSpace(NameInputPath) && !string.IsNullOrWhiteSpace(NameOutputPath))
            {
                await _nameBuilder.BuildNameAsync(NameInputPath, NameOutputPath);
                StatusMessage = "Name conversion completed.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostConvertTabsAsync()
        {
            if (!string.IsNullOrWhiteSpace(TabInputPath) && !string.IsNullOrWhiteSpace(TabOutputPath))
            {
                await _tabToXmlConverter.ConvertTabToXmlAsync(TabInputPath, TabOutputPath);
                StatusMessage = "Tab to XML conversion completed.";
            }

            return Page();
        }
    }
}