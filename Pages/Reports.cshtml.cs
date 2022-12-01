using LibraryApp.Models;
using LibraryDbApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryDbApp.Pages
{
    public class ReportsModel : PageModel
    {
        public IList<StaffModel> Staff { get; set; } = default!;
        public IList<CustomerModel> Customers { get; set; } = default!;

        public void OnGet()
        {
            Staff = DbService.GetStaffReport();
            Customers = DbService.GetCustomerReport();
        }
    }
}
