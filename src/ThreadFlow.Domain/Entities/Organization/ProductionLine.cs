using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// সুইং বা প্রোডাকশন লাইন এনটিটি (ফ্লোরের ক্ষুদ্রতম উৎপাদন ইউনিট)
/// লাইভ বান্ডেল স্ক্যানিং, প্রতি ঘণ্টার আউটপুট, এফিসিয়েন্সি ও অ্যান্ডন স্ক্রিন ডেটা ধারণ করে।
/// </summary>
public class ProductionLine : AuditableEntity, ITenantScopedEntity
{
    /// <summary>প্যারেন্ট টেন্যান্টের আইডি (মাল্টি-টেন্যান্ট আইসোলেশন)</summary>
    public Guid TenantId { get; set; }

    /// <summary>প্যারেন্ট সেকশনের আইডি (ফরেন কি)</summary>
    public Guid SectionId { get; set; }

    /// <summary>লাইনের ক্রমিক নম্বর (যেমন: ১, ২, ৩)</summary>
    public int LineNumber { get; set; } = 1;

    /// <summary>লাইনের অনন্য সংক্ষিপ্ত কোড (যেমন: LINE_01, SEW_L05)</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>লাইনের পরিচিতিমূলক নাম (যেমন: Line 01 - Knit Polo Dedicated)</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>লাইন সুপারভাইজারের নাম - অপশনাল</summary>
    public string? SupervisorName { get; set; }

    /// <summary>লাইন চিফ বা কোয়ালিটি কন্ট্রোলারের নাম - অপশনাল</summary>
    public string? LineChiefName { get; set; }

    /// <summary>লাইনে মোট সুইং মেশিনের সংখ্যা (ডিফল্ট: ৩০)</summary>
    public int TotalMachines { get; set; } = 30;

    /// <summary>লাইনে নিয়োজিত হেল্পারদের সংখ্যা (ডিফল্ট: ৫)</summary>
    public int HelperCount { get; set; } = 5;

    /// <summary>প্রতি ঘণ্টার লক্ষ্যমাত্রা পোশাক উৎপাদন সংখ্যা (যেমন: ১৫০ পিস/ঘণ্টা) - অপশনাল</summary>
    public int? TargetHourlyOutput { get; set; }

    /// <summary>লক্ষ্যমাত্রা এফিসিয়েন্সি শতাংশ (জিরো-ফ্লোট ডেসিমাল রুল, যেমন: ৬৫.০০%)</summary>
    public decimal TargetEfficiency { get; set; } = 65.00m;

    /// <summary>দৈনিক সাধারণ শিফট সময়কাল মিনিটে (৮ ঘণ্টা = ৪৮০ মিনিট)</summary>
    public int DailyCapacityMinutes { get; set; } = 480;

    /// <summary>লাইনের মাথায় স্থাপিত অ্যান্ডন টিভি ডিসপ্লের আইপি অ্যাড্রেস বা ডিভাইস আইডি - অপশনাল</summary>
    public string? AndonDisplayIp { get; set; }

    /// <summary>লাইনের অতিরিক্ত বিবরণ বা স্পেশাল নোট - অপশনাল</summary>
    public string? Description { get; set; }

    /// <summary>লাইন চালু নাকি বন্ধ</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public Section? Section { get; set; }
}
