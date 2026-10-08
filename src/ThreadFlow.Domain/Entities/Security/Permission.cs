using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// সিস্টেমের সূক্ষ্ম পারমিশন বা অ্যাকশন প্রিভিলেজ এনটিটি (যেমন: Merchandising.Styles.Create, Cutting.LaySheet.Approve)
/// কোড-ফার্স্ট ডাইনামিক রিফ্লেকশন স্ক্যানার দ্বারা অটো-ডিসকভার ও সিঙ্ক হয়।
/// </summary>
public class Permission : BaseEntity
{
    /// <summary>অনন্য পূর্ণ পারমিশন কোড (যেমন: Merchandising.Styles.Create)</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>মূল বিজনেস মডিউলের নাম (যেমন: Merchandising, Cutting, Sewing)</summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>নির্দিষ্ট রিসোর্স বা ফিচারের নাম (যেমন: Styles, PurchaseOrders, LaySheets)</summary>
    public string Resource { get; set; } = string.Empty;

    /// <summary>অনুমোদিত অ্যাকশনের নাম (যেমন: View, Create, Edit, Delete, Approve, Export)</summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>পারমিশনের সহজবোধ্য বিবরণ (অ্যাডমিন স্ক্রিনে রোলে পারমিশন নির্ধারণের সময় দেখাবে) - অপশনাল</summary>
    public string? Description { get; set; }

    /// <summary>পারমিশনটি সক্রিয় নাকি অবলুপ্ত</summary>
    public bool IsActive { get; set; } = true;

    // নেভিগেশন প্রোপার্টিজ
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
