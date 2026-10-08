using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Entities.Organization;
using ThreadFlow.Domain.Enums;

namespace ThreadFlow.Domain.Entities.Tenancy;

/// <summary>
/// রুট টেন্যান্ট এনটিটি (মাল্টি-টেন্যান্ট SaaS আর্কিটেকচারের মাদার কন্টেইনার)
/// সমস্ত অর্গানাইজেশনাল, সিকিউরিটি ও ট্রানজ্যাকশন ডেটার সর্বোচ্চ আইসোলেশন প্যারেন্ট।
/// </summary>
public class Tenant : AuditableEntity
{
    /// <summary>টেন্যান্টের অনন্য সংক্ষিপ্ত কোড (যেমন: APEX)</summary>
    public string TenantCode { get; set; } = string.Empty;

    /// <summary>ক্লায়েন্ট গ্রুপের পূর্ণ প্রাতিষ্ঠানিক নাম (যেমন: Apex Holdings Ltd.)</summary>
    public string TenantName { get; set; } = string.Empty;

    /// <summary>ক্লাউড SaaS সাবডোমেন (যেমন: apex -> apex.threadflow.io)</summary>
    public string Subdomain { get; set; } = string.Empty;

    /// <summary>কাস্টম ডোমেন (যেমন: erp.apex.com) - অপশনাল</summary>
    public string? CustomDomain { get; set; }

    /// <summary>টেন্যান্টের মূল অ্যাডমিন ইমেইল অ্যাড্রেস</summary>
    public string AdminEmail { get; set; } = string.Empty;

    /// <summary>জরুরি যোগাযোগ বা ওটিপির জন্য মোবাইল নম্বর - অপশনাল</summary>
    public string? AdminPhone { get; set; }

    /// <summary>ক্লায়েন্টের অফিসিয়াল লোগো ইমেজ ইউআরএল - অপশনাল</summary>
    public string? LogoUrl { get; set; }

    /// <summary>সাবস্ক্রিপশন প্ল্যান টায়ার (Starter, Professional, Enterprise)</summary>
    public SubscriptionTier SubscriptionTier { get; set; } = SubscriptionTier.Professional;

    /// <summary>সাবস্ক্রিপশনের মেয়াদ শেষ হওয়ার তারিখ (লাইফটাইম হলে নাল)</summary>
    public DateTimeOffset? SubscriptionExpiresAt { get; set; }

    /// <summary>টেন্যান্টের সক্রিয়তা স্ট্যাটাস (অ্যাক্টিভ নাকি সাসপেন্ডেড)</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন কালেকশন
    public ICollection<Company> Companies { get; set; } = new List<Company>();
}
