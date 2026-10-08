# 🚀 রুলবুক ০৮: ডেভঅপস, ডকার ও গিট ওয়ার্কফ্লো স্ট্যান্ডার্ডস (DevOps, Docker & Git)

> **দায়িত্বপ্রাপ্ত লিড**: কবীর (DevOps, Cloud Infrastructure & SRE)  
> **ক্যাটাগরি**: Docker Compose, কনটেইনার হাইজিন, গিট কনভেনশন ও সিআই/সিডি  
> **ভার্সন**: 1.0.0 | **স্ট্যাটাস**: অলঙ্ঘনীয় নিয়ম (Non-Negotiable)

---

## ১. ১০০% ডকার-বেইজড ও জিরো লোকাল রিসোর্স রুল (100% Docker-Based Architecture Law)

বসের সুস্পষ্ট নির্দেশনা: **"full project docker based hoba local pc er kono resource use hoba na local pc te docker install asa sei docker use hoba"**।

### মূল ইঞ্জিনিয়ারিং নীতিমালা:
1. **জিরো হোস্ট ডিপেনডেন্সি (Zero Host Pollution)**:
   - ডেভেলপারের লোকাল পিসিতে কোনো ডেটাবেজ (PostgreSQL), ক্যাশ (Redis), অথ (Keycloak) বা আলাদা রানটাইম সরাসরি হোস্ট সিস্টেমে ইনস্টল বা ডিপেন্ড করা সম্পূর্ণ নিষিদ্ধ।
   - লোকাল পিসিতে শুধুমাত্র **Docker Desktop** ইনস্টল থাকবে এবং প্রজেক্টের সমস্ত কিছু কন্টেইনারের ভেতর চলবে।
2. **ফুল-স্ট্যাক কন্টেইনারাইজেশন (`docker-compose.yml`)**:
   - `threadflow-postgres`: PostgreSQL 16+
   - `threadflow-redis`: Redis 7.2 / Valkey
   - `threadflow-keycloak`: Keycloak 24+ OIDC Identity Server
   - `threadflow-minio`: MinIO S3 Object Storage
   - `threadflow-mailpit`: Local SMTP Server
   - `threadflow-backend`: ASP.NET Core 10 / .NET 10 WebApi Container (With Live Volume Mount for Hot Reload)
   - `threadflow-frontend`: React 19 + TypeScript + Vite Container (With Live Volume Mount for HMR)
3. **সিঙ্গেল-কমান্ড ডেভেলপমেন্ট বুটস্ট্র্যাপ**:
   - যেকোনো মেশিনে প্রজেক্ট রান করতে মাত্র ১টি কমান্ড দিতে হবে:
     ```bash
     docker compose up --build -d
     ```
4. **পারসিস্টেন্ট ভলিউম (Named Volumes)**:
   - ডেটাবেজ ও স্টোরেজের ডেটা কন্টেইনার রিস্টার্টে ডিলিট হবে না। `postgres_data`, `redis_data`, `minio_data` ভলিউম দিয়ে পারসিস্ট করবে।
5. **বাধ্যতামূলক হেলথ-চেক (Health Checks)**:
   - ব্যাকএন্ড চালু হওয়ার আগে পোস্টগ্রেস ও রেডিস প্রস্তুত হয়েছে কিনা তা ডকার কম্পোজ `depends_on: { condition: service_healthy }` দিয়ে চেক করবে।
6. **লাইটওয়েট অফিশিয়াল ইমেজ**:
   - ভারী ইমেজের বদলে শুধুমাত্র অফিশিয়াল আলপাইন ইমেজ ব্যবহার করতে হবে: `postgres:16-alpine`, `redis:7.2-alpine`, `mcr.microsoft.com/dotnet/sdk:8.0-alpine`।

---

## ২. কনভেনশনাল কমিট ও গিট ডিসিপ্লিন (Conventional Commits Law)

গিট কমিট হিস্ট্রি হবে প্রফেশনাল ও সুস্পষ্ট। কোনো অস্পষ্ট মেসেজ (যেমন: `"update"`, `"fixed bug"`, `"wip"`) সম্পূর্ণ নিষিদ্ধ।

### কমিট ফরম্যাট:
`<type>(<scope>): <short description in present tense>`

| টাইপ | ব্যবহার ক্ষেত্র | বাস্তব উদাহরণ |
| :--- | :--- | :--- |
| `feat` | নতুন ডোমেন ফিচার বা এপিআই | `feat(merchandising): implement reverse T&A critical path calculator` |
| `fix` | বিদ্যমান কোনো বাগ বা ইস্যু ফিক্স | `fix(cutting): resolve ply height boundary check on knit lays` |
| `refactor` | লজিক পরিবর্তন ছাড়া কোড ক্লিনআপ | `refactor(finance): optimize double-entry journal voucher query` |
| `test` | ইউনিট বা ইন্টিগ্রেশন টেস্ট সংযোজন | `test(payroll): add 100% unit tests for overtime pay formula` |
| `docs` | আর্কিটেকচার বা স্পেক্স ডকুমেন্টেশন | `docs(architecture): update ADR for .NET 10 and React 19 stack` |
| `chore` | প্যাকেজ আপডেট বা কনফিগ পরিবর্তন | `chore(deps): update EF Core to version 10` |

---

## ৩. ব্রাঞ্চিং স্ট্র্যাটেজি ও অ্যাটমিক স্প্রিন্ট রুল (Branching Strategy)
1. `main`: প্রোডাকশন-রেডি স্টেবল কোড। সরাসরি মেইনে পুশ সম্পূর্ণ ব্লক থাকবে।
2. `develop`: কারেন্ট স্প্রিন্টের সমন্বিত কোডবেস।
3. `feat/<domain>-<feature-name>`: নির্দিষ্ট ফিচারের জন্য শাখা (যেমন: `feat/domain-01-bom-builder`)।
4. প্রতিটি PR মার্জ হওয়ার আগে মায়ার অটোমেটেড টেস্ট এবং তানভীরের কোড রিভিউ বাধ্যতামূলক।
