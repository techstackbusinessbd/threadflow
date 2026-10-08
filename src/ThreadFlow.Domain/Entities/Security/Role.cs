using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// ব্যবহারকারীর রোল বা ভূমিকা এনটিটি (আরব্যাক অ্যাক্সেস কন্ট্রোল)
/// প্ল্যাটফর্মের গ্লোবাল সিস্টেম রোল এবং টেন্যান্টের নিজস্ব কাস্টম রোল উভয়ই সাপোর্ট করে।
/// </summary>
public class Role : AuditableEntity
{
    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (সিস্টেম রোলের ক্ষেত্রে নাল, কাস্টম রোলের ক্ষেত্রে টেন্যান্ট আইডি)</summary>
    public Guid? TenantId { get; set; }

    /// <summary>রোলের অনন্য সংক্ষিপ্ত কোড (যেমন: SUPER_ADMIN, SR_MERCHANDISER)</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>রোলের পরিচিতিমূলক প্রদর্শন নাম (যেমন: Senior Merchandiser, Cutting Master)</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>এই রোলের ক্ষমতা বা দায়িত্বের সংক্ষিপ্ত বিবরণ - অপশনাল</summary>
    public string? Description { get; set; }

    /// <summary>বিল্ট-ইন সুরক্ষিত সিস্টেম রোল কিনা (ট্রু হলে ডিলিট/কোড পরিবর্তন নিষিদ্ধ)</summary>
    public bool IsSystemRole { get; set; } = false;

    /// <summary>রোল সক্রিয় নাকি নিষ্ক্রিয়</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
