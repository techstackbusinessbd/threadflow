# 🛡️ রুলবুক ০৫: আইডেন্টিটি, পলিসি-বেসড অথোরাইজেশন ও ডেটা স্কোপ ইঞ্জিন (Security, Auth & Data Scope)

> **দায়িত্বপ্রাপ্ত লিড**: মায়া (QA, Security & Reliability Engineer) & নাবিলা (Lead BA)  
> **ক্যাটাগরি**: Keycloak OIDC, ASP.NET Core Policy-Based Auth, Fine-Grained Permissions, 7-Tier Data Scope Engine  
> **ভার্সন**: 2.0.0 (Officially Aligned with Technology Stack SRS) | **স্ট্যাটাস**: অলঙ্ঘনীয় নিয়ম (Non-Negotiable)

---

## ১. আইডেন্টিটি বনাম অথোরাইজেশন পৃথকীকরণ (Identity vs Authorization Boundary)

```
[ব্রাউজার / ইউজার]
       │
       ▼
1. Authentication (Keycloak OIDC) ──> "আপনি কে?" (Rahim, Production Manager)
       │
       ▼
2. System Permission (PostgreSQL) ──> "আপনার কি অ্যাকশন পারমিশন আছে?" (production.output.approve)
       │
       ▼
3. Data Scope Engine (Org Hierarchy)──> "আপনি কোন ফ্যাক্টরি ও লাইনের ডেটা এক্সেস করছেন?" (Factory-01, Lines 01-10)
       │
       ▼
4. Workflow Policy (State Machine)──> "ডকুমেন্টের বর্তমান স্টেট কি ভ্যালিড?" (Status == SUBMITTED, QC == PASS)
       │
       ▼
5. Segregation of Duties (SoD)    ──> "ক্রিয়েটর আর এপ্রুভার কি ভিন্ন ব্যক্তি?" (Creator != Approver)
       │
       ▼
6. Immutable Audit Trail (Audit)  ──> "Old Value, New Value, IP, Timestamp অডিট লগে সেভ"
       │
       ▼
[অপারেশন সফল / ৪০৩ Forbidden]
```

- **Keycloak-এর দায়িত্ব**: শুধুমাত্র সেন্ট্রালাইজড আইডেন্টিটি, SSO, MFA, এবং গ্লোবাল রোল।
- **ERP ডেটাবেজের দায়িত্ব**: ৩,০০০+ ফাইন-গ্রেইন্ড পারমিশন, ৭-লেয়ার ডেটা স্কোপ এবং অ্যাপ্রুভাল লিমিট ম্যাট্রিক্স।

---

## ২. ৭-লেয়ার অর্গানাইজেশনাল ডেটা স্কোপ ইঞ্জিন (7-Tier Data Scope Engine)

$$\text{Company} \longrightarrow \text{Business Unit} \longrightarrow \text{Factory} \longrightarrow \text{Building} \longrightarrow \text{Floor} \longrightarrow \text{Section} \longrightarrow \text{Line}$$

1. **সার্ভার সাইড ফিল্টারিং বাধ্যতামূলক**:
   - ফ্রন্টএন্ড থেকে আসা কোনো `factory_id` বা `line_id` অন্ধভাবে বিশ্বাস করা সম্পূর্ণ নিষিদ্ধ।
   - ব্যাকএন্ডের `DataScopeRequirementHandler` স্বয়ংক্রিয়ভাবে ইউজারের বৈধ স্কোপ ডেটাবেজ কুয়েরিতে ইনজেক্ট করবে:
     ```sql
     SELECT * FROM production_outputs 
     WHERE factory_id = @UserAssignedFactoryId 
       AND line_id IN (@UserAssignedLines);
     ```
2. **Deny-by-Default রুল**:
   - কোনো ইউজারের কোনো নির্দিষ্ট ফ্যাক্টরি বা লাইনের জন্য স্পষ্ট অ্যাসাইনমেন্ট না থাকলে এক্সেস সম্পূর্ণ ব্লক (`403 Forbidden`) হবে।

---

## ৩. ফাইন-গ্রেইনড পারমিশন ট্যাক্সোনমি (Permission Taxonomy)

সমস্ত পারমিশন সুনির্দিষ্ট ৩-অংশের ফরম্যাটে থাকবে:
$$\mathbf{\langle module\rangle.\langle resource\rangle.\langle action\rangle}$$

- `merchandising.order.create`
- `cutting.plan.approve`
- `cutting.bundle.transfer`
- `sewing.output.log`
- `quality.inspection.approve`
- `admin.role.assign`

### এন্ডপয়েন্ট প্রটেকশন স্ট্যান্ডার্ড (ASP.NET Core):
```csharp
[Authorize(Policy = "permission:cutting.plan.approve")]
[HttpPost("{id:guid}/approve")]
public async Task<IActionResult> ApproveCuttingPlan(Guid id, [FromBody] ApprovePlanCommand command)
{
    command.PlanId = id;
    var result = await _mediator.Send(command);
    return Ok(result);
}
```

---

## ৪. সেগ্রিগেশন অব ডিউটিস ও অ্যাপ্রুভাল ম্যাট্রিক্স (SoD & Limits)

1. **Creator $\neq$ Approver নীতি**:
   - মার্চেন্ডাইজার যে কস্টিং বানিয়েছে, সে নিজে তা এপ্রুভ করতে পারবে না (`CreatorId != CurrentUserId`)।
2. **ডায়নামিক অ্যাপ্রুভাল লিমিট**:
   - ট্রানজ্যাকশন কোয়ান্টিটি বা অ্যামাউন্ট অনুযায়ী অ্যাপ্রুভাল অথরিটি পলিসি রুল দ্বারা নিয়ন্ত্রিত হবে:
     - অ্যাডজাস্টমেন্ট $\le 100$ pcs $\rightarrow$ Manager Approval
     - অ্যাডজাস্টমেন্ট $> 100$ pcs $\rightarrow$ DGM / GM Approval

---

## ৫. ফরেনসিক অডিট লগিং স্ট্যান্ডার্ড (Immutable Audit Trail)

- প্রতিটি কনফিগারেশন চেঞ্জ, রোল অ্যাসাইনমেন্ট, অর্ডার এপ্রুভাল বা স্টক অ্যাডজাস্টমেন্টে স্বয়ংক্রিয়ভাবে `audit_logs` টেবিলে রেকর্ড হবে:
  `{ id, user_id, action, module, resource_type, resource_id, old_value, new_value, ip_address, user_agent, correlation_id, created_at }`।
- কোনো অডিট রেকর্ড এডিট বা ডিলিট করা আর্কিটেকচারালি অসম্ভব।
