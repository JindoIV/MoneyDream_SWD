namespace MoneyDreamAPI.Dto.ChatDto
{
    public class CreateChatRequest
    {
        public int User1ID  { get; set; }
        public int User2ID { get; set; }
    }

    public class CreateChatPendingRequest
    {
        public int CustomerID { get; set; }
    }

    public class AddAdminToConversationRequest
    {
        public int AdminID { get; set; }
        public int ConversationID { get; set; }
    }
}
