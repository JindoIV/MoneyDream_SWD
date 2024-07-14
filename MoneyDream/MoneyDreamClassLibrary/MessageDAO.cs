using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{

    public class ConversationResponse
    {
        public int conversationId { get; set; }
        public int userAccountId { get; set; }
        public string userName { get; set; }
    }

    public class MessageDAO
    {
        private static readonly object InstanceLock = new object();
        private static MessageDAO instance = null;

        public static MessageDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MessageDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Message> GetConversation(int conversationId)
        {
            List<Message> messages;

            try
            {

                var context = new MoneyDreamContext();
                messages = (from m in context.Messages
                            join c in context.Conversations on m.ConversationId equals c.ConversationId
                            where c.ConversationId == conversationId
                            select m).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return messages;
        }

        public IEnumerable<ConversationUser> GetConversationIdsByUserID(int userID)
        {
            var response = new List<ConversationUser>();
            try
            {

                var context = new MoneyDreamContext();
                var conversations = (from c in context.Conversations
                            join cu in context.ConversationUsers on c.ConversationId equals cu.ConversationId
                            join a in context.Accounts on cu.AccountId equals a.AccountId
                                     where cu.AccountId == userID
                            select cu).ToList();
                /**

                foreach (var conversation in conversations)
                {
                   var conversationResponse = new ConversationUser();
                    conversationResponse.conversationId = conversation.ConversationId;
                    conversationResponse.userAccountId = conversation.UserAccountId;
                    conversationResponse.userName= conversation.UserName;

                    response.Add(conversationResponse);
                }
                **/
                return conversations;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /**
        public (IEnumerable<object>,IEnumerable<Account>) GetConversationInfoByUserID(int userID)
        {
            var conversationResponse = new List<object>();
            var userResponse = new List<Account>();
            try
            {

                var context = new MoneyDreamContext();
                var query = (from c in context.Conversations
                                     join cu in context.ConversationUsers on c.ConversationId equals cu.ConversationId
                                     where cu.AccountId == userID
                                     select new
                                     {
                                         con = c,
                                         user = cu

                                     }).ToList();

                var people = (from a in context.Accounts
                              join cu in context.ConversationUsers
                              on a.AccountId equals cu.AccountId
                              select a).ToList();

                var conversations = new List<object>();
                foreach ( var q in query )
                {
                    if (q.user.AccountId == userID)
                    {
                        conversations.Add(new
                        {
                            q.con.ConversationId,
                            q.con.CreateAt,
                            q.con.Type,
                            q.user.AccountId
                        });
                    break;
                    }
                }
                
                return (conversations, people);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        **/

        public List<object> GetConversationsWithUserInfo(int userID)
        {
            try
            {
                var context = new MoneyDreamContext();

                var userCurrentConversations = (from c in context.Conversations
                                                join cu in context.ConversationUsers on c.ConversationId equals cu.ConversationId
                                                where cu.AccountId == userID
                                                select c.ConversationId

                                ).ToArray();
                var response = new List<object>();


                foreach ( var item in userCurrentConversations )
                {
                    var conversationsWithUserInfo = (from c in context.Conversations
                                                     join cu in context.ConversationUsers on c.ConversationId equals cu.ConversationId
                                                     join a in context.Accounts on cu.AccountId equals a.AccountId
                                                     where cu.ConversationId == item 
                                                     select new
                                                     {
                                                         conversation = c,
                                                         conversationUser = cu,
                                                         account = a
                                                     }).ToList();

                    foreach (var c in conversationsWithUserInfo)
                    {
                        if(c.account.AccountId != userID)
                        response.Add(new
                        {
                            c.conversation.ConversationId,
                            c.conversation.Type,
                            c.conversation.CreateAt,
                            c.account.AccountId,
                            c.account.FullName,
                            c.account.Picture
                        });
                    }
                }


                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<object> GetPendingConversations()
        {
            try
            {
                var context = new MoneyDreamContext();

                var conversationsWithUserInfo = (from c in context.Conversations
                                                 join cu in context.ConversationUsers on c.ConversationId equals cu.ConversationId
                                                 join a in context.Accounts on cu.AccountId equals a.AccountId
                                                 where c.Type == "PENDING"
                                                 group new { c, a } by c.ConversationId into grouped
                                                 where grouped.Count() == 1 // Filter conversations with only one account
                                                 select new
                                                 {
                                                     conversation = grouped.First().c,
                                                     account = grouped.First().a
                                                 }).ToList();

                var response = new List<object>();
                foreach (var c in conversationsWithUserInfo)
                {
                    response.Add(new
                    {
                      
                            c.conversation.ConversationId,
                            c.conversation.Type,
                            c.conversation.CreateAt,
                            c.account.AccountId,
                            c.account.FullName,
                            c.account.Picture

                    });
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CreateNewConversation(Conversation conversation,int accountId1, int accountId2)
        {
            try
            {

                var context = new MoneyDreamContext();

                var existingConversation = context.Conversations
                    .Where(c => c.ConversationUsers.Any(u => u.AccountId == accountId1) &&
                                c.ConversationUsers.Any(u => u.AccountId == accountId2))
                    .FirstOrDefault();

                if (existingConversation != null)
                {
                    return existingConversation.ConversationId;
                }
                else
                {
                    context.Add(conversation);
                    context.SaveChanges();
                    ConversationUser newUser = new ConversationUser();
                    newUser.ConversationId = conversation.ConversationId;
                    newUser.AccountId = accountId1;
                    context.Add(newUser);
                    context.SaveChanges();

                    ConversationUser newUser2 = new ConversationUser();
                    newUser2.ConversationId = conversation.ConversationId;
                    newUser2.AccountId = accountId2;
                    context.Add(newUser2);
                    context.SaveChanges();
                }

                return conversation.ConversationId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CreateNewConversation(Conversation conversation, int customerID)
        {
            try
            {

                var context = new MoneyDreamContext();

                var existingConversation = context.Conversations
                    .Where(c => c.ConversationUsers.Any(u => u.AccountId == customerID)).SingleOrDefault();

                if (existingConversation != null)
                {
                    return existingConversation.ConversationId;
                }
                else
                {
                    context.Add(conversation);
                    context.SaveChanges();
                    ConversationUser newUser = new ConversationUser();
                    newUser.ConversationId = conversation.ConversationId;
                    newUser.AccountId = customerID;
                    context.Add(newUser);
                    context.SaveChanges();
                }

                return conversation.ConversationId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int AddAdminToConversation(int adminID, int conversationID)
        {
            try
            {

                var context = new MoneyDreamContext();
                var conversation = context.Conversations.SingleOrDefault(c => c.ConversationId == conversationID);
                var existingConversation = context.Conversations
                    .Where(c => c.ConversationUsers.Any(u => u.AccountId == adminID) &&
                                c.ConversationId == conversationID)
                    .FirstOrDefault();

                if (existingConversation != null)
                {
                    existingConversation.Type = "CHAT";
                    context.Entry<Conversation>(existingConversation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return existingConversation.ConversationId;

                }
                else
                {
                    ConversationUser newUser = new ConversationUser();
                    newUser.ConversationId = conversationID;
                    newUser.AccountId = adminID;
                    context.Add(newUser);
                    context.SaveChanges();
                }

                if(conversation !=null)
                {
                    conversation.Type = "CHAT";
                    context.Entry<Conversation>(conversation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                
                return conversationID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteConversation(int conversationID)
        {
            try
            {
                var context = new MoneyDreamContext();

                // Delete Message 
                var deleteMessage = (from m in context.Messages
                                     where m.ConversationId == conversationID
                                     select m).ToList();

                foreach (var message in deleteMessage)
                {
                    context.Messages.Remove(message);
                }

                context.SaveChanges();


                var userInConversation = (from c in context.ConversationUsers
                                          where c.ConversationId == conversationID
                                          select c).ToList();

                foreach (var c in userInConversation)
                {
                    context.ConversationUsers.Remove(c);
                }
                context.SaveChanges();


                var conver = context.Conversations.SingleOrDefault(c => c.ConversationId == conversationID);
                context.Conversations.Remove(conver);
                context.SaveChanges();


                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public void CreateMessage(Message message)
        {
            try
            {
                var context = new MoneyDreamContext();
                var checkConversation = context.Conversations.SingleOrDefault(c => c.ConversationId == message.ConversationId);
                if (checkConversation == null)
                {
                    throw new Exception("Do not exist conversation");
                } else
                {
                    context.Messages.Add(message);
                    context.SaveChanges();
                }


            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}

