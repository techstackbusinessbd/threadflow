using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Enums;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// ফিজিক্যাল ম্যানুফ্যাকচারিং ফ্যাক্টরি বা কারখানা এনটিটি
/// নির্দিষ্ট ইন্ডাস্ট্রিয়াল আর্কিটাইপ (Knit, Woven, Denim, Sweater, Wash, Embellishment) ধারণ করে।
/// </summary>
public class Factory : AuditableEntity, ITenantScopedEntity
{
    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (মাল্টি-টেন্যান্ট আইসোলেশন)</summary>
    public Guid TenantId { get; set; }

    /// <summary>প্যারেন্ট কোম্পানির আইডি (ফরেন কি)</summary>
    public Guid CompanyId { get; set; }

    /// <summary>বিজনেস ইউনিটের আইডি (অপশনাল ফরেন কি)</summary>
    public Guid? BusinessUnitId { get; set; }

    /// <summary>কারখানার অনন্য সংক্ষিপ্ত কোড (যেমন: FAC_01, KNT_UNIT_1)</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>কারখানার পূর্ণ নাম (যেমন: Apex Knitting & Sewing Complex)</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>ফ্যাক্টরি আর্কিটাইপ (Knit, Woven, Denim, Sweater, Composite ইত্যাদি)</summary>
    public FactoryArchetype Archetype { get; set; } = FactoryArchetype.KnitDedicated;

    /// <summary>বন্ডেড ওয়্যারহাউজ লাইসেন্স নম্বর (শুল্কমুক্ত ফেব্রিক আমদানির জন্য) - অপশনাল</summary>
    public string? BondLicenseNumber { get; set; }

    /// <summary>বিজিএমইএ / বিকেএমইএ মেম্বারশিপ রেজিস্ট্রেশন নম্বর - অপশনাল</summary>
    public string? BgmeaRegNumber { get; set; }

    /// <summary>ফায়ার সার্ভিস লাইসেন্স নম্বর (সেফটি ও কমপ্লায়েন্স অডিটের জন্য) - অপশনাল</summary>
    public string? FireLicenseNumber { get; set; }

    /// <summary>কারখানার জিএম অপারেশন / ফ্যাক্টরি হেড-এর নাম - অপশনাল</summary>
    public string? FactoryManagerName { get; set; }

    /// <summary>কারখানার অফিস বা গেট ফোন নম্বর - অপশনাল</summary>
    public string? ContactPhone { get; set; }

    /// <summary>কারখানার অফিসিয়াল ইমেইল অ্যাড্রেস - অপশনাল</summary>
    public string? ContactEmail { get; set; }

    /// <summary>কারখানার ফিজিক্যাল অবস্থান / ঠিকানা</summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>শহর বা জেলা (যেমন: Gazipur, Narayanganj) - অপশনাল</summary>
    public string? City { get; set; }

    /// <summary>দেশ (ডিফল্ট: Bangladesh)</summary>
    public string Country { get; set; } = "Bangladesh";

    /// <summary>কারখানা চালু নাকি সাময়িক বন্ধ</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public Company? Company { get; set; }
    public BusinessUnit? BusinessUnit { get; set; }
    public ICollection<Building> Buildings { get; set; } = new List<Building>();
}
