# ⚡ রুলবুক ০৩: ব্যাকএন্ড ASP.NET Core ও ক্লিন আর্কিটেকচার স্ট্যান্ডার্ডস (Backend & Clean Architecture)

> **দায়িত্বপ্রাপ্ত লিড**: আসিফ (Senior Core & Full-Stack Engineer) & তানভীর (Principal Architect)  
> **ক্যাটাগরি**: ASP.NET Core 10, .NET 10 / C# 14 (.NET 8/9 LTS Baseline), Clean Architecture, MediatR, CQRS, FluentValidation  
> **ভার্সন**: 2.0.0 (Officially Aligned with Technology Stack SRS) | **স্ট্যাটাস**: অলঙ্ঘনীয় নিয়ম (Non-Negotiable)

---

## ১. .NET ক্লিন আর্কিটেকচার ও ৪-লেয়ার অর্গানাইজেশন

```
ThreadFlow.sln
├── ThreadFlow.Domain/         # পিওর বিজনেস ডোমেন (Entities, Value Objects, Domain Events, Enums)
├── ThreadFlow.Application/    # Use Cases, CQRS (MediatR), DTOs, FluentValidation, Interfaces
├── ThreadFlow.Infrastructure/ # EF Core DbContext, Npgsql, Dapper, Redis, MinIO, Keycloak, Hangfire
└── ThreadFlow.WebApi/         # Controllers, Middlewares, Policy Handlers, OpenAPI, Swagger
```

- **ডোমেন লেয়ারের স্বাধীনতা**: `ThreadFlow.Domain` সম্পূর্ণ ফ্রেমওয়ার্ক ও ডাটাবেজ ডিপেন্ডেন্সি মুক্ত থাকবে।
- **থিন কন্ট্রোলার রুল (Thin Controllers)**: কন্ট্রোলারে কোনো বিজনেস লজিক থাকবে না; কন্ট্রোলার কেবল HTTP রিকোয়েস্ট গ্রহণ করবে এবং `IMediator.Send(command)` কল করবে।

---

## ২. রিকোয়েস্ট পাইপলাইন ও কঠোর ভ্যালিডেশন (FluentValidation)

1. **মেডিয়েটর পাইপলাইন অটোমেশন**:
   - প্রতিটি কমান্ড ও কুয়েরিতে `ValidationBehavior<TRequest, TResponse>` স্বয়ংক্রিয়ভাবে এক্সিকিউট হবে।
   - ইনভ্যালিড ডাটা হ্যান্ডলারে পৌঁছানোর আগেই `ValidationException` থ্রো হবে।
2. **কঠোর FluentValidation রুলস**:
   ```csharp
   public class CreateCuttingPlanCommandValidator : AbstractValidator<CreateCuttingPlanCommand>
   {
       public CreateCuttingPlanCommandValidator()
       {
           RuleFor(x => x.StyleId)
               .NotEmpty().WithMessage("Style ID is mandatory.");

           RuleFor(x => x.FabricRollIds)
               .NotEmpty().WithMessage("At least one fabric roll must be selected.");

           RuleFor(x => x.TotalPcs)
               .GreaterThan(0).WithMessage("Planned quantity must be greater than zero.");
       }
   }
   ```

---

## ৩. ইউনিফাইড এরর রেসপন্স (RFC 7807 / 9457 ProblemDetails)

- সিস্টেমে কোনো অবস্থাতেই কাঁচা ডেটাবেজ এক্সেপশন বা ইন্টারনাল স্ট্যাক-ট্রেস ক্লায়েন্টে লিক হতে পারবে না।
- প্রতিটি এক্সেপশন গ্লোবাল `ExceptionHandlingMiddleware`-এর মাধ্যমে ইউনিফাইড RFC 7807 ফরম্যাটে রিটার্ন হবে:
  ```json
  {
    "type": "https://threadflow.erp/errors/validation-failed",
    "title": "Validation Failed",
    "status": 400,
    "code": "VALIDATION_ERROR",
    "detail": "One or more validation errors occurred.",
    "traceId": "00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01",
    "errors": {
      "TotalPcs": ["Planned quantity must be greater than zero."]
    }
  }
  ```
- **১০০% প্রফেশনাল ইংরেজি**: সমস্ত সিস্টেম মেসেজ ও এরর ডেসক্রিপশন আন্তর্জাতিক প্রফেশনাল বিজনেস ইংরেজিতে হবে।

---

## ৪. বাধ্যতামূলক পেজিনেশন ও কুয়েরি ফিল্টারিং (Paged Result Standard)

1. **লিস্ট এন্ডপয়েন্টে পেজিনেশন বাধ্যতামূলক**:
   - সমস্ত কালেকশন এন্ডপয়েন্টে `pageNumber`, `pageSize` (ডিফল্ট: ২০, সর্বোচ্চ: ১০০) বাধ্যতামূলক:
   ```csharp
   public record PagedResult<T>(
       IReadOnlyList<T> Items,
       int PageNumber,
       int PageSize,
       int TotalCount,
       int TotalPages);
   ```
2. **সার্ভার সাইড ফিল্টারিং ও সর্টিং**:
   - ক্লায়েন্ট সাইড ফিল্টারিং নয়; সব সর্ট ও ফিল্টার প্যারামিটার ব্যাকএন্ড SQL কুয়েরিতে এক্সিকিউট হবে।

---

## ৫. কোডে বাধ্যতামূলক সহজ বাংলা কমেন্ট নীতি (Mandatory Clear Bangla Code Comments Law)

বসের সুনির্দিষ্ট ও অলঙ্ঘনীয় ডিরেক্টিভ:
1. **প্রতিটি ক্লাস ও মেথডে সহজ বাংলা কমেন্ট**:
   - কোডের ক্লাস, ইন্টারফেস, সার্ভিস মেথড, ডিটিও এবং বিজনেস লজিকে স্পষ্ট ও প্রাঞ্জল বাংলায় XML ডক (`/// <summary> ... </summary>`) অথবা ইনলাইন কমেন্ট (`// ...`) লিখতে হবে।
2. **মেইনটেইনেবিলিটি নিশ্চয়তা**:
   - কমেন্ট এমন সহজ ভাষায় হবে যেন যেকোনো নতুন বা জুনিয়র ডেভেলপার দেখেই বুঝতে পারে—কোন লজিক কেন লেখা হয়েছে, কী ইনপুট নিচ্ছে এবং কী আউটপুট দিচ্ছে।
   - কোড আন্তর্জাতিক নিয়মে ক্লিন সি-শার্প হবে, কিন্তু তার ডকুমেন্টেশন ও কমেন্ট বাংলায় ক্রিস্টাল-ক্লিয়ার থাকবে।
