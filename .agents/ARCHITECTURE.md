# System Architecture & Technical Source of Truth (ARCHITECTURE.md)

> **Core Purpose**: This document is the ultimate, persistent source of truth for the software architecture. No agent, team member, or code change may violate the standards set here. Any architectural drift must be resolved against this document.

---

## 1. System Overview & Vision
- **Product Name**: **ThreadFlow ERP** (Enterprise Apparel & Garments OS)
- **Tagline**: *"Precision from Thread to Shipment"*
- **Vision**: "পোশাক প্রস্তুতকারী কারখানাকে বিশৃঙ্খল এক্সেল শিট ও ধীরগতির সেকেলে সফটওয়্যার থেকে চিরতরে মুক্ত করে, বিশ্বের সবচেয়ে বুদ্ধিমান ও সহজ ডিজিটাল অপারেটিং সিস্টেমে (The Global Apparel OS) রূপান্তর করা।"
- **Mission**: "কারখানার প্রতিটি সুতা থেকে শুরু করে জাহাজে মাল তোলা পর্যন্ত সব তথ্যকে সিঙ্গেল সোর্স অব ট্রুথে এনে—মার্চেন্ডাইজারের গতি ৩ গুণ বাড়ানো, ফেব্রিক অপচয় ১-২% কমিয়ে কোটি টাকা সাশ্রয় এবং ১০০% অন-টাইম শিপমেন্ট নিশ্চিত করা।"
- **Architecture Style**: Modular Monolith / Clean Architecture with **SaaS-Ready Hybrid Multi-Tenancy** (Dual Deployment: Public Cloud SaaS or Dedicated Enterprise On-Premise, zero code changes).
- **Domain Scope**: 8 Core Value Stream Domains (Merchandising, Procurement, IE/Planning, Cutting, Embellishment, Sewing, Washing, Finishing) + 4 Enterprise Backbone Domains (Commercial, Finance, HR/Payroll, Plant Maintenance)
- **Product Archetypes Supported**: `KNIT`, `WOVEN_NON_DENIM`, `WOVEN_DENIM`, `SWEATER`, `HYBRID_COMBO` (Universal Multi-Category & Mixed-BOM Support)
- **8-Tier Multi-Tenant & Spatial Hierarchy**: $\mathbf{Tenant \ (Client \ Group)} \longrightarrow \mathbf{Company} \longrightarrow \mathbf{Business \ Unit} \longrightarrow \mathbf{Factory} \longrightarrow \mathbf{Building} \longrightarrow \mathbf{Floor} \longrightarrow \mathbf{Section} \longrightarrow \mathbf{Line}$

---

## 2. The 5 Core System Logics (Non-Negotiable Architecture Pillars)
1. **Single Source of Truth**: মার্চেন্ডাইজার যে স্টাইল ও BOM দেবে, পুরো ফ্যাক্টরি সেটাই ব্যবহার করবে। নো ডুপ্লিকেট এন্ট্রি বা এক্সেল শিট।
2. **Precision Math & Zero Float Leakage**: কাপড়ের ওজন ও টাকায় ফ্লোটিং-পয়েন্ট এরর নিষিদ্ধ। দশমিকের ৪ ঘর পর্যন্ত নিখুঁত গাণিতিক প্রিসিশন।
3. **Active Guardian Engine**: ডেডলাইন মিস হওয়ার আগেই প্রো-অ্যাক্টিভ অ্যালার্ট ও পুশ নোটিফিকেশন।
4. **Offline-Resilient Floor Sync**: ফ্লোরে ওয়াইফাই বন্ধ হলেও বারকোড স্ক্যানিং থামবে না; অফলাইনে শতভাগ চলবে এবং নেট পেলে সিঙ্ক হবে।
5. **Keyboard-First Ergonomics**: মাউস ছাড়া স্প্রেডশিটের মতো দ্রুত গতিতে সম্পূর্ণ ডাটা এন্ট্রি।

### 🏛️ Pillar 1: Clean Architecture & Multi-Tenant Decoupling
- **Domain Layer**: Pure business entities, rules, calculation formulas, and Multi-Tenancy contracts (`ITenantEntity`, `BaseTenantEntity`).
- **Application Layer**: Use cases, service orchestrators, input validation DTOs, and `ITenantService`.
- **Infrastructure Layer**: Database access (EF Core with automatic `TenantId` Query Filter & Dapper), Redis distributed cache, Keycloak OIDC, file storage.
- **Presentation Layer**: UI views (React 19 Vite SPA), REST APIs, SignalR real-time hubs, and `TenantResolutionMiddleware`.

### 🛡️ Pillar 2: Data Integrity & ACID Compliance
- **Mandatory UUIDv7 Primary Keys**: সাধারণ ইন্টিজার ID (`1, 2, 3...`) ও র‍্যান্ডম UUIDv4 সম্পূর্ণ নিষিদ্ধ। সমস্ত টেবিলে আধুনিক **UUIDv7 (RFC 9562 - Time-Ordered UUID)** প্রাইমারি কি হিসেবে ব্যবহৃত হবে (`uuid_generate_v7()`)। এটি B-Tree ইনডেক্স ফ্র্যাগমেন্টেশন রোধ করে, মিলিসেকেন্ড ক্রনোলজিক্যাল সর্টিং নিশ্চিত করে এবং অফলাইন ডিভাইসে কনফ্লিক্ট-ফ্রি জেনারেশন দেয়।
- **Declarative Range Partitioning for High-Volume Tables**: কোটি কোটি রো হ্যান্ডেল করার জন্য আলাদা ইয়ারলি ডাটাবেজ নিষিদ্ধ। পরিবর্তে একক সেন্ট্রাল ডাটাবেজে `PostgreSQL Native Declarative Range Partitioning` (Yearly/Monthly) ব্যবহার করতে হবে (`sewing_bundle_scans`, `inventory_stock_ledgers`, `hr_biometric_punches` ইত্যাদির জন্য)। `Partition Pruning` নিশ্চিত করে কোটি রো-এর মধ্যেও মিলিসেকেন্ড কুয়েরি পারফরম্যান্স বজায় থাকবে।
- **Hot/Warm/Cold Data Lifecycle**: চলতি বছর লাইভ হট স্টোরেজে, ১-৪ বছরের ডাটা কমপ্রেসড রিড-অনলি পার্টিশনে এবং ৫+ বছরের ডাটা অ্যানালিটিক্যাল কোল্ড আর্কাইভে স্থানান্তরিত হবে।
- Financial transactions, stock levels, and order tracking must run inside database transactions.
- Zero floating-point calculations for money/inventory: Use integers (cents) or exact Decimal math.
- Soft-deletes with audit trails for all critical enterprise records (`created_by`, `updated_by`, `deleted_at`).

### ⚡ Pillar 3: Resilient API & Performance
- Pagination by default on all list endpoints (Cursor or Offset/Limit with max cap).
- Database indexing on all Foreign Keys and frequently queried status/filter columns.
- Structured JSON error responses with standardized error codes.

---

## 3. Technology Stack Decisions (ADRs) — Officially Locked by CTO (SRS Baseline)

| Component | Selected Technology | Rationale |
| :--- | :--- | :--- |
| **Architecture Pattern** | **Modular Monolith (Domain-Driven Clean Architecture) + SaaS-Ready Hybrid Multi-Tenancy** | High development velocity, zero microservice network latency overhead, strict bounded contexts ready for future service extraction, and dual deployment (SaaS / On-Premise). |
| **Backend Framework** | **ASP.NET Core 10 on .NET 10, C# 14 (on .NET 8/9 LTS base)** | Banking-grade enterprise performance (9.5/10), multi-threaded floor queues, EF Core 10, Dapper micro-ORM for read optimization, native decimal precision. |
| **Frontend Platform & Theme** | **React 19 + TypeScript + Vite (SPA) + Tailwind CSS 4 + shadcn/ui** | Blazing-fast internal enterprise SPA, zero SSR hydration overhead, Radix UI accessibility, Oceanic Slate theme tokens. |
| **Data Grid Engine** | **AG Grid Enterprise** | Mandatory for all tabular views: DOM virtualization, pinned columns, Excel export, and cell editing for 50,000+ cutting/bundle records. |
| **State Management** | **TanStack Query v5 (Server) + Redux Toolkit (Client)** | TanStack Query for API caching, refetching, mutations; Redux Toolkit strictly for logged-in user snapshot, active factory context, and UI state. |
| **Identity & Authentication** | **Keycloak (OIDC / OAuth 2.0)** | Centralized identity management, SSO, MFA, session lifecycle. Keycloak handles identity & tenant claim, not ERP business permissions. |
| **Authorization & Data Scope** | **Policy-Based AuthZ + Custom 8-Tier Tenant & Spatial Data Scope Engine** | ASP.NET Core dynamic policies, DB-backed fine-grained permissions (`<mod>.<res>.<act>`), SoD enforcement, and Tenant $\rightarrow$ Company $\rightarrow$ Line spatial isolation. |
| **Primary Database** | **PostgreSQL 16+ + UUIDv7 (RFC 9562) + Native Partitioning** | 100% ACID compliance, relational integrity, zero index fragmentation via time-ordered UUIDv7, declarative range partitioning. |
| **Data Access & ORM** | **EF Core 10 (Commands/Writes) + Dapper (High-Speed Reads)** | EF Core for transactional aggregate persistence and DDD invariants with global `TenantId` filter; Dapper for sub-50ms aggregated reports. |
| **Caching & In-Memory** | **Redis 7.2 / Valkey + ASP.NET Core SignalR** | Distributed permission cache, data scope cache, rate limiter, and real-time WebSocket push for shop-floor Andon TV screens and supervisor alerts. |
| **Object Storage** | **MinIO (S3-Compatible Object Store)** | Secure storage for heavy binary assets: Tech Packs, lab dips, inspection photos, and invoices. Zero binary blob storage in PostgreSQL. |
| **Background Jobs** | **Hangfire** | Persistent background processing, cron jobs, heavy PDF/Excel generation (QuestPDF & ClosedXML). |
| **DevOps & Containers** | **100% Docker Containerized (Docker Compose + Nginx)** | Zero local PC host dependencies. All services (.NET 10 API, React 19 Vite, PostgreSQL 16, Redis, Keycloak, MinIO, Mailpit) execute exclusively inside Docker containers with live volume hot reloading. |
| **Testing Suite** | **xUnit + Vitest + React Testing Library + Playwright** | Fast backend unit tests, frontend component tests, and automated Playwright E2E coverage for critical ERP workflows. |
| **Observability** | **Serilog + OpenTelemetry + Prometheus + Grafana** | Structured JSON logging, W3C distributed tracing, sub-300ms API latency and sub-100ms DB query monitoring. |

---

## 4. The 12 Non-Negotiable Directives of the Founder / CTO
1. **১০০% ডাইনামিক সিস্টেম (Zero Hardcoding)**: নো হার্ডকোডেড স্ট্যাটাস বা অপশন। সব ড্রপডাউন ও সেটিংস ডেটাবেজ ও অ্যাডমিন প্যানেল চালিত।
2. **জিরো সিনট্যাক্স এরর ও জিরো ওয়ার্নিং**: C# 14 / .NET 10 TreatWarningsAsErrors, TypeScript `strict: true`, ESLint জিরো ওয়ার্নিং পলিসি।
3. **অফিশিয়াল ডকুমেন্টেশন স্ট্যান্ডার্ড**: ASP.NET Core 10, React 19, EF Core 10, Tailwind v4 Oxide-এর অফিশিয়াল ডকুমেন্টেশন কঠোরভাবে অনুসরণ।
4. **ফুল সিস্টেম অ্যাডমিন প্যানেল কনফিগারেশন**: টলারেন্স লিমিট, কোড জেনারেশন ও রুলস অ্যাডমিন কন্ট্রোল্ড।
5. **ডায়নামিক UI লেবেল/বাটন নেমিং ইঞ্জিন**: অ্যাডমিন স্ক্রিনের বাটন ও লেবেল নাম পরিবর্তন করতে পারবে; ডাটাবেজ কলাম অপরিবর্তিত থাকবে (`ui_label_dictionary`)।
6. **বাধ্যতামূলক এন্টারপ্রাইজ DataTable (AG Grid Enterprise)**: সাধারণ HTML `<table>` সম্পূর্ণ নিষিদ্ধ। সর্টিং, ফিল্টারিং, পিনিং, ভার্চুয়ালাইজেশন ও এক্সেল এক্সপোর্ট আবশ্যক।
7. **১০০% সার্ভারসাইড ভ্যালিডেশন**: ক্লায়েন্ট ভ্যালিডেশনের ওপর কোনো নির্ভরতা নেই; FluentValidation + MediatR পাইপলাইন ভ্যালিডেশন বাধ্যতামূলক।
8. **জিরো অপ্রয়োজনীয় টেক্সট বা ডাটা**: কোনো ফ্লাফ বা অপ্রয়োজনীয় ডেসক্রিপশন নয়; শুধু ফাংশনাল ও প্রয়োজনীয় তথ্য।
9. **সহজ ও হিউম্যান-রিডেবল ভাষা**: জটিল দুর্বোধ্য শব্দ বর্জন; বাস্তব গার্মেন্টস কারখানার সহজবোধ্য আন্তর্জাতিক ইংরেজি কপি।
10. **রোবোটিক বা এআই ভাইব সম্পূর্ণ নিষিদ্ধ**: নো এআই ক্লিশে বা রোবোটিক বুলি; ১০০% প্রফেশনাল এন্টারপ্রাইজ সফটওয়্যার ভাইব।
11. **ক্লিন ফাইল ও ফোল্ডার নেমিং**: কোনো রোবোটিক বা অটো-জেনারেটেড নাম নয়; ইন্ডাস্ট্রি স্ট্যান্ডার্ড আর্কিটেকচারাল নাম (`ThreadFlow.Domain`, `ThreadFlow.WebApi`, `apps/web/features/merchandising`).
12. **নিট, ক্লিন ও আল্ট্রা ইউজার-ফ্রেন্ডলি UI/UX**: মিনিমালিস্ট ডিজাইন, প্রফেশনাল হোয়াইটস্পেস, ডার্ক/লাইট মোড ও চোখের আরামদায়ক এন্টারপ্রাইজ কালার প্যালেট।
13. **কোডে বাধ্যতামূলক সহজ বাংলা কমেন্ট ও মেইনটেইনেবিলিটি রুল (Mandatory Clear Bangla Code Comments Law)**: কোড এবং ভ্যারিয়েবল আন্তর্জাতিক স্ট্যান্ডার্ডে পরিষ্কার ইংরেজিতে লেখা হবে, কিন্তু কোডের প্রতিটি ক্লাস, ইন্টারফেস, মেথড, বিজনেস ক্যালকুলেশন এবং জটিল ব্লকে **বাধ্যতামূলক সহজ ও প্রাঞ্জল বাংলায় কমেন্ট** লিখতে হবে। কমেন্ট এমন সহজ ও তথ্যবহুল হতে হবে যাতে যেকোনো নতুন বা জুনিয়র/মিড-লেভেল ডেভেলপার এক নজরেই লজিক বুঝতে পারে এবং দীর্ঘমেয়াদে সিস্টেম কোনো ঝামেলা ছাড়াই মেইনটেইন করতে পারে।
14. **টেবিলের বিজনেস কলামে বাধ্যতামূলক সিটিও অ্যাপ্রুভাল নীতি (Mandatory CTO Approval for Business Columns)**: ডেটাবেজ টেবিলের সিস্টেম রিকোয়ার্ড কলাম (`Id`, `TenantId`, `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`, `IsDeleted`, `DeletedAt`, `Version`, `IsDemo`) ছাড়া—যেকোনো টেবিলের বিজনেস/ডোমেইন কলাম কী কী হবে, তা মাইগ্রেশন করার পূর্বে বাধ্যতামূলকভাবে বস (CTO)-এর সামনে উপস্থাপন করে অ্যাপ্রুভাল নিতে হবে।

---

## 5. Official Architectural & Technical Documentation Index
86: - [01. Vision, Mission & Core Logics](file:///g:/ERP/rmg-erp/threadflow/docs/vision-mission-and-core-logic.md)
87: - [02. Technology Benchmark & Rationale](file:///g:/ERP/rmg-erp/threadflow/docs/02-technology-stack-benchmark-and-comparative-analysis.md)
88: - [03. Development Standards & Rules](file:///g:/ERP/rmg-erp/threadflow/docs/03-development-rules-and-engineering-standards.md)
89: - [04. 3-Tier Enterprise Modeling & Archetypes](file:///g:/ERP/rmg-erp/threadflow/docs/04-three-tier-enterprise-modeling-and-factory-archetypes.md)
90: - [05. Dynamic Architecture & UI Governance](file:///g:/ERP/rmg-erp/threadflow/docs/05-dynamic-system-architecture-and-ui-governance.md)
91: - [06. Design System & OKLCH Color Palette](file:///g:/ERP/rmg-erp/threadflow/docs/06-design-system-and-color-palette.md)
92: - [07. Dynamic Code & Sequence Engine](file:///g:/ERP/rmg-erp/threadflow/docs/07-dynamic-code-and-sequence-engine-architecture.md)
93: - [08. Reusable UI Components & AG Grid Library](file:///g:/ERP/rmg-erp/threadflow/docs/08-reusable-ui-components-and-design-system-library.md)
94: - [09. High-Volume Partitioning & Scaling](file:///g:/ERP/rmg-erp/threadflow/docs/09-high-volume-database-partitioning-and-scaling-architecture.md)
95: - [10. Tech Stack SRS & Authorization Engine](file:///g:/ERP/rmg-erp/threadflow/docs/10-technology-stack-srs-and-authorization-engine.md)
96: - [11. Core Database Schemas & Entity Dictionary](file:///g:/ERP/rmg-erp/threadflow/docs/11-core-database-schemas-and-entity-dictionary.md)
97: - [12. Auth, User Lifecycle & Master Data Hub](file:///g:/ERP/rmg-erp/threadflow/docs/12-auth-user-management-and-master-data-specification.md)
98: - [13. Core API Contracts & AG Grid Protocols](file:///g:/ERP/rmg-erp/threadflow/docs/13-core-api-contracts-and-payload-specifications.md)
99: - [14. Solution Structure & Monorepo Blueprint](file:///g:/ERP/rmg-erp/threadflow/docs/14-solution-structure-and-monorepo-blueprint.md)
