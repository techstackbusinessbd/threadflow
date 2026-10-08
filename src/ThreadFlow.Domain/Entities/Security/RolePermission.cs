using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// রোল ও পারমিশনের মেনি-টু-মেনি জংশন এনটিটি (Role-Permission Mapping)
/// কোন রোলে কোন কোন গ্র্যানুলার অ্যাকশন পারমিশন সক্রিয় থাকবে তা বেঁধে দেয়।
/// </summary>
public class RolePermission : BaseEntity
{
    /// <summary>প্যারেন্ট রোলের আইডি (কম্পোজিট পিকে ও ফরেন কি)</summary>
    public Guid RoleId { get; set; }

    /// <summary>প্রদত্ত পারমিশনের আইডি (কম্পোজিট পিকে ও ফরেন কি)</summary>
    public Guid PermissionId { get; set; }

    // নেভিগেশন প্রোপার্টিজ
    public Role? Role { get; set; }
    public Permission? Permission { get; set; }
}
