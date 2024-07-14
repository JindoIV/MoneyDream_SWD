using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.IRepository
{
 

    public interface IMessageRepository
    {
        IEnumerable<Message> GetConversation(int conversationID);
        IEnumerable<ConversationUser> GetConversationIdsByUserID(int userID);
        List<object> GetConversationByUserID(int userID);
        int CretateConversation(Conversation conversation, int userID1, int userID2);
        int CretateConversation(Conversation conversation, int userID1);
        int AddAdminToConversation( int AdminID,int ConversationID);

        bool DeleteConversation(int conversationId);

        void CreateMessage(Message message);

        List<object> GetPendingConversations();
    }
}
