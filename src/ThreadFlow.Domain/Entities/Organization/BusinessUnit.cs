using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Enums;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// অপারেশনাল বিজনেস ইউনিট বা বিশেষায়িত ডিভিশন এনটিটি (যেমন: Apparel, Spinning, Dyeing, Washing)
/// কোম্পানির অধীনে আলাদা বিভাগীয় প্রধান ও অপারেশনাল পরিধি ধারণ করে।
/// </summary>
public class BusinessUnit : AuditableEntity, ITenantScopedEntity
{
    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (মাল্টি-টেন্যান্ট আইসোলেশন)</summary>
    public Guid TenantId { get; set; }

    /// <summary>প্যারেন্ট কোম্পানির আইডি (ফরেন কি)</summary>
    public Guid CompanyId { get; set; }

    /// <summary>বিজনেস ইউনিটের অনন্য সংক্ষিপ্ত কোড (যেমন: BU_APP, BU_DYE)</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>বিজনেস ইউনিটের পূর্ণ নাম (যেমন: Apparel Manufacturing Division)</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>বিজনেস ইউনিটের ধরন (Apparel, Spinning, Knitting, Dyeing ইত্যাদি)</summary>
    public BusinessUnitType Type { get; set; } = BusinessUnitType.ApparelManufacturing;

    /// <summary>ইউনিট প্রধান / সিওও (COO) বা ইডি (ED)-এর নাম - অপশনাল</summary>
    public string? HeadOfUnitName { get; set; }

    /// <summary>বিজনেস ইউনিটের অফিসিয়াল ইমেইল - অপশনাল</summary>
    public string? Email { get; set; }

    /// <summary>বিজনেস ইউনিটের যোগাযোগের ফোন নম্বর - অপশনাল</summary>
    public string? PhoneNumber { get; set; }

    /// <summary>ইউনিটের বাস্তব অপারেশনাল অফিস বা প্ল্যান্টের ঠিকানা - অপশনাল</summary>
    public string? Address { get; set; }

    /// <summary>ইউনিটের কার্যক্রম বা কাজের পরিধির সংক্ষিপ্ত বিবরণ - অপশনাল</summary>
    public string? Description { get; set; }

    /// <summary>ইউনিট সক্রিয় নাকি বন্ধ</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public Company? Company { get; set; }
    public ICollection<Factory> Factories { get; set; } = new List<Factory>();
}
