using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Metadata;
using MoneyDreamAPI.Dto.ChatDto;
using System.Text.RegularExpressions;
using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamClassLibrary.Repository;
using System.Security.Claims;
using Newtonsoft.Json.Linq;

namespace MoneyDreamAPI.Services
{

    public interface IChatService
    {
        Task SendMessage(CreateMessageRequest request);
        Task SendMessageToConversation(CreateMessageRequest request);

        Task JoinRoom(string roomID);

        IEnumerable<Message> GetConversation(int conversationID);

        IEnumerable<ConversationUser> GetConversationIdsByUserID(int userId);

        public IEnumerable<object> GetConversationByUserID(int userID);

        int CreateConversation(CreateChatRequest request);
        int CreateConversation(CreateChatPendingRequest request);
        int AddAdminToConversation(AddAdminToConversationRequest request);

        bool DeleteConversation(int conversationID);

        List<object> GetPendingConversations();



    }
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        private readonly IMessageRepository _messageRepository;

        public ChatService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
            _messageRepository = new MessageRepository();
        }
        
        public async Task SendMessage(CreateMessageRequest request)
        {
            // Save to database
            Message m = new Message();
            m.SenderId = request.SenderId;
            m.ConversationId = request.ConversationId;
            m.MessageContent = request.Message;
            m.CreateAt = DateTime.Now.ToString();

            _messageRepository.CreateMessage(m);
            // Send to hub;

            var message = new
            {
                m.SenderId,
                m.MessageContent,
                CreateAt = DateTime.Now.ToString()
            };

          //  await _hubContext.Clients.Group(m.ConversationId.ToString()).SendAsync("ReceiveMessage", m.ConversationId.ToString(), message);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }
       
        public async Task SendMessageToConversation(CreateMessageRequest request)
        {
            // Save to database
            Message m = new Message();
            m.SenderId = request.SenderId;
            m.ConversationId = request.ConversationId;
            m.MessageContent = request.Message;
            m.CreateAt = DateTime.Now.ToString();

            _messageRepository.CreateMessage(m);
            // Send to hub;
            var message = new
            {
                m.SenderId,
                m.MessageContent,
                m.AttachedFileUrl,
                m.ConversationId,
                CreateAt = DateTime.Now.ToString()
            };

            Console.WriteLine(message);

            await _hubContext.Clients.Groups(request.ConversationId.ToString()).SendAsync("ReceiveMessage", message);

        }

        public int CreateConversation(CreateChatRequest request)
        {   
            Conversation conv = new Conversation();
            conv.Type = "CHAT";
            conv.Name = "Name";
            conv.CreateAt = DateTime.Now.ToString();
            return _messageRepository.CretateConversation(conv, request.User1ID, request.User2ID);
        }

        public int CreateConversation(CreateChatPendingRequest request)
        {
            Conversation conv = new Conversation();
            conv.Type = "PENDING";
            conv.Name = "Name";
            conv.CreateAt = DateTime.Now.ToString();
            return _messageRepository.CretateConversation(conv, request.CustomerID);
        }

        public int AddAdminToConversation(AddAdminToConversationRequest request)
        {
            return _messageRepository.AddAdminToConversation(request.AdminID, request.ConversationID);
        }

        public bool DeleteConversation(int conversationID)
        {
            return _messageRepository.DeleteConversation(conversationID);
        }

        public IEnumerable<Message> GetConversation(int conversationID)
        {
            return _messageRepository.GetConversation(conversationID);
        }

        public async Task JoinRoom(string roomID)
        {
            await _hubContext.Groups.AddToGroupAsync(roomID, roomID);
        }

        public IEnumerable<ConversationUser> GetConversationIdsByUserID(int userID)
        {
            return _messageRepository.GetConversationIdsByUserID(userID);
        }

        public IEnumerable<object> GetConversationByUserID(int userID)
        {
            var query = _messageRepository.GetConversationByUserID(userID);
           
            return query;
        }


        List<object> IChatService.GetPendingConversations()
        {
            return _messageRepository.GetPendingConversations();
        }
    }

    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public override async Task OnConnectedAsync()
        {
            var claim = Context.User.Claims;

            var userId = int.Parse(Context.User.Claims.First(x => x.Type == "id").Value);

            var conversations = _chatService.GetConversationIdsByUserID(userId);

            Console.WriteLine("Connected! UserID: " + userId);

            foreach (var c in conversations)
            {
                Console.WriteLine(c.ConversationId);
                await Groups.AddToGroupAsync(Context.ConnectionId,c.ConversationId.ToString());
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

        }

        public async Task SendMessage(CreateMessageRequest request)
        {
            await _chatService.SendMessage(request);
        }

        public async Task SendMessageToConversation(CreateMessageRequest request)
        {
            await _chatService.SendMessageToConversation(request);
           // await Clients.OthersInGroup(request.ConversationId.ToString()).SendAsync("ReceiveMessage",request.Message);
        }

        public string GetConnectionID() => Context.ConnectionId;

    }

}