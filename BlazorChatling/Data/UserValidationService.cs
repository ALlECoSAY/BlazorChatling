using BlazorChatling.Data.DBContextFiles;
using Microsoft.EntityFrameworkCore;

namespace BlazorChatling.Data
{
    public class UserValidationService
    {

        private readonly ChatlingDBContext _chatlingDBContext;
        public UserValidationService(ChatlingDBContext chatlingDBContext) {
            _chatlingDBContext = chatlingDBContext;
        }

        public async Task<Users> isValid(string name, string password) {

            
            var sff = await _chatlingDBContext.Users
                .Where(x => x.Name == name).AsNoTracking().ToListAsync();
            
            return sff.Count > 0 ? sff[0]:null;
        }





    }
}
