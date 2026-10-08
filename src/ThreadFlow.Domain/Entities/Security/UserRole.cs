using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// ব্যবহারকারী ও রোলের মেনি-টু-মেনি জংশন এনটিটি (User-Role Mapping)
/// একজন ব্যবহারকারীর সাথে তার একাধিক অ্যাসাইন করা রোল বেঁধে দেয়।
/// </summary>
public class UserRole : BaseEntity
{
    /// <summary>প্যারেন্ট ব্যবহারকারীর আইডি (কম্পোজিট পিকে ও ফরেন কি)</summary>
    public Guid UserId { get; set; }

    /// <summary>প্রদত্ত রোলের আইডি (কম্পোজিট পিকে ও ফরেন কি)</summary>
    public Guid RoleId { get; set; }

    // নেভিগেশন প্রোপার্টিজ
    public User? User { get; set; }
    public Role? Role { get; set; }
}
