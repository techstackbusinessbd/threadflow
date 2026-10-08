using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Enums;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// অপারেশনাল প্রোডাকশন বিভাগ বা সেকশন এনটিটি (যেমন: Cutting Room, Sewing Lines, Finishing, Quality Audit)
/// প্রতিটি সেকশনের ইনচার্জ, কাজের ধরন ও ওয়ার্কস্টেশন ক্যাপাসিটি ধারণ করে।
/// </summary>
public class Section : AuditableEntity, ITenantScopedEntity
{
    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (মাল্টি-টেন্যান্ট আইসোলেশন)</summary>
    public Guid TenantId { get; set; }

    /// <summary>প্যারেন্ট ফ্লোরের আইডি (ফরেন কি)</summary>
    public Guid FloorId { get; set; }

    /// <summary>সেকশনের অনন্য সংক্ষিপ্ত কোড (যেমন: SEC_CUT, SEC_SEW_A, SEC_FIN)</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>সেকশনের নাম (যেমন: Central Cutting Section, Sewing Unit-1)</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>সেকশনের কাজের ধরন (Cutting, Sewing, Finishing, Store, Quality ইত্যাদি)</summary>
    public SectionType Type { get; set; } = SectionType.Sewing;

    /// <summary>সেকশন ইনচার্জ বা ফ্লোর সুপারভাইজারের নাম - অপশনাল</summary>
    public string? InChargeName { get; set; }

    /// <summary>সেকশন ডেস্কে যোগাযোগের ফোন বা ইন্টারকম নম্বর - অপশনাল</summary>
    public string? ContactPhone { get; set; }

    /// <summary>মোট ওয়ার্কস্টেশন বা টেবিল সংখ্যা (যেমন কাটিং টেবিল বা ইন্সপেকশন টেবিল, ডিফল্ট: ০)</summary>
    public int TotalWorkstations { get; set; } = 0;

    /// <summary>সেকশনের অতিরিক্ত বিবরণ বা লেআউট নোট - অপশনাল</summary>
    public string? Description { get; set; }

    /// <summary>সেকশন চালু নাকি বন্ধ</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public Floor? Floor { get; set; }
    public ICollection<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();
}
