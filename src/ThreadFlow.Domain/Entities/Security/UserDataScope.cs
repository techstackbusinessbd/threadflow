using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// ব্যবহারকারীর ৮-লেয়ার স্থানিক ও ভৌগোলিক ডেটা স্কোপ এনটিটি (Spatial Data Scope & Boundary)
/// ব্যবহারকারী কোন কোম্পানি, কারখানা, ভবন, ফ্লোর, সেকশন বা লাইনের ডেটা দেখতে পারবে তা নিয়ন্ত্রণ করে।
/// কোনো লেভেল নাল (NULL) থাকার অর্থ হলো সেই প্যারেন্টের অধীনে সমস্ত সাব-লেভেলে আনরেস্ট্রিক্টেড এক্সেস থাকবে।
/// </summary>
public class UserDataScope : AuditableEntity, ITenantScopedEntity
{
    /// <summary>প্যারেন্ট ব্যবহারকারীর আইডি (ফরেন কি)</summary>
    public Guid UserId { get; set; }

    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (মাল্টি-টেন্যান্ট আইসোলেশন)</summary>
    public Guid TenantId { get; set; }
    
    /// <summary>কোম্পানি ফিল্টার (নাল হলে টেন্যান্টের সব কোম্পানি)</summary>
    public Guid? CompanyId { get; set; }

    /// <summary>ফ্যাক্টরি ফিল্টার (নাল হলে কোম্পানির সব কারখানা)</summary>
    public Guid? FactoryId { get; set; }

    /// <summary>ভবন ফিল্টার (নাল হলে কারখানার সব ভবন)</summary>
    public Guid? BuildingId { get; set; }

    /// <summary>ফ্লোর ফিল্টার (নাল হলে ভবনের সব তলা)</summary>
    public Guid? FloorId { get; set; }

    /// <summary>সেকশন ফিল্টার (যেমন: শুধু Cutting Room-এ সীমাবদ্ধ করতে চাইলে)</summary>
    public Guid? SectionId { get; set; }

    /// <summary>প্রোডাকশন লাইন ফিল্টার (যেমন: নির্দিষ্ট কোনো সুইং লাইনে সীমাবদ্ধ করতে চাইলে)</summary>
    public Guid? ProductionLineId { get; set; }

    /// <summary>স্কোপ রুল সক্রিয় নাকি নিষ্ক্রিয়</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public User? User { get; set; }
}
