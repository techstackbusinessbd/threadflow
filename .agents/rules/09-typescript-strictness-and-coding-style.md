# 🏛️ রুলবুক ০৯: টাইপস্ক্রিপ্ট স্ট্রিক্টনেস ও কোডিং স্টাইল স্ট্যান্ডার্ডস (TypeScript & Clean Code)

> **দায়িত্বপ্রাপ্ত লিড**: তানভীর (Tech Lead) & আসিফ (Backend Lead)  
> **ক্যাটাগরি**: TypeScript কনফিগ, ক্লিন কোড প্রিন্সিপাল, নেমিং কনভেনশন ও কোড ফরম্যাটিং  
> **ভার্সন**: 1.0.0 | **স্ট্যাটাস**: অলঙ্ঘনীয় নিয়ম (Non-Negotiable)

---

## ১. স্ট্রিক্ট টাইপস্ক্রিপ্ট নীতি (The Zero 'any' Law)

আমাদের কোডবেসে টাইপস্ক্রিপ্টের কোনো ঢিলেঢালা কনফিগারেশন থাকবে না। `tsconfig.json`-এ নিচের ফ্ল্যাগগুলো বাধ্যতামূলক:

```json
{
  "compilerOptions": {
    "strict": true,
    "noImplicitAny": true,
    "strictNullChecks": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "exactOptionalPropertyTypes": true
  }
}
```

### অলঙ্ঘনীয় নিয়ম:
1. **`any` কিওয়ার্ড সম্পূর্ণ নিষিদ্ধ**:
   - কোনো ভ্যারিয়েবল, প্যারামিটার বা রিটার্ন টাইপে `any` লেখা যাবে না। টাইপ অজানা থাকলে `unknown` ব্যবহার করে টাইপ গার্ড (Type Guard) দিয়ে ন্যারো করতে হবে।
2. **প্রতিটি ফাংশনে এক্সপ্লিসিট রিটার্ন টাইপ বাধ্যতামূলক**:
   - টাইপস্ক্রিপ্ট ইনফারেন্সের ওপর নির্ভর না করে ফাংশন ঠিক কী রিটার্ন করবে তা স্পষ্টভাবে লিখতে হবে:
     ```typescript
     // ❌ নিষিদ্ধ:
     calculateEfficiency(output, target) { return (output / target) * 100; }

     // ✅ বাধ্যতামূলক:
     calculateEfficiency(output: number, target: number): Decimal {
       return new Decimal(output).div(target).mul(100);
     }
     ```

---

## ২. নেমিং কনভেনশন স্ট্যান্ডার্ড (Naming Conventions)

| কোড এলিমেন্ট | কেসিং স্টাইল | বাস্তব উদাহরণ |
| :--- | :--- | :--- |
| **ক্লাস ও টাইপ/ইন্টারফেস** | `PascalCase` | `CostingSheet`, `CuttingOrderEntity`, `FabricRollDto` |
| **ভেরিয়েবল ও মেথড** | `camelCase` | `calculateMarkerConsumption`, `totalGrossWeight` |
| **কনস্ট্যান্ট ও ইনাম (Enum)** | `UPPER_SNAKE_CASE` | `MAX_PLY_HEIGHT_KNIT`, `DEFAULT_CURRENCY_USD` |
| **ফাইল ও ফোল্ডার নেম** | `kebab-case` | `consumption-calculator.ts`, `cutting-order.dto.ts` |
| **বুলিয়ান ভেরিয়েবল** | `is/has/should` প্রিফিক্স | `isApproved`, `hasDefects`, `shouldRecalculate` |

---

## ৩. ম্যাজিক নাম্বার নিষিদ্ধ (No Magic Numbers)
কোডের ভেতর সরাসরি কোনো রহস্যময় সংখ্যা (যেমন: `208`, `0.02`, `15`) হার্ডকোড করা নিষিদ্ধ। প্রতিটি সংখ্যা একটি সেলফ-এক্সপ্ল্যানেটরি কনস্ট্যান্টে ডিক্লেয়ার হতে হবে:
```typescript
// ❌ নিষিদ্ধ:
const otRate = (basicSalary / 208) * 2;

// ✅ অনুমোদিত:
export const BANGLADESH_LABOR_LAW_OT_DIVIDER = 208;
export const OVERTIME_MULTIPLIER = 2.0;

const hourlyBasic = new Decimal(basicSalary).div(BANGLADESH_LABOR_LAW_OT_DIVIDER);
const otRate = hourlyBasic.mul(OVERTIME_MULTIPLIER);
```

---

## ৪. ফাংশনাল সাইজ ও ক্লিন কোড রুল (Function Purity & Size)
1. **এক ফাংশন, এক দায়িত্ব (Single Responsibility)**:
   - কোনো ফাংশন ৫০ লাইনের বেশি হতে পারবে না। বড় ফাংশন থাকলে তাকে অর্থপূর্ণ ছোট ছোট প্রাইভেট ফাংশনে ভাগ করতে হবে।
2. **ডিপ নেস্টিং নিষিদ্ধ (Max 2-3 Indentation Levels)**:
   - `if { if { if { ... } } }` এড়াতে **আর্লি রিটার্ন / গার্ড ক্লজ (Early Return)** প্যাটার্ন ব্যবহার করতে হবে।
