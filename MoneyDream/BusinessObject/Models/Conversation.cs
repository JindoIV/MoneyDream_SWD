using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Conversation
{
    public int ConversationId { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? CreateAt { get; set; }

    public virtual ICollection<ConversationUser> ConversationUsers { get; set; } = new List<ConversationUser>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
