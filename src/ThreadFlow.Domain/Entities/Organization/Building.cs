using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// কারখানার ভেতরের ভৌগোলিক ভবন বা শেড এনটিটি (যেমন: Main Production Building, Warehouse Shed)
/// কম্পাউন্ডের বিভিন্ন ফ্লোর ধারণ করে এবং কমপ্লায়েন্স ও ফায়ার সেফটি অডিট ডেটা বহন করে।
/// </summary>
public class Building : AuditableEntity, ITenantScopedEntity
{
    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (মাল্টি-টেন্যান্ট আইসোলেশন)</summary>
    public Guid TenantId { get; set; }

    /// <summary>প্যারেন্ট কারখানার আইডি (ফরেন কি)</summary>
    public Guid FactoryId { get; set; }

    /// <summary>ভবনের অনন্য সংক্ষিপ্ত কোড (যেমন: BLD_01, SHED_A)</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>ভবনের নাম (যেমন: Main Sewing Complex, Fabric Warehouse Shed)</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>মোট তলার সংখ্যা (যেমন: ৫ তলা ভবন বা ১ তলা প্রি-ফ্যাব শেড, ডিফল্ট: ১)</summary>
    public int TotalFloors { get; set; } = 1;

    /// <summary>ভবনের মোট আয়তন (স্কয়ার ফিটে - ক্যাপাসিটি ও স্পেস অডিটের জন্য) - অপশনাল</summary>
    public decimal? TotalAreaSqft { get; set; }

    /// <summary>কনস্ট্রাকশন বা স্ট্রাকচার টাইপ (যেমন: RCC Multi-Story, Steel Shed) - অপশনাল</summary>
    public string? ConstructionType { get; set; }

    /// <summary>জরুরি ফায়ার এক্সিট / সেফটি সিঁড়ির সংখ্যা (কমপ্লায়েন্স ফিল্ড, ডিফল্ট: ০)</summary>
    public int FireExitCount { get; set; } = 0;

    /// <summary>ভবনের অতিরিক্ত নোট বা বিবরণ - অপশনাল</summary>
    public string? Description { get; set; }

    /// <summary>ভবন সক্রিয় নাকি মেইনটেন্যান্সে বন্ধ</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public Factory? Factory { get; set; }
    public ICollection<Floor> Floors { get; set; } = new List<Floor>();
}
