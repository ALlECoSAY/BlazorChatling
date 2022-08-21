using BlazorChatling.Data.DBContextFiles;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BlazorChatling.Data
{
    public class ChatsDAOService
    {
        private readonly ChatlingDBContext _chatlingDBContext;
        public ChatsDAOService(ChatlingDBContext chatlingDBContext) {
            _chatlingDBContext = chatlingDBContext;
        }

        public async Task<List<Chats>> getChatsByUser(int userId) {

            var result = await _chatlingDBContext.Chats.FromSqlRaw(
                "SELECT Chats.id, Chats.name " +
                "FROM (Chats INNER JOIN Chat_User ON Chats.id = Chat_User.id_chat) " +
                "WHERE Chat_User.id_user= {0}", userId).AsNoTracking().ToListAsync();
            
            return result;
        }

        public async Task<bool> isUserAuthorizedInChat(int userId, int chatId) {
            var result = await _chatlingDBContext.ChatUser
                .Where(x => x.IdChat == chatId && x.IdUser == userId).AsNoTracking().ToListAsync();
            

            return result.Count > 0;
        }

        public async Task<List<Messages>> getMessages(int chatId, int userId, int shift, int number = 20) {


            var pChatId = new SqlParameter("@id_chat", System.Data.SqlDbType.Int);
            var pUserId = new SqlParameter("@id_user", System.Data.SqlDbType.Int);
            var pStart = new SqlParameter("@start", System.Data.SqlDbType.Int);
            var pEnd = new SqlParameter("@end", System.Data.SqlDbType.Int);

            pChatId.Value = chatId;
            pUserId.Value = userId;
            pStart.Value = shift * number+1;
            pEnd.Value = (shift+1)*number;

            var result = await _chatlingDBContext.Messages
                .FromSqlRaw(
            "SELECT id, id_chat, id_user, id_reply_message, msg_text, msg_time, is_edited, is_visible_to_creator, is_reply_visible_to_group FROM (SELECT Row_Number() OVER (ORDER BY id DESC) AS RowIndex, * from Messages WHERE id_chat = @id_chat AND (id_user <> @id_user OR is_visible_to_creator = 1) ) as Sub WHERE Sub.RowIndex >= @start AND Sub.RowIndex <= @end"
            , pChatId, pUserId, pStart, pEnd).AsNoTracking().ToListAsync();


            return  result;
        }

        public void insertMessage(Messages message) {
            _chatlingDBContext.Messages.Add(message);
            _chatlingDBContext.SaveChanges();
        }

        public void deleteMessage(Messages message) {
            _chatlingDBContext.Messages.Remove(message);
            _chatlingDBContext.SaveChanges();
        }

        public void editMessage(Messages message) {
            _chatlingDBContext.Messages.Update(message);
            _chatlingDBContext.SaveChanges();
        }



        public async Task<Users> getUser(int id) {


            var result = await _chatlingDBContext.Users.Where(x => x.Id == id).AsNoTracking().ToListAsync();
                    
            return (result)[0];

        }

        public async Task<List<Users>> getUsersInChat(int chat_id) {
            var result = await _chatlingDBContext.Users.FromSqlRaw(
                "SELECT Users.id, Users.name, Users.tag " +
                "FROM (Users INNER JOIN Chat_User ON Users.id = Chat_User.id_user) " +
                "WHERE Chat_User.id_chat= {0}", chat_id).AsNoTracking().ToListAsync();

            return result;

        }



    }
}
