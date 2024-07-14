using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateofBirth { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Picture { get; set; }

    public string? Status { get; set; }

    public string? Created { get; set; }

    public int RoleId { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<AccountAddress> AccountAddresses { get; set; } = new List<AccountAddress>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<ConversationUser> ConversationUsers { get; set; } = new List<ConversationUser>();

    public virtual ICollection<EmailToken> EmailTokens { get; set; } = new List<EmailToken>();

    public virtual ICollection<ExportInfo> ExportInfos { get; set; } = new List<ExportInfo>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
