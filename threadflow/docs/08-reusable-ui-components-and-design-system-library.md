# 🎨 থ্রেডফ্লো রিইউজেবল ইউআই কম্পোনেন্ট লাইব্রেরি স্পেসিফিকেশন
### *ThreadFlow Enterprise Reusable UI Component Architecture (`@threadflow/ui`)*

> **ডকুমেন্ট আইডি**: `TF-DOC-008-REUSABLE-UI-COMPONENTS`  
> **প্রণেতা**: 🎨 সজীব (Frontend Craftsman), 🏛️ তানভীর (Principal Architect), 🛡️ মায়া (QA & Accessibility)  
> **অনুমোদনকারী**: 👑 বস (Founder / CTO / Head of Engineering)  
> **টেকনোলজি স্ট্যাক**: React 19 + TypeScript + Vite (SPA) + Radix UI + shadcn/ui + Tailwind CSS v4 + AG Grid Enterprise + Lucide Icons  
> **স্ট্যাটাস**: অফিসিয়াল ফ্রন্টএন্ড স্ট্যান্ডার্ড | **ভার্সন**: 2.0.0

---

## ১. ভূমিকা ও কম্পোনেন্ট গভর্নেন্স নীতি (Component Philosophy)

থ্রেডফ্লো ১২টি মাস্টার ডোমেনের একটি বিশাল এন্টারপ্রাইজ সিস্টেম। যদি প্রতিটি ডেভেলপার নিজ ইচ্ছামতো বাটন, ইনপুট বা টেবিল তৈরি করে, তবে সফটওয়্যার অগোছালো হয়ে পড়বে। বসের নির্দেশ অনুযায়ী:
1. **একক সোর্স অব ট্রুথ (Zero Reinventing the Wheel)**: সমস্ত পেজের ফর্ম, বাটন, মোডাল ও টেবিল আসবে শেয়ার্ড কম্পোনেন্ট লাইব্রেরি থেকে।
2. **অ্যাক্সেসিবিলিটি ও এরগোনোমিক্স (Keyboard-First)**: প্রতিটি কম্পোনেন্টে মাউস ছাড়াও কিবোর্ড শর্টকাট (`Tab`, `Enter`, `Esc`, `Arrow Keys`) দিয়ে দ্রুত কাজ করা যাবে।
3. **ডার্ক ও লাইট মোড বাই-ডিফল্ট**: আলাদা কোড লিখতে হবে না; প্রতিটি কম্পোনেন্ট আমাদের [06-design-system-and-color-palette.md](06-design-system-and-color-palette.md) টোকেন মেনে স্বয়ংক্রিয়ভাবে লাইট/ডার্ক মোড হ্যান্ডেল করবে।

---

## ২. কম্পোনেন্ট হায়ারার্কি আর্কিটেকচার (Atomic Design Pattern)

```
┌────────────────────────────────────────────────────────────────────────────────────────┐
│                        THREADFLOW COMPONENT LIBRARY HIERARCHY                          │
├────────────────────┬───────────────────────────────────────────────────────────────────┤
│ ১. Atoms (বেসিক)   │ Button, Input, Select, Badge, Checkbox, Tooltip, Avatar, Kbd      │
├────────────────────┼───────────────────────────────────────────────────────────────────┤
│ ২. Molecules       │ SearchableCombobox, DateRangePicker, CurrencyField, FormFieldRow  │
├────────────────────┼───────────────────────────────────────────────────────────────────┤
│ ৩. Organisms       │ EnterpriseDataTable, SlideOverDrawer, ActionDialog, DynamicToken  │
├────────────────────┼───────────────────────────────────────────────────────────────────┤
│ ৪. RMG Specialized │ BOMMatrixGrid, FabricShadeBadge, RelaxationTimer, AndonTVBoard    │
└────────────────────┴───────────────────────────────────────────────────────────────────┘
```

---

## ৩. কোর রিইউজেবল কম্পোনেন্ট স্পেসিফিকেশন (Core Components)

### ৩.১ ফর্ম ও ডাটা এন্ট্রি কম্পোনেন্টস (Data Input Suite)

#### ১. `SearchableCombobox` (ভার্চুয়ালাইজড বায়ার/ফেব্রিক সিলেক্টর)
* **প্রয়োজনীয়তা**: আরএমজি ফ্যাক্টরিতে হাজার হাজার ফেব্রিক বা বায়ার থাকে। সাধারণ HTML Dropdown ১০,০০০ আইটেম লোড করলে ব্রাউজার ফ্রিজ হয়ে যায়।
* **ফিচার**: TanStack Virtual চালিত সার্চ ড্রপডাউন, কিবোর্ড নেভিগেশন (`↑` `↓` `Enter`), ইনস্ট্যান্ট সার্চ ক্যাশিং।
* **ব্যবহার**:
```tsx
<SearchableCombobox
  label="Select Buyer"
  placeholder="Type buyer name or code..."
  fetchOptions={async (query) => searchBuyersApi(query)}
  value={selectedBuyerId}
  onChange={(buyer) => setSelectedBuyerId(buyer.id)}
  required
/>
```

#### ২. `CurrencyAmountInput` (প্রিসিশন কারেন্সি ফিল্ড)
* **প্রয়োজনীয়তা**: ডলারে এলসি ও কস্টিং এন্ট্রির সময় ফ্লোটিং পয়েন্ট এরর সম্পূর্ণ রোধ করা।
* **ফিচার**: কারেন্সি সিম্বল টগল (`$`, `৳`, `€`), ৪-ডিজিট ডেসিমাল প্রিসিশন মাস্কিং, অটো কমা সেপারেটর (যেমন: `$1,250,500.2500`)।

#### ৩. `BarcodeScannerInput` (হ্যান্ডহেল্ড গান ও ক্যামেরা স্ক্যানার)
* **প্রয়োজনীয়তা**: ফেব্রিক রোল, বান্ডেল টিকিট বা কাটিং পার্টস স্ক্যানিং।
* **ফিচার**: ইউএসবি বারকোড স্ক্যানার গান ডিটেকশন (<৫০ms কি-স্ট্রোক ডিটেক্টর) এবং ক্যামেরা স্ক্যানার ফালব্যাক।

---

### ৩.২ ডেটা ডিসপ্লে ও এন্টারপ্রাইজ গ্রিড (DataGrid & Display)

#### ১. `EnterpriseDataTable` (বসের বাধ্যতামূলক TanStack Table র্যাপার)
* **বসের অলঙ্ঘনীয় নীতি**: *"Must DataTable use hoba, normal table use hoba na."*
* **ফিচারস**:
  * স্টিকি হেডার (`sticky top-0`)
  * পিন্ড কলাম (যেমন: Style Code ও Actions কলাম সবসময় ফিক্সড স্ক্রিনে থাকবে)
  * জিব্রা স্ট্রাইপিং ও সফট রো হোভার
  * গ্লোবাল ইনস্ট্যান্ট সার্চ ফিল্টার ও কলাম ড্রপডাউন ফিল্টার
  * এক্সেল / CSV ইনস্ট্যান্ট এক্সপোর্ট
  * পেজিনেশন ও রো সিলেকশন
* **ব্যবহার**:
```tsx
<EnterpriseDataTable
  data={styleList}
  columns={styleColumns}
  isLoading={isLoading}
  pinnedColumns={{ left: ['styleCode'], right: ['actions'] }}
  onExportCsv={() => exportToCsv(styleList)}
  pagination={{ pageSize: 25 }}
/>
```

#### ২. `StatusBadge` (সিম্যান্টিক ট্রাফিক লাইট ব্যাজ)
* **প্রয়োজনীয়তা**: অর্ডার ও কিউসি স্ট্যাটাস এক পলকে চেনা।
* **ভেরিয়েন্টস**: `success` (Emerald), `warning` (Amber), `danger` (Rose), `info` (Sky), `special` (Violet)।
* **ব্যবহার**:
```tsx
<StatusBadge variant="success">Approved</StatusBadge>
<StatusBadge variant="danger">Late Delivery</StatusBadge>
```

#### ৩. `KPISummaryCard` (ড্যাশবোর্ড মেট্রিক কার্ড)
* **ফিচার**: বড় বোল্ড নাম্বার, ট্রেন্ড ইন্ডিকেটর (সবুজ আপ / লাল ডাউন অ্যারো), মাইক্রো-স্পার্কলাইন।

---

### ৩.৩ ফিডব্যাক ও ওভারলে কম্পোনেন্টস (Feedback & Overlays)

#### ১. `SlideOverDrawer` (কনটেক্সচুয়াল ড্রয়ার)
* **সুবিধা**: পেজ ছেড়ে না গিয়ে টেবিলের কোনো রো-তে ক্লিক করলে ডানপাশ থেকে স্লাইড হয়ে সম্পূর্ণ ডিটেইলস (যেমন: স্টাইলের পুরো BOM বা ফেব্রিক টেস্ট রিপোর্ট) ওপেন হবে।
* **ব্যবহার**: Radix Sheet চালিত, `Esc` চাপলে বন্ধ হয়ে আগের টেবিলে নিখুঁত ফোকাস ফিরিয়ে নেয়।

#### ২. `ActionConfirmDialog` (ডেস্ট্রাক্টিভ অ্যাকশন গার্ড)
* **সুবিধা**: কোনো ডাটা ডিলিট বা স্ট্যাটাস ক্লোজ করার আগে টাইপ-টু-কনফার্ম বা সতর্কবার্তা পপআপ।

#### ৩. `Toast` (সিস্টেম নোটিফিকেশন)
* **লাইব্রেরি**: Sonner ইন্টিগ্রেশন। স্ক্রিনের নিচে-ডানে নিখুঁত নন-ইনট্রুসিভ সাকসেস/এরর টোস্ট।

---

### ৩.৪ আরএমজি স্পেশালাইজড কম্পোনেন্টস (Factory Domain Suite)

#### ১. `BOMMatrixGrid` (সাইজ $\times$ কালার ২ডি গ্রিড ম্যাট্রিক্স)
* **সমস্যা**: টি-শার্টের ১০টি সাইজ (S, M, L, XL, XXL) এবং ৫টি রঙের জন্য ৫০ বার রো এন্ট্রি করা দুঃসাধ্য।
* **সমাধান**: স্প্রেডশিটের মতো ২ডি ম্যাট্রিক্স গ্রিড, যেখানে কিবোর্ডের `Tab` ও `Arrow` কী দিয়ে দ্রুত কোয়ান্টিটি ইনপুট দেওয়া যায়।

#### ২. `RelaxationCountdownBadge` (ফেব্রিক রিল্যাক্সেশন টাইমার)
* **ফিচার**: লাইভ কাউন্টডাউন ব্যাজ (০-২৪ ঘণ্টা), চলমান অবস্থায় নীল পালসিং লাইট, সম্পন্ন হলে গ্রিন চেকমার্ক।

#### ৩. `DynamicTokenBuilder` (কোড ও সিকোয়েন্স ফরম্যাট বিল্ডার)
* **ফিচার**: অ্যাডমিন প্যানেলে কোম্পানি বা স্টাইল কোডের ফরম্যাট তৈরির ভিজ্যুয়াল চিপস ও লাইভ প্রিভিউ ইন্টারফেস।

---

## ৪. ফোল্ডার স্ট্রাকচার ও প্যাকেজ অর্গানাইজেশন

মনোরিপোতে সমস্ত কম্পোনেন্ট `@threadflow/ui` প্যাকেজে থাকবে:

```
packages/ui/
├── src/
│   ├── atoms/
│   │   ├── button.tsx
│   │   ├── input.tsx
│   │   ├── badge.tsx
│   │   ├── select.tsx
│   │   ├── checkbox.tsx
│   │   └── kbd.tsx
│   ├── molecules/
│   │   ├── searchable-combobox.tsx
│   │   ├── currency-amount-input.tsx
│   │   ├── date-range-picker.tsx
│   │   └── barcode-scanner-input.tsx
│   ├── organisms/
│   │   ├── enterprise-datatable.tsx
│   │   ├── slide-over-drawer.tsx
│   │   ├── action-confirm-dialog.tsx
│   │   └── dynamic-token-builder.tsx
│   ├── rmg/
│   │   ├── bom-matrix-grid.tsx
│   │   ├── relaxation-countdown-badge.tsx
│   │   └── fabric-shade-swatch.tsx
│   └── index.ts
└── package.json
```

---

## ৫. ইঞ্জিনিয়ারিং টিমের সাইন-অফ

- **🎨 সজীব (Frontend Craftsman)**:
  *"বস, এই ডকুমেন্ট আমাদের জন্য বাইবেল! পুরো অ্যাপে কোথাও কোনো এলোমেলো বাটন বা আনস্টাইলড ইনপুট থাকবে না। প্রতিটি কম্পোনেন্ট দেখতে প্রিমিয়াম এবং ব্যবহারে বাটারি-স্মুথ হবে।"*
- **🏛️ তানভীর (Principal Architect)**:
  *"শেয়ার্ড `@threadflow/ui` প্যাকেজ থাকায় আমাদের ফ্রন্টএন্ড কোডবেস ডুপ্লিকেশন মুক্ত এবং ১০০% মেইনটেইনেবল থাকবে।"*
- **🛡️ মায়া (QA Specialist)**:
  *"কিবোর্ড এক্সেসিবিলিটি (WAI-ARIA), স্ক্রিন রিডার সাপোর্ট এবং ডার্ক মোড টেস্ট—সবকিছু প্রতিটি কম্পোনেন্টে বাধ্যতামূলক করা হয়েছে।"*

---

> **সংরক্ষিত ও কার্যকর**: `threadflow/docs/08-reusable-ui-components-and-design-system-library.md`
