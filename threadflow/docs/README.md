# 📚 থ্রেডফ্লো অফিসিয়াল আর্কিটেকচার ও গভর্নেন্স ডকুমেন্টস (Docs Directory)
### *Official System Architecture, Modeling & Strategic Standards*

এই ডিরেক্টরিতে থ্রেডফ্লো ইআরপির সমস্ত উচ্চস্তরের আর্কিটেকচারাল পলিসি, এন্টারপ্রাইজ মডেলিং এবং টেকনিক্যাল স্ট্যান্ডার্ড নথিভুক্ত করা হয়েছে।

---

## 📑 নথিসমূহের সূচিপত্র (Documentation Index)

1. 🏛️ [vision-mission-and-core-logic.md](vision-mission-and-core-logic.md)  
   - **বিষয়বস্তু**: থ্রেডফ্লোর অফিসিয়াল ভিশন, মিশন ও ৫টি অলঙ্ঘনীয় কোর সিস্টেম লজিক ম্যানিফেস্টো।
2. 📊 [02-technology-stack-benchmark-and-comparative-analysis.md](02-technology-stack-benchmark-and-comparative-analysis.md)  
   - **বিষয়বস্তু**: গ্লোবাল আরএমজি ইআরপিগুলোর টেক স্ট্যাক বেঞ্চমার্কিং ও অপশন ১ (TypeScript Modular Monolith) নির্বাচনের বিশদ অ্যানালাইসিস।
3. 📜 [03-development-rules-and-engineering-standards.md](03-development-rules-and-engineering-standards.md)  
   - **বিষয়বস্তু**: সফটওয়্যার ডেভেলপমেন্টের সার্বিক নিয়মাবলী ও ইঞ্জিনিয়ারিং স্ট্যান্ডার্ড।
4. 🏢 [04-three-tier-enterprise-modeling-and-factory-archetypes.md](04-three-tier-enterprise-modeling-and-factory-archetypes.md)  
   - **বিষয়বস্তু**: **৩-টায়ার এন্টারপ্রাইজ অর্গানাইজেশনাল মডেল ও মাল্টি-আর্কিটাইপ কনফিগারেশন** (Conglomerate Group $\rightarrow$ Factory Units with `operational_type` $\rightarrow$ Style Archetype Dynamic Routing & Zero UI Clutter)।
5. 🛡️ [05-dynamic-system-architecture-and-ui-governance.md](05-dynamic-system-architecture-and-ui-governance.md)  
   - **বিষয়বস্তু**: **বসের ১২টি অলঙ্ঘনীয় মূলনীতি — ডায়নামিক আর্কিটেকচার, জিরো হার্ডকোডিং, অ্যাডমিন লেবেল ইঞ্জিন, TanStack DataTable ও হিউম্যান UI/UX গভর্নেন্স**।
6. 🎨 [06-design-system-and-color-palette.md](06-design-system-and-color-palette.md)  
   - **বিষয়বস্তু**: **অফিসিয়াল ডিজাইন সিস্টেম ও OKLCH কালার প্যালেট স্পেসিফিকেশন** (Tailwind v4 Oxide, Slate/Zinc Neutrals, Semantic Traffic Lights, 50-Foot Andon Display & TanStack Table Shading Tokens)।
7. 🏛️ [07-dynamic-code-and-sequence-engine-architecture.md](07-dynamic-code-and-sequence-engine-architecture.md)  
   - **বিষয়বস্তু**: **ডায়নামিক কোড ও অটো-সিকোয়েন্স জেনারেশন ইঞ্জিন** (Company Code, Style Code, PO, Cut Number ইত্যাদি সিস্টেম অটো-জেনারেট করবে, অ্যাডমিন প্যানেল থেকে টোকেনাইজড ফরম্যাট কনফিগারেশন ও লাইভ প্রিভিউ)।
8. 🎨 [08-reusable-ui-components-and-design-system-library.md](08-reusable-ui-components-and-design-system-library.md)  
   - **বিষয়বস্তু**: **রিইউজেবল ইউআই কম্পোনেন্ট লাইব্রেরি স্পেসিফিকেশন (`@threadflow/ui`)** (Atoms, Molecules, Organisms, বসের বাধ্যতামূলক EnterpriseDataTable, SearchableCombobox, BOMMatrixGrid ও এরগোনোমিক কিবোর্ড কন্ট্রোল)।
9. 🗄️ [09-high-volume-database-partitioning-and-scaling-architecture.md](09-high-volume-database-partitioning-and-scaling-architecture.md)  
   - **বিষয়বস্তু**: **হাই-ভলিউম ডেটাবেজ স্কেলিং ও টেবিল পার্টিশনিং আর্কিটেকচার** (PostgreSQL Native Declarative Partitioning, কোটি কোটি ডেটা হ্যান্ডেল করার কৌশল, Partition Pruning, pg_partman অটোমেশন ও Hot/Warm/Cold টায়ারিং)।
10. 🔐 [10-technology-stack-srs-and-authorization-engine.md](10-technology-stack-srs-and-authorization-engine.md)  
    - **বিষয়বস্তু**: **এন্টারপ্রাইজ টেকনোলজি স্ট্যাক SRS ও অথোরাইজেশন ইঞ্জিন** (.NET 10 Clean Architecture, React 19 Vite SPA, AG Grid Enterprise, Keycloak OIDC, এবং ৭-লেয়ার ডেটা স্কোপ হায়ারার্কি)।
11. 🗄️ [11-core-database-schemas-and-entity-dictionary.md](11-core-database-schemas-and-entity-dictionary.md)  
    - **বিষয়বস্তু**: **কোর ডেটাবেজ স্কিমা ও এনটিটি ডিকশনারি ব্লুপ্রিন্ট** (PostgreSQL 16+ DDL, UUIDv7 প্রাইমারি কি, ৭-লেয়ার অর্গানাইজেশনাল টপোলজি, কি-ক্লোাক ইউজার ম্যাপিং, অডিট লগ পার্টিশনিং ও কোড সিকোয়েন্স)।
12. 👤 [12-auth-user-management-and-master-data-specification.md](12-auth-user-management-and-master-data-specification.md)  
    - **বিষয়বস্তু**: **অথেন্টিকেশন, ইউজার লাইফসাইকেল, রোল-পলিসি ও মাস্টার ডেটা স্পেসিফিকেশন** (Keycloak OIDC Integration, ৫-দফা ইউজার স্টেট মেশিন, ৩,০০০+ গ্র্যানুলার পারমিশন, সোড ইঞ্জিন ও মাস্টার ডেটা হাব)।
13. 📡 [13-core-api-contracts-and-payload-specifications.md](13-core-api-contracts-and-payload-specifications.md)  
    - **বিষয়বস্তু**: **কোর এপিআই কন্ট্রাক্ট ও পেলোড স্পেসিফিকেশন** (ইউনিফাইড রেসপন্স এনভেলপ, AG Grid SSRM প্রোটোকল, RFC 7807 ProblemDetails, Idempotency-Key এবং সিগন্যালআর রিয়েল-টাইম ইভেন্ট স্কিমা)।
14. 🏗️ [14-solution-structure-and-monorepo-blueprint.md](14-solution-structure-and-monorepo-blueprint.md)  
    - **বিষয়বস্তু**: **সলিউশন স্ট্রাকচার ও মনোরেপো ব্লুপ্রিন্ট** (সি# .NET 10 সলিউশন ট্রি, React 19 Vite SPA লেআউট, ডকার কম্পোজ মাল্টি-সার্ভিস অর্কেস্ট্রেশন এবং ইনিশিয়াল বুটস্ট্র্যাপ সিড স্ট্র্যাটেজি)।

---

> **সংরক্ষণ ও তত্ত্বাবধান**: থ্রেডফ্লো ভার্চুয়াল ইঞ্জিনিয়ারিং টিম
