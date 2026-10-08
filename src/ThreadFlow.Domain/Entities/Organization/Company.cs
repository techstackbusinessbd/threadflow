using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Entities.Tenancy;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// লিগ্যাল কোম্পানি বা ব্যবসায়িক প্রতিষ্ঠান এনটিটি (গ্রুপের অধীনে নিবন্ধিত প্রতিষ্ঠান)
/// মাল্টি-টেন্যান্ট স্কোপ কার্যকর করে এবং বেস একাউন্টিং কারেন্সি ও আইনি তথ্য ধারণ করে।
/// </summary>
public class Company : AuditableEntity, ITenantScopedEntity
{
    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (মাল্টি-টেন্যান্ট আইসোলেশন)</summary>
    public Guid TenantId { get; set; }

    /// <summary>কোম্পানির সংক্ষিপ্ত ইউনিক কোড (যেমন: ASKML)</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>কোম্পানির নিবন্ধিত পূর্ণ আইনি নাম (যেমন: Apex Spinning & Knitting Mills Ltd.)</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>সিটি কর্পোরেশন / মিউনিসিপ্যালিটি ট্রেড লাইসেন্স নম্বর - অপশনাল</summary>
    public string? TradeLicenseNumber { get; set; }

    /// <summary>কোম্পানির ইলেকট্রনিক ট্যাক্স আইডেন্টিফিকেশন নম্বর (e-TIN) - অপশনাল</summary>
    public string? TaxIdentificationNumber { get; set; }

    /// <summary>বিজনেস আইডেন্টিফিকেশন নম্বর বা ভ্যাট রেজিস্ট্রেশন নম্বর (BIN/VAT) - অপশনাল</summary>
    public string? VatRegistrationNumber { get; set; }

    /// <summary>মূল হিসাবরক্ষণ কারেন্সি কোড (যেমন: USD, BDT)</summary>
    public string BaseCurrencyCode { get; set; } = "USD";

    /// <summary>আইনি ও সরকারি রেজিস্টার্ড অফিসের ঠিকানা (RJSC / ট্রেড লাইসেন্স অনুযায়ী)</summary>
    public string? RegisteredAddress { get; set; }

    /// <summary>বাস্তব কর্পোরেট হেড অফিসের অপারেশনাল ঠিকানা (মার্চেন্ডাইজিং ও হেড অফিস)</summary>
    public string? CorporateAddress { get; set; }

    /// <summary>কর্পোরেট হেড অফিসের যোগাযোগের ফোন নম্বর - অপশনাল</summary>
    public string? CorporatePhone { get; set; }

    /// <summary>কর্পোরেট হেড অফিসের অফিসিয়াল ইমেইল - অপশনাল</summary>
    public string? CorporateEmail { get; set; }

    /// <summary>কোম্পানির অফিশিয়াল ওয়েবসাইট ইউআরএল - অপশনাল</summary>
    public string? Website { get; set; }

    /// <summary>কোম্পানির ব্র্যান্ড লোগো ইমেজ ইউআরএল (লেটারহেড ও রিপোর্টে ব্যবহৃত হবে) - অপশনাল</summary>
    public string? LogoUrl { get; set; }

    /// <summary>কোম্পানির সক্রিয়তা স্ট্যাটাস (অ্যাক্টিভ নাকি বন্ধ)</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public Tenant? Tenant { get; set; }
    public ICollection<BusinessUnit> BusinessUnits { get; set; } = new List<BusinessUnit>();
    public ICollection<Factory> Factories { get; set; } = new List<Factory>();
}
