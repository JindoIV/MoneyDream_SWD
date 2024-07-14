using Microsoft.AspNetCore.Mvc;
using MoneyDreamAPI.Dto.ApiResponse;
using MoneyDreamAPI.Dto.ChatDto;
using MoneyDreamAPI.Dto.ImageExtension;
using MoneyDreamAPI.Services;

namespace MoneyDreamAPI

{
    [ApiController]
    [Route("api/chat")]

    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("/createMessage")]
        public IActionResult CreateMessage(CreateMessageRequest request)
        {
            try
            {
                var a = _chatService.SendMessage(request);
                return ApiResponse.Success(a);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }

        [HttpPost("/createConversation")]
        public IActionResult CreateChat(CreateChatRequest request)
        {
            try
            {
               var response = _chatService.CreateConversation(request);
                return ApiResponse.Success(response);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null,e.Message);
            }
        }

        [HttpPost("/createPendingConversation")]
        public IActionResult CreateChat(CreateChatPendingRequest request)
        {
            try
            {
                var response = _chatService.CreateConversation(request);
                return ApiResponse.Success(response);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }

        [HttpPost("/addAdminToConversation")]
        public IActionResult AddAdminToChat(AddAdminToConversationRequest request)
        {
            try
            {
                var response = _chatService.AddAdminToConversation(request);
                return ApiResponse.Success(response);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }

        [HttpGet("/getConversation")]
        public IActionResult GetChat(int conversationID)
        {
            try
            {
                var a = _chatService.GetConversation(conversationID);
                return ApiResponse.Success(a);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }

        [HttpGet("/getConversationByUserID")]
        public IActionResult GetChatByUserID(int userID)
        {
            try
            {
                var a = _chatService.GetConversationByUserID(userID);
                return ApiResponse.Success(a);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }


        [HttpGet("/getPendingConversation")]
        public IActionResult GetPendingConversations()
        {
            try
            {
                var a = _chatService.GetPendingConversations();
                return ApiResponse.Success(a);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }

        [HttpDelete("/deleteConversation")]
        public IActionResult DeleteChat(int conversationID)
        {
            try
            {
                var a = _chatService.DeleteConversation(conversationID);
                return ApiResponse.Success(a);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }

       
    }
}
