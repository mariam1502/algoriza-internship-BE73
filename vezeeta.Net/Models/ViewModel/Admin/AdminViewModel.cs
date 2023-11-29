using System.ComponentModel.DataAnnotations;

namespace vezeeta.Net.Models.ViewModel.Admin
{
    public class AdminViewModel
    {
       public string Email { get; set; }

       [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string specialization { get; set; }
        public string Gendre { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }

    }
}
