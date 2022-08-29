
using System.ComponentModel.DataAnnotations;

namespace BlazorChatling.Data
{
    //model for logininput
    public partial class User
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Source { get; set; }
        public int PubId { get; set; }

        public string ConfirmPassword { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

}
