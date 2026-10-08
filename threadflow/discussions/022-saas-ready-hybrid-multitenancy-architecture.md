# 📋 ডিসকাশন ০২২: SaaS-Ready Hybrid Multi-Tenancy আর্কিটেকচার চূড়ান্তকরণ ও সাইন-অফ
### *Discussion 022: SaaS-Ready Hybrid Multi-Tenancy Architecture Finalization & ADR Sign-Off*

- **তারিখ**: ৮ অক্টোবর ২০২৬
- **উপস্থিত সদস্যবৃন্দ**:
  - 👑 বস (Founder / CTO / Head of Engineering)
  - 🏛️ তানভীর (Principal Architect & Tech Lead)
  - 🎯 রাফি (Product & Delivery Manager)
  - 📋 নাবিলা (Lead BA & Domain Specialist)
  - ⚡ আসিফ (Senior Core & Full-Stack Lead)
  - 🎨 সজীব (Frontend & Product Experience Craftsman)
  - 🗄️ ফাহিম (Database Architect & Data Engineer)
  - 🚀 কবীর (DevOps, Cloud Infrastructure & SRE)
  - 🛡️ মায়া (QA, Security & Reliability Engineer)
  - 📚 শাকিল (Technical Writer & Documentation Specialist)

---

## ১. আলোচনার প্রেক্ষাপট ও বসের কৌশলগত সিদ্ধান্ত

বসের সরাসরি প্রশ্ন ছিল: *"ei project ta ki saas naki non saas"* এবং টিমের সুপারিশ জানার পর বসের চূড়ান্ত রায়: **"SaaS-Ready Hybrid Architecture eta final er jonno doc update lagla koro"**।

এই সিদ্ধান্তের মাধ্যমে থ্রেডফ্লো ইআরপি-কে এমন এক বৈপ্লবিক স্থাপত্যে নিয়ে যাওয়া হলো, যা একই সাথে:
1. **B2B Cloud SaaS**: মাঝারি কারখানা ও বায়িং হাউসের কাছে মাসিক/বাৎসরিক সাবস্ক্রিপশন (MRR/ARR) মডেলে সেল করা যাবে।
2. **Dedicated Enterprise On-Premise**: মেগা গার্মেন্ট গ্রুপগুলোর জন্য তাদের নিজস্ব সার্ভার বা প্রাইভেট ক্লাউডে সিঙ্গেল-টেন্যান্ট হিসেবে চালানো যাবে।

---

## ২. টিমের যৌথ আর্কিটেকচারাল আপডেট সারসংক্ষেপ

### ২.১ 🏛️ তানভীর (Principal Architect) & 🗄️ ফাহিম (Database Architect)
- **টপ-লেভেল টেন্যান্ট লেয়ার**:
  $$\mathbf{Tenant} \longrightarrow \mathbf{Company} \longrightarrow \mathbf{Business \ Unit} \longrightarrow \mathbf{Factory} \longrightarrow \mathbf{Building} \longrightarrow \mathbf{Floor} \longrightarrow \mathbf{Section} \longrightarrow \mathbf{Line}$$
- **রুট `tenants` স্কিমা**:
  - `tenants` টেবিলে `tenant_code`, `subdomain` (`{tenant}.threadflow.erp`), `custom_domain`, `subscription_tier`, `isolation_strategy` (`SHARED_ROW_LEVEL` বা VIP-দের জন্য `DEDICATED_DATABASE`) সংযুক্ত।
- **ডেটাবেজ ও ওআরএম লেভেলে টেন্যান্ট আইসোলেশন**:
  - সমস্ত কোর টেবিলে `tenant_id UUID NOT NULL REFERENCES tenants(id)` যুক্ত করা হয়েছে।
  - কম্পোজিট ইউনিক কনস্ট্রেইন্ট: যেমন `(tenant_id, code)` বা `(tenant_id, email)`।
  - EF Core 10 গ্লোবাল কুয়েরি ফিল্টার: `e => e.TenantId == _tenantService.CurrentTenantId`।
  - পোস্টগ্রেস মাল্টি-টেন্যান্ট ইনডেক্সিং পলিসি আপডেট।

### ২.২ ⚡ আসিফ (Backend Lead) & 🛡️ মায়া (QA & Security)
- **টেন্যান্ট রেজোলিউশন মিডলওয়্যার**:
  - সাবডোমেন (`apex.threadflow.erp`) বা HTTP হেডার `X-Tenant-Id` দিয়ে প্রতি রিকোয়েস্টে টেন্যান্ট শনাক্তকরণ।
  - Keycloak JWT টোকেনের ভেতর `tenant_id` ক্লেইম ভ্যালিডেশন (ক্রস-টেন্যান্ট ডেটা লিক প্রতিরোধ)।
  - Redis ডিস্ট্রিবিউটেড লকে টেন্যান্ট নেমস্পেস: `idemp:{tenantId}:{userId}:{key}`।

### ২.৩ 🎨 সজীব (Frontend Craftsman)
- **ফ্রন্টএন্ড টেন্যান্ট অ্যাওয়ারনেস**:
  - `/api/v1/auth/me` এন্ডপয়েন্টে ইউজারের টেন্যান্ট মেটাডেটা (`tenantId`, `tenantCode`, `tenantName`, `subscriptionTier`) সরবরাহ।
  - ফ্রন্টএন্ড স্টোরে টেন্যান্ট ব্র্যান্ডিং (লোগো, সাবডোমেন) স্বয়ংক্রিয়ভাবে লোড হবে।

---

## ৩. আপডেটেড ডকুমেন্টস ট্র্যাকিং

| ডকুমেন্ট | আপডেটের বিষয়বস্তু |
| :--- | :--- |
| [docs/10-technology-stack-srs-and-authorization-engine.md](file:///g:/ERP/rmg-erp/threadflow/docs/10-technology-stack-srs-and-authorization-engine.md) | Section 6.0 Multi-Tenancy Architecture (SaaS-Ready Hybrid Model), 8-Tier Spatial Hierarchy |
| [docs/11-core-database-schemas-and-entity-dictionary.md](file:///g:/ERP/rmg-erp/threadflow/docs/11-core-database-schemas-and-entity-dictionary.md) | `tenants` DDL, `tenant_id` FK in all tables, Composite Unique Constraints, Tenant Indexes |
| [docs/12-auth-user-management-and-master-data-specification.md](file:///g:/ERP/rmg-erp/threadflow/docs/12-auth-user-management-and-master-data-specification.md) | Keycloak Tenant Integration, 8-Tier Data Scope Engine, Global vs Tenant-Scoped Master Data |
| [docs/13-core-api-contracts-and-payload-specifications.md](file:///g:/ERP/rmg-erp/threadflow/docs/13-core-api-contracts-and-payload-specifications.md) | `X-Tenant-Id` header contract, tenant-aware `/api/v1/auth/me` response |
| [docs/14-solution-structure-and-monorepo-blueprint.md](file:///g:/ERP/rmg-erp/threadflow/docs/14-solution-structure-and-monorepo-blueprint.md) | `ITenantService`, `TenantResolutionMiddleware`, `TenantInterceptor`, Root Tenant Seed |
| [.agents/ARCHITECTURE.md](file:///g:/ERP/rmg-erp/.agents/ARCHITECTURE.md) | SaaS-Ready Hybrid Multi-Tenancy architectural pillar lock |
| [.agents/TASKS.md](file:///g:/ERP/rmg-erp/.agents/TASKS.md) & [.agents/PROGRESS.md](file:///g:/ERP/rmg-erp/.agents/PROGRESS.md) | Sprint Board & Progress synchronization |

---

## ৪. টিমের চূড়ান্ত সাইন-অফ

🎯 **রাফি (Product Manager)**:
> *"বস, এই সিদ্ধান্তের ফলে ThreadFlow ERP শুধুমাত্র একটি সফটওয়্যার নয়, একটি বহু মিলিয়ন ডলার ভ্যালুয়েশনের গ্লোবাল ক্লাউড প্ল্যাটফর্মের ভিত্তি পেল। আমাদের সমস্ত ডকুমেন্টেশন এখন শতভাগ সিঙ্কড এবং নিখুঁত।"*
