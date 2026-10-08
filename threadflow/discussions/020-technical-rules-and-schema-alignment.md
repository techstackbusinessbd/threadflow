# 📝 ডিসকাশন নং ০২০: টেকনিক্যাল রুলবুক আধুনিকায়ন ও কোর ডেটাবেজ স্কিমা সাইন-অফ
### *Technical Rules Modernization & Core Database Schema Blueprint Alignment*

- **তারিখ**: ০৮ অক্টোবর, ২০২৬
- **টাইপ**: টেকনিক্যাল রুলবুক সিঙ্ক্রোনাইজেশন ও ডেটাবেজ স্কিমা সাইন-অফ
- **উপস্থিতি**:
  - 👑 বস (Founder / CTO / Head of Engineering)
  - 🏛️ তানভীর (Principal Architect & Tech Lead)
  - 🎯 রাফি (Product & Delivery Lead)
  - 📋 নাবিলা (Lead Business Analyst & RMG Specialist)
  - ⚡ আসিফ (Senior Backend & API Lead)
  - 🎨 সজীব (Frontend & Product Experience Craftsman)
  - 🗄️ ফাহিম (Database Architect)
  - 🚀 কবীর (DevOps & SRE)
  - 🛡️ মায়া (QA & Security Specialist)
  - 📚 শাকিল (Documentation Specialist)

---

## ১. মিটিংয়ের প্রেক্ষাপট ও বসের নির্দেশনার বাস্তবায়ন
বসের নির্দেশনা অনুযায়ী:
> *"all documents deep analysis koro & kono issue thakla amaka bolo"* এবং পরবর্তীতে *"proceed"*।

টিমের ডিপ অডিটে যে সমস্ত পুরনো টেকনিক্যাল রুলস এবং ড্রাফট অসঙ্গতি চিহ্নিত হয়েছিল, পুরো টিম যৌথভাবে সেগুলোকে সম্পূর্ণ আধুনিকায়ন ও সিঙ্ক্রোনাইজ করেছে।

---

## ২. সম্পন্ন হওয়া কারিগরি সিঙ্ক ও ডকুমেন্টেড অ্যাকশনসমূহ

1. **রুলবুক আধুনিকায়ন সম্পন্ন**:
   - `02-database-transactions-and-orm.md`: EF Core 10 ট্রানজ্যাকশন, Dapper কোয়েরি, C# `decimal` এবং PostgreSQL UUIDv7 стандарта আপডেট।
   - `03-backend-dotnet-and-clean-architecture.md`: ASP.NET Core 10, C# 14, Clean Architecture, MediatR, FluentValidation, ও RFC 7807 ProblemDetails স্ট্যান্ডার্ডে রূপান্তর।
   - `04-frontend-react-vite-and-aggrid.md`: React 19, Vite (SPA), AG Grid Enterprise, TanStack Query v5, ও Redux Toolkit আর্কিটেকচারে রূপান্তর।
   - `05-security-auth-and-rbac.md`: Keycloak OIDC আইডেন্টিটি, ASP.NET Core পলিসি-বেসড অথোরাইজেশন ও ৭-লেয়ার ডেটা স্কোপ ইঞ্জিনে রূপান্তর।
   - `08-devops-docker-and-git-workflow.md`: .NET 10 ও Nginx Vite বিল্ড ডকার ইমেজে রূপান্তর।
   - `.agents/rules/` এবং `threadflow/rules/` উভয় ডিরেক্টরিতে ১০০% মিররিং ও সিঙ্ক সম্পন্ন।
2. **কোর ডেটাবেজ স্কিমা স্পেসিফিকেশন প্রণয়ন সম্পন্ন**:
   - [11-core-database-schemas-and-entity-dictionary.md](../docs/11-core-database-schemas-and-entity-dictionary.md) ফাইলে ৭-লেয়ার অর্গানাইজেশন টেবিল (`companies` $\rightarrow$ `lines`), Keycloak সিঙ্ক টেবিল (`users`), ফাইন-গ্রেইনড পারমিশন ও ৭-লেয়ার ডেটা স্কোপ (`user_data_scopes`), ডাইনামিক UI লেবেল (`ui_label_dictionaries`), এবং অটো-কোড জেনারেটর ইঞ্জিনের শতভাগ DDL ও ERD প্রস্তুত করা হয়েছে।
3. **ডকুমেন্টেশন রেফারেন্স আপডেট**:
   - `docs/03`, `docs/05`, এবং `docs/08` ফাইলগুলো থেকে সমস্ত পুরনো ড্রাফট টেক্সট ক্লিন করে নতুন স্পেকের সাথে সামঞ্জস্যপূর্ণ করা হয়েছে।

---

## ৩. বর্তমান প্রজেক্ট স্ট্যাটাস ও পরবর্তী ধাপ

- **ডকুমেন্টেশন স্ট্যাটাস**: **সম্পূর্ণ সামঞ্জস্যপূর্ণ ও ১০০% সিঙ্গেল সোর্স অব ট্রুথ (Single Source of Truth) প্রতিষ্ঠিত ✅**।
- **কোডিং স্ট্যাটাস**: বসের নির্দেশ অনুযায়ী কোডিং আপাতত হোল্ডে রয়েছে।
- **পরবর্তী সম্ভাব্য ধাপ**:
  - API কন্ট্রাক্ট ও DTO স্পেসিফিকেশন (`docs/12-api-contracts-and-payload-specifications.md`) প্রণয়ন।
  - বসের চূড়ান্ত গ্রিন সিগন্যাল পেলেই .NET Clean Architecture সলিউশন ও React 19 Vite রিপোজিটরি স্ক্যাফোল্ডিং শুরু করা।

---

> **অফিসিয়াল ডকুমেন্ট**: `threadflow/docs/11-core-database-schemas-and-entity-dictionary.md`  
> **অনুমোদনকারী**: বস (Founder / CTO / Head of Engineering)
