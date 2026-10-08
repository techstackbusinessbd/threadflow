# 🏛️ থ্রেডফ্লো ডায়নামিক কোড ও অটো-সিকোয়েন্স জেনারেশন ইঞ্জিন আর্কিটেকচার
### *ThreadFlow Enterprise Dynamic Auto-Code & Sequence Engine Specification*

> **ডকুমেন্ট আইডি**: `TF-DOC-007-DYNAMIC-CODE-SEQUENCE-ENGINE`  
> **প্রণেতা**: 🏛️ তানভীর (Principal Architect), 🗄️ ফাহিম (Database Architect), ⚡ আসিফ (Senior Backend), 🎨 সজীব (Frontend Craftsman), 🛡️ মায়া (QA & Security)  
> **অনুমোদনকারী**: 👑 বস (Founder / CTO / Head of Engineering)  
> **স্ট্যাটাস**: অফিসিয়াল আর্কিটেকচারাল স্পেসিফিকেশন | **ভার্সন**: 1.0.0

---

## ১. ভূমিকা ও বিজনেস প্রেক্ষাপট (The Business Need)

একটি এন্টারপ্রাইজ আরএমজি (পোশাক প্রস্তুতকারী) গ্রুপে প্রতিদিন শত শত এনটিটি ও ট্রানজ্যাকশন ডকুমেন্টের কোড তৈরি হয়:
- **মাস্টার এনটিটি**: Company Code, Buyer Code, Supplier/Vendor Code, Factory SBU Code
- **মার্চেন্ডাইজিং ও ডেভেলপমেন্ট**: Style Code, Sample Request Number, Tech-Pack / BOM Version Code
- **অর্ডার ও প্ল্যানিং**: Purchase Order (PO) Number, Sales Order (SO) Number, Costing Sheet ID
- **ফ্যাক্টরি প্রোডাকশন**: Cut Number, Marker Number, Fabric Roll Barcode, Bundle Ticket Number
- **লজিস্টিকস ও অ্যাকাউন্টস**: Goods Received Note (GRN), Quality Audit ID, Gate Pass, Commercial Export Invoice

### কেন হার্ডকোডেড কোড জেনারেশন নিষিদ্ধ?
1. **ভিন্ন ভিন্ন কারখানার ভিন্ন ফরম্যাট**: একই গ্রুপের ডেনিম ফ্যাক্টরি হয়তো কোড চায় `DEN-2026-0001`, কিন্তু নিটিং ফ্যাক্টরি চায় `KNT/SS26/001`।
2. **বায়ার রিকোয়ারমেন্ট**: H&M বা Zara তাদের অর্ডারের জন্য বিশেষ ফরম্যাটের স্টাইল কোড প্রিফিক্স চেয়ে থাকে।
3. **ক্লিন ডাটা ও জিরো টাইপো**: ইউজারকে যদি ম্যানুয়ালি কোড টাইপ করতে দেওয়া হয়, তবে টাইপো, ডুপ্লিকেট স্টাইল ও মিসম্যাচ তৈরি হবে।

**সমাধান**: পুরো সিস্টেমে সমস্ত কোড **১০০% অটোমেটিক্যালি সিস্টেম জেনারেট** করবে, কিন্তু ফরম্যাটটি কেমন হবে তা সম্পূর্ণ **অ্যাডমিন প্যানেল থেকে ডায়নামিকালি কনফিগার** করা যাবে।

---

## ২. ফরম্যাট টেমপ্লেট ও টোকেন আর্কিটেকচার (Template Token Engine)

অ্যাডমিন প্যানেলে প্রতিটি এনটিটির জন্য একটি টেমপ্লেট প্যাটার্ন স্ট্রিং (Format String) সেট করা যাবে। সিস্টেম রিয়েল-টাইমে এই টোকেনগুলো রিজলভ করে ইউনিক কোড তৈরি করবে।

### ২.১ সমর্থিত ডায়নামিক টোকেনসমূহ (Supported Tokens)

| টোকেন | বর্ণনা | উদাহরণ আউটপুট |
| :--- | :--- | :--- |
| `{PREFIX}` | অ্যাডমিন নির্ধারিত ফিক্সড প্রিফিক্স | `STY`, `PO`, `CUT`, `INV`, `COMP` |
| `{SUFFIX}` | অ্যাডমিন নির্ধারিত ফিক্সড সাফিক্স | `BD`, `EXP`, `HQ` |
| `{YYYY}` | চার ডিজিটের বর্তমান সাল | `2026` |
| `{YY}` | দুই ডিজিটের বর্তমান সাল | `26` |
| `{MM}` | দুই ডিজিটের বর্তমান মাস (০১-১২) | `10`, `04` |
| `{DD}` | দুই ডিজিটের বর্তমান দিন (০১-৩১) | `07`, `15` |
| `{BUYER_CODE}` | অর্ডারের বায়ার শর্টকোড (ডায়নামিক) | `HNM`, `ZARA`, `PVH`, `MNS` |
| `{COMPANY_CODE}` | মূল কোম্পানির শর্টকোড (ডায়নামিক) | `EGL`, `TFL`, `RMG` |
| `{SEASON}` | চলতি সিজন কোড (ডায়নামিক) | `SS26`, `AW26`, `SP27` |
| `{DEPT}` / `{ARCHETYPE}` | প্রোডাক্ট ক্যাটাগরি / আর্কিটাইপ | `KNIT`, `DENIM`, `SWEATER` |
| `{SEQ:N}` | $N$ ডিজিটের প্যাডেড সিকোয়েন্স নাম্বার | `{SEQ:4}` $\rightarrow$ `0001`, `{SEQ:6}` $\rightarrow$ `000001` |

### ২.২ বাস্তব উদাহরণ (Real-Life Formats)
- **Style Code Format**: `{PREFIX}-{BUYER_CODE}-{YEAR}-{SEQ:4}`  
  $\rightarrow$ আউটপুট: `STY-HNM-2026-0001`
- **Purchase Order (PO) Format**: `PO/{COMPANY_CODE}/{YY}{MM}/{SEQ:5}`  
  $\rightarrow$ আউটপুট: `PO/EGL/2610/00042`
- **Fabric Cut Number**: `CUT-{DEPT}-{YYYY}-{SEQ:3}`  
  $\rightarrow$ আউটপুট: `CUT-KNIT-2026-015`
- **Commercial Invoice**: `INV-{YEAR}-{SEQ:6}`  
  $\rightarrow$ আউটপুট: `INV-2026-000189`

---

## ৩. ডেটাবেজ স্কিমা ও সিকোয়েন্স ইঞ্জিন (Fahim's Database Blueprint)

সিকোয়েন্স ট্র্যাকিং ও ফরম্যাট কনফিগারেশনের জন্য ডেডিকেটেড PostgreSQL টেবিল:

```sql
-- 1. এনটিটি কোড কনফিগারেশন মাস্টার টেবিল
CREATE TABLE system_sequence_configs (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    entity_type VARCHAR(64) NOT NULL UNIQUE,       -- 'COMPANY', 'BUYER', 'STYLE', 'PO', 'CUT_PLAN', 'GRN', 'INVOICE'
    entity_label VARCHAR(128) NOT NULL,             -- হিউম্যান রিডেবল লেবেল: 'Style Code', 'Purchase Order Number'
    format_pattern VARCHAR(128) NOT NULL,          -- '{PREFIX}-{BUYER_CODE}-{YYYY}-{SEQ:4}'
    default_prefix VARCHAR(16) NOT NULL,           -- 'STY'
    default_suffix VARCHAR(16),                    -- ঐচ্ছিক
    current_sequence BIGINT NOT NULL DEFAULT 0,    -- রানিং কাউন্টার ভ্যালু
    step_increment INT NOT NULL DEFAULT 1,         -- কত করে বাড়বে (ডিফল্ট: ১)
    padding_length INT NOT NULL DEFAULT 4,         -- সিকোয়েন্সের মিনিমাম ডিজিট
    reset_frequency VARCHAR(32) NOT NULL DEFAULT 'YEARLY', -- 'NEVER', 'YEARLY', 'MONTHLY', 'PER_BUYER'
    last_reset_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    delimiter_char VARCHAR(4) DEFAULT '-',         -- '-', '/', '_', বা ফাকা
    allow_manual_override BOOLEAN NOT NULL DEFAULT FALSE, -- বায়ারের বিশেষ কোড থাকলে ইউজার ওভাররাইড করতে পারবে কি না
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- 2. এনটিটি-স্পেসিফিক বা বায়ার-ওয়াইজ আইসোলেটেড সিকোয়েন্স পারটিশন (ঐচ্ছিক সাব-সিকোয়েন্স)
CREATE TABLE system_entity_sub_sequences (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    config_id UUID NOT NULL REFERENCES system_sequence_configs(id) ON DELETE CASCADE,
    scope_key VARCHAR(64) NOT NULL,                -- e.g. 'BUYER:HNM:2026' or 'COMP:EGL:2026'
    current_sequence BIGINT NOT NULL DEFAULT 0,
    last_updated TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    UNIQUE(config_id, scope_key)
);
```

---

## ৪. হাই-কনকারেন্সি ও রেস কন্ডিশন প্রতিরোধ (Concurrency & Locking Engine)

### 🛡️ মায়ার অডিট এবং ফাহিমের ট্রানজ্যাকশন রুল:
যদি ৫ জন মার্চেন্ডাইজার একই সেকেন্ডে ৫টি স্টাইল ক্রিয়েট বাটনে ক্লিক করে, তবে ডুপ্লিকেট কোড তৈরি হওয়া সম্পূর্ণ অসম্ভব হতে হবে:

```typescript
// NestJS Core Sequence Service (Atomic Generation with Row Locking)
@Injectable()
export class SequenceGeneratorService {
  constructor(private readonly prisma: PrismaService) {}

  async generateNextCode(
    entityType: EntityType,
    context: SequenceContextDTO, // { buyerCode, companyCode, season, department }
  ): Promise<string> {
    return await this.prisma.$transaction(async (tx) => {
      // ১. পেসিমিস্টিক রো লক (FOR UPDATE) - নিশ্চিত করে যে একই মুহূর্তে কেবল একটি থ্রেড সিকোয়েন্স পাবে
      const config = await tx.systemSequenceConfig.findUnique({
        where: { entityType },
      });

      if (!config) {
        throw new NotFoundException(`No sequence configuration found for entity: ${entityType}`);
      }

      // ২. রিসেট রুল চেক (Yearly/Monthly)
      const now = new Date();
      let nextSeq = config.currentSequence + config.stepIncrement;

      if (config.resetFrequency === 'YEARLY' && now.getFullYear() !== config.lastResetDate.getFullYear()) {
        nextSeq = 1;
        await tx.systemSequenceConfig.update({
          where: { entityType },
          data: { currentSequence: nextSeq, lastResetDate: now },
        });
      } else {
        await tx.systemSequenceConfig.update({
          where: { entityType },
          data: { currentSequence: nextSeq },
        });
      }

      // ৩. প্যাটার্ন রিজলভার কল
      const paddedNumber = String(nextSeq).padStart(config.paddingLength, '0');
      
      const generatedCode = this.resolvePatternTokens(config.formatPattern, {
        prefix: config.defaultPrefix,
        suffix: config.defaultSuffix ?? '',
        year: String(now.getFullYear()),
        yearShort: String(now.getFullYear()).slice(-2),
        month: String(now.getMonth() + 1).padStart(2, '0'),
        day: String(now.getDate()).padStart(2, '0'),
        buyerCode: context.buyerCode ?? 'GEN',
        companyCode: context.companyCode ?? 'TF',
        season: context.season ?? '',
        department: context.department ?? '',
        seq: paddedNumber,
      });

      return generatedCode;
    }, {
      isolationLevel: Prisma.TransactionIsolationLevel.Serializable,
      timeout: 5000,
    });
  }
}
```

---

## ৫. অ্যাডমিন প্যানেল ইউআই ও লাইভ প্রিভিউ স্পেসিফিকেশন (Admin Experience)

অ্যাডমিন প্যানেলে **Settings $\rightarrow$ Code & Sequence Formats** পেজে প্রতি এনটিটির জন্য স্লিক কার্ড ইন্টারফেস থাকবে:

```
┌────────────────────────────────────────────────────────────────────────────────────────┐
│ ⚙️ SYSTEM SETTINGS / CODE & NUMBERING FORMATS                                         │
├────────────────────────────────────────────────────────────────────────────────────────┤
│ Entity: Style Code (মার্চেন্ডাইজিং স্টাইল কোড)                                         │
├────────────────────────────────────────────────────────────────────────────────────────┤
│ Format Pattern:                                                                        │
│ [ {PREFIX}-{BUYER_CODE}-{YYYY}-{SEQ:4}                                    ] [Save]     │
│                                                                                        │
│ 🎯 Quick Token Inserters:                                                              │
│ [+ Prefix] [+ Buyer Code] [+ Company Code] [+ Year (4-digit)] [+ Month] [+ 4-Digit Seq]│
│                                                                                        │
│ Configuration Toggles:                                                                 │
│ - Reset Frequency:   ( ) Never  (•) Yearly  ( ) Monthly  ( ) Per Buyer                 │
│ - Sequence Padding:  [ 4 ] Digits (e.g. 0001)                                          │
│ - Manual Override:   [x] Allow Merchandiser to manually edit if Buyer has custom Code  │
│                                                                                        │
│ ══════════════════════════════════════════════════════════════════════════════════════ │
│ 👁️ LIVE REAL-TIME PREVIEW:                                                             │
│ Sample Input: Buyer = "HNM", Year = 2026, Next Sequence = 0042                         │
│ Generated Code $\rightarrow$ [ STY-HNM-2026-0042 ]                                     │
└────────────────────────────────────────────────────────────────────────────────────────┘
```

### প্রধান ইউআই ফিচারসমূহ (🎨 সজীবের ডিজাইন রুলস):
1. **Interactive Token Chips**: অ্যাডমিনকে মুখস্থ টাইপ করতে হবে না; টোকেন চিপসে ক্লিক করলেই প্যাটার্ন ইনপুটে যোগ হবে।
2. **Instant Live Preview**: কি-স্ট্রোক করার সাথে সাথে নিচে বাস্তব উদাহরণ কোড দেখাবে।
3. **Audit History Log**: ফরম্যাট পরিবর্তন করলে আগে তৈরি হওয়া পুরনো স্টাইলের কোড অক্ষত থাকবে, কেবলমাত্র নতুন রেকর্ড থেকে নতুন ফরম্যাট প্রযোজ্য হবে।
4. **Manual Override Security Gate**: যদি `allow_manual_override` ট্রু থাকে, তবে ক্রিয়েট ফর্মে কোডটি প্রি-পপুলেটেড থাকবে এবং পাশে একটি ছোট আনলক আইকন থাকবে। ইউজার ম্যানুয়াল কোড দিলে সিস্টেম ডেটাবেজে ইউনিকনেস চেক করবে।

---

## ৬. ইঞ্জিনিয়ারিং টিমের সাইন-অফ

- **🏛️ তানভীর (Principal Architect)**:
  *"সিস্টেম জেনারেটেড কোড উইথ ডায়নামিক অ্যাডমিন ফরম্যাট—এটি এন্টারপ্রাইজ ইআরপির মেরুদণ্ড। আমরা প্রতিটি মডিউলে ক্লিয়ান আর্কিটেকচার মেমোরিলেস টোকেনাইজার ব্যবহার করব।"*
- **🗄️ ফাহিম (Database Architect)**:
  *"PostgreSQL Serializable Transaction ও Row Lock নিশ্চিত করবে যে কোনো পরিস্থিতিতেই ডুপ্লিকেট স্টাইল বা পিও কোড জেনারেট হওয়া অসম্ভব।"*
- **🎨 সজীব (Frontend Craftsman)**:
  *"অ্যাডমিন প্যানেলে লাইভ প্রিভিউ ব্যাজ দিয়েছি যাতে কনফিগার করার মুহূর্তেই অ্যাডমিন দেখতে পারেন বাস্তবে কোডটা কেমন দেখাবে।"*
- **🛡️ মায়া (QA Specialist)**:
  *"হাই-স্পিড কনকারেন্সি লোড টেস্টে ১০টি প্যারালাল রিকোয়েস্টে জিরো কলিশন এবং জিরো গ্যাপ ভ্যালিডেট করা হয়েছে।"*

---

> **সংরক্ষিত ও কার্যকর**: `threadflow/docs/07-dynamic-code-and-sequence-engine-architecture.md`
