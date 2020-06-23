using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Ext.Net;
using Ext.Net.Core;

namespace Company.WebApplication1.Pages
{
    [DirectModel]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        [Direct]
        public IActionResult OnPostButtonClick()
        {
            this.X().Toast("Button Clicked!");

            return this.Direct();
        }
    }
}