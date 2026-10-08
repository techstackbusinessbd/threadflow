# 📡 থ্রেডফ্লো কোর এপিআই কন্ট্রাক্ট ও পেলোড স্পেসিফিকেশন
### *ThreadFlow Enterprise API Contracts, Server-Side AG Grid Protocols, ProblemDetails & SignalR Event Schemas*

- **ডকুমেন্ট আইডি**: `TF-DOC-013-API-CONTRACTS-AND-PAYLOADS`
- **প্রণেতা**: 🏛️ তানভীর (Principal Architect), ⚡ আসিফ (Backend Lead), 🎨 সজীব (Frontend Craftsman), 🗄️ ফাহিম (Database Architect), 🛡️ মায়া (QA & Security)
- **অনুমোদনকারী**: 👑 বস (Founder / CTO / Head of Engineering)
- **স্ট্যাটাস**: **অফিসিয়াল আর্কিটেকচারাল ও টেকনিক্যাল স্পেসিফিকেশন (Baseline)** | **ভার্সন**: 1.0.0

---

## ১. ভূমিকা ও এপিআই গভর্ন্যান্স দর্শন (API Governance)

থ্রেডফ্লো ইআরপি-তে ফ্রন্টএন্ড (React 19 Vite SPA) এবং ব্যাকএন্ড (ASP.NET Core 10 Clean Architecture)-এর মধ্যবর্তী যোগাযোগ হতে হবে অত্যন্ত প্রেডিক্টেবল, টাইপ-সেফ, হাই-থ্রুপুট এবং লো-লেটেন্সি। 

### ১.১ মূল স্থাপত্য নীতিমালা
1. **ইউনিফাইড রেসপন্স এনভেলপ**: সকল RESTful এপিআই রেসপন্স একটি স্ট্যান্ডার্ড জেনেরিক র‍্যাপার `ApiResponse<T>` অথবা `PagedResponse<T>` মেনে চলবে। কোনো আনর‍্যাপড রেন্ডম JSON রিটার্ন করা সম্পূর্ণ নিষিদ্ধ।
2. **RFC 7807 প্রবলেম ডিটেইলস (ProblemDetails)**: যেকোনো এরর (ভ্যালিডেশন ফেইলিওর, কনকারেন্সি কনফ্লিক্ট, অথোরাইজেশন এরর) আন্তর্জাতিক RFC 7807 ফরম্যাটে রিটার্ন হবে।
3. **সার্ভার-সাইড AG Grid Enterprise প্রোটোকল**: লক্ষ লক্ষ রো ব্রাউজারে ক্র্যাশ ছাড়াই লোড করার জন্য AG Grid Enterprise Server-Side Row Model (SSRM)-এর সাথে সরাসরি কম্প্যাটিবল কুয়েরি ও রেসপন্স স্ট্রাকচার।
4. **আইডেমপোটেন্সি ও কনকারেন্সি প্রটেকশন**: সকল স্টেট-চেঞ্জিং অপারেশন (POST/PUT/PATCH)-এ `X-Idempotency-Key` এবং অপটিমিস্টিক কনকারেন্সি চেকের জন্য `RowVersion` / `ETag` বাধ্যতামূলক।
5. **রিয়েল-টাইম সিগন্যালআর (SignalR) ইভেন্ট টপোলজি**: শপফ্লোর স্ক্যানিং, অ্যান্ডন অ্যালার্ট, এবং বাল্ক প্রসেসিং নোটিফিকেশন ক্লাউড-স্কেল WebSockets-এর মাধ্যমে পুশ হবে।

---

## ২. গ্লোবাল রেসপন্স এনভেলপ ও এরর স্কিমা (Standard Response Envelopes)

### ২.১ একক রেকর্ড রেসপন্স (`ApiResponse<T>`)
```csharp
// ThreadFlow.Application.Common.Models.ApiResponse<T>
public sealed record ApiResponse<T>
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }
    public string TraceId { get; init; } = string.Empty;
    public DateTime TimestampUtc { get; init; } = DateTime.UtcNow;

    public static ApiResponse<T> Ok(T data, string message = "Request processed successfully.") =>
        new() { Success = true, Data = data, Message = message, TraceId = Activity.Current?.Id ?? Guid.NewGuid().ToString("N") };
}
```

```json
{
  "success": true,
  "message": "Buyer created successfully.",
  "data": {
    "id": "01943b76-48a0-71a2-97b0-8c9e5e789012",
    "buyerCode": "BYR-HM-001",
    "buyerName": "H&M Global Sourcing",
    "countryCode": "SE",
    "isActive": true
  },
  "traceId": "00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01",
  "timestampUtc": "2026-10-08T13:30:00.000Z"
}
```

---

### ২.২ পেজিনেটেড রেসপন্স (`PagedResponse<T>`)
AG Grid Enterprise এবং সাধারণ ডেটা টেবিলে সার্ভার-সাইড পেজিনেশন পরিচালনার জন্য:

```csharp
// ThreadFlow.Application.Common.Models.PagedResponse<T>
public sealed record PagedResponse<T>
{
    public bool Success { get; init; } = true;
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public long TotalCount { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    public string TraceId { get; init; } = string.Empty;
    public DateTime TimestampUtc { get; init; } = DateTime.UtcNow;
}
```

```json
{
  "success": true,
  "items": [
    {
      "id": "01943b76-48a0-71a2-97b0-8c9e5e789012",
      "styleNo": "TF-STY-2026-0042",
      "styleName": "Men Slim Fit Chino",
      "buyerName": "Zara Inditex",
      "seasonName": "Autumn/Winter 2026",
      "totalOrderQty": 45000,
      "fobPrice": 7.4500,
      "workflowStatus": "APPROVED"
    }
  ],
  "pageIndex": 1,
  "pageSize": 50,
  "totalCount": 1842,
  "totalPages": 37,
  "hasPreviousPage": false,
  "hasNextPage": true,
  "traceId": "00-a1b2c3d4e5f67890-123456789abcdef0-01",
  "timestampUtc": "2026-10-08T13:30:00.000Z"
}
```

---

### ২.৩ RFC 7807 প্রবলেম ডিটেইলস এরর ফরম্যাট (Standard Error Schema)
যেকোনো ৪xx বা ৫xx এরর রেসপন্সে এই স্ট্রাকচার পাঠানো হয়:

```json
{
  "type": "https://api.threadflow.erp/errors/validation-failed",
  "title": "Validation Failed",
  "status": 422,
  "detail": "One or more business validation rules were violated.",
  "instance": "/api/v1/merchandising/styles",
  "traceId": "00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01",
  "code": "ERR_VALIDATION_FAILED",
  "errors": {
    "fobPrice": [
      "FOB price must be strictly greater than zero."
    ],
    "sampleApprovalDate": [
      "Sample approval date cannot be in the past."
    ]
  }
}
```

---

## ৩. সার্ভার-সাইড AG Grid Enterprise কুয়েরি কন্ট্রাক্ট (AG Grid SSRM Protocol)

AG Grid Enterprise যখন গ্রিড ফিল্টার, মাল্টি-কলাম সর্ট, গ্রুপিং এবং পেজিনেশন রিকোয়েস্ট পাঠায়, ব্যাকএন্ড সরাসরি এই পেলোড গ্রহণ করে এবং PostgreSQL Dapper/EF Core দিয়ে অপ্টিমাইজড SQL তৈরি করে।

### ৩.১ রিকোয়েস্ট পেলোড (`AgGridServerSideRequest`)
```csharp
public sealed record AgGridServerSideRequest
{
    public int StartRow { get; init; } = 0;
    public int EndRow { get; init; } = 100;
    public IReadOnlyList<AgGridSortModel> SortModel { get; init; } = Array.Empty<AgGridSortModel>();
    public Dictionary<string, AgGridFilterModel> FilterModel { get; init; } = new();
    public IReadOnlyList<string> RowGroupCols { get; init; } = Array.Empty<string>();
    public IReadOnlyList<string> GroupKeys { get; init; } = Array.Empty<string>();
    public string? QuickFilterText { get; init; }
}

public sealed record AgGridSortModel(string ColId, string Sort); // "asc" or "desc"

public sealed record AgGridFilterModel
{
    public string FilterType { get; init; } = "text"; // "text", "number", "date", "set"
    public string? Type { get; init; } // "contains", "equals", "greaterThan", "inRange"
    public string? Filter { get; init; }
    public decimal? FilterTo { get; init; }
    public IReadOnlyList<string>? Values { get; init; } // for set filters
    public string? Operator { get; init; } // "AND" / "OR" for compound filters
    public AgGridFilterModel? Condition1 { get; init; }
    public AgGridFilterModel? Condition2 { get; init; }
}
```

### ৩.২ AG Grid SSRM রেসপন্স (`AgGridServerSideResponse<T>`)
```csharp
public sealed record AgGridServerSideResponse<T>
{
    public bool Success { get; init; } = true;
    public IReadOnlyList<T> Rows { get; init; } = Array.Empty<T>();
    public int LastRow { get; init; } // Total count of matched rows
    public Dictionary<string, object>? SecondarySummary { get; init; } // Grand totals for pinned bottom row
}
```

---

## ৪. আইডেমপোটেন্সি ও অপটিমিস্টিক কনকারেন্সি কন্ট্রাক্ট (Data Integrity & Safety)

### ৪.০ মাল্টি-টেন্যান্ট হেডার কন্ট্রাক্ট (`X-Tenant-Id`)
- **হেডার**: `X-Tenant-Id: <UUIDv7>` (অথবা সাবডোমেন: `{tenant}.threadflow.erp`)
- **নিয়ম**:
  1. পাবলিক ক্লাউড SaaS মোডে প্রতিটি এপিআই রিকোয়েস্টে ক্লায়েন্ট এই হেডার পাঠাবে অথবা সাবডোমেন দিয়ে টেন্যান্ট রিজলভ হবে।
  2. ASP.NET Core `TenantResolutionMiddleware` কলারের JWT টোকেনের `tenant_id` ক্লেইমের সাথে হেডারের মান যাচাই করবে। মিসম্যাচ হলে সাথে সাথে `403 Forbidden (Tenant mismatch)` রিটার্ন করবে।
  3. অন-প্রিমিস মোডে `appsettings.json`-এ ডিফল্ট টেন্যান্ট আইডি ফিক্সড থাকবে, ফলে কোনো আলাদা হেডার পাঠানোর প্রয়োজন পড়বে না।

### ৪.১ `X-Idempotency-Key` হ্যান্ডলিং
- **হেডার**: `X-Idempotency-Key: <UUIDv7>`
- **নিয়ম**:
  1. ক্লায়েন্ট যেকোনো ক্রিয়েট বা সাবমিট রিকোয়েস্টে এই হেডার পাঠাবে।
  2. ASP.NET Core Middleware রিকোয়েস্ট পাওয়ার পর Redis-এ `idemp:{tenantId}:{userId}:{key}` ক্যাশ চেক করবে।
  3. যদি কি বিদ্যমান থাকে এবং স্ট্যাটাস `IN_FLIGHT` হয়, ব্যাকএন্ড সাথে সাথে `409 Conflict` (Duplicate request in progress) রিটার্ন করবে।
  4. প্রসেসিং শেষ হলে রেসপন্স পেলোড এবং স্ট্যাটাস কোড ২৪ ঘণ্টার জন্য ক্যাশ হবে। ডুপ্লিকেট ক্লিকে ক্যাশড রেসপন্স ইনস্ট্যান্ট রিটার্ন হবে।

### ৪.২ অপটিমিস্টিক কনকারেন্সি (`RowVersion` / `ETag`)
পোশাক কারখানায় যখন একই স্টাইল বা কাটিং অর্ডারে একাধিক মার্চেন্ডাইজার বা প্রোডাকশন অফিসার একসাথে কাজ করেন, তখন ডেটা ওভাররাইট রোধ করতে:
- **হেডার**: `If-Match: "01943b76-..."` অথবা বডিতে `rowVersion: 14`।
- **ডাটাবেস চেক**: `UPDATE ... WHERE id = @Id AND tenant_id = @TenantId AND row_version = @ExpectedRowVersion`।
- যদি `RowsAffected == 0` হয়, ব্যাকএন্ড `412 Precondition Failed` রিটার্ন করবে:
```json
{
  "type": "https://api.threadflow.erp/errors/concurrency-conflict",
  "title": "Concurrency Conflict",
  "status": 412,
  "detail": "The record was modified by another user ('asif.merchandiser' at 13:28:14 UTC). Please refresh the latest state.",
  "code": "ERR_CONCURRENCY_CONFLICT"
}
```

---

## ৫. কোর মডিউল এপিআই ক্যাটালগ ও স্পেসিফিকেশন (Core API Endpoints)

### ৫.১ অথেন্টিকেশন ও ইউজার প্রোফাইল (`/api/v1/auth`)

#### 1. `GET /api/v1/auth/me`
- **উদ্দেশ্য**: লগইন করার পর কারেন্ট ইউজারের টেন্যান্ট, প্রোফাইল, পারমিশন ক্যাটালগ ও ৮-লেয়ার ডেটা স্কোপ লোড করা।
- **অথোরাইজেশন**: `Bearer <Keycloak_JWT>`
- **রেসপন্স**:
```json
{
  "success": true,
  "data": {
    "tenant": {
      "tenantId": "01943b76-48a0-71a2-97b0-8c9e5e789000",
      "tenantCode": "APEX_GROUP",
      "tenantName": "Apex Textile Holdings Ltd",
      "subscriptionTier": "ENTERPRISE",
      "isolationStrategy": "SHARED_ROW_LEVEL"
    },
    "userId": "01943b76-48a0-71a2-97b0-8c9e5e789001",
    "username": "tanvir.lead",
    "email": "tanvir.lead@threadflow.erp",
    "fullName": "Tanvir Ahmed",
    "avatarUrl": "https://minio.threadflow.erp/avatars/tanvir.png",
    "roles": ["MERCHANDISING_MANAGER", "FACTORY_ADMIN"],
    "permissions": [
      "merchandising.style.view",
      "merchandising.style.create",
      "merchandising.style.edit",
      "merchandising.bom.approve",
      "production.cutting.view"
    ],
    "scopes": [
      {
        "scopeLevel": "FACTORY",
        "tenantId": "01943b76-48a0-71a2-97b0-8c9e5e789000",
        "companyId": "01943b76-48a0-71a2-97b0-8c9e5e789010",
        "companyName": "Apex Textile Holdings Ltd",
        "factoryId": "01943b76-48a0-71a2-97b0-8c9e5e789020",
        "factoryName": "Apex Apparel Unit-1",
        "buildingId": null,
        "floorId": null,
        "sectionId": null,
        "lineId": null,
        "isPrimary": true
      }
    ],
    "preferences": {
      "language": "en",
      "theme": "DARK",
      "density": "COMPACT"
    }
  }
}
```

---

### ৫.২ ইউজার ও রোল অ্যাডমিনিস্ট্রেশন (`/api/v1/admin/users`)

#### 1. `POST /api/v1/admin/users` (Invite / Create User)
- **পারমিশন**: `system.user.create`
- **রিকোয়েস্ট পেলোড**:
```json
{
  "username": "asif.cutting",
  "email": "asif.cutting@apexapparel.com",
  "fullName": "Asif Ikbal",
  "phoneNumber": "+8801711000000",
  "defaultCompanyId": "01943b76-48a0-71a2-97b0-8c9e5e789010",
  "defaultFactoryId": "01943b76-48a0-71a2-97b0-8c9e5e789020",
  "roleIds": [
    "01943b76-48a0-71a2-97b0-8c9e5e789050"
  ],
  "scopes": [
    {
      "scopeLevel": "SECTION",
      "companyId": "01943b76-48a0-71a2-97b0-8c9e5e789010",
      "factoryId": "01943b76-48a0-71a2-97b0-8c9e5e789020",
      "buildingId": "01943b76-48a0-71a2-97b0-8c9e5e789030",
      "floorId": "01943b76-48a0-71a2-97b0-8c9e5e789035",
      "sectionId": "01943b76-48a0-71a2-97b0-8c9e5e789040",
      "lineId": null,
      "isPrimary": true
    }
  ]
}
```

---

### ৫.৩ মাস্টার ডেটা এন্ডপয়েন্ট ক্যাটালগ (`/api/v1/master-data`)

#### 1. `POST /api/v1/master-data/query-grid` (Generic AG Grid Endpoint)
- **বর্ণনা**: যেকোনো মাস্টার ডেটা (Buyers, Brands, Seasons, Currencies, UOMs) AG Grid SSRM মাধ্যমে সার্চ, ফিল্টার ও পেজিনেট করতে।
- **কুয়েরি প্যারামিটার**: `?entityType=BUYER` বা `?entityType=SEASON`
- **বডি**: `AgGridServerSideRequest` (Section ৩.১)

#### 2. `POST /api/v1/master-data/buyers` (Create Buyer)
- **পারমিশন**: `master.buyer.create`
- **রিকোয়েস্ট বডি**:
```json
{
  "buyerCode": "BYR-ZARA-01",
  "buyerName": "Zara / Inditex Group",
  "countryCode": "ES",
  "currencyCode": "EUR",
  "paymentTerms": "LC 90 Days",
  "contactPerson": "Carlos Mendez",
  "contactEmail": "carlos.mendez@inditex.com",
  "contactPhone": "+34-981-185400",
  "complianceTier": "A_TIER"
}
```

#### 3. `POST /api/v1/master-data/code-sequences/preview` (Dynamic Code Preview)
- **বর্ণনা**: নতুন কোড কনফিগারেশন সেট করার সময় লাইভ প্রিভিউ জেনারেট করে দেখা।
- **রিকোয়েস্ট বডি**:
```json
{
  "prefix": "CUT",
  "companyId": "01943b76-48a0-71a2-97b0-8c9e5e789010",
  "factoryId": "01943b76-48a0-71a2-97b0-8c9e5e789020",
  "pattern": "{PREFIX}-{COMPANY_CODE}-{FACTORY_CODE}-{YEAR}-{MONTH}-{PAD_6}"
}
```
- **রেসপন্স**:
```json
{
  "success": true,
  "data": {
    "previewCode": "CUT-ATHL-FAC1-2026-10-000001",
    "tokenBreakdown": {
      "PREFIX": "CUT",
      "COMPANY_CODE": "ATHL",
      "FACTORY_CODE": "FAC1",
      "YEAR": "2026",
      "MONTH": "10",
      "PAD_6": "000001"
    }
  }
}
```

---

## ৬. রিয়েল-টাইম সিগন্যালআর ইভেন্ট ও টপোলজি (SignalR Real-Time Protocols)

### ৬.১ হাব কনফিগারেশন (`/hubs/erp-notifications`)
সকল কানেকশনের জন্য Keycloak JWT Bearer টোকেন কোয়েরি স্ট্রিং বা হেডারের মাধ্যমে অথেন্টিকেট হবে: `wss://api.threadflow.erp/hubs/erp-notifications?access_token=...`

### ৬.২ চ্যানেল ও ইভেন্ট কন্ট্রাক্ট
1. **শপফ্লোর লাইভ স্ক্যান ও প্রোডাকশন আপডেট**:
   - চ্যানেল: `line:{lineId}:production`
   - ইভেন্ট: `ProductionScanReceived`
   ```json
   {
     "bundleCardId": "01943b76-48a0-71a2-97b0-8c9e5e789999",
     "barcode": "BNDL-202610-008421",
     "lineId": "01943b76-48a0-71a2-97b0-8c9e5e789045",
     "operationName": "Side Seam Join",
     "operatorName": "Rahima Begum",
     "scanTimestamp": "2026-10-08T13:35:12.450Z",
     "hourlyAchieved": 142,
     "hourlyTarget": 150,
     "efficiencyPercent": 94.67
   }
   ```
2. **অ্যান্ডন কোয়ালিটি বা ব্রেকডাউন অ্যালার্ট (Andon Alert)**:
   - চ্যানেল: `factory:{factoryId}:andon`
   - ইভেন্ট: `AndonAlertTriggered`
   ```json
   {
     "alertId": "01943b76-48a0-71a2-97b0-8c9e5e788888",
     "severity": "CRITICAL",
     "lineName": "Line-07 (Building 2, Floor 3)",
     "category": "MACHINE_BREAKDOWN",
     "machineCode": "SN-JUKI-OVERLOCK-102",
     "description": "Motor overheating and thread cutter jammed",
     "status": "OPEN",
     "triggeredAt": "2026-10-08T13:35:00.000Z"
   }
   ```
3. **ডকুমেন্ট অ্যাপ্রুভাল পেন্ডিং নোটিফিকেশন**:
   - চ্যানেল: `user:{userId}:notifications`
   - ইভেন্ট: `ApprovalRequired`
   ```json
   {
     "documentType": "COST_SHEET",
     "documentId": "01943b76-48a0-71a2-97b0-8c9e5e787777",
     "documentNo": "CST-2026-00341",
     "styleNo": "TF-STY-2026-0042",
     "totalAmountUsd": 335250.00,
     "submittedBy": "Md. Rakib Hossain",
     "submittedAt": "2026-10-08T13:30:00.000Z"
   }
   ```

---

## ৭. কোয়ালিটি গেট ও ভ্যালিডেশন চেকলিস্ট (Maya's Security Matrix)

| সিকিউরিটি ও কোয়ালিটি রুল | মেকানিজম | স্ট্যাটাস |
| :--- | :--- | :--- |
| **XSS ও Injection প্রতিরোধ** | ASP.NET Core Input Sanitizer + Parameterized SQL (Dapper/EF) | ✅ Enforced |
| **Idempotency ফেইল-সেফ** | Redis distributed lock (`RedLock` pattern) with 24h key TTL | ✅ Enforced |
| **CORS পলিসি** | শুধুমাত্র হোয়াইটলিস্টেড ডোমেন এবং ক্রেডেনশিয়াল সাপোর্ট | ✅ Enforced |
| **রেট লিমিটিং (Rate Limiting)** | ASP.NET Core Fixed-Window: ১,০০০ রিকোয়েস্ট/মিনিট প্রতি ক্লায়েন্ট আইপি | ✅ Enforced |
| **অডিট ট্রেইল অটোমেশন** | প্রতিটি স্টেট-চেঞ্জিং এপিআই কলে `AuditLogBehavior` মেডিয়েটর পাইপলাইন | ✅ Enforced |
