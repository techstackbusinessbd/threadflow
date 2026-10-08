# 🧪 ডোমেন ১২: ইন্ডাস্ট্রিয়াল গার্মেন্টস ওয়াশিং প্ল্যান্ট ও ওয়েট/ড্রাই প্রসেসিং — পূর্ণাঙ্গ স্পেসিফিকেশন
### *Domain 12: Industrial Garment Washing Plant & Wet/Dry Processing Specification*

> **ডকুমেন্ট স্ট্যাটাস**: অফিসিয়াল স্পেসিফিকেশন (Approved by CTO)  
> **লেখক**: নাবিলা (Lead BA), তানভীর (Architect), আসিফ (Backend Lead), মায়া (QA/Security)  
> **তারিখ**: ০৭ অক্টোবর, ২০২৬ | **ভার্সন**: 1.0.0

---

## ১. ভূমিকা ও ডোমেন পরিধি (Executive Summary & Scope)

একটি পোশাক কারখানায় (বিশেষ করে ডেনিম, টুইল কার্গো, ক্যাজুয়াল জ্যাকেট এবং সফট-ফিনিশ নিটওয়্যারে) সুইং লাইন থেকে সেলাইকৃত কাঁচা পোশাক (Grey Garments) সরাসরি আয়রনিং ও প্যাকিং ফিনিশিংয়ে যেতে পারে না। 

এই পোশাকগুলোকে বায়ারের নির্দিষ্ট স্ট্যান্ডার্ড ও টেক্সচারে রূপান্তর করতে **ইন্ডাস্ট্রিয়াল ওয়াশিং প্ল্যান্টে** পাঠানো হয়। 

এই ডোমেনটি সুইং থেকে আন-ওয়াশড পোশাক রিসিভ করা, ড্রাই প্রসেস (হ্যান্ড স্যান্ডিং, হুইস্কার, লেজার), ওয়েট প্রসেস (এনজাইম স্টোন ওয়াশ, ব্লিচ, সফটনার), হাইড্রো-ড্রাইং, ওয়াশ শ্রিনকেজ ও মেজারমেন্ট অডিট ($\pm 0.5$ ইঞ্চি), স্পেকট্রোফোটোমিটার দিয়ে শেড গ্রুপিং (Shade A, B, C, D), ওয়াশ ডিফেক্ট লস এবং পরিবেশবান্ধব ETP ও ZDHC কমপ্লায়েন্স নিশ্চিত করে ডোমেন ০৬ ফিনিশিংয়ে হ্যান্ডঅফ সম্পন্ন করে।

---

## ২. ১০টি মাইক্রো সাব-প্রসেসের বিস্তারিত বিবরণ (The 10 Micro Sub-Processes)

### ১২.১ ওয়াশ রেসিপি ও কেমিক্যাল ডজিং মাস্টার (Wash Recipe Master)
- **বাস্তবতা**: বায়ারের অনুমোদিত ল্যাব-ডিপ ও ফার্স্ট ওয়াশ রেফারেন্স অনুযায়ী মাস্টার রেসিপি তৈরি।
- **ফর্মুলা (Liquor Ratio & Chemical Dosing)**:
  $$\text{Total Water Required (Liters)} = \text{Garment Batch Dry Weight (Kg)} \times \text{Liquor Ratio (e.g. 1:8)}$$
  $$\text{Chemical Quantity (g/kg)} = \text{Water Volume (Liters)} \times \text{Dosage (g/L)}$$
- **ইনপুট**: Approved Style Tech Pack, Approved Wash Sample, Buyer RSL / MRSL Guidelines।
- **আউটপুট**: `MasterWashRecipe` (Status: `LOCKED`)।

---

### ১২.২ সুইং ফ্লোর থেকে আন-ওয়াশড গ্রে গার্মেন্টস রিসিভিং ও ব্যাচিং (Batching)
- **বাস্তবতা**: সুইং থেকে আসা সেলাই করা পোশাক বান্ডেল বারকোড স্ক্যান করে রিসিভ করা এবং ওয়াশিং মেশিনের ধারণক্ষমতা (Capacity: e.g. 250 Kg belly washer) অনুযায়ী ব্যাচ তৈরি।
- **ফর্মুলা (Batch Capacity Loading)**:
  $$\text{Machine Loading Utilization (\%)} = \left(\frac{\text{Actual Garment Dry Weight}}{\text{Rated Machine Capacity}}\right) \times 100 \quad (80\% - 90\% \text{ Standard})$$
- **আউটপুট**: `WashBatchTicket` (Batch No, Machine No, Total Pieces, Gross Weight)।

---

### ১২.৩ ড্রাই প্রসেস এক্সিকিউশন ও কোয়ালিটি (Dry Process Execution)
- **বাস্তবতা**: পানিতে ভেজানোর আগে শুকনো কাপড়ে ম্যানুয়াল বা লেজার দিয়ে ফেডিং ও ড্যামেজ তৈরি।
  - হ্যান্ড স্ক্র্যাপিং / স্যান্ডিং (Hand Sanding / Scraping)
  - হুইস্কারিং (Whisker) ও চেভরন
  - লেজার ড্রয়িং (Laser Whisker / Destruction)
  - ৩ডি রিংকেল স্প্রে ও কিউরিং ওভেনে বেকিং ($140^\circ\text{C}$ - $150^\circ\text{C}$, ২০ মিনিট)
- **আউটপুট**: `DryProcessInspectionLog` (Status: `PASSED` / `REWORK`)।

---

### ১২.৪ ওয়েট প্রসেস এক্সিকিউশন (Wet Process: Enzyme, Stone, Bleach, Tint)
- **বাস্তবতা**: বেলী ওয়াশার মেশিনে পানির নির্দিষ্ট তাপমাত্রা ও পিএইচ নিয়ন্ত্রণে রাসায়নিক প্রক্রিয়া চালানো।
- **প্রক্রিয়া ধাপসমূহ**:
  1. *Desizing*: স্টার্চ দূরীকরণ ($60^\circ\text{C}$, আলফা-অ্যামাইলেজ এনজাইম)।
  2. *Enzyme / Stone Wash*: সেলুলোজ এনজাইম ও পিউমিস স্টোন দিয়ে কালার ফেড ($50^\circ\text{C} - 55^\circ\text{C}$, pH $4.5 - 5.5$ অ্যাসিড এনজাইম)।
  3. *Bleaching*: হাইপোক্লোরাইট বা পারঅক্সাইড দিয়ে অতিরিক্ত হাইলাইট।
  4. *Neutralization*: মেটাবাইসালফাইট দিয়ে ক্লোরিন নিষ্ক্রিয়করণ।
  5. *Softener Bath*: ক্যাটায়নিক সিলিকন সফটনার দিয়ে কাপড়ে আরামদায়ক ফিল তৈরি ($40^\circ\text{C}$, pH $5.5$)।
- **আউটপুট**: `WetProcessExecutionCycleLog`।

---

### ১২.৫ হাইড্রো-এক্সট্রাকশন, টাম্বলার ড্রাইং ও কিউরিং ওভেন (Drying & Curing)
- **বাস্তবতা**: কাপড় থেকে সেন্ট্রিফিউগাল ফোর্সে ৭০% পানি বের করে স্টিম টাম্বলার ড্রায়ারে শুকানো।
- **মনিটরিং**: ড্রায়ার তাপমাত্রা ($70^\circ\text{C} - 80^\circ\text{C}$), আদ্রতা সেন্সর ও কুল-ডাউন সাইকেল (১০ মিনিট ঠান্ডা বাতাস দিয়ে ক্রিস দূরীকরণ)।

---

### ১২.৬ পোস্ট-ওয়াশ মেজারমেন্ট ও শ্রিনকেজ অডিট (Post-Wash Measurement)
- **বাস্তবতা**: ওয়াশের পানিতে ও তাপে কাপড়ের সুতা কুঁকড়ে যায়। ওয়াশের পর বায়ারের স্পেক চার্ট অনুযায়ী প্রতি সাইজের মেজারমেন্ট মাপা।
- **ফর্মুলা (Shrinkage Percentage & Tolerance)**:
  $$\text{Dimensional Change (\%)} = \left(\frac{\text{Post-Wash Length} - \text{Pre-Wash Length}}{\text{Pre-Wash Length}}\right) \times 100$$
  $$\text{Acceptance Condition}: |\text{Actual Measurement} - \text{Buyer Spec}| \le 0.5 \text{ Inch (Tolerance Gate)}$$
- **লেগ টুইস্ট (Torque / Leg Spiral Test)**: ডেনিম প্যান্টের সাইড সিম সামনে ঘুরে এলে (Leg Twist $> 3\%$) পুরো লট রিজেক্ট।
- **আউটপুট**: `PostWashMeasurementAudit` (Status: `SPEC_APPROVED` / `OUT_OF_TOLERANCE`)।

---

### ১২.৭ ওয়াশ শেড সেপারেশন ও ব্যান্ডিং (Shade Banding: Lot A, B, C, D)
- **বাস্তবতা**: একই স্টাইলের ৫,০০০ প্যান্ট ওয়াশ করলে কাপড়ের মূল সুতার পার্থক্যের কারণে হালকা শেড তারতম্য হয়।
- **শেড গ্রুপিং**: লাইটবক্সে বায়ার অনুমোদিত স্ট্যান্ডার্ডের সাপেক্ষে শেড A (Standard), Shade B (Slightly Darker), Shade C (Slightly Lighter)-এ আলাদা করে ট্যাগিং।
- **কার্টন রুল**: এক কার্টনে শুধুমাত্র একই শেড ব্যান্ডের প্যান্ট প্যাক করা যাবে; শেড মিক্সড কার্টনিং সম্পূর্ণ সফটওয়্যার-লক থাকবে।
- **আউটপুট**: `WashShadeGroupTicket` (Shade Group A, B, C)।

---

### ১২.৮ ওয়াশিং ডিফেক্ট ও ড্যামেজ রিকনসিলিয়েশন (Defect & Process Loss)
- **বাস্তবতা**: ওয়াশিং মেশিনে অতিরিক্ত পাথরের ঘষা বা হাই কেমিক্যালে কাপড় ফেটে যাওয়া, পকেট কাফ ছেঁড়া, বা মেটালিক জিপার অক্সিডেশন।
- **ফর্মুলা (Wash Process Loss)**:
  $$\text{Wash Damage Rate (\%)} = \left(\frac{\text{Damaged / Torn Pieces}}{\text{Total Batch Loaded Pieces}}\right) \times 100$$
- **অ্যালোয়েন্স রুল**: অনুমোদিত সীমা (সাধারণত $\le 1.0\%$) অতিক্রম করলে দায়বদ্ধতা ওয়াশ ম্যানেজারের চার্জে যাবে।
- **আউটপুট**: `WashDamageReconciliationLog`।

---

### ১২.৯ ZDHC MRSL লেভেল ৩ ও ETP ওয়াটার ট্রিটমেন্ট কমপ্লায়েন্স (Environmental Audit)
- **বাস্তবতা**: বায়ারের ZDHC (Zero Discharge of Hazardous Chemicals) গাইডলাইন অনুযায়ী ক্ষতিকর ভারী ধাতু ও ননাইলফেনল নিষিদ্ধ।
- **ETP প্যারামিটার মনিটরিং**:
  - ওয়াশ বর্জ্যের পিএইচ: $6.5 - 8.5$
  - BOD: $\le 30 \text{ mg/L}$
  - COD: $\le 200 \text{ mg/L}$
  - TDS: $\le 2,100 \text{ mg/L}$
- **আউটপুট**: `ETPDischargeComplianceLog`।

---

### ১২.১০ ডোমেন ০৬ (ফিনিশিং ও প্যাকিং) এবং ডোমেন ০৮ (ফাইন্যান্স)-এ হ্যান্ডঅফ (Handoff)
- **ফিনিশিংয়ে হ্যান্ডঅফ**: মেজারমেন্ট ও শেড ব্যান্ড পাসকৃত শুকনো পোশাক সরাসরি ডোমেন ০৬-এ আয়রনিং ও পলি-প্যাকিংয়ের জন্য ডিসপ্যাচ (`WASH_PASSED_DISPATCHED_TO_FINISHING`)।
- **ফাইন্যান্সে হ্যান্ডঅফ**: প্রতি পোশাকে ওয়াশ কেমিক্যাল খরচ, পানি-গ্যাস বিল এবং প্রসেস লস ফাইন্যান্স অর্ডার কস্টিং লেজারে পোস্টিং।

---

## ৩. স্টেট মেশিন ও ট্রানজিশন লাইফসাইকেল (State Machine)

```
[GREY_GARMENTS_RECEIVED] ──> [BATCH_FORMED] ──> [DRY_PROCESS_ACTIVE] ──> [DRY_QC_PASSED]
                                                                                │
                                                                                ▼
                                                                     [WET_PROCESS_ACTIVE]
                                                                                │
                                                                                ▼
                                                                     [HYDRO_AND_DRYING]
                                                                                │
                                                                                ▼
                                                                     [POST_WASH_QC_AUDIT]
                                                                                │
                           ┌────────────────────────────────────────────────────┴────────────────────────────────────────────────────┐
                           ▼                                                                                                         ▼
                 [MEASUREMENT_PASSED]                                                                                      [OUT_OF_SPEC_REJECTED]
                           │                                                                                                         │
                           ▼                                                                                                         ▼
                 [SHADE_BANDING_DONE]                                                                                      [WASH_DEFECT_DISPOSAL]
                           │ (Shade A / B / C Tagged)
                           ▼
             [DISPATCHED_TO_FINISHING_FLOOR]
```

---

## ৪. ডেটাবেজ ডেটা ডিকশনারি (PostgreSQL Schema)

```sql
-- ১. মাস্টার ওয়াশ রেসিপি
CREATE TABLE wash_recipes (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    company_id UUID NOT NULL,
    style_id UUID NOT NULL,
    recipe_code VARCHAR(50) UNIQUE NOT NULL,
    wash_type VARCHAR(50) NOT NULL, -- 'ENZYME_STONE', 'BLEACH_WASH', 'ACID_WASH', 'SILICONE_SOFTENER'
    liquor_ratio VARCHAR(20) NOT NULL DEFAULT '1:8',
    total_cycle_time_minutes INT NOT NULL,
    recipe_details JSONB NOT NULL, -- কেমিক্যাল লিস্ট, গ্রাম/লিটার, তাপমাত্রা, পিএইচ
    status VARCHAR(30) NOT NULL DEFAULT 'APPROVED',
    created_by UUID NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- ২. ওয়াশিং ব্যাচ টিকেট
CREATE TABLE wash_batches (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    recipe_id UUID NOT NULL REFERENCES wash_recipes(id),
    batch_number VARCHAR(50) UNIQUE NOT NULL,
    machine_id UUID NOT NULL,
    total_pieces INT NOT NULL,
    dry_weight_kg DECIMAL(10, 2) NOT NULL,
    liquor_volume_liters DECIMAL(10, 2) NOT NULL,
    status VARCHAR(30) NOT NULL DEFAULT 'BATCH_FORMED', -- 'BATCH_FORMED', 'IN_PROCESS', 'DRYING', 'COMPLETED'
    started_at TIMESTAMPTZ NULL,
    completed_at TIMESTAMPTZ NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- ৩. পোস্ট-ওয়াশ মেজারমেন্ট ও শ্রিনকেজ অডিট
CREATE TABLE post_wash_measurement_audits (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    batch_id UUID NOT NULL REFERENCES wash_batches(id),
    sample_size_inspected INT NOT NULL,
    waist_spec_diff_inch DECIMAL(4, 2) NOT NULL,
    inseam_spec_diff_inch DECIMAL(4, 2) NOT NULL,
    leg_twist_percentage DECIMAL(5, 2) NOT NULL,
    is_within_tolerance BOOLEAN NOT NULL,
    audited_by UUID NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- ৪. ওয়াশ শেড ব্যান্ডিং ও ফিনিশিং ডিসপ্যাচ
CREATE TABLE wash_shade_groupings (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    batch_id UUID NOT NULL REFERENCES wash_batches(id),
    shade_band VARCHAR(20) NOT NULL, -- 'SHADE_A_STANDARD', 'SHADE_B_DARK', 'SHADE_C_LIGHT'
    piece_count INT NOT NULL,
    spectro_delta_e DECIMAL(5, 2) NULL, -- Spectrophotometer Delta E কালার ডিফারেন্স
    finishing_gatepass_number VARCHAR(50) UNIQUE NOT NULL,
    dispatched_to_finishing_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
```

---

## ৫. আরবিএসি (Role-Based Access Control) পারমিশন ম্যাট্রিক্স

| রোল | রেসিপি তৈরি ও লক | ব্যাচ তৈরি ও মেশিন লোড | ড্রাই/ওয়েট প্রসেস সাইন | পোস্ট-ওয়াশ মেজারমেন্ট সাইন | শেড ব্যান্ডিং ও ফিনিশিং রিলিজ |
| :--- | :---: | :---: | :---: | :---: | :---: |
| **WASH_MASTER** | ✅ পূর্ণ | ✅ পূর্ণ | ✅ পূর্ণ | ❌ রিড-অনলি | ⚠️ প্রস্তাবক |
| **WASH_QC_INSPECTOR** | ❌ রিড-অনলি | ❌ নেই | ⚠️ ইন-প্রসেস অডিট | ✅ সম্পূর্ণ অনুমোদন/বাতিল | ✅ শেড অনুমোদন |
| **SEWING_SUPERVISOR** | ❌ নেই | ❌ নেই | ❌ নেই | ❌ নেই | ❌ নেই |
| **FINISHING_MANAGER** | ❌ রিড-অনলি | ❌ রিড-অনলি | ❌ রিড-অনলি | ❌ রিড-অনলি | ✅ রিসিভ কনফার্ম |
| **ETP_COMPLIANCE_OFFICER** | ❌ রিড-অনলি | ❌ নেই | ❌ নেই | ❌ নেই | ⚠️ ETP লগ অডিট |

---

## ৬. এজ কেস ও ফ্যাক্টরি ফ্রড গার্ডস (Edge Cases & Anti-Fraud)

1. **পাথরে কাপড় ছিঁড়ে যাওয়া চেপে যাওয়া (Concealed Wash Tears)**:  
   *প্রতিরোধ*: হাইড্রো থেকে কাপড় বের করার পর ১০০% আলোযুক্ত পরিদর্শন টেবিলে প্রতি পিস টেয়ারিং অডিট বাধ্যতামূলক। ড্যামেজ পিস চেপে ফিনিশিংয়ে পাঠালে ফিনিশিং টেবিলে স্ক্যান রিজেক্ট হবে।
2. **শেড মিক্সড কার্টনিং কেলেঙ্কারি**:  
   *প্রতিরোধ*: সিস্টেমে কার্টনিং স্ক্যানার চালু থাকবে। কার্টনে যদি Shade A-এর সাথে ১টি Shade C-এর পোশাক স্ক্যান করা হয়, বারকোড স্ক্যানার তৎক্ষণাৎ রেড অ্যালার্ম বাজাবে এবং কার্টন বন্ধ করতে দেবে না।
3. **নিষিদ্ধ কেমিক্যাল ব্যবহার**:  
   *প্রতিরোধ*: বায়ারের ZDHC MRSL ডেটাবেজে নিষিদ্ধ কেমিক্যালের বারকোড ইনভেন্টরি স্টোরে স্ক্যান করলেই ওয়াশ রেসিপি ডেটাবেজে সফটওয়্যার ব্লক করবে।

---

## ৭. ক্রস-ডোমেন ইভেন্ট কন্ট্রাক্টস (JSON Event Payloads)

```json
// ১. ওয়াশ সফল হয়ে ফিনিশিংয়ে ডিসপ্যাচ ইভেন্ট:
{
  "eventId": "evt_wash_done_449922",
  "eventType": "WASH_PASSED_DISPATCHED_TO_FINISHING",
  "domain": "DOMAIN_12_WASHING",
  "timestamp": "2026-10-07T18:28:00.000Z",
  "payload": {
    "batchNumber": "WB-2026-00912",
    "styleId": "stl_denim_slim_fit_09",
    "shadeBand": "SHADE_A_STANDARD",
    "passedPieces": 450,
    "damagedPieces": 3,
    "averageInseamDiffInch": -0.15,
    "legTwistPercent": 1.2,
    "curingTempVerifiedC": 148,
    "targetFinishingFloor": "FINISHING_FLOOR_02"
  }
}

// ২. ওয়াশ কেমিক্যাল ব্যয় ফাইন্যান্সে পোস্টিং ইভেন্ট:
{
  "eventId": "evt_wash_cost_778811",
  "eventType": "WASH_CHEMICAL_COST_ACCUMULATED",
  "domain": "DOMAIN_12_WASHING",
  "timestamp": "2026-10-07T18:29:00.000Z",
  "payload": {
    "batchNumber": "WB-2026-00912",
    "styleId": "stl_denim_slim_fit_09",
    "totalChemicalCostUsd": 312.45,
    "costPerGarmentUsd": 0.6943,
    "waterConsumedLiters": 3600
  }
}
```

---

> **নথি সংরক্ষণ**: `threadflow/business-processes/domain-12-washing/specification.md`  
> **স্ট্যাটাস**: বসের নির্দেশনায় স্পেসিফিকেশন ১০০% সম্পন্ন ও অনুমোদিত।
