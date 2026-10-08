# 🗄️ রুলবুক ০২: ডেটাবেজ স্কিমা, ট্রানজ্যাকশন ও ওআরএম স্ট্যান্ডার্ডস (Database, Transactions & ORM)

> **দায়িত্বপ্রাপ্ত লিড**: ফাহিম (Database Architect & Data Engineer) & আসিফ (Backend Lead)  
> **ক্যাটাগরি**: PostgreSQL 16+, EF Core 10, Dapper, স্কিমা কনভেনশন, ট্রানজ্যাকশনাল ইন্টিগ্রিটি ও ওআরএম রুলস  
> **ভার্সন**: 2.0.0 (Officially Aligned with Technology Stack SRS) | **স্ট্যাটাস**: অলঙ্ঘনীয় নিয়ম (Non-Negotiable)

---

## ১. ডেটাবেজ ইঞ্জিন ও কনভেনশন রুলস (PostgreSQL Conventions)

1. **টেবিল ও কলাম নেমিং কনভেনশন**:
   - টেবিলের নাম হবে **প্লুরাল এবং snake_case**: যেমন `styles`, `purchase_orders`, `cutting_lay_sheets`, `fabric_rolls`, `bundle_cards`।
   - **প্রাইমারি কি (Primary Key)**: সাধারণ অটো-ইনক্রিমেন্ট ইন্টিজার ID (`1, 2, 3...`) বা আন-সর্টেড র‍্যান্ডম UUIDv4 সম্পূর্ণ নিষিদ্ধ। সবসময় আধুনিক **UUIDv7 (RFC 9562 - Time-Ordered / Sequential UUID)** ব্যবহার করতে হবে: `id UUID DEFAULT uuid_generate_v7()`। এটি B-Tree ইনডেক্স ফ্র্যাগমেন্টেশন মুক্ত রাখে, সুপার-ফাস্ট ইনসার্ট দেয় এবং মিলিসেকেন্ড ক্রনোলজিক্যাল সর্টিং নিশ্চিত করে।
   - ফরেন কি হবে `<singular_table_name>_id UUID`: যেমন `style_id UUID`, `factory_id UUID`, `vendor_id UUID`, `cutting_order_id UUID`।
2. **টাইমস্ট্যাম্প মানদণ্ড**:
   - ডেটাবেজের সমস্ত টাইমস্ট্যাম্প বাধ্যতামূলকভাবে `TIMESTAMPTZ` (UTC) হতে হবে। লোকাল টাইমস্ট্যাম্প বা টাইমজোন ছাড়া ডেটা সেভ করা সম্পূর্ণ নিষিদ্ধ।

---

## ২. অলঙ্ঘনীয় জিরো-ফ্লোট নিয়ম (The Zero Float Math Law)

```sql
-- ❌ সম্পূর্ণ নিষিদ্ধ (FORBIDDEN!):
consumption REAL,
unit_price DOUBLE PRECISION,
total_amount FLOAT

-- ✅ বাধ্যতামূলক স্ট্যান্ডার্ড (MANDATORY):
consumption DECIMAL(12, 4) NOT NULL,
unit_price DECIMAL(14, 4) NOT NULL,
total_amount DECIMAL(18, 4) NOT NULL,
currency_exchange_rate DECIMAL(10, 6) NOT NULL
```

### কোড লেভেল নিয়ম (C# / EF Core 10):
- ব্যাকএন্ড কোডে টাকা, কারেন্সি বা কাপড়ের কনসাম্পশনে কখনোই `float` বা `double` ব্যবহার করা যাবে না।
- সবসময় C# নেটিভ **`decimal`** ব্যবহার করতে হবে এবং EF Core-এ প্রিসিশন কনফিগার করতে হবে:
  ```csharp
  // EF Core Entity Configuration
  builder.Property(x => x.Consumption)
      .HasPrecision(12, 4)
      .IsRequired();

  builder.Property(x => x.UnitPrice)
      .HasPrecision(14, 4)
      .IsRequired();
  ```

---

## ৩. বাধ্যতামূলক মাল্টি-স্টেপ ট্রানজ্যাকশন রুল (The Mandatory ACID Transaction Law)

যদি কোনো ব্যবসায়িক অপারেশনে ১টির বেশি রো বা টেবিলে রাইট হয়, তবে তা অবশ্যই ডেটাবেজ ট্রানজ্যাকশনের মধ্যে আবদ্ধ থাকতে হবে:
- **বাস্তব উদাহরণ**: 
  1. স্টোর থেকে কাপড় ইস্যু $\rightarrow$ ফেব্রিক রোল ব্যালেন্স মাইনাস $\rightarrow$ স্টোর লেজারে আউটওয়ার্ড এন্ট্রি $\rightarrow$ কাটিং রুমে রিসিভ।
  2. এই ৪টি স্টেপের যেকোনো একটি ফেইল করলে পুরো অপারেশন স্বয়ংক্রিয়ভাবে রোলব্যাক (Rollback) হবে। আংশিক পরিবর্তন কোনোভাবেই ডেটাবেজে স্থায়ী হতে পারবে না।

```csharp
// ✅ EF Core 10 Approved Transaction Pattern:
await using var transaction = await _dbContext.Database.BeginTransactionAsync(
    IsolationLevel.ReadCommitted, cancellationToken);

try
{
    var roll = await _dbContext.FabricRolls.FindAsync(new object[] { rollId }, cancellationToken);
    roll.DeductStock(quantityToIssue);

    var ledgerEntry = new InventoryLedgerEntry(rollId, quantityToIssue, TransactionType.Issue);
    await _dbContext.InventoryLedgers.AddAsync(ledgerEntry, cancellationToken);

    var cuttingIssue = new CuttingFabricIssue(rollId, cuttingOrderId, quantityToIssue);
    await _dbContext.CuttingFabricIssues.AddAsync(cuttingIssue, cancellationToken);

    await _dbContext.SaveChangesAsync(cancellationToken);
    await transaction.CommitAsync(cancellationToken);
}
catch (Exception)
{
    await transaction.RollbackAsync(cancellationToken);
    throw;
}
```

---

## ৪. হাই-স্পিড রিড অপটিমাইজেশন (Dapper Micro-ORM)

- এগ্রিগেটেড ড্যাশবোর্ড, লাইভ প্রোডাকশন এফিসিয়েন্সি বোর্ড এবং হাজার হাজার রো-এর জটিল জয়েন কোয়েরিতে EF Core ব্যবহার না করে **Dapper** দিয়ে অপটিমাইজড SQL চালাতে হবে:
```csharp
// ✅ Dapper High-Speed Query Pattern:
const string sql = @"
    SELECT line_id, SUM(hourly_output_qty) AS total_output, AVG(efficiency_rate) AS avg_efficiency
    FROM sewing_line_outputs
    WHERE factory_id = @FactoryId AND production_date = @ProductionDate
    GROUP BY line_id;";

var summary = await _dapperConnection.QueryAsync<LineEfficiencySummaryDto>(sql, new { factoryId, productionDate });
```

---

## ৫. সফট-ডিলিট ও অডিট ট্রেইল স্ট্যান্ডার্ড (Audit Trail & Soft Deletion)

পোশাক কারখানায় অডিট কমপ্লায়েন্সের কারণে কোনো অর্ডার, ইনভয়েস বা মেটেরিয়াল রেকর্ড ফিজিক্যালি মোছা যাবে না।

### প্রতিটি কোর টেবিলে নিচের ফিল্ডগুলো বাধ্যতামূলক:
```sql
is_deleted BOOLEAN NOT NULL DEFAULT false,
deleted_at TIMESTAMPTZ NULL,
created_by UUID NOT NULL,
created_at TIMESTAMPTZ NOT NULL DEFAULT clock_timestamp(),
updated_by UUID NOT NULL,
updated_at TIMESTAMPTZ NOT NULL DEFAULT clock_timestamp(),
xmin XID NOT NULL -- PostgreSQL Native Optimistic Concurrency Token
```

- **অপটিমিস্টিক কনকারেন্সি কন্ট্রোল**:
  - EF Core-এ `builder.Property<uint>("Version").IsRowVersion()` অথবা `xmin` ম্যাপিংয়ের মাধ্যমে কনকারেন্ট রাইট কনফ্লিক্ট হ্যান্ডেল করতে হবে।

---

## ৬. ইনডেক্সিং ও কোয়েরি পারফরম্যান্স রুলস (Indexing Standards)

1. **ফরেন কি ইনডেক্সিং**:
   - প্রতিটি Foreign Key কলামে বাধ্যতামূলক ডেটাবেজ ইনডেক্স (`builder.HasIndex(x => x.StyleId)`) থাকতে হবে।
2. **কম্পোজিট ইনডেক্সিং**:
   - `company_id`, `status`, `factory_id`, এবং বারকোড ফিল্ডে কম্পোজিট ইনডেক্স থাকতে হবে:
     ```csharp
     builder.HasIndex(x => new { x.CompanyId, x.Status });
     builder.HasIndex(x => x.BarcodeNumber).IsUnique();
     ```
3. **ফুল টেবিল স্ক্যান ও `SELECT *` নিষিদ্ধ**:
   - রিড কোয়েরিতে `.AsNoTracking()` এবং `.Select(x => new OrderListDto { ... })` প্রজেকশন বাধ্যতামূলক।
