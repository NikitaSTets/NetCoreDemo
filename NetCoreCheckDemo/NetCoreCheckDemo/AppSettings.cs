using System.ComponentModel.DataAnnotations;

namespace NetCoreCheckDemo
{
    public class AppSettings
    {
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$", ErrorMessage = "Error!!!!")]
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}