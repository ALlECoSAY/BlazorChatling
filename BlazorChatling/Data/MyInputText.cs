using System.ComponentModel.DataAnnotations;

namespace BlazorChatling.Data
{
    public class MyInputText
    {
        [Required]
        [StringLength(200, ErrorMessage = "message is too long.", MinimumLength = 1 )]
        public string text;

    }
}
