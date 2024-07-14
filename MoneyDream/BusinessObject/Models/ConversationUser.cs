using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class ConversationUser
{
    public int ConversationUserId { get; set; }

    public int ConversationId { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;
}
