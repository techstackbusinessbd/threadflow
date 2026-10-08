# 🏛️ থ্রেডফ্লো ডাইনামিক সিস্টেম আর্কিটেকচার ও ইউআই/ইউএক্স গভর্নেন্স স্ট্যান্ডার্ড
### *ThreadFlow 100% Dynamic Engine, Zero Hardcoding & Ergonomic UI/UX Governance*

> **ডকুমেন্ট আইডি**: `TF-DOC-005-DYNAMIC-GOVERNANCE`  
> **প্রণেতা**: 🏛️ তানভীর (Principal Architect), 🎨 সজীব (Frontend Craftsman), ⚡ আসিফ (Senior Backend), 🛡️ মায়া (QA & Security)  
> **অনুমোদনকারী**: 👑 বস (Founder / CTO / Head of Engineering)  
> **স্ট্যাটাস**: অলঙ্ঘনীয় প্রাতিষ্ঠানিক সংবিধান (Non-Negotiable Constitutional Law) | **ভার্সন**: 1.0.0

---

## ১. বসের ১২টি অলঙ্ঘনীয় মূলনীতি (The 12 Ironclad Laws of Engineering)

আমাদের প্রতিষ্ঠাতা ও সিটিও (বস)-এর সরাসরি নির্দেশনায় থ্রেডফ্লো ইআরপির প্রতিটি লাইন কোড এবং প্রতিটি স্ক্রিন নিচের ১২টি অলঙ্ঘনীয় নীতিতে পরিচালিত হবে:

```
┌─────────────────────────────────────────────────────────────────────────────────────────────────┐
│                          THE 12 IRONCLAD LAWS OF THREADFLOW ERP                                 │
├────┬───────────────────────────────────────────┬────────────────────────────────────────────────┤
│ নং │ নীতিমালা (Directive)                      │ কারিগরি বাস্তবায়ন (Technical Enforcement)        │
├────┼───────────────────────────────────────────┼────────────────────────────────────────────────┤
│ ১  │ ১০০% ডাইনামিক সিস্টেম (Zero Hardcoding)     │ নো হার্ডকোডেড অপশন, স্ট্যাটাস বা ড্রপডাউন; DB চালিত│
│ ২  │ জিরো সিনট্যাক্স এরর ও জিরো ওয়ার্নিং       │ TypeScript `strict: true`, ESLint জিরো ওয়ার্নিং │
│ ৩  │ অফিশিয়াল ডকুমেন্টেশন অনুসরণ                 │ React 19, Vite, ASP.NET Core, EF Core 10, Tailwind v4│
│ ৪  │ ফুল সিস্টেম অ্যাডমিন প্যানেল কনফিগারেশন   │ সিস্টেম প্যারামিটার ও বিজনেস রুলস অ্যাডমিন কন্ট্রোল্ড│
│ ৫  │ ডায়নামিক UI লেবেল/বাটন নেমিং ইঞ্জিন        │ অ্যাডমিন লেবেল বদলাতে পারবে; DB কলাম নাম অক্ষুণ্ণ│
│ ৬  │ বাধ্যতামূলক এন্টারপ্রাইজ DataTable         │ সাধারণ HTML Table নিষিদ্ধ; AG Grid Enterprise আবশ্যক │
│ ৭  │ ১০০% সার্ভারসাইড ভ্যালিডেশন               │ HTML ভ্যালিডেশন নয়; FluentValidation + Zod স্কিমা  │
│ ৮  │ জিরো অপ্রয়োজনীয় টেক্সট বা ডাটা            │ নো ফ্লাফ, নো আননেসেসারি কন্টেন্ট; প্রিসাইজ তথ্য│
│ ৯  │ সহজ ও হিউম্যান-রিডেবল ভাষা               │ জটিল দুর্বোধ্য শব্দ বর্জন; স্পষ্ট সহজবোধ্য ভাষা  │
│ ১০ │ রোবোটিক বা এআই ভাইব সম্পূর্ণ নিষিদ্ধ       │ কোনো কৃত্রিম বা রোবোটিক বুলি নয়; অথেনটিক বিজনেস UI│
│ ১১ │ ক্লিন ফাইল ও ফোল্ডার নেমিং                 │ স্ট্যান্ডার্ড ইন্ডাস্ট্রি আর্কিটেকচারাল নাম      │
│ ১২ │ নিট, ক্লিন ও আল্ট্রা ইউজার-ফ্রেন্ডলি UI   │ মিনিমালিস্টিক, প্রিমিয়াম ও চোখের আরামদায়ক ডিজাইন│
└────┴───────────────────────────────────────────┴────────────────────────────────────────────────┘
```

---

## ২. নীতি ১ ও ৪: ১০০% ডাইনামিক সিস্টেম ও অ্যাডমিন কনফিগারেশন ইঞ্জিন (Zero Hardcoding)

### ২.১ কোনো হার্ডকোডিং চলবে না (Zero Hardcoded Values)
- কোডের কোথাও কোনো ড্রপডাউন ভ্যালু (যেমন: ফেব্রিক টাইপ, বায়ার কারেন্সি, সাইজ চার্ট, সুইং অপারেশনের নাম, ট্যাক্স রেট, শিপমেন্ট ইনকোটার্মস) হার্ডকোড করা সম্পূর্ণ নিষিদ্ধ।
- সমস্ত প্যারামিটার ডেটাবেজের মাস্টার লুকআপ টেবিল (`system_lookups`, `system_settings`, `uom_registry`, `tax_configuration`) থেকে লোড হবে।

### ২.২ সেন্ট্রাল অ্যাডমিন সেটিংস প্যানেল (Dynamic System Preferences)
সিস্টেমের যেকোনো গ্লোবাল প্যারামিটার অ্যাডমিন প্যানেল থেকে কনফিগার করা যাবে:
1. **টলারেন্স ও রাউন্ডিং লিমিট**: ফেব্রিক রিসিভিংয়ে কত শতাংশ শর্টেজ বা এক্সেস অনুমোদিত (যেমন: $\pm 2\%$)—তা অ্যাডমিন কনফিগার করবে।
2. **অটোমেটেড নম্বর জেনারেশন প্যাটার্ন**: ইনভয়েস, পিও বা বান্ডেল কোড তৈরির প্রিফিক্স ও সিকোয়েন্স (যেমন: `PO-2026-0001` বা `CUT-KNIT-9921`) অ্যাডমিন থেকে পরিবর্তনযোগ্য।
3. **অনুমোদন ম্যাট্রিক্স (Approval Workflow Matrix)**: কত টাকার পারচেজ অর্ডারে ডিএমডি বা এমডির অনুমোদন লাগবে তা কোনো হার্ডকোডেড লজিক নয়; ডায়নামিক রুল ইঞ্জিন দ্বারা নির্ধারিত হবে।

---

## ৩. নীতি ৫: ডায়নামিক UI লেবেল ও বাটন নেমিং ইঞ্জিন (UI Label Customization Engine)

বসের সুনির্দিষ্ট নির্দেশ: **"Admin chaila UI/UX er button, input, label er name change korte parba but database er na."**

এটি একটি অত্যন্ত শক্তিশালী আর্কিটেকচারাল প্যাটার্ন। ডেটাবেজের ফিল্ড নাম (যেমন: `buyer_po_number`, `fabric_consumption_qty`) সর্বদা আন্তর্জাতিক নিয়মে ফিক্সড ও টাইপ-সেফ থাকবে। কিন্তু স্ক্রিনের ইউজার ইন্টারফেসে বাটন টেক্সট, ইনপুট ফিল্ড লেবেল, প্লেসহোল্ডার এবং হেল্প টেক্সট অ্যাডমিন প্যানেল থেকে কাস্টমাইজ করা যাবে।

### ৩.১ ডাটাবেজ স্কিমা: `ui_label_dictionary`
```typescript
interface UiLabelDictionary {
  id: string;
  moduleKey: string;      // যেমন: "merchandising", "cutting_room", "inventory"
  screenKey: string;      // যেমন: "style_creation", "cut_bundle_generation"
  elementKey: string;     // ইউনিক আইডেন্টিফায়ার, যেমন: "btn_submit_order", "lbl_fabric_gsm"
  elementType: 'BUTTON' | 'LABEL' | 'INPUT_PLACEHOLDER' | 'TOOLTIP' | 'MODAL_TITLE';
  
  defaultLabelEn: string; // ডিফল্ট: "Submit Style", "Fabric GSM"
  customLabelEn?: string; // অ্যাডমিনের কাস্টম নাম: "Save & Lock Tech Pack", "Weight per Area"
  customLabelBn?: string; // বাংলা অনুবাদ: "স্টাইল সংরক্ষণ করুন", "কাপড়ের ওজন (জিএসএম)"
  
  isVisible: boolean;     // অ্যাডমিন চাইলে ফিল্ডটি হাইডও করতে পারবে
  isSystemLocked: boolean;// কোর প্রাইমারি কি ফিল্ডগুলো ডিলিট হওয়া রোধে
}
```

### ৩.২ ফ্রন্টএন্ড হুক ইমপ্লিমেন্টেশন (`useDynamicLabel`)
ফ্রন্টএন্ডে সরাসরি হার্ডকোডেড লেবেল লেখার বদলে হুক ব্যবহার করা হবে:
```tsx
// ❌ ভুল পদ্ধতি (Hardcoded):
// <button>Submit Order</button>
// <label>Fabric GSM</label>

// ✅ সঠিক থ্রেডফ্লো পদ্ধতি (Dynamic UI Dictionary):
const { t } = useUiDictionary("merchandising.style_creation");

<label>{t("lbl_fabric_gsm", "Fabric GSM")}</label>
<input placeholder={t("ph_fabric_gsm", "Enter GSM (e.g., 180)")} />
<button>{t("btn_submit_order", "Submit Style")}</button>
```
- অ্যাডমিন যখন অ্যাডমিন প্যানেলে গিয়ে `btn_submit_order`-এর নাম বদলে করবে *"Confirm & Send to IE"*, তখন সাথে সাথে সমস্ত মার্চেন্ডাইজারের স্ক্রিনে বাটনটির নাম কোনো কোড ডেপ্লয়মেন্ট ছাড়াই বদলে যাবে!

---

## ৪. নীতি ৬: সাধারণ টেবিল সম্পূর্ণ নিষিদ্ধ — বাধ্যতামূলক এন্টারপ্রাইজ DataTable (TanStack Table v8)

বসের সুনির্দিষ্ট নির্দেশ: **"Must DataTable use hoba, normal table use hoba na."**

গার্মেন্টস ফ্যাক্টরির একটি মার্চেন্ডাইজিং অর্ডারে ২০টি কালার ও ৮টি সাইজের গ্রিড থাকে, কাটিং রুমে শত শত বান্ডেল থাকে, আর ফ্লোরে হাজার হাজার অপারেটরের লাইন ডাটা থাকে। সাধারণ অপরিকল্পিত HTML `<table>` এখানে সম্পূর্ণ নিষিদ্ধ।

### ৪.১ থ্রেডফ্লো স্ট্যান্ডার্ড DataTable স্পেসিফিকেশন (`EnterpriseDataTable`)
প্রতিটি টেবিল অবশ্যই নিচের ফিচার সমৃদ্ধ হতে হবে:
1. **সার্ভারসাইড ও ক্লায়েন্টসাইড ভার্চুয়ালাইজেশন**: ১০,০০০ রো হলেও ব্রাউজার হ্যাং করবে না (`@tanstack/react-virtual`)।
2. **স্মার্ট মাল্টি-কলাম সর্টিং ও ফিল্টারিং**: বায়ার, স্টাইল, ডেডলাইন বা স্ট্যাটাস দিয়ে ইনস্ট্যান্ট ফিল্টারিং।
3. **কলাম পিনিং ও হাইডিং (Column Pinning)**: এক্সেলের মতো অর্ডার নম্বর বা ছবি বামে পিন করে রাখা এবং ইউজার তার সুবিধামতো অপ্রয়োজনীয় কলাম হাইড করতে পারবে।
4. **এক্সেল ও পিডিএফ এক্সপোর্ট (One-Click Export)**: যেকোনো টেবিলের ডাটা মার্চেন্ডাইজার এক ক্লিকে সম্পূর্ণ এক্সেল শিট (`.xlsx`) বা পিডিএফ রিপোর্ট আকারে ডাউনলোড করতে পারবে।
5. **কীবোর্ড ন্যাভিগেশন**: অ্যারো কি দিয়ে রো সিলেক্ট করা এবং `Enter` চাপলে ডিটেইলস ভিউ খোলা।
6. **ঘনত্ব নিয়ন্ত্রণ (Density Toggle)**: কমপ্যাক্ট মোড (ফ্লোর সুপারভাইজারদের জন্য বেশি ডাটা একসাথে দেখার জন্য) বনাম আরামদায়ক স্পেসড মোড।

---

## ৫. নীতি ৭: সার্ভারসাইড ভ্যালিডেশন বাধ্যতামূলক — HTML ভ্যালিডেশন সম্পূর্ণ বাতিল

বসের সুনির্দিষ্ট নির্দেশ: **"Serverside validation hoba html validation na."**

ব্রাউজারের ডিফল্ট HTML5 ভ্যালিডেশন (যেমন: `<input required />` বা `type="number"`) কখনোই সিকিউরিটি বা ডেটা ইন্টিগ্রিটির গ্যারান্টি দেয় না; ব্রাউজার ইন্সপেক্ট করে এটি যেকোনো সময় বাইপাস করা সম্ভব।

### ৫.১ থ্রেডফ্লোর সার্ভার-ফার্স্ট Zod ভ্যালিডেশন আর্কিটেকচার
1. **জিরো ট্রাস্ট ক্লায়েন্ট ইনপুট (Zero Trust)**: ক্লায়েন্ট থেকে আসা প্রতিটি বাইট ডেটা সার্ভারে কঠোরভাবে যাচাই হবে।
2. **শেয়ার্ড Zod কন্ট্রাক্ট (`packages/contracts`)**:
   ```typescript
   export const CreateStyleSchema = z.object({
     styleNumber: z.string().min(2, "স্টাইল নম্বর কমপক্ষে ২ অক্ষরের হতে হবে").max(50),
     buyerId: z.string().uuid("বৈধ বায়ার সিলেক্ট করুন"),
     archetype: z.enum(['KNIT', 'WOVEN_NON_DENIM', 'WOVEN_DENIM', 'SWEATER', 'HYBRID_COMBO']),
     orderQuantity: z.number().int().positive("অর্ডার সংখ্যা অবশ্যই ১ বা তার বেশি হতে হবে"),
     targetFobPrice: z.number().positive("টার্গেট FOB মূল্য ০-এর বেশি হতে হবে"),
     deliveryDate: z.coerce.date().refine(date => date > new Date(), "ডেলিভারি ডেট ভবিষ্যতের তারিখ হতে হবে"),
   });
   ```
3. **NestJS গ্লোবাল পাইপ প্রটেকশন**:
   - কোনো রিকোয়েস্টে অঘোষিত কোনো ফিল্ড ঢুকলে NestJS সাথে সাথে `400 Bad Request` ফিরিয়ে দেবে (`forbidNonWhitelisted: true`)।
   - সব এরর রেসপন্স ইউজার-ফ্রেন্ডলি ভাষায় নির্দিষ্ট ফিল্ডের নামসহ ফেরত আসবে:
     ```json
     {
       "success": false,
       "error": {
         "code": "VALIDATION_ERROR",
         "message": "ইনপুট তথ্যে ভুল রয়েছে",
         "details": [
           { "field": "deliveryDate", "issue": "ডেলিভারি ডেট অতীতের হতে পারবে না" }
         ]
       }
     }
     ```

---

## ৬. নীতি ৮, ৯ ও ১০: মানুষের জন্য ইউআই — জিরো রোবোটিক/এআই ভাইব ও সহজ ভাষা

বসের সুনির্দিষ্ট নির্দেশ: **"UI/UX e unnecessary kono text or content or data hoba na, text must human readable & easy hoba kono complex word use hoba na, robotic or ai vibe hoba na."**

### ৬.১ জিরো রোবোটিক ক্লিশে ও অপ্রয়োজনীয় টেক্সট (Anti-Fluff Protocol)
- **নিষিদ্ধ শব্দগুচ্ছ**: "As an AI-driven platform...", "Intelligent deep learning automated recommendation...", "System generated neural computation...", "Lorem ipsum...", ইত্যাদি ভুয়া টেক্সট স্ক্রিনে থাকা কঠোরভাবে নিষিদ্ধ।
- **স্ক্রিনের পরিচ্ছন্নতা**: কোনো স্ক্রিনে অপ্রয়োজনীয় প্যারাগ্রাফ বা দীর্ঘ ব্যাখ্যা থাকবে না। শুধুমাত্র প্রাসঙ্গিক অ্যাকশন, পরিচ্ছন্ন ডেটা গ্রিড, স্পষ্ট বাটন এবং সংক্ষিপ্ত স্ট্যাটাস ব্যাজ থাকবে।

### ৬.২ খাঁটি গার্মেন্টস ও এন্টারপ্রাইজ পরিভাষা (Human Ergonomics)
- সফটওয়্যারের প্রতিটি শব্দ এমন হবে যা সাভার, গাজীপুর বা চট্টগ্রামের একজন মার্চেন্ডাইজার, কাটিং মাস্টার বা ফ্লোর ইনচার্জ প্রতিদিন ব্যবহার করেন:
  - জটিল: *"Execute automated procurement requisition orchestrator"* $\longrightarrow$ **সহজ: "ফেব্রিক রিকুইজিশন পাঠান" (Send Fabric Requisition)**
  - জটিল: *"Perform algorithmic quality defect mitigation"* $\longrightarrow$ **সহজ: "অল্টারেশন লিস্ট" (Alteration List)**
  - জটিল: *"Initiate bilateral intercompany accounting debit transaction"* $\longrightarrow$ **সহজ: "ইন্টারনাল ওয়াশ চালান" (Internal Wash Challan)**

---

## ৭. নীতি ১১: ফোল্ডার ও ফাইল নেমিং স্ট্যান্ডার্ড (Zero Robotic File Names)

বসের সুনির্দিষ্ট নির্দেশ: **"File & folder name e robotic or ai vibe hoba na."**

প্রজেক্টের আর্কিটেকচারে কোনো `ai_agent_...`, `bot_logic_...`, `generated_helper_...`, `temp_code_...` জাতীয় নাম থাকবে না। সবকিছু আন্তর্জাতিক মানের এন্টারপ্রাইজ সফটওয়্যার ইঞ্জিনিয়ারিং প্যাটার্ন মেনে চলবে:

```text
packages/
├── contracts/                  # টাইপস্ক্রিপ্ট ইন্টারফেস ও Zod ভ্যালিডেশন স্কিমা
│   └── src/
│       ├── merchandising/     # style.schema.ts, bom.schema.ts
│       ├── cutting/           # bundle.schema.ts, marker.schema.ts
│       └── common/            # api-response.schema.ts, pagination.schema.ts
└── database/                   # প্রিসমা ও ডেটাবেজ মাইগ্রেশন
    └── prisma/
        └── schema.prisma

apps/
├── api/                        # NestJS কোর ব্যাকএন্ড
│   └── src/
│       ├── modules/
│       │   ├── auth/          # auth.controller.ts, auth.service.ts
│       │   ├── organization/  # factory-unit.service.ts
│       │   ├── merchandising/ # style.service.ts, costing.service.ts
│       │   └── ui-dictionary/ # label.service.ts (ডায়নামিক লেবেল ইঞ্জিন)
│       └── core/              # filters, interceptors, guards
└── web/                        # Next.js 15 অ্যাডমিন ও ম্যানেজমেন্ট ড্যাশবোর্ড
    └── src/
        ├── app/
        │   ├── (auth)/        # login/
        │   └── (dashboard)/   # merchandising/, cutting/, washing/, admin/settings/
        ├── components/
        │   ├── data-table/    # data-table.tsx, data-table-toolbar.tsx, pagination.tsx
        │   └── ui/            # button.tsx, dialog.tsx, input.tsx (Tailwind v4)
        └── hooks/
            └── use-ui-dictionary.ts
```

---

## ৮. নীতি ১২: নিট, ক্লিন ও আল্ট্রা ইউজার-ফ্রেন্ডলি UI/UX (Sojib's Aesthetics)

- **মিনিমালিস্ট ও প্রফেশনাল স্পেসিং**: কোনো স্ক্রিনে উপচে পড়া ডাটা বা আঁটসাঁট ঘিঞ্জি লেআউট থাকবে না; সঠিক প্যাডিং ও হোয়াইটস্পেস থাকবে।
- **এক-নজরে বোঝা যায় এমন স্ট্যাটাস ব্যাজ (Visual Color Semantics)**:
  - 🟢 **Active / Approved**: নরম সবুজ ব্যাকগ্রাউন্ড (`bg-emerald-50 text-emerald-700`)
  - 🟡 **In Progress / Pending**: উষ্ণ অ্যাম্বার ব্যাকগ্রাউন্ড (`bg-amber-50 text-amber-700`)
  - 🔴 **Rejected / Critical Late**: পরিচ্ছন্ন লাল ব্যাকগ্রাউন্ড (`bg-rose-50 text-rose-700`)
- **জিরো ব্ল্যাঙ্ক স্ক্রিন ও ইনস্ট্যান্ট ফিডব্যাক**: ডেটা লোড হওয়ার সময় নিখুঁত স্কেলিটন কার্ড দেখা যাবে। বাটন ক্লিকে ইনস্ট্যান্ট অপটিমিস্টিক রেসপন্স ও টোস্ট নোটিফিকেশন আসবে।

---

## ৯. পেজ বনাম মোডাল বনাম স্লাইড-ওভার ড্রয়ার গভর্নেন্স (Page vs Modal vs Slide-over Drawer)

বসের সুনির্দিষ্ট নির্দেশ: **"Page & Modal kokhon use hoba?"**

গার্মেন্টস ফ্যাক্টরির দৈনন্দিন কাজের গতি এবং ইউজারের সুবিধার জন্য থ্রেডফ্লোতে ৩টি সুস্পষ্ট ডিসপ্লে প্যাটার্ন নির্ধারিত:

### ৯.১ পেজ বনাম মোডাল ডিসিশন ম্যাট্রিক্স

| প্যারামিটার | 📄 ডেডিকেটেড পেজ (Full Page) | 🪟 মোডাল / ডায়ালগ (Modal / Dialog) | 🚪 স্লাইড-ওভার ড্রয়ার (Slide-over Sheet) |
| :--- | :--- | :--- | :--- |
| **কাজের সময়কাল** | দীর্ঘমেয়াদী কাজ (> ২ মিনিট গভীর মনোযোগ) | আল্ট্রা-কুইক অ্যাকশন (< ৩০ সেকেন্ড) | রো ডিটেইলস কুইক ইন্সপেকশন (< ১ মিনিট) |
| **ইনপুট ফিল্ড সংখ্যা** | ৪টির বেশি ফিল্ড বা মাল্টি-সেকশন | ১ থেকে সর্বোচ্চ ৪টি ইনপুট ফিল্ড | ভিউ-অনলি সামারি বা ২-৩টি কুইক এডিট |
| **ইউআরএল ও হিস্ট্রি** | নিজস্ব ইউনিক URL (বুকমার্ক ও রিফ্রেশ সেফ) | কোনো URL পরিবর্তন নেই (ওভারলে) | অপশনাল URL কুয়েরি প্যারাম (`?drawer=id`) |
| **ব্যবহারের ক্ষেত্র** | - ডোমেন মাস্টার ডেটাটেবিল হাব<br/>- স্টাইল ও BOM ক্রিয়েশন ফ্লো<br/>- আইই লাইন প্ল্যানিং গ্যান্ট চার্ট<br/>- মাসিক পেরোল ডিসবার্সমেন্ট | - ডেসট্রাক্টিভ অ্যাকশন কনফার্মেশন<br/>- কুইক লুকআপ (যেমন: Add New Brand)<br/>- পাসওয়ার্ড রিসেট বা ওটিপি<br/>- ফ্লোর ক্যামেরা QR স্ক্যানার | - ডেটাটেবিলের যেকোনো রো-এর প্রিভিউ<br/>- ফ্যাব্রিক রোল ইন্সপেকশন লগ<br/>- বান্ডেল কিউসি হিস্টোরি দেখা |

### ৯.২ সজীবের ৩টি গোল্ডেন ইউএক্স রুলস:
1. **নো স্ক্রলবার যুদ্ধ (No Modal Scrollbar War)**: মোডালের উচ্চতা স্ক্রিনের ৭০%-এর বেশি হওয়া নিষিদ্ধ। কোনো ফর্মে লম্বা স্ক্রলবার লাগলে সেটি অবশ্যই ফুল পেজ হতে হবে।
2. **অ্যাক্সিডেন্টাল ডাটা লস প্রতিরোধ**: ইউজার যদি মোডালে কিছু টাইপ করে ভুলে বাইরে ক্লিক করে, তবে মোডাল সাথে সাথে বন্ধ হবে না; অ্যালার্ট দেবে: *"You have unsaved changes. Discard?"*
3. **কীবোর্ড এক্সেসিবিলিটি**: `Esc` চাপলে যেকোনো মোডাল বন্ধ হবে এবং ওপেন হওয়ার সাথে সাথে স্বয়ংক্রিয়ভাবে কার্সর প্রথম ইনপুট ফিল্ডে ফোকাস হবে।

---

## ১০. ১০০% আন্তর্জাতিক প্রফেশনাল ইংরেজি স্ট্যান্ডার্ড (100% English UI/UX Law)

বসের অলঙ্ঘনীয় নির্দেশ: **"ui/ux, message, notification etc must english hoba."**

বসের সাথে টিমের আলোচনা বাংলায় হলেও, সফটওয়্যারটির ফ্রন্টএন্ড, এরর মেসেজ এবং নোটিফিকেশন **১০০% খাঁটি ও প্রফেশনাল আন্তর্জাতিক ইংরেজিতে** পরিচালিত হবে:

### ১০.১ ইংরেজিতে বাধ্যতামূলক কম্পোনেন্টস
1. **UI/UX লেবেল, বাটন ও প্লেসহোল্ডার**:
   - বাটন: `"Save Changes"`, `"Confirm Order"`, `"Download Excel"`, `"Submit for Approval"`
   - লেবেল: `"Buyer Style Code"`, `"Fabric Consumption (Kg/Dzn)"`, `"Target Delivery Date"`
   - প্লেসহোল্ডার: `"Search by style, buyer, or PO number..."`
2. **সার্ভারসাইড Zod ভ্যালিডেশন ও এরর মেসেজ**:
   - `"Style code must be at least 2 characters long."`
   - `"Delivery date cannot be in the past."`
   - `"Insufficient fabric stock in Warehouse Bin A-12."`
3. **টোস্ট নোটিফিকেশন ও সিস্টেম অ্যালার্ট**:
   - 🟢 Success: `"Purchase Order PO-2026-081 created successfully."`
   - 🟡 Warning: `"Line 04 is currently operating below 65% efficiency."`
   - 🔴 Error: `"Failed to save changes. Please review the highlighted fields."`
4. **কনফার্মেশন ডায়ালগ**:
   - Title: `"Cancel Purchase Order?"`
   - Description: `"Are you sure you want to cancel PO-2026-081? This action will release reserved inventory and cannot be undone."`
   - Buttons: `[Keep Order]` / `[Yes, Cancel Order]`
5. **এক্সপোর্ট ডকুমেন্টস ও পিডিএফ (Reports)**:
   - Commercial Invoice, Packing List, Tech Pack, BOM Sheet—সবকিছু ১০০% আন্তর্জাতিক স্ট্যান্ডার্ড ইংরেজিতে রেন্ডার হবে।

---

## ১১. ইঞ্জিনিয়ারিং টিমের আনুষ্ঠানিক শপথ ও সাইন-অফ

- **🏛️ তানভীর (Principal Architect)**:
  *"বসের এই সংবিধান আমাদের সিস্টেমকে বিশ্বের শীর্ষস্থানীয় Katana বা SAP-এর চেয়েও ক্লিন, ডাইনামিক এবং আন্তর্জাতিক মানের করে তুলল।"*
- **🎨 সজীব (Frontend Craftsman)**:
  *"আমাদের ইন্টারফেসে পেজ, মোডাল ও স্লাইড-ওভার ড্রয়ার থাকবে নিখুঁত নিয়মে। প্রতিটি বাটন, টোস্ট এবং এরর মেসেজ হবে ১০০% ক্রিস্টাল-ক্লিয়ার আন্তর্জাতিক ইংরেজি।"*
- **⚡ আসিফ (Senior Backend)**:
  *"সার্ভারসাইড Zod ভ্যালিডেশনের সমস্ত এরর এনভেলপ আন্তর্জাতিক ইংরেজিতে ইউজার-ফ্রেন্ডলি ভাষায় রেডি থাকবে।"*
- **🛡️ মায়া (QA & Security Specialist)**:
  *"আমি প্রতিটি পিআরে টেস্ট চালাব যাতে কোনো বাংলা বা রোবোটিক শব্দ ইউআই বা মেসেজে লিক না হতে পারে।"*

---

> **অফিসিয়াল আর্কিটেকচারাল রেকর্ড**: `threadflow/docs/05-dynamic-system-architecture-and-ui-governance.md`  
> **অনুমোদনকারী**: বস (Founder / CTO / Head of Engineering)
