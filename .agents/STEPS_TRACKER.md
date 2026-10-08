# 🧭 ThreadFlow ERP — গেটেড স্টেপ-বাই-স্টেপ এক্সিকিউশন রোডম্যাপ
### *Gate-Locked Sequential Step Tracker & Zero-Bypass Protocol*

> **উদ্দেশ্য**: প্রজেক্টের প্রতিটি স্টেপ দৃশ্যমান রাখা, কোনো স্টেপ যাতে বাদ বা মিস না পড়ে এবং বসের সুনির্দিষ্ট অনুমোদন ছাড়া কোনো অবস্থাতেই পরবর্তী স্টেপে জাম্প/বাইপাস না করা যায় তা নিশ্চিত করা।  
> **দায়িত্বপ্রাপ্ত সমন্বয়ক**: 🎯 রাফি (Chief Project Coordinator & SPOC)  
> **সর্বোচ্চ অনুমোদনকারী**: 👑 বস (Founder / CTO / Head of Engineering)

---

## 🔒 গেটেড ফ্রেমওয়ার্কের অলঙ্ঘনীয় নিয়মাবলী (Zero-Bypass Laws)

1. **একবারে একটি মাত্র স্টেপ (Strictly Single Active Step)**:
   - পুরো প্রজেক্টে যেকোনো মুহূর্তে শুধুমাত্র **১টি মাত্র স্টেপ** `🔄 IN PROGRESS` থাকতে পারবে।
   - বাকি পরবর্তী সকল স্টেপ বাধ্যতামূলকভাবে `🔒 LOCKED` থাকবে।
2. **পূর্বশর্ত ছাড়া আনলক সম্পূর্ণ নিষিদ্ধ (No Bypass Guarantee)**:
   - পূর্ববর্তী স্টেপ ১০০% সম্পন্ন এবং টেস্টে প্রমাণিত না হওয়া পর্যন্ত পরবর্তী স্টেপের তালা খোলা সম্পূর্ণ নিষিদ্ধ।
3. **৩-পয়েন্ট কমপ্লিশন গেট (Definition of Done - DoD)**:
   প্রতিটি স্টেপ শেষ হতে হলে ৩টি প্রমাণ আবশ্যক:
   - [x] **কোড/কনফিগ এক্সিকিউশন**: কোনো সিনট্যাক্স বা রানটাইম এরর নেই।
   - [x] **মায়ার QA ভেরিফিকেশন**: সিকিউরিটি ও স্ট্যাবিলিটি পাস।
   - [x] **শাকিলের অডিট লগ**: পরিবর্তন রেকর্ড করা হয়েছে।
4. **বসের গেটওয়ে অনুমোদন (CTO Checkpoint Approval)**:
   - প্রতিটি স্টেপ শেষ হলে রাফি বসকে ফলাফল ও প্রুফ দেখাবে।
   - বসের সুস্পষ্ট বার্তা (যেমন: *"Step X.X Approved, go to Step X.Y"*) পাওয়ার পরেই কেবল পরবর্তী স্টেপ আনলক হবে।

---

## 📊 স্ট্যাটাস প্রতীক পরিচিতি (Legend)
- `✅ COMPLETED & VERIFIED`: সম্পন্ন, পরীক্ষিত এবং বসের দ্বারা অনুমোদিত।
- `🔄 IN PROGRESS`: বর্তমানে চলমান (টিম শুধুমাত্র এই নির্দিষ্ট কাজে নিয়োজিত)।
- `🔒 LOCKED`: তালাবদ্ধ (আগের স্টেপ শেষ না হওয়া পর্যন্ত এটিতে হাত দেওয়া সম্পূর্ণ নিষিদ্ধ)।

---

# 🗺️ মাস্টার এক্সিকিউশন রোডম্যাপ (Phase-wise Step Tracker)

---

### 🏛️ PHASE 0: সিস্টেম আর্কিটেকচার, ডোমেন ব্লুপ্রিন্ট ও গিট সেটআপ
> **স্ট্যাটাস**: `✅ 100% COMPLETED & VERIFIED`

- [x] **Step 0.1**: ১২টি পোশাক শিল্প ডোমেনের বিস্তারিত বিজনেস স্পেসিফিকেশন সম্পন্ন (`threadflow/business-processes/`)
- [x] **Step 0.2**: ১৪টি কোর টেকনিক্যাল আর্কিটেকচার ব্লুপ্রিন্ট ও SRS চূড়ান্ত (`threadflow/docs/`)
- [x] **Step 0.3**: ১০ জন বিশেষজ্ঞ ইঞ্জিনিয়ারের ডেডিকেটেড রুলবুক প্রণয়ন (`threadflow/rules/specialists/`)
- [x] **Step 0.4**: SaaS-Ready হাইব্রিড মাল্টি-টেন্যান্সি ও ১০০% ডকার আর্কিটেকচার লক
- [x] **Step 0.5**: গিট রিপোজিটরি ইনিশিয়ালাইজেশন, `.gitignore` কনফিগারেশন এবং GitHub রিমোটের সাথে `main` ও `develop` ব্রাঞ্চ সিঙ্ক

---

### 🐳 PHASE 1: ১০০% ডকার-বেইজড কনটেইনার ক্লাস্টার ও ইনফ্রাস্ট্রাকচার (`deploy/`)
> **স্ট্যাটাস**: `🔒 READY FOR CTO APPROVAL` | **দায়িত্বে**: 🚀 কবীর (DevOps Lead)

- [x] **Step 1.1**: **ডকার কম্পোজ ক্লাস্টার কনফিগারেশন ফাইলসমূহ প্রস্তুতকরণ**
  - `deploy/docker-compose.yml` (৭টি সার্ভিস: postgres, redis, keycloak, minio, mailpit, backend, frontend)
  - `deploy/.env.example` (এনভায়রনমেন্ট টেমপ্লেট)
  - `deploy/docker/postgres/init-db.sql` (UUIDv7 জেনারেটর ও কি-ক্লোক ডেটাবেজ প্রোভিশনিং)
  - `deploy/docker/keycloak/threadflow-realm.json` (Realm, Client ও Base Roles)
  - `deploy/docker/backend/Dockerfile.dev` (.NET 10 SDK উইথ `dotnet watch`)
  - `deploy/docker/frontend/Dockerfile.dev` (Node 22 উইথ Vite HMR)
  - *DoD*: সমস্ত কনফিগারেশন ফাইল ভ্যালিডেট করা এবং কোনো সিক্রেট লিক না থাকা।
  - **Status**: `✅ COMPLETED & VERIFIED (Syntax 100% Validated via docker compose config)`

- [x] **Step 1.2**: **ডকার ক্লাস্টার বুটস্ট্র্যাপ ও সার্ভিসেস হেলথ-চেক ভেরিফিকেশন**
  - কমান্ড: `docker compose up -d postgres redis keycloak minio mailpit`
  - পোস্টগ্রেস, রেডিস, কি-ক্লোক, মিনিও এবং মেইলপিট কন্টেইনার হেলথ ও পোর্ট বাইন্ডিং ভেরিফিকেশন।
  - *DoD*: প্রতিটি সার্ভিস `healthy` ও সচল থাকতে হবে (ভেরিফাইড: PostgreSQL 16 UUIDv7 টেস্টেড, Redis PONG, Keycloak realm imported, MinIO ready, Mailpit online)।
  - **Status**: `✅ COMPLETED & VERIFIED (All 5 containers running healthy & responsive)`

---

### ⚡ PHASE 2: .NET 10 ব্যাকএন্ড সলিউশন স্কাফোল্ডিং (Clean Architecture)
> **স্ট্যাটাস**: `🔒 READY FOR CTO APPROVAL` | **দায়িত্বে**: ⚡ আসিফ (Backend Lead) & 🏛️ তানভীর (Architect)

- [x] **Step 2.1**: **Clean Architecture সলিউশন কাঠামো তৈরি (`src/ThreadFlow.sln`)**
  - `ThreadFlow.Domain` (Core Entities, Value Objects, Domain Events, Enums)
  - `ThreadFlow.Application` (CQRS, MediatR, FluentValidation, DTOs, Interfaces)
  - `ThreadFlow.Infrastructure` (EF Core 10, Dapper, PostgreSQL, Redis, MinIO, Keycloak)
  - `ThreadFlow.WebApi` (ASP.NET Core REST API, Middlewares, SignalR Hubs)
  - *DoD*: সলিউশন জিরো এরর ও জিরো ওয়ার্নিংয়ে বিল্ড হতে হবে (ভেরিফাইড: `0 Warning(s), 0 Error(s)` in `00:00:11.13`).
  - **Status**: `✅ COMPLETED & VERIFIED (Clean Architecture .sln fully compiled)`

- [x] **Step 2.2**: **মাল্টি-টেন্যান্সি ও অর্গানাইজেশনাল কোর ডোমেন এনটিটি মডেলিং**
  - `Tenant`, `Company`, `BusinessUnit`, `Factory`, `Building`, `Floor`, `Section`, `ProductionLine`
  - `AuditableEntity` (Base entity with UUIDv7, Timestamps, Concurrency Token, `IsDemo` flag)
  - `ITenantScopedEntity` ইন্টারফেস।
  - সিকিউরিটি এনটিটিজ: `User`, `Role`, `Permission`, `RolePermission`, `UserRole`, `UserDataScope`
  - *DoD*: ডোমেন এনটিটি ভ্যালিডেশন এবং সলিউশন জিরো এররে বিল্ড হওয়া (ভেরিফাইড: `0 Warning(s), 0 Error(s)`).
  - **Status**: `✅ COMPLETED & VERIFIED (All Core Entities & Multi-Tenancy Models Compiled)`

- [x] **Step 2.3**: **EF Core 10 `ApplicationDbContext` ও গ্লোবাল কোয়েরি ফিল্টার সেটআপ**
  - `TenantId`, `IsDeleted`, এবং `IsDemo` গ্লোবাল কোয়েরি ফিল্টার ইনজেকশন।
  - `TenantInterceptor` ও `AuditableEntityInterceptor` (অটো অডিট ও সফট ডিলিট ট্র্যাকিং)।
  - PostgreSQL snake_case নেমিং ও কনকারেন্সি টোকেন ম্যাপিং।
  - *DoD*: মাল্টি-টেন্যান্ট আইসোলেশন ইন্টারসেপ্টর ও ডিবি কনটেক্সট টেস্ট সফল (ভেরিফাইড: `0 Warning(s), 0 Error(s)`).
  - **Status**: `✅ COMPLETED & VERIFIED (ApplicationDbContext & Multi-Tenant Interceptors Active)`

- [/] **Step 2.4**: **ইনিশিয়াল ডেটাবেজ মাইগ্রেশন ও বেস সিডিং ফ্রেমওয়ার্ক**
  - **Sub-step 2.4.1**: *কোর টেবিলসমূহের বিজনেস কলাম রিভিউ ও সিটিও অনুমোদন (One-by-One Review)*
    - [x] টেবিল ১: `tenants` (Approved & Domain Updated)
    - [x] টেবিল ২: `companies` (Approved & Domain Updated)
    - [x] টেবিল ৩: `business_units` (Approved & Domain Updated)
    - [x] টেবিল ৪: `factories` (Approved & Domain Updated)
    - [x] টেবিল ৫: `buildings` (Approved & Domain Updated)
    - [x] টেবিল ৬: `floors` (Approved & Domain Updated)
    - [x] টেবিল ৭: `sections` (Approved & Domain Updated)
    - [x] টেবিল ৮: `production_lines` (Approved & Domain Updated)
    - [x] টেবিল ৯: `users` (Approved & Domain Updated)
    - [x] টেবিল ১০: `roles` (Approved & Domain Updated)
    - [ ] টেবিল ১১: `permissions` (Next)
    - [ ] টেবিল ১২: `role_permissions`
    - [ ] টেবিল ১৩: `user_roles`
    - [ ] টেবিল ১৪: `user_data_scopes`
  - **Sub-step 2.4.2**: প্রথম মাইগ্রেশন জেনারেশন: `Initial_Core_Topology_And_Tenancy` (EF Core Code-First)
  - **Sub-step 2.4.3**: ডকার PostgreSQL কনটেইনারে মাইগ্রেশন রান ও ফিজিক্যাল টেবিল ভেরিফিকেশন
  - **Sub-step 2.4.4**: সিডিং ফ্রেমওয়ার্ক: রুট টেন্যান্ট (`Apex Group`), সিস্টেম সুপার অ্যাডমিন, কারেন্সি, UOM ও ডাইনামিক পারমিশন স্ক্যানার (`SystemBootstrapSeeder` vs `DevelopmentDemoSeeder` উইথ `SeedDemoData` ফ্ল্যাগ)
  - *DoD*: ডেটাবেজে স্কিমা তৈরি ও সফল সিডিং ভেরিফিকেশন (ভেরিফাইড ইন রানিং কনটেইনার)।
  - **Status**: `[/] IN PROGRESS (Sub-step 2.4.1 Active: 10/14 Tables Approved)`

- [ ] **Step 2.5**: **WebApi হোস্ট পাইপলাইন ও সিকিউরিটি মিডলওয়্যার কনফিগারেশন**
  - `TenantResolutionMiddleware` (`X-Tenant-Id` ও Subdomain রিজলভার)
  - RFC 7807 `GlobalExceptionMiddleware`
  - Swagger / OpenAPI 3.0 ডকুমেন্টেশন উইথ JWT Bearer Auth
  - *DoD*: `GET /health` এবং Swagger UI ব্রাউজারে সফল রেসপন্স প্রদান।
  - **Status**: `🔒 LOCKED (Blocked by Step 2.4)`

---

### 🎨 PHASE 3: React 19 ফ্রন্টএন্ড SPA স্কাফোল্ডিং (`apps/web`)
> **স্ট্যাটাস**: `🔒 LOCKED` | **দায়িত্বে**: 🎨 সজীব (Frontend Craftsman)

- [ ] **Step 3.1**: **Vite + React 19 + TypeScript প্রজেক্ট ইনিশিয়ালাইজেশন**
  - প্যাকেজ ডিপেনডেন্সি ও পাথ অ্যালাইয়েন্স (`@/*`) কনফিগারেশন।
  - **Status**: `🔒 LOCKED (Blocked by Phase 2)`

- [ ] **Step 3.2**: **টেইলউইন্ড সিএসএস ৪ (Tailwind v4 Oxide) ও OKLCH কালার প্যালেট সেটআপ**
  - `index.css`-এ থিম টোকেন, স্লেট নিউট্রালস এবং প্রিমিয়াম টাইপোগ্রাফি ইন্টিগ্রেশন।
  - **Status**: `🔒 LOCKED (Blocked by Step 3.1)`

- [ ] **Step 3.3**: **কোর রিইউজেবল ইউআই কম্পোনেন্ট লাইব্রেরি সেটআপ (`@threadflow/ui`)**
  - বাটন, ইনপুট, ভার্চুয়ালাইজড কম্বোবক্স, স্লাইড-ওভার ড্রয়ার, মোডাল।
  - **Status**: `🔒 LOCKED (Blocked by Step 3.2)`

- [ ] **Step 3.4**: **এন্টারপ্রাইজ AG Grid র‍্যাপার কম্পোনেন্ট ইমপ্লিমেন্টেশন**
  - SSRM (Server-Side Row Model) কানেক্টর ও কাস্টম সেল রেন্ডারার।
  - **Status**: `🔒 LOCKED (Blocked by Step 3.3)`

- [ ] **Step 3.5**: **অ্যাপ্লিকেশন শেল, এরগনোমিক লেআউট ও সাইডবার নেভিগেশন**
  - রেসপনসিভ ড্যাশবোর্ড লেআউট, হেডার, সাইডবার ও ব্রেডক্রাম্ব।
  - **Status**: `🔒 LOCKED (Blocked by Step 3.4)`

---

### 🛡️ PHASE 4: অথেন্টিকেশন, OIDC ও ৭-টায়ার ডেটা স্কোপ ইন্টিগ্রেশন
> **স্ট্যাটাস**: `🔒 LOCKED` | **দায়িত্বে**: ⚡ আসিফ, 🎨 সজীব, 🛡️ মায়া

- [ ] **Step 4.1**: Keycloak OIDC ব্যাকএন্ড ও ফ্রন্টএন্ড ইন্টিগ্রেশন (JWT Validation & Token Refresh)
- [ ] **Step 4.2**: ইউজার রোল ও গ্র্যানুলার পারমিশন গার্ড (`HasPermission` পলিসি)
- [ ] **Step 4.3**: ৮-লেয়ার ডেটা স্কোপ ফিল্টারিং ভেরিফিকেশন (কোম্পানি/ফ্যাক্টরি সুইচিং)
- [ ] **Step 4.4**: মায়ার ফুল সিকিউরিটি ও পেনিট্রেশন অডিট সাইন-অফ
- **Status**: `🔒 LOCKED`

---

## 📌 রানিং ফেজ ও গেট স্ট্যাটাস (Current Dashboard)

| ফেজ নম্বর | ফেজের বিবরণ | স্ট্যাটাস | দায়িত্বপ্রাপ্ত লিড |
| :---: | :--- | :---: | :---: |
| **Phase 0** | আর্কিটেকচার, স্পেক্স ও গিট ইনিশিয়ালাইজেশন | `✅ DONE` | পুরো টিম |
| **Phase 1** | ১০০% ডকার ক্লাস্টার কনফিগারেশন (`deploy/`) | `✅ DONE` | 🚀 কবীর |
| **Phase 2** | .NET 10 ব্যাকএন্ড সলিউশন (Clean Architecture) | `🔒 LOCKED` | ⚡ আসিফ |
| **Phase 3** | React 19 Vite ফ্রন্টএন্ড SPA (`apps/web`) | `🔒 LOCKED` | 🎨 সজীব |
| **Phase 4** | অথেন্টিকেশন ও ৭-লেয়ার ডেটা স্কোপ ইঞ্জিন | `🔒 LOCKED` | 🛡️ মায়া & ⚡ আসিফ |
