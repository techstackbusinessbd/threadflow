# 🎨 ডোমেন ১১: এমবেলিশমেন্ট, প্রিন্টিং, এমব্রয়ডারি ও সাব-কন্ট্রাক্ট প্রসেসিং — পূর্ণাঙ্গ স্পেসিফিকেশন
### *Domain 11: Embellishment, Printing, Embroidery & Subcontract Processing Specification*

> **ডকুমেন্ট স্ট্যাটাস**: অফিসিয়াল স্পেসিফিকেশন (Approved by CTO)  
> **লেখক**: নাবিলা (Lead BA), তানভীর (Architect), আসিফ (Backend Lead), মায়া (QA/Security)  
> **তারিখ**: ০৭ অক্টোবর, ২০২৬ | **ভার্সন**: 1.0.0

---

## ১. ভূমিকা ও ডোমেন পরিধি (Executive Summary & Scope)

তৈরি পোশাক শিল্পে (বিশেষ করে নিটওয়্যার, পোলো শার্ট, ডেনিম এবং হুডিতে) প্রায় ৭০% থেকে ৮০% অর্ডারে কাটিংয়ের পর কাটা ফেব্রিক পার্টস সরাসরি সেলাই লাইনে যেতে পারে না। 

পোশাকের ফ্রন্ট পার্ট, ব্যাক পার্ট, স্লিভ বা পকেটে বায়ারের অনুমোদন অনুযায়ী বিভিন্ন ধরণের আর্টওয়ার্ক প্রিন্ট (Screen, Rubber, High-Density, Puff, Heat Transfer, Foil) বা কম্পিউটারাইজড এমব্রয়ডারি (Appliqué, Sequin, 3D Foam) করতে হয়। 

এই ডোমেনটি কাটিং রুম থেকে পার্টস রিসিভ করা, ইন-হাউস ও এক্সটার্নাল সাব-কন্ট্রাক্ট চালান পরিচালনা, কোয়ালিটি ল্যাব টেস্ট (কিউরিং ও ওয়াশ ফাস্টনেস), ড্যামেজ রিকনসিলিয়েশন, কাটিং থেকে রি-কাটিং (Re-cut) এনে বান্ডেল সম্পন্ন করা এবং সুইং ফ্লোরে ডিসপ্যাচ করার সম্পূর্ণ লাইফসাইকেল শতভাগ নিখুঁতভাবে পরিচালনা করে।

---

## ২. ১০টি মাইক্রো সাব-প্রসেসের বিস্তারিত বিবরণ (The 10 Micro Sub-Processes)

### ১১.১ এমবেলিশমেন্ট ওয়ার্ক অর্ডার ও প্লেসমেন্ট টেক প্যাক ম্যাপিং (Work Order & Specs)
- **বাস্তবতা**: মার্চেন্ডাইজারের টেক প্যাক অনুযায়ী কোন পার্টসে (Front, Back, Sleeve), কোন সাইজের জন্য, কত কালারের প্রিন্ট বা কত হাজার স্টিচের এমব্রয়ডারি হবে তার সুনির্দিষ্ট ম্যাপিং।
- **ফর্মুলা (Embroidery Cost Formula)**:
  $$\text{Embroidery Cost per Piece} = \left(\frac{\text{Total Stitch Count}}{1,000}\right) \times \text{Rate per 1,000 Stitches} + \text{Appliqué Cost}$$
- **ইনপুট**: Approved Style BOM, Artwork Vector File, Color Separation Chart, Embroidery .DST File।
- **আউটপুট**: `EmbellishmentWorkOrder` (Status: `SPEC_LOCKED`)।

---

### ১১.২ কাট প্যানেল ডিসপ্যাচ, ডেলিভারি চালান ও সিকিউরিটি গেটপাস (Cut Panel Dispatch)
- **বাস্তবতা**: কাটিং রুম থেকে ১২০ পিসের বান্ডেলগুলো শেড ও লট অনুযায়ী প্যাক করে ইন-হাউস প্রিন্ট সেকশন বা বাইরের সাব-কন্ট্রাক্ট ফ্যাক্টরিতে পাঠানো।
- **ফর্মুলা (Dispatch Reconciliation)**:
  $$\text{Total Dispatched Panels} = \sum_{i=1}^{n} (\text{Bundle Quantity}_i)$$
- **গেটপাস রুল**: প্রতিটি ডেলিভারি চালানে বারকোড ও সিকিউরিটি গেটপাস থাকবে। সিকিউরিটি অফিসার চালানের বারকোড স্ক্যান না করা পর্যন্ত ফ্যাক্টরি গেট দিয়ে ফেব্রিক প্যানেল বের হওয়া সম্পূর্ণ ব্লক থাকবে।
- **আউটপুট**: `EmbellishmentDeliveryChallan` & `SecurityGatePass`।

---

### ১১.৩ ইন-হাউস ও সাব-কন্ট্রাক্ট এমবেলিশমেন্ট এক্সিকিউশন ট্র্যাকিং (Execution Tracking)
- **বাস্তবতা**: কারখানার টেবিল প্রিন্ট বা অটোমেটিক ক্যারোসেল মেশিন অথবা মাল্টি-হেড এমব্রয়ডারি মেশিনে ব্যাচ-ওয়াইজ প্রোডাকশন রান।
- **ট্র্যাকিং**: মেশিনের দৈনিক আউটপুট, ব্যবহৃত স্ক্রিন মেশ কাউন্ট (১০০-১২০ মেশ), ডাইস/কালার ব্যাচ এবং প্রতি ঘণ্টার গতি পর্যবেক্ষণ।

---

### ১১.৪ এমবেলিশমেন্ট কোয়ালিটি গেট ও ল্যাব টেস্ট (Quality Control & Curing Audit)
- **বাস্তবতা**: প্রিন্ট শুকানোর পর সঠিক তাপে কিউরিং না হলে ওয়াশিংয়ের সময় প্রিন্ট ফেটে যায় বা উঠে যায়। এমব্রয়ডারিতে সুই ভাঙলে ফেব্রিক প্যানেল কেটে যায় (Needle Cut)।
- **বাধ্যতামূলক ল্যাব টেস্ট**:
  1. **কিউরিং টেম্পারেচার টেস্ট**: পাইরোমিটার দিয়ে হিট চেম্বারের তাপমাত্রা ($150^\circ\text{C} - 165^\circ\text{C}$) নিরীক্ষা।
  2. **ওয়াশ ফাস্টনেস টেস্ট (ISO 105-C06)**: ৫ বার গরম পানিতে ধোয়ার পর প্রিন্ট ক্র্যাকিং বা কালার ব্লিডিং টেস্ট।
  3. **ক্রকিং / রাব টেস্ট (AATCC 8)**: ড্রাই ও ওয়েট ঘষা দিয়ে কালার ট্রান্সফার টেস্ট (মিনিমাম গ্রেড ৪.০)।
  4. **এমব্রয়ডারি নিডেল কাট অডিট**: এমব্রয়ডারি বর্ডারে সুচের আঘাতে কাপড় ফেটে গেছে কিনা তা ম্যাগনিফাইং গ্লাস দিয়ে পরীক্ষা।
- **আউটপুট**: `EmbellishmentQCAudit` (Status: `PASSED` / `REJECTED`)।

---

### ১১.৫ প্যানেল রিকনসিলিয়েশন ও ড্যামেজ অডিট (Reconciliation & Defect Logging)
- **বাস্তবতা**: চালানে পাঠানো প্যানেল বনাম কাজ শেষে ফেরত আসা প্যানেল মেলানো।
- **ফর্মুলা (Defect Percentage)**:
  $$\text{Defect Rate (\%)} = \left(\frac{\text{Defective / Damaged Panels}}{\text{Total Dispatched Panels}}\right) \times 100$$
- **ড্যামেজ শ্রেণীবিন্যাস**:
  - *প্রিন্ট ডিফেক্ট*: স্মিজ (Smudge), মিস-রেজিস্ট্রেশন (Color Shift), প্রিন্ট ক্র্যাক, পিনহোল।
  - *এমব্রয়ডারি ডিফেক্ট*: সুতা ছেঁড়া, টেনশন লুজ, নিডেল কাট।
- **আউটপুট**: `PanelReconciliationReport` (Good Count, Damaged Count, Missing Count)।

---

### ১১.৬ কাটিং রুম রি-কাটিং রিকুইজিশন ও শেড ম্যাচিং গেট (Re-Cut Requisition)
- **বাস্তবতা**: যদি ১২০ পিসের বান্ডেলে ৫টি ফ্রন্ট পার্ট পুড়ে নষ্ট হয়, তবে কাটিং রুম থেকে অবশ্যই ঠিক সেই ৫টি পার্টসই পুনরায় কাটতে হবে।
- **শেড ম্যাচিং আইন (The Strict Shade-Matching Law)**:
  - রি-কাটিংয়ের কাপড় অবশ্যই অরিজিনাল কাটিং অর্ডারের **একই ফেব্রিক রোল এবং একই শেড লট (Shade A/B/C)** থেকে কাটতে হবে। অন্য কোনো রোল থেকে কাটলে বডির সাথে শেড মিসম্যাচ হবে এবং বায়ার পুরো লট বাতিল করবে।
- **ফর্মুলা (Fabric Required for Re-cut)**:
  $$\text{Re-cut Fabric (kg/yds)} = \text{Re-cut Quantity} \times \text{Part Consumption}$$
- **আউটপুট**: `ReCutChallan` (কাটিং রুম থেকে পার্টস রিপ্লেসমেন্ট চালান)।

---

### ১১.৭ বান্ডেল রি-অ্যাসেম্বলি ও কমপ্লিশন গেট (Bundle Re-Assembly & Integrity)
- **বাস্তবতা**: রি-কাটিং হয়ে আসা নতুন পার্টসে পুনরায় প্রিন্ট/এমব্রয়ডারি করিয়ে এনে অরিজিনাল বান্ডেলে জোড়া লাগানো।
- **১০০% কমপ্লিশন গেট**: বান্ডেলের মোট পিস (যেমন: ১২০ পিস) ১০০% পূর্ণ না হওয়া পর্যন্ত এবং কিউসি কিউআর কোড স্ক্যান না করা পর্যন্ত বান্ডেল সুইং ফ্লোরে পাঠানো সম্পূর্ণ সফটওয়্যার-লক থাকবে।
- **আউটপুট**: `BundleReassembledTicket` (Status: `READY_FOR_SEWING`)।

---

### ১১.৮ সাব-কন্ট্রাক্ট ভেন্ডর লেজার ও পিস-রেট বিলিং অডিট (Subcontract Billing Audit)
- **বাস্তবতা**: বাইরে থার্ড-পার্টি ভেন্ডর দিয়ে প্রিন্ট করালে চালানের পিস রেট অনুযায়ী বিল তৈরি এবং ড্যামেজ পেনাল্টি কর্তন।
- **ফর্মুলা (Net Payable to Vendor)**:
  $$\text{Net Payable} = (\text{Good Passed Panels} \times \text{Agreed Rate}) - (\text{Damaged Panels above Allowance} \times \text{Fabric Cost Penalty})$$
- **আউটপুট**: `VendorBillingVoucher` (ফাইন্যান্স ডোমেন ০৮-এ সিঙ্ক)।

---

### ১১.৯ স্ক্রিন, ব্লক, আর্টওয়ার্ক ও এমব্রয়ডারি DST ফাইল লাইব্রেরি (Asset Management)
- **বাস্তবতা**: বায়ারের অনুমোদিত আর্টওয়ার্কের ভেক্টর ফাইল, কালার প্যান্টোন কোড, প্রিন্ট স্ক্রিন ফ্রেম এবং এমব্রয়ডারি পাঞ্চিং ফাইল (.DST) ক্লাউড স্টোরেজে ডিজিটাল ট্র্যাকিং।
- **আউটপুট**: `EmbellishmentAssetLibrary`।

---

### ১১.১০ সুইং ফ্লোর ও ফাইন্যান্স ম্যানেজমেন্ট লেজারে হ্যান্ডঅফ (Handoff)
- **সুইং ফ্লোরে হ্যান্ডঅফ**: সম্পূর্ণ ও কিউসি পাসকৃত বান্ডেল সরাসরি সুইং লাইনে ইস্যু (`BUNDLE_EMBELLISHMENT_COMPLETED`)।
- **ফাইন্যান্সে হ্যান্ডঅফ**: সাব-কন্ট্রাক্ট বিল এবং ড্যামেজ ফেব্রিক খরচের অ্যাকাউন্টস পেয়াবল ও কস্টিং ভ্যারিয়েন্স জার্নাল পোস্টিং।

---

## ৩. স্টেট মেশিন ও ট্রানজিশন লাইফসাইকেল (State Machine)

```
[DRAFT] ──> [SPEC_LOCKED] ──> [DISPATCHED_TO_PROCESS] ──> [IN_PROCESSING]
                                                               │
                                                               ▼
                                                      [QC_INSPECTION]
                                                               │
                           ┌───────────────────────────────────┴───────────────────────────────────┐
                           ▼                                                                       ▼
                   [ALL_PASSED]                                                           [DAMAGE_DETECTED]
                           │                                                                       │
                           │                                                                       ▼
                           │                                                           [RE_CUT_REQUESTED]
                           │                                                                       │
                           │                                                                       ▼
                           │                                                           [RE_CUT_RECEIVED]
                           │                                                                       │
                           │                                                                       ▼
                           │                                                           [RE_PROCESS_PASSED]
                           │                                                                       │
                           └───────────────────────────────────┬───────────────────────────────────┘
                                                               │
                                                               ▼
                                                  [BUNDLE_FULLY_REASSEMBLED]
                                                               │
                                                               ▼
                                                 [DISPATCHED_TO_SEWING_FLOOR]
```

---

## ৪. ডেটাবেজ ডেটা ডিকশনারি (PostgreSQL Schema)

```sql
-- ১. এমবেলিশমেন্ট ওয়ার্ক অর্ডার
CREATE TABLE embellishment_work_orders (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    company_id UUID NOT NULL,
    style_id UUID NOT NULL,
    order_id UUID NOT NULL,
    work_order_number VARCHAR(50) UNIQUE NOT NULL,
    process_type VARCHAR(30) NOT NULL, -- 'SCREEN_PRINT', 'RUBBER_PRINT', 'EMBROIDERY', 'HEAT_SEAL', 'AOP'
    service_provider_type VARCHAR(20) NOT NULL, -- 'IN_HOUSE', 'SUBCONTRACT'
    vendor_id UUID NULL,
    target_part VARCHAR(50) NOT NULL, -- 'FRONT_PANEL', 'BACK_PANEL', 'LEFT_SLEEVE'
    artwork_file_url TEXT NOT NULL,
    color_count INT NOT NULL DEFAULT 1,
    stitch_count INT NULL, -- এমব্রয়ডারির জন্য
    rate_per_piece DECIMAL(12, 4) NOT NULL,
    status VARCHAR(30) NOT NULL DEFAULT 'DRAFT',
    is_deleted BOOLEAN NOT NULL DEFAULT false,
    deleted_at TIMESTAMPTZ NULL,
    created_by UUID NOT NULL,
    updated_by UUID NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- ২. ডিসপ্যাচ ও ডেলিভারি চালান
CREATE TABLE embellishment_challans (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    work_order_id UUID NOT NULL REFERENCES embellishment_work_orders(id),
    challan_number VARCHAR(50) UNIQUE NOT NULL,
    gate_pass_number VARCHAR(50) UNIQUE NOT NULL,
    total_bundles INT NOT NULL,
    total_panels INT NOT NULL,
    dispatched_at TIMESTAMPTZ NOT NULL,
    received_at TIMESTAMPTZ NULL,
    gate_passed_out_at TIMESTAMPTZ NULL,
    status VARCHAR(30) NOT NULL DEFAULT 'DISPATCHED', -- 'DISPATCHED', 'RECEIVED_BY_VENDOR', 'COMPLETED'
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- ৩. প্যানেল রিকনসিলিয়েশন ও ড্যামেজ লগ
CREATE TABLE embellishment_reconciliations (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    challan_id UUID NOT NULL REFERENCES embellishment_challans(id),
    bundle_id UUID NOT NULL,
    dispatched_quantity INT NOT NULL,
    good_quantity INT NOT NULL,
    damaged_quantity INT NOT NULL,
    missing_quantity INT NOT NULL DEFAULT 0,
    defect_reason VARCHAR(100) NULL, -- 'SMUDGE', 'NEEDLE_CUT', 'BURNT', 'REGISTRATION_SHIFT'
    responsible_party VARCHAR(30) NOT NULL, -- 'VENDOR', 'IN_HOUSE_OPERATOR', 'CUTTING_FAULT'
    re_cut_status VARCHAR(30) NOT NULL DEFAULT 'NONE', -- 'NONE', 'REQUESTED', 'CUT_DONE', 'REASSEMBLED'
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- ৪. রি-কাটিং রিকুইজিশন টেবিল
CREATE TABLE re_cut_requisitions (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    reconciliation_id UUID NOT NULL REFERENCES embellishment_reconciliations(id),
    requisition_number VARCHAR(50) UNIQUE NOT NULL,
    cutting_order_id UUID NOT NULL,
    required_quantity INT NOT NULL,
    target_shade VARCHAR(20) NOT NULL, -- 'SHADE_A', 'SHADE_B', 'SHADE_C'
    fabric_roll_id UUID NOT NULL, -- বাধ্যতামূলক একই রোল/লট
    status VARCHAR(30) NOT NULL DEFAULT 'PENDING_CUT', -- 'PENDING_CUT', 'CUTTING_COMPLETED', 'RE_EMBELLISHED'
    approved_by UUID NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
```

---

## ৫. আরবিএসি (Role-Based Access Control) পারমিশন ম্যাট্রিক্স

| রোল | ওয়ার্ক অর্ডার তৈরি | চালান ইস্যু ও গেটপাস | কিউসি অডিট ও ল্যাব টেস্ট | রি-কাটিং রিকুইজিশন অনুমোদন | ভেন্ডর বিল অনুমোদন |
| :--- | :---: | :---: | :---: | :---: | :---: |
| **MERCHANDISER** | ✅ পূর্ণ | ❌ রিড-অনলি | ❌ রিড-অনলি | ❌ রিড-অনলি | ❌ রিড-অনলি |
| **EMBELLISHMENT_SUPERVISOR** | ❌ রিড-অনলি | ✅ পূর্ণ | ✅ ইন-প্রসেস | ⚠️ রিকোয়েস্ট তৈরি | ❌ নেই |
| **CUTTING_MASTER** | ❌ রিড-অনলি | ❌ নেই | ❌ নেই | ✅ রি-কাটিং অনুমোদন ও কাটিং | ❌ নেই |
| **QC_INSPECTOR** | ❌ রিড-অনলি | ❌ রিড-অনলি | ✅ সম্পূর্ণ অনুমোদন/বাতিল | ⚠️ ডিফেক্ট ভেরিফাই | ❌ নেই |
| **SECURITY_OFFICER** | ❌ নেই | ✅ গেটপাস স্ক্যান ও এক্সিট | ❌ নেই | ❌ নেই | ❌ নেই |
| **COMMERCIAL_ACCOUNTS** | ❌ রিড-অনলি | ❌ রিড-অনলি | ❌ রিড-অনলি | ❌ রিড-অনলি | ✅ বিল অনুমোদন ও পে |

---

## ৬. এজ কেস ও ফ্যাক্টরি ফ্রড গার্ডস (Edge Cases & Anti-Fraud)

1. **ভেন্ডরের ড্যামেজ চুরি ফ্রড**: সাব-কন্ট্রাক্ট ভেন্ডর ভালো প্যানেল নিজের কাছে রেখে মিথ্যা ড্যামেজ রিপোর্ট দাবি করা।  
   *প্রতিরোধ*: ভেন্ডরকে ফিজিক্যালি নষ্ট পার্টস ফেরত দিতে হবে; নষ্ট পার্টসের ছবি ও কিউসি বারকোড স্ক্যান ছাড়া রি-কাটিং ইস্যু সম্পূর্ণ ব্লক থাকবে।
2. **শেড অমিল ডিজাস্টার**: রি-কাটিংয়ের সময় কাটিং মাস্টার ফ্লোরে পড়ে থাকা অন্য কাপড়ের টুকরো থেকে পার্টস কেটে দেওয়া।  
   *প্রতিরোধ*: সিস্টেমে অরিজিনাল কাটিংয়ের `fabric_roll_id` এবং `shade_lot` ম্যাচ না করলে রি-কাটিং চালান ডেটাবেজে জেনারেট হবে না।
3. **গেটপাসহীন চোরাই মাল পাচার**: চালানের বাইরে অতিরিক্ত পার্টস ফ্যাক্টরি থেকে বের করে নেওয়া।  
   *প্রতিরোধ*: প্রতিটি চালানের সাথে ইউনিক কিউআর কোড সিকিউরিটি গেটে স্ক্যান হতে হবে; চালানের সংখ্যার চেয়ে বেশি বা কম হলে গেট লক অ্যালার্ম বাজবে।

---

## ৭. ক্রস-ডোমেন ইভেন্ট কন্ট্রাক্টস (JSON Event Payloads)

```json
// ১. কাটিং রুম থেকে রি-কাটিং রিকোয়েস্ট ইভেন্ট:
{
  "eventId": "evt_recut_req_887711",
  "eventType": "RE_CUT_REQUISITION_TRIGGERED",
  "domain": "DOMAIN_11_EMBELLISHMENT",
  "timestamp": "2026-10-07T18:24:00.000Z",
  "payload": {
    "requisitionNumber": "RECUT-2026-00441",
    "styleId": "stl_tshirt_crew_001",
    "bundleId": "bndl_cut_lot_44_012",
    "partName": "FRONT_PANEL",
    "requiredPieces": 4,
    "mandatoryShade": "SHADE_A",
    "originalFabricRollId": "roll_single_jersey_cotton_781",
    "reason": "HEAT_CURE_BURNT_DAMAGE"
  }
}

// ২. বান্ডেল সম্পূর্ণ হয়ে সুইং ফ্লোরে ডিসপ্যাচ ইভেন্ট:
{
  "eventId": "evt_embellish_done_991122",
  "eventType": "BUNDLE_EMBELLISHMENT_COMPLETED",
  "domain": "DOMAIN_11_EMBELLISHMENT",
  "timestamp": "2026-10-07T18:25:00.000Z",
  "payload": {
    "bundleBarcode": "BNDL-STYLE-441-SZ-L-012",
    "totalPieces": 120,
    "qcInspectionStatus": "PASSED",
    "curingTempVerifiedC": 155,
    "washFastnessGrade": 4.5,
    "destinationSewingLine": "LINE_04"
  }
}
```

---

> **নথি সংরক্ষণ**: `threadflow/business-processes/domain-11-embellishment/specification.md`  
> **স্ট্যাটাস**: বসের পর্যালোচনার ভিত্তিতে স্পেসিফিকেশন ১০০% সম্পূর্ণ ও অনুমোদিত।
