using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{

 

    public class MessageRepository : IMessageRepository
    {
        public int AddAdminToConversation(int AdminID, int ConversationID) => MessageDAO.Instance.AddAdminToConversation(AdminID, ConversationID);
        public void CreateMessage(Message message) => MessageDAO.Instance.CreateMessage(message);
        public int CretateConversation(Conversation conversation, int userID1, int userID2) =>
            MessageDAO.Instance.CreateNewConversation(conversation, userID1, userID2);
        public int CretateConversation(Conversation conversation, int userID1) =>
        MessageDAO.Instance.CreateNewConversation(conversation, userID1);
        public bool DeleteConversation(int conversationId) => MessageDAO.Instance.DeleteConversation(conversationId);

        public IEnumerable<Message> GetConversation(int conversationID) => MessageDAO.Instance.GetConversation(conversationID);


        IEnumerable<ConversationUser> IMessageRepository.GetConversationIdsByUserID(int userID) => MessageDAO.Instance.GetConversationIdsByUserID(userID);

        List<object> IMessageRepository.GetConversationByUserID(int userID) 
            => MessageDAO.Instance.GetConversationsWithUserInfo(userID);

        List<object> IMessageRepository.GetPendingConversations()
          => MessageDAO.Instance.GetPendingConversations();

    }
}
