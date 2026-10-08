using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Entities.Tenancy;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// অ্যাপ্লিকেশন ব্যবহারকারী এনটিটি (Keycloak Identity Provider-এর সাথে সিঙ্ককৃত)
/// টেন্যান্ট মেম্বারশিপ, এইচআর পদবি, বিভাগ ও আরব্যাক এক্সেস প্রোফাইল ধারণ করে।
/// </summary>
public class User : AuditableEntity, ITenantScopedEntity
{
    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (মাল্টি-টেন্যান্ট আইসোলেশন)</summary>
    public Guid TenantId { get; set; }

    /// <summary>কীক্লোক আইডেন্টিটি সার্ভারের সাথে সিঙ্ক করার জন্য ওআইডিসি সাবজেক্ট আইডি (sub)</summary>
    public string KeycloakUserId { get; set; } = string.Empty;

    /// <summary>লগইন ইউজারনেম (টেন্যান্টের ভেতরে অনন্য)</summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>ব্যবহারকারীর অফিশিয়াল যোগাযোগের ইমেইল অ্যাড্রেস</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>ব্যবহারকারীর পূর্ণ নাম (যেমন: মোঃ আসাদুজ্জামান)</summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>কোম্পানির এইচআর এমপ্লয়ি কার্ড নম্বর / বায়োমেট্রিক আইডি (যেমন: EMP-1042) - অপশনাল</summary>
    public string? EmployeeId { get; set; }

    /// <summary>ব্যবহারকারীর পদবি (যেমন: Sr. Merchandiser, IE Manager, Cutting Master) - অপশনাল</summary>
    public string? Designation { get; set; }

    /// <summary>কাজের বিভাগ (যেমন: Merchandising, Quality Assurance, Finance) - অপশনাল</summary>
    public string? Department { get; set; }

    /// <summary>মোবাইল ফোন নম্বর (এসএমএস নোটিফিকেশন বা জরুরি যোগাযোগের জন্য) - অপশনাল</summary>
    public string? PhoneNumber { get; set; }

    /// <summary>প্রোফাইল ছবি ইমেজ ইউআরএল - অপশনাল</summary>
    public string? AvatarUrl { get; set; }

    /// <summary>টেন্যান্টের প্রধান সুপার অ্যাডমিন ফ্ল্যাগ (ডিফল্ট: false)</summary>
    public bool IsSuperAdmin { get; set; } = false;

    /// <summary>সর্বশেষ সিস্টেমে লগইন করার টাইমস্ট্যাম্প - অপশনাল</summary>
    public DateTimeOffset? LastLoginAt { get; set; }

    /// <summary>অ্যাকাউন্ট সক্রিয় নাকি নিষ্ক্রিয়</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public Tenant? Tenant { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserDataScope> DataScopes { get; set; } = new List<UserDataScope>();
}
