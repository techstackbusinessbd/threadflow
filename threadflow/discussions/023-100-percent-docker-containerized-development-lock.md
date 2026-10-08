# 📋 ডিসকাশন ০২৩: ১০০% ডকার-বেইজড আর্কিটেকচার ও জিরো হোস্ট পলুশন সাইন-অফ
### *Discussion 023: 100% Docker-Containerized Ecosystem & Zero Host Pollution Mandate*

- **তারিখ**: ৮ অক্টোবর ২০২৬
- **উপস্থিত সদস্যবৃন্দ**:
  - 👑 বস (Founder / CTO / Head of Engineering)
  - 🚀 কবীর (DevOps, Cloud Infrastructure & SRE)
  - 🏛️ তানভীর (Principal Architect & Tech Lead)
  - ⚡ আসিফ (Senior Core & Full-Stack Lead)
  - 🎨 সজীব (Frontend & Product Experience Craftsman)
  - 🗄️ ফাহিম (Database Architect & Data Engineer)
  - 🛡️ মায়া (QA, Security & Reliability Engineer)
  - 🎯 রাফি (Product & Delivery Manager)
  - 📋 নাবিলা (Lead BA & Domain Specialist)
  - 📚 শাকিল (Technical Writer & Documentation Specialist)

---

## ১. আলোচনার মূল সূত্র ও বসের অলঙ্ঘনীয় ডিরেক্টিভ

বসের সুনির্দিষ্ট ও চূড়ান্ত সিদ্ধান্ত:
> **"full project docker based hoba local pc er kono resource use hoba na local pc te docker install asa sei docker use hoba"**

অর্থাৎ, ডেভেলপারের লোকাল পিসিতে ডেটাবেজ, ক্যাশ, আইডেন্টিটি সার্ভার, অবজেক্ট স্টোরেজ এমনকি ব্যাকএন্ড ও ফ্রন্টএন্ড রানটাইমও সরাসরি হোস্ট অপারেটিং সিস্টেমে কোনো রিসোর্স বা ডিপেনডেন্সি ব্যবহার করবে না। লোকাল পিসিতে শুধুমাত্র **Docker Desktop** থাকবে এবং প্রজেক্টের সমস্ত সার্ভিস ডকার কন্টেইনারের ভেতর সুরক্ষিত ও আইসোলেটেডভাবে রান করবে।

---

## ২. টিমের টেকনিক্যাল অডিট ও সমাধান (DevOps Verification)

🚀 **কবীর (DevOps Lead)**:
> *"বস, আমরা লোকাল পিসি অডিট করেছি:*
> - **ডকার স্ট্যাটাস**: আপনার মেশিনে `Docker version 29.7.2` এবং `Docker Compose version v5.4.0` ইতিমধ্যে সফলভাবে ইনস্টল করা আছে (`Docker Desktop.exe` লোকেশন: `AppData\Local\Programs\DockerDesktop`)।
> - **জিরো হোস্ট ডিপেনডেন্সি স্ট্র্যাটেজি**:
>   1. হোস্ট সিস্টেমে কোনো লোকাল PostgreSQL, Redis, Keycloak বা MinIO সার্ভিস রান করার প্রয়োজন নেই।
>   2. আমরা `deploy/docker-compose.yml`-এ ব্যাকএন্ড (`threadflow-backend`) এবং ফ্রন্টএন্ড (`threadflow-frontend`)-কেও ফুল কন্টেইনার হিসেবে ডিফাইন করেছি।
>   3. লাইভ ডেভেলপমেন্টের জন্য আমরা ভলিউম মাউন্ট (`../src:/app/src` এবং `../apps/web:/app`) কনফিগার করেছি, ফলে হোস্ট পিসিতে ফাইল সেভ করলেই ডকার কন্টেইনারের ভেতরে .NET `dotnet watch` এবং Vite HMR স্বয়ংক্রিয়ভাবে ইনস্ট্যান্ট হট-রিলোড হবে!
>   4. একটি মাত্র কমান্ড: `docker compose up --build -d` দিলেই পুরো এন্টারপ্রাইজ সিস্টেম কন্টেইনার ক্লাস্টারে চালু হয়ে যাবে।"*

---

## ৩. ৭-সার্ভিস ফুল কন্টেইনারাইজড স্ট্যাক (`docker-compose.yml`)

```
                          ┌───────────────────────────┐
                          │   Host PC (Docker Desktop)│
                          └─────────────┬─────────────┘
                                        │ docker compose up -d
     ┌───────────────────────────┬──────┴────────────────────────┬───────────────────────────┐
     ▼                           ▼                               ▼                           ▼
┌──────────────┐        ┌──────────────────┐           ┌──────────────────┐        ┌──────────────────┐
│  PostgreSQL  │        │   Redis 7.2      │           │   Keycloak 24    │        │     MinIO S3     │
│  (Port 5432) │        │   (Port 6379)    │           │   (Port 8080)    │        │ (Port 9000/9001) │
└──────▲───────┘        └────────▲─────────┘           └────────▲─────────┘        └────────▲─────────┘
       │                         │                              │                           │
       └─────────────────────────┼──────────────────────────────┴───────────────────────────┘
                                 │ Internal Bridge Network
                        ┌────────┴─────────┐
                        │ .NET 10 WebApi   │ ◄─── Volume Mount: ./src (dotnet watch live reload)
                        │ (Port 5000)      │
                        └────────▲─────────┘
                                 │ REST / SignalR
                        ┌────────┴─────────┐
                        │ React 19 Vite    │ ◄─── Volume Mount: ./apps/web (Vite HMR live reload)
                        │ (Port 3000)      │
                        └──────────────────┘
```

| কন্টেইনার নাম | ইমেজ / টেকনোলজি | হোস্ট পোর্ট | মাউন্টেড ভলিউম / রোল |
| :--- | :--- | :---: | :--- |
| `threadflow-postgres` | `postgres:16-alpine` | `5432` | `postgres_data` (পারসিস্টেন্ট ডাটা স্টোর) |
| `threadflow-redis` | `redis:7.2-alpine` | `6379` | `redis_data` (ডিস্ট্রিবিউটেড ক্যাশ ও লক) |
| `threadflow-keycloak` | `quay.io/keycloak:24.0.5` | `8080` | `threadflow-realm.json` (OIDC অথেন্টিকেশন) |
| `threadflow-minio` | `minio/minio:RELEASE` | `9000 / 9001` | `minio_data` (ফাইল ও ডক স্টোরেজ) |
| `threadflow-mailpit` | `axllent/mailpit:latest` | `1025 / 8025` | লোকাল ইমেইল ও নোটিফিকেশন মক |
| `threadflow-backend` | ASP.NET Core 10 (.NET SDK) | `5000` | `./src:/app/src` (লাইভ হট-রিলোড) |
| `threadflow-frontend` | React 19 Vite (Node 22) | `3000` | `./apps/web:/app` (লাইভ এইচএমআর) |

---

## ৪. গভর্ন্যান্স ও রুলবুক সিঙ্ক্রোনাইজেশন

1. **[docs/14: সলিউশন স্ট্রাকচার ও মনোরেপো ব্লুপ্রিন্ট](file:///g:/ERP/rmg-erp/threadflow/docs/14-solution-structure-and-monorepo-blueprint.md)**:
   - `docker-compose.yml`-এ ব্যাকএন্ড ও ফ্রন্টএন্ড কন্টেইনার সার্ভিস যোগ করে ৭-সার্ভিস ফুল ক্লাস্টার লক করা হয়েছে।
2. **[rules/08: ডেভঅপস ও ডকার রুলবুক](file:///g:/ERP/rmg-erp/threadflow/rules/08-devops-docker-and-git-workflow.md)**:
   - "১০০% ডকার-বেইজড ও জিরো হোস্ট পলুশন রুল" অলঙ্ঘনীয় আইন হিসেবে লিপিবদ্ধ করা হয়েছে।
3. **[rules/specialists/08-kabir-devops.md](file:///g:/ERP/rmg-erp/threadflow/rules/specialists/08-kabir-devops.md)**:
   - কবীর ভাইয়ের স্পেশালিস্ট রুলবুকে ডকার-ফার্স্ট জিরো হোস্ট ডিপেনডেন্সি এনফোর্স করা হয়েছে।
4. **[.agents/ARCHITECTURE.md](file:///g:/ERP/rmg-erp/.agents/ARCHITECTURE.md)**:
   - ডেভঅপস ও ডিপ্লয়মেন্ট রো-তে "100% Docker Containerized (Zero local PC host dependencies)" আপডেট করা হয়েছে।

---

## ৫. টিমের চূড়ান্ত সাইন-অফ

🚀 **কবীর (DevOps Lead)**:
> *"বস, আপনার পিসিতে কোনো টুলস বা সার্ভিস ইনস্টল করে পিসি ভারি করার প্রশ্নই ওঠে না। সম্পূর্ণ প্রজেক্ট ডকারের কনটেইনার বাউন্ডারির ভেতর চলবে। যখন কাজ করবেন ডকার আপ থাকবে, কাজ শেষ হলে এক কমান্ডে সব বন্ধ। হোস্ট পিসি থাকবে ১০০% ক্লিন!"*
