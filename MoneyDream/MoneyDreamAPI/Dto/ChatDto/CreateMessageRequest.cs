namespace MoneyDreamAPI.Dto.ChatDto
{
    public class CreateMessageRequest
    {
        public int SenderId { get; set; }
        public int ConversationId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? AttachmentFileUrl { get; set; }
        public string? CreateAt { get; set; } = DateTime.Now.ToString();
    }
}
