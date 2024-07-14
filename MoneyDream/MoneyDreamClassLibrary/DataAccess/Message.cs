using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class Message
{
    public int MessageId { get; set; }

    public int SenderId { get; set; }

    public int ConversationId { get; set; }

    public string MessageContent { get; set; } = null!;

    public string? AttachedFileUrl { get; set; }

    public string? CreateAt { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;
}
