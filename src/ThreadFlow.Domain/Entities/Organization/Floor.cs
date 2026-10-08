using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// ভবনের তলা বা ফ্লোর এনটিটি (যেমন: Ground Floor, 1st Floor - Cutting Section)
/// ফ্লোরের কার্যকর উৎপাদন স্পেস ও শ্রমিক ধারণক্ষমতা কমপ্লায়েন্স ডেটা ধারণ করে।
/// </summary>
public class Floor : AuditableEntity, ITenantScopedEntity
{
    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (মাল্টি-টেন্যান্ট আইসোলেশন)</summary>
    public Guid TenantId { get; set; }

    /// <summary>প্যারেন্ট ভবনের আইডি (ফরেন কি)</summary>
    public Guid BuildingId { get; set; }

    /// <summary>তলার ক্রমিক সংখ্যা (যেমন: ০ = গ্রাউন্ড ফ্লোর, ১ = ১ম তলা, -১ = বেসমেন্ট)</summary>
    public int FloorNumber { get; set; } = 1;

    /// <summary>ফ্লোরের অনন্য সংক্ষিপ্ত কোড (যেমন: FL_G, FL_01, FL_02)</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>ফ্লোরের পরিচিতিমূলক নাম (যেমন: 1st Floor - Cutting Section)</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>কার্যকর উৎপাদন আয়তন (স্কয়ার ফিটে - ক্যাপাসিটি প্ল্যানিংয়ের জন্য) - অপশনাল</summary>
    public decimal? UsableAreaSqft { get; set; }

    /// <summary>সর্বোচ্চ শ্রমিক ধারণক্ষমতা (বায়ার সোশ্যাল কমপ্লায়েন্স ও আইএলও অডিট) - অপশনাল</summary>
    public int MaxWorkerOccupancy { get; set; } = 0;

    /// <summary>ফ্লোরে স্থাপিত অগ্নিনির্বাপক সিলিন্ডারের সংখ্যা (ফায়ার সেফটি অডিট) - অপশনাল</summary>
    public int FireExtinguisherCount { get; set; } = 0;

    /// <summary>ফ্লোরের অতিরিক্ত বিবরণ বা লেআউট নোট - অপশনাল</summary>
    public string? Description { get; set; }

    /// <summary>ফ্লোর সক্রিয় নাকি লেআউট সংস্কারে বন্ধ</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public Building? Building { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
