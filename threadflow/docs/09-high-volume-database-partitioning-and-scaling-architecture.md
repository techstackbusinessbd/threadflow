# 🗄️ থ্রেডফ্লো হাই-ভলিউম ডেটাবেজ স্কেলিং ও টেবিল পার্টিশনিং আর্কিটেকচার
### *ThreadFlow Enterprise Database Partitioning, Scaling & Lifecycle Architecture*

> **ডকুমেন্ট আইডি**: `TF-DOC-009-DATABASE-PARTITIONING-AND-SCALING`  
> **প্রণেতা**: 🗄️ ফাহিম (Database Architect), 🏛️ তানভীর (Principal Architect), 🚀 কবীর (DevOps & SRE), ⚡ আসিফ (Senior Backend)  
> **অনুমোদনকারী**: 👑 বস (Founder / CTO / Head of Engineering)  
> **স্ট্যাটাস**: অফিসিয়াল আর্কিটেকচারাল পলিসি (Officially Locked) | **ভার্সন**: 1.0.0

---

## ১. ভূমিকা ও চ্যালেঞ্জের রূপরেখা (Problem Statement)

থ্রেডফ্লো আরএমজি ইআরপিতে প্রতিদিন হাজার হাজার ওয়ার্কার ও শত শত প্রোডাকশন লাইন থেকে বিপুল পরিমাণ ডেটা তৈরি হয়:
- **সুইং ও কাটিং ফ্লোর**: প্রতিদিন গড়ে ৫০,০০০ থেকে ২,০০,০০০ বারকোড ও বান্ডেল টিকিট স্ক্যান।
- **এইচআর ও পে-রোল**: দিনে ২ বার ১০,০০০ কর্মীর বায়োমেট্রিক পাঞ্চ এন্ট্রি।
- **ইনভেন্টরি ও ফেব্রিক স্টোর**: শত শত রোল রিসিভ, ফেব্রিক ইস্যু ও স্টক মুভমেন্ট লেজার।

এক বছরে এই ডেটার পরিমাণ দাঁড়ায় **কয়েক কোটিতে (Crores of Records)**। যদি সব ডেটা একটি সাধারণ টেবিলে জমা হয়, তবে ২-৩ বছর পর ইনডেক্স ব্লোট হবে, মেমোরি শেষ হয়ে যাবে এবং এমডি-র মাসিক রিপোর্ট জেনারেট হতে মিনিট খানেক সময় লেগে যাবে।

---

## ২. আর্কিটেকচারাল সমাধান: ইউনিফাইড পার্টিশনড ডেটাবেজ

আমরা প্রাচীন আমলের "Yearly Database" (`db_2025`, `db_2026`) পদ্ধতি সম্পূর্ণ বর্জন করেছি। কারণ এতে ক্রস-ইয়ার অর্ডার, মাল্টি-ইয়ার অডিট এবং ফরেন কি সম্পর্ক ভেঙে যায়। 

এর পরিবর্তে **একটি মাত্র সেন্ট্রাল ডেটাবেজে PostgreSQL Native Declarative Range Partitioning** পদ্ধতি গ্রহণ করা হয়েছে।

```
┌────────────────────────────────────────────────────────────────────────────────────────┐
│                   THREADFLOW UNIFIED PARTITIONED DATABASE TOPOLOGY                     │
├────────────────────────────────────────────────────────────────────────────────────────┤
│                                Logical Master Table                                    │
│                              `sewing_bundle_scans`                                     │
│                (Clients, Prisma ORM & APIs query this unified table)                   │
├──────────────────────────┬──────────────────────────┬──────────────────────────────────┤
│       Partition 1        │       Partition 2        │           Partition 3            │
│ `bundle_scans_2025`      │ `bundle_scans_2026`      │       `bundle_scans_2027`        │
│ (Historical Warm Data)   │ (Active HOT Data / NVMe) │    (Pre-allocated Future Buffer) │
└──────────────────────────┴──────────────────────────┴──────────────────────────────────┘
```

---

## ৩. পার্টিশনড টেবিলসমূহের তালিকা (Candidate Tables)

যেসব টেবিলে বছরে ৫০ লাখের বেশি রো তৈরি হবে, সেগুলো বাধ্যতামূলকভাবে পার্টিশনড হবে:

| টেবিলের নাম | ডোমেন | পার্টিশনিং স্ট্র্যাটেজি | পার্টিশন ব্যবধান (Interval) |
| :--- | :--- | :--- | :--- |
| `sewing_bundle_scans` | ডোমেন ০৫ (Sewing) | `RANGE (scanned_at)` | প্রতি বছর (Yearly) অথবা ত্রৈমাসিক |
| `fabric_roll_inspections` | ডোমেন ০২/০৪ (Fabric) | `RANGE (inspected_at)` | প্রতি বছর (Yearly) |
| `inventory_stock_ledgers` | ডোমেন ০২ (Store) | `RANGE (posted_at)` | প্রতি বছর (Yearly) |
| `hr_biometric_punches` | ডোমেন ০৯ (HR/Payroll) | `RANGE (punch_time)` | প্রতি মাস (Monthly) |
| `machine_sensor_telemetry`| ডোমেন ১০ (Maintenance) | `RANGE (recorded_at)` | প্রতি মাস (Monthly) |

---

## ৪. পার্টিশনিং স্কিমা ও SQL ইমপ্লিমেন্টেশন ব্লুপ্রিন্ট

```sql
-- ১. লজিক্যাল প্যারেন্ট টেবিল তৈরি (UUIDv7 + Range Partitioning)
CREATE TABLE sewing_bundle_scans (
    id UUID NOT NULL DEFAULT uuid_generate_v7(),
    company_id UUID NOT NULL,
    style_id UUID NOT NULL,
    bundle_id UUID NOT NULL,
    operator_id UUID NOT NULL,
    operation_code VARCHAR(32) NOT NULL,
    quantity_passed INT NOT NULL DEFAULT 0,
    quantity_defected INT NOT NULL DEFAULT 0,
    scanned_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    
    -- কম্পোজিট প্রাইমারি কি (পার্টিশন কি অবশ্যই প্রাইমারি কিতে থাকতে হয়)
    CONSTRAINT pk_sewing_bundle_scans PRIMARY KEY (id, scanned_at)
) PARTITION BY RANGE (scanned_at);

-- ২. ইয়ারলি ফিজিক্যাল পার্টিশন টেবিল
CREATE TABLE sewing_bundle_scans_2025 PARTITION OF sewing_bundle_scans
    FOR VALUES FROM ('2025-01-01 00:00:00+00') TO ('2026-01-01 00:00:00+00');

CREATE TABLE sewing_bundle_scans_2026 PARTITION OF sewing_bundle_scans
    FOR VALUES FROM ('2026-01-01 00:00:00+00') TO ('2027-01-01 00:00:00+00');

CREATE TABLE sewing_bundle_scans_2027 PARTITION OF sewing_bundle_scans
    FOR VALUES FROM ('2027-01-01 00:00:00+00') TO ('2028-01-01 00:00:00+00');

-- ৩. স্বয়ংক্রিয় লোকাল ইনডেক্সিং (প্রতিটি পার্টিশনে বি-ট্রি ইনডেক্স অটো-ক্রিয়েট হবে)
CREATE INDEX idx_sewing_scans_style_scanned ON sewing_bundle_scans (style_id, scanned_at DESC);
CREATE INDEX idx_sewing_scans_operator ON sewing_bundle_scans (operator_id, scanned_at DESC);
```

---

## ৫. স্বয়ংক্রিয় পার্টিশন ব্যবস্থাপনা (`pg_partman` Automation)

ম্যানুয়ালি কোনো ডেভেলপারকে নতুন বছরের পার্টিশন বানাতে হবে না। আমরা PostgreSQL-এর ইন্ডাস্ট্রি স্ট্যান্ডার্ড এক্সটেনশন **`pg_partman`** এবং ব্যাকগ্রাউন্ড ক্রন জব ব্যবহার করব:

```sql
-- প্রতি বছর নতুন পার্টিশন অটো তৈরি ও প্রাক-বরাদ্দ (Pre-make 1 partition ahead)
SELECT partman.create_parent(
    p_parent_table => 'public.sewing_bundle_scans',
    p_control => 'scanned_at',
    p_type => 'native',
    p_interval => '1 year',
    p_premake => 1
);
```

---

## ৬. ৩-টায়ার ডেটা লাইফসাইকেল (Hot $\rightarrow$ Warm $\rightarrow$ Cold Tiering)

```
[০ - ১২ মাস]       ──────> HOT DATA (NVMe SSD, লাইভ রিড/রাইট, ইন-মেমোরি ক্যাশ)
      ↓
[১ - ৪ বছর]        ──────> WARM DATA (PostgreSQL কমপ্রেসড পার্টিশন, রিড-অনলি অডিট)
      ↓
[৫+ বছর (পুরোনো)]  ──────> COLD ARCHIVE (Parquet ফাইল / অবজেক্ট স্টোরেজ / অ্যানালিটিক্স)
```

1. **Hot Storage (চলতি বছর)**:
   - অতি উচ্চগতির স্টোরেজ। ফ্যাক্টরির লাইভ বারকোড স্ক্যানিং ও রিয়েল-টাইম ড্যাশবোর্ড এখান থেকে সেকেন্ডের ভগ্নাংশে ডেটা পাবে।
2. **Warm Storage (১-৪ বছর)**:
   - পুরোনো বছরের পার্টিশনগুলো `read_only` করে দেওয়া হবে এবং পোস্টগ্রেস টেবিলস্পেস কমপ্রেশন অন থাকবে, যাতে ডিস্ক স্পেস ৬০% পর্যন্ত বেঁচে যায়।
3. **Cold Archive (৫ বছরের পুরোনো)**:
   - যেসব অর্ডারের এলসি ও অডিট সম্পূর্ণ বন্ধ, সেগুলো অটোমেটিক স্ক্রিপ্ট দিয়ে ব্যাকআপ নিয়ে রেগুলার ডেটাবেজ হালকা রাখা হবে।

---

## ৭. হাই-স্পিড ফ্লোর স্ক্যান বাফারিং (Redis Live Buffer)

সুইং ফ্লোরে শিফট শুরুর সময় বা পিক আওয়ারে যখন প্রতি সেকেন্ডে শত শত স্ক্যান রিকোয়েস্ট আসবে:
1. PWA ক্লায়েন্ট থেকে স্ক্যান ডেটা প্রথমে **Redis 7 / Valkey** ইন-মেমোরি লিস্টে পুশ হবে (<৫ মিলি-সেকেন্ড রেসপন্স)।
2. ব্যাকগ্রাউন্ড **NestJS BullMQ Consumer** প্রতি ১০০ms পর পর বাল্ক ব্যাচ (Batch Insert) আকারে পোস্টগ্রেস পার্টিশনে রাইট করবে।
3. ফলে পোস্টগ্রেস ট্রানজ্যাকশন লগে কোনো ট্রাফিক জ্যাম হবে না।

---

## ৮. ইঞ্জিনিয়ারিং টিমের সাইন-অফ

- **🗄️ ফাহিম (Database Architect)**:
  *"১০ কোটি রো হোক বা ৫০ কোটি, Partition Pruning-এর কারণে পোস্টগ্রেস কেবল কাঙ্ক্ষিত বছরের পার্টিশন স্ক্যান করবে। কোয়েরি স্পিড থাকবে অলওয়েজ রকেট ফাস্ট।"*
- **🏛️ তানভীর (Principal Architect)**:
  *"ক্রস-ইয়ার অর্ডারের জন্য কোনো জটিল জয়েন বা ডেটা মাইগ্রেশনের ঝামেলা রইল না। অ্যাপ্লিকেশন লেভেলে Prisma শুধু একটি টেবিলই দেখতে পাবে।"*
- **🚀 কবীর (DevOps & SRE)**:
  *"ডকারাইজড পোস্টগ্রেসে `pg_partman` কনফিগার করে দিচ্ছি। নতুন বছরের পার্টিশন নিজে নিজেই তৈরি হবে, জিরো ডাউনটাইম।"*

---

> **সংরক্ষিত ও কার্যকর**: `threadflow/docs/09-high-volume-database-partitioning-and-scaling-architecture.md`
