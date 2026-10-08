# 🏛️ রুলবুক ০১: সিস্টেম আর্কিটেকচার ও মডুলার মনোলিথ স্ট্যান্ডার্ডস (Architecture & Modular Monolith)

> **দায়িত্বপ্রাপ্ত লিড**: তানভীর (Principal Architect & Tech Lead)  
> **ক্যাটাগরি**: সিস্টেম আর্কিটেকচার, ডোমেন-ড্রাইভেন ডিজাইন (DDD) ও মডুলারিটি  
> **ভার্সন**: 1.0.0 | **স্ট্যাটাস**: অলঙ্ঘনীয় নিয়ম (Non-Negotiable)

---

## ১. আর্কিটেকচারাল ফিলোসফি (The Core Philosophy)
ThreadFlow ERP একটি **Domain-Driven Modular Monolith**। এর অর্থ হলো:
- সম্পূর্ণ কোডবেস একটি ইউনিফাইড রিপোজিটরিতে থাকবে যা সহজে ডেপ্লয় ও টেস্ট করা যায়।
- কিন্তু লজিক্যালি ১০টি ডোমেন থাকবে সম্পূর্ণ স্বাধীন এবং স্বয়ংসম্পূর্ণ বাউন্ডেড কনটেক্সট (Bounded Context) হিসেবে।
- কোনো অবস্থাতেই সিস্টেম স্প্যাগেটি কোডে পরিণত হতে দেওয়া যাবে না।

---

## ২. ডোমেন আইসোলেশন ও জিরো ক্রস-ডিবি এক্সেস রুল (The Zero Direct DB Access Law)

```
[ Domain 04: Cutting ] ──────❌ DIRECT DB QUERY ──────> [ Domain 01: Merchandising DB ] (FORBIDDEN!)
         │
         │ (APPROVED METHOD 1: Public Module Service)
         ▼
[ CuttingService ] ────────> [ MerchandisingPublicService.getApprovedBom(styleId) ]
         │
         │ (APPROVED METHOD 2: Async Domain Event)
         ▼
[ EventBus.publish(BUNDLE_CREATED) ] ───> [ SewingEventHandler / FinanceEventHandler ]
```

### কঠোর নিয়মাবলী:
1. **কোনো ডোমেন সার্ভিস অন্য কোনো ডোমেনের ডেটাবেজ টেবিল সরাসরি কুয়েরি (`SELECT`/`UPDATE`/`JOIN`) করতে পারবে না**:
   - উদাহরণ: `CuttingService` কখনোই সরাসরি `prisma.styleBom.findFirst()` করতে পারবে না।
   - তাকে অবশ্যই `MerchandisingModule`-এর এক্সপোর্ট করা `MerchandisingPublicService.getBomDetails(bomId)` মেথড কল করতে হবে।
2. **প্রতিটি ডোমেন মডিউলের একটি `PublicService` থাকবে**:
   - যা অন্য ডোমেনের জন্য একটি পরিষ্কার কন্ট্রাক্ট বা ইন্টারফেস হিসেবে কাজ করবে। ইন্টারনাল প্রাইভেট সার্ভিসগুলো মডিউলের বাইরে এক্সপোর্ট হবে না।
3. **ক্রস-ডোমেন নোটিফিকেশনে ইভেন্ট-বাস বাধ্যতামূলক**:
   - যখন কোনো ডোমেনে কোনো গুরুত্বপূর্ণ স্টেট পরিবর্তন হবে (যেমন: কাটিংয়ে বান্ডেল তৈরি বা ফিনিশিংয়ে প্যাকিং সম্পূর্ণ), তখন সিঙ্ক্রোনাস ডিপেন্ডেন্সির বদলে ডোমেন ইভেন্ট এমিট করতে হবে (`EventEmitter2` বা RabbitMQ)।

---

## ৩. ৪-লেয়ার ক্লিন আর্কিটেকচার লেয়ারিং রুল (The 4-Layer Clean Separation)

প্রতিটি ডোমেন মডিউলের ভেতর নিচের ৪টি লেয়ার কঠোরভাবে বজায় রাখতে হবে:

```
apps/api/src/modules/domain-01-merchandising/
  ├── domain/             # Layer 1: পিওর ডোমেন এন্টিটি, রুলস, স্টেট মেশিন ও গাণিতিক সূত্র (No Framework/DB)
  │   ├── entities/
  │   ├── value-objects/
  │   └── rules/          # যেমন: consumption-calculator.ts, bom-versioning-rules.ts
  ├── application/        # Layer 2: ইউজ-কেস ও সার্ভিস অর্কেস্ট্রেশন, DTOs
  │   ├── services/       # যেমন: create-techpack.service.ts
  │   ├── dtos/           # যেমন: create-bom.dto.ts
  │   └── ports/          # ইনপুট/আউটপুট ইন্টারফেসেস
  ├── infrastructure/     # Layer 3: ডেটাবেজ রিপোজিটরি, ওআরএম অ্যাডাপ্টার, থার্ড পার্টি ইন্টিগ্রেশন
  │   ├── repositories/   # যেমন: prisma-bom.repository.ts
  │   └── adapters/
  └── presentation/       # Layer 4: কন্ট্রোলার, ইভেন্ট লিসেনার ও পাবলিক গেটওয়ে
      ├── controllers/    # যেমন: bom.controller.ts (শুধুমাত্র HTTP ও DTO ভ্যালিডেশন)
      └── public/         # merchandising.public-service.ts
```

### লেয়ারিং নিষেধাজ্ঞা:
- **Layer 1 (Domain)**: কোনো ডেটাবেজ ইম্পোর্ট (`@prisma/client`), NestJS ডেকোরেটর (`@Injectable`) বা থার্ড-পার্টি লাইব্রেরি থাকতে পারবে না। এটি পিওর জাভাস্ক্রিপ্ট/টাইপস্ক্রিপ্ট লজিক।
- **Layer 4 (Presentation / Controllers)**: কোনো বিজনেস ক্যালকুলেশন কন্ট্রোলারের ভেতর লেখা সম্পূর্ণ নিষিদ্ধ। কন্ট্রোলার শুধু রিকোয়েস্ট গ্রহণ করবে, গার্ড চালাবে এবং অ্যাপ্লিকেশান সার্ভিস কল করবে।

---

## ৪. ডিপেন্ডেন্সি ইনজেকশন ও ইনভার্সন অব কন্ট্রোল (IoC Rules)
1. **জিরো ডিরেক্ট ইনস্ট্যান্সিয়েশন**:
   - কোডের ভেতর কোথাও `const svc = new MyService()` বা `const db = new Database()` লেখা সম্পূর্ণ নিষিদ্ধ।
   - সব ডিপেন্ডেন্সি NestJS কন্টেইনারের মাধ্যমে কন্সট্রাক্টরে ইনজেক্ট করতে হবে:
     ```typescript
     constructor(
       private readonly bomCalculator: BomCalculatorDomainService,
       private readonly bomRepo: BomRepositoryPort,
     ) {}
     ```
2. **সার্কুলার ডিপেন্ডেন্সি নিষিদ্ধ (Zero Circular Dependency)**:
   - মডিউল এ যদি মডিউল বি-কে কল করে, তবে মডিউল বি কখনোই সরাসরি মডিউল এ-কে কল করতে পারবে না। 
   - এমন প্রয়োজন হলে তাদের শেয়ার্ড ইভেন্ট বাসের মাধ্যমে ডিকাপল করতে হবে।

---

## ৫. মাইক্রোসার্ভিস রেডি এক্সট্রাকশন রুল (Extraction Readiness)
ThreadFlow মনোলিথ হিসেবে শুরু হলেও এর প্রতি ডোমেন এমনভাবে তৈরি হবে যাতে ভবিষ্যতে যেকোনো দিন নির্দিষ্ট কোনো ডোমেনকে (যেমন: ডোমেন ০৫ সুইং ফ্লোর আইওটি) ১ দিনের মধ্যে আলাদা মাইক্রোসার্ভিসে কনভার্ট করা যায়। 
- কোনো গ্লোবাল অবজেক্ট বা শেয়ার্ড স্টেট থাকা নিষিদ্ধ।
- ডোমেন কনফিগারেশন প্রতিটি মডিউলের নিজস্ব স্কোপে সীমাবদ্ধ থাকবে।
