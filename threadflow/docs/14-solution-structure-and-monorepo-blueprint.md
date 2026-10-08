# 🏗️ থ্রেডফ্লো সলিউশন স্ট্রাকচার ও মনোরেপো ব্লুপ্রিন্ট
### *ThreadFlow Enterprise Clean Architecture Monorepo Layout, Project Manifest & Docker Orchestration Blueprint*

- **ডকুমেন্ট আইডি**: `TF-DOC-014-SOLUTION-STRUCTURE-AND-MONOREPO`
- **প্রণেতা**: 🏛️ তানভীর (Principal Architect), ⚡ আসিফ (Backend Lead), 🎨 সজীব (Frontend Craftsman), 🚀 কবীর (DevOps Lead), 🗄️ ফাহিম (Database Architect)
- **অনুমোদনকারী**: 👑 বস (Founder / CTO / Head of Engineering)
- **স্ট্যাটাস**: **অফিসিয়াল আর্কিটেকচারাল ও টেকনিক্যাল স্পেসিফিকেশন (Baseline)** | **ভার্সন**: 1.0.0

---

## ১. ভূমিকা ও মনোরেপো আর্কিটেকচার দর্শন (Architectural Manifesto)

থ্রেডফ্লো ইআরপি একটি এন্টারপ্রাইজ-গ্রেড **মডুলার মনোলিথ (Modular Monolith)** হিসেবে ডিজাইন করা হয়েছে, যা ভবিষ্যতে প্রয়োজনভেদে হাই-স্কেল মাইক্রোসার্ভিসে বিভক্ত করা সম্ভব হবে। 
আমরা সম্পূর্ণ সোর্স কোডকে একটি পরিষ্কার, সুশৃঙ্খল এবং টাইপ-সেফ মনোরেপো কাঠামোর অধীনে সাজাব:
1. **ব্যাকএন্ড**: ASP.NET Core 10 / .NET 10 / C# 14 Clean Architecture (.NET 8/9 LTS ব্যাকওয়ার্ড-কম্প্যাটিবল)।
2. **ফ্রন্টএন্ড**: React 19 + TypeScript + Vite SPA (`apps/web`)।
3. **ডেভেলপমেন্ট ক্লাউড ও কন্টেইনার**: Docker Compose দিয়ে সম্পূর্ণ সেলফ-কন্টেইন্ড লোকাল ডেভ এনভায়রনমেন্ট (PostgreSQL 16, Redis 7.2, Keycloak 24+, MinIO)।

---

## ২. মাস্টার ডিরেক্টরি ট্রি ও ফাইল লেআউট (Repository Tree Layout)

```
rmg-erp/
│
├── .agents/                                # Virtual Engineering Team Governance & Anti-Drift Engine
│   ├── ARCHITECTURE.md                     # Living Architectural Source of Truth
│   ├── TASKS.md                            # Living Sprint Board & Task Tracker
│   ├── PROGRESS.md                         # Session Handover & State Persistence
│   └── rules/                              # 9-Domain Engineering Standards & 10 Specialist Rulebooks
│
├── threadflow/                             # ThreadFlow ERP Official Specification & Blueprint Vault
│   ├── docs/                               # Master Architecture Documents (01 to 14)
│   ├── business-processes/                 # 12 RMG Master Domains (Merchandising to Washing)
│   ├── discussions/                        # Historical Architectural Decisions & RFCs
│   └── market-research/                    # Competitive Benchmarking Reports
│
├── deploy/                                 # DevOps, Docker, Helm & Infrastructure Manifests
│   ├── docker/
│   │   ├── postgres/                       # Postgres init scripts (UUIDv7, pg_partman, extensions)
│   │   │   └── init-db.sql
│   │   ├── keycloak/                       # Keycloak realm import & theme assets
│   │   │   └── threadflow-realm.json
│   │   └── minio/                          # Bucket initialization scripts
│   ├── docker-compose.yml                  # Local development multi-container orchestration
│   ├── docker-compose.override.yml.example # Local developer overrides
│   └── .env.example                        # Universal environment variables template
│
├── src/                                    # C# .NET 10 Clean Architecture Solution (Backend)
│   ├── ThreadFlow.sln                      # Visual Studio / Rider Master Solution File
│   │
│   ├── ThreadFlow.Domain/                  # Core Enterprise Domain (Zero External Dependencies)
│   │   ├── Common/                         # BaseEntity, BaseAuditableEntity, BaseTenantEntity, ValueObject, AggregateRoot
│   │   ├── Enums/                          # SubscriptionTier, IsolationStrategy, ScopeLevel, WorkflowStatus
│   │   ├── Entities/                       # Rich Domain Entities
│   │   │   ├── Tenancy/                    # Tenant, TenantSubscription, TenantBilling
│   │   │   ├── Organization/               # Company, BusinessUnit, Factory, Building, Floor, Section, Line
│   │   │   ├── Identity/                   # User, Role, Permission, UserDataScope, RolePermission
│   │   │   ├── MasterData/                 # Buyer, Brand, Season, Currency, Uom, HsCode, GarmentType
│   │   │   ├── Configuration/              # UiLabelDictionary, CodeSequenceDefinition
│   │   │   └── Auditing/                   # AuditLog, SecurityEventLog
│   │   ├── Events/                         # Domain Events (IDomainEvent, EntityCreatedEvent, etc.)
│   │   └── Exceptions/                     # DomainException, BusinessRuleValidationException
│   │
│   ├── ThreadFlow.Application/             # Use Cases, CQRS & Business Rules (MediatR)
│   │   ├── Common/
│   │   │   ├── Behaviors/                  # ValidationBehavior, PerformanceBehavior, LoggingBehavior, TransactionBehavior
│   │   │   ├── Interfaces/                 # ITenantService, IApplicationDbContext, ICurrentUserService, ICacheService
│   │   │   ├── Models/                     # ApiResponse, PagedResponse, AgGridRequest, CurrentUserSnapshot, TenantContext
│   │   │   └── Security/                   # HasPermissionAttribute, RequireScopeAttribute
│   │   ├── Features/                       # Vertical Slice Feature Handlers (Commands & Queries)
│   │   │   ├── Tenancy/                    # RegisterTenantCommand, SwitchTenantCommand
│   │   │   ├── Auth/                       # GetCurrentUserQuery, RefreshTokenCommand
│   │   │   ├── Admin/                      # UserManagement, RoleManagement, ScopeAssignment
│   │   │   ├── MasterData/                 # BuyerQueries, DynamicCodeGenerationCommand, UiLabelCommands
│   │   │   └── Organization/               # CompanyCommands, FactoryHierarchyQueries
│   │   └── DependencyInjection.cs          # MediatR, FluentValidation, AutoMapper registration
│   │
│   ├── ThreadFlow.Infrastructure/          # External Concerns & Concrete Implementations
│   │   ├── Persistence/                    # EF Core 10 & Dapper Engine
│   │   │   ├── ApplicationDbContext.cs     # PostgreSQL DbContext with Global Query Filters (8-Tier Scope & TenantId)
│   │   │   ├── Configurations/             # IEntityTypeConfiguration<T> Fluent Mappings
│   │   │   ├── Interceptors/               # TenantInterceptor, AuditableEntityInterceptor, DispatchDomainEventsInterceptor
│   │   │   ├── Migrations/                 # EF Core Code-First Migrations
│   │   │   └── Repositories/               # Dapper Query Execution Handlers (High-Speed AG Grid Reads)
│   │   ├── Identity/                       # Keycloak OIDC Client & Token Validation (Tenant Claim extraction)
│   │   ├── Caching/                        # Redis Cache Service with Distributed Lock (RedLock)
│   │   ├── Storage/                        # MinIO S3 Object Storage Provider
│   │   ├── BackgroundJobs/                 # Hangfire Recurring Tasks & Sequence Resets
│   │   └── DependencyInjection.cs          # Register DB, Redis, Keycloak, MinIO services
│   │
│   └── ThreadFlow.WebApi/                  # Host & Presentation Layer (ASP.NET Core Web API)
│       ├── Controllers/                    # Clean REST Controllers or Fast Minimal API Endpoints
│       │   ├── V1/
│       │   │   ├── AuthController.cs
│       │   │   ├── AdminUsersController.cs
│       │   │   ├── MasterDataController.cs
│       │   │   └── OrganizationController.cs
│       ├── Middlewares/                    # TenantResolutionMiddleware, GlobalExceptionMiddleware (RFC 7807), IdempotencyMiddleware
│       ├── Hubs/                           # SignalR Real-Time Hubs (NotificationHub, ProductionScanHub)
│       ├── Program.cs                      # Host Bootstrapper, OpenTelemetry, Serilog, Swagger OpenAPI 3.0
│       └── appsettings.json                # Application configuration
│
├── tests/                                  # Automated Testing Suite (Maya's Test Enforcer)
│   ├── ThreadFlow.Domain.UnitTests/        # Fast in-memory pure domain logic tests
│   ├── ThreadFlow.Application.UnitTests/   # MediatR Handler & Validation logic tests
│   └── ThreadFlow.IntegrationTests/        # Testcontainers (Real PostgreSQL 16 & Redis dockerized test runs)
│
├── apps/                                   # Frontend Applications
│   └── web/                                # React 19 + TypeScript + Vite SPA
│       ├── index.html                      # HTML5 entry with Inter font
│       ├── vite.config.ts                  # Vite config (path aliases, proxy, build optimization)
│       ├── package.json                    # React 19, Vite, Tailwind v4, AG Grid Enterprise, TanStack Query
│       ├── src/
│       │   ├── app/                        # App root, Router, Global Providers
│       │   │   ├── App.tsx
│       │   │   ├── Router.tsx              # React Router v7 / TanStack Router
│       │   │   └── providers/              # AuthProvider, ThemeProvider, QueryProvider, ReduxProvider
│       │   ├── components/                 # Reusable UI Library
│       │   │   ├── ui/                     # Primitives (Button, Input, Dialog, Dropdown, Tabs - shadcn)
│       │   │   ├── layout/                 # MainLayout, Header, Sidebar, CommandPalette, Breadcrumbs
│       │   │   ├── grid/                   # ThreadFlowAgGrid Enterprise Wrapper (SSRM ready)
│       │   │   └── feedback/               # AndonAlertBadge, StatusLight, LoadingSkeleton, EmptyState
│       │   ├── features/                   # Modular Domain UI Slices
│       │   │   ├── auth/                   # Keycloak Login Callback, Profile Drawer
│       │   │   ├── admin/                  # User Management, Role Matrix, 7-Tier Scope Assignor
│       │   │   ├── master-data/            # Buyer List, Dynamic Sequence Generator Configurator
│       │   │   └── organization/           # Factory Tree Topology Viewer
│       │   ├── hooks/                      # Custom React Hooks
│       │   │   ├── useAuth.ts              # Current User, Token refresh
│       │   │   ├── usePermission.ts        # hasPermission('merchandising.style.create')
│       │   │   ├── useDataScope.ts         # Active Company/Factory selector
│       │   │   └── useAgGridServerSide.ts  # AG Grid SSRM datasource hook
│       │   ├── lib/                        # Core Utilities
│       │   │   ├── api-client.ts           # Axios instance with Interceptors, Bearer Token & Idempotency
│       │   │   ├── keycloak.ts             # Keycloak JS OIDC adapter
│       │   │   └── ag-grid-license.ts      # AG Grid Enterprise setup
│       │   ├── store/                      # Redux Toolkit Global State
│       │   │   ├── index.ts                # Root store
│       │   │   └── slices/                 # authSlice, uiSlice, scopeSlice
│       │   └── types/                      # Global TypeScript definitions
│
└── packages/                               # Shared Internal Libraries & Contracts
    └── contracts/                          # Auto-generated TypeScript types from Backend DTOs
```

---

## ৩. ডকার ডেভেলপমেন্ট অর্কেস্ট্রেশন (`deploy/docker-compose.yml`)

কবীর (DevOps) নিশ্চিত করেছে যে যেকোনো ডেভেলপার বা সিআই পাইপলাইন মাত্র একটি কমান্ডে সম্পূর্ণ স্ট্যাক লোকাল মেশিনে রান করতে পারবে:

```yaml
version: '3.9'

services:
  # 1. PostgreSQL 16 Enterprise Database
  postgres:
    image: postgres:16-alpine
    container_name: threadflow-postgres
    restart: always
    environment:
      POSTGRES_DB: threadflow_db
      POSTGRES_USER: threadflow_admin
      POSTGRES_PASSWORD: ThreadFlowDevSecure2026!
      POSTGRES_INITDB_ARGS: "--encoding=UTF-8 --lc-collate=C --lc-ctype=C"
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
      - ./docker/postgres/init-db.sql:/docker-entrypoint-initdb.d/init-db.sql
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U threadflow_admin -d threadflow_db"]
      interval: 10s
      timeout: 5s
      retries: 5

  # 2. Redis 7.2 High-Throughput Cache & Distributed Lock
  redis:
    image: redis:7.2-alpine
    container_name: threadflow-redis
    restart: always
    command: ["redis-server", "--requirepass", "RedisDevSecure2026!", "--maxmemory", "512mb", "--maxmemory-policy", "volatile-lru"]
    ports:
      - "6379:6379"
    volumes:
      - redis_data:/data
    healthcheck:
      test: ["CMD", "redis-cli", "-a", "RedisDevSecure2026!", "ping"]
      interval: 10s
      timeout: 5s
      retries: 5

  # 3. Keycloak 24 Enterprise Identity Provider (OIDC / OAuth 2.0)
  keycloak:
    image: quay.io/keycloak/keycloak:24.0.5
    container_name: threadflow-keycloak
    restart: always
    command: start-dev --import-realm
    environment:
      KEYCLOAK_ADMIN: admin
      KEYCLOAK_ADMIN_PASSWORD: KeycloakAdmin2026!
      KC_DB: postgres
      KC_DB_URL: jdbc:postgresql://postgres:5432/threadflow_keycloak
      KC_DB_USERNAME: threadflow_admin
      KC_DB_PASSWORD: ThreadFlowDevSecure2026!
    ports:
      - "8080:8080"
    volumes:
      - ./docker/keycloak/threadflow-realm.json:/opt/keycloak/data/import/threadflow-realm.json
    depends_on:
      postgres:
        condition: service_healthy

  # 4. MinIO S3-Compatible Enterprise Object Storage
  minio:
    image: minio/minio:RELEASE.2024-05-10T01-41-38Z
    container_name: threadflow-minio
    restart: always
    command: server /data --console-address ":9001"
    environment:
      MINIO_ROOT_USER: minio_admin
      MINIO_ROOT_PASSWORD: MinioAdmin2026!
    ports:
      - "9000:9000"
      - "9001:9001"
    volumes:
      - minio_data:/data

  # 5. Mailpit (Local SMTP Mock for User Invitation & Alerts)
  mailpit:
    image: axllent/mailpit:latest
    container_name: threadflow-mailpit
    restart: always
    ports:
      - "1025:1025"
      - "8025:8025"

  # 6. Backend API (ASP.NET Core 10 / .NET 10 WebApi Container)
  backend:
    build:
      context: ../
      dockerfile: deploy/docker/backend/Dockerfile.dev
    container_name: threadflow-backend
    restart: always
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      ASPNETCORE_URLS: http://+:5000
      ConnectionStrings__DefaultConnection: "Host=postgres;Port=5432;Database=threadflow_db;Username=threadflow_admin;Password=ThreadFlowDevSecure2026!"
      Redis__ConnectionString: "redis:6379,password=RedisDevSecure2026!"
      Keycloak__Authority: "http://keycloak:8080/realms/threadflow"
      Keycloak__Audience: "threadflow-api"
      MinIO__Endpoint: "minio:9000"
      MinIO__AccessKey: "minio_admin"
      MinIO__SecretKey: "MinioAdmin2026!"
    ports:
      - "5000:5000"
    volumes:
      - ../src:/app/src
    depends_on:
      postgres:
        condition: service_healthy
      redis:
        condition: service_healthy
      keycloak:
        condition: service_started

  # 7. Frontend Web SPA (React 19 + TypeScript + Vite Container)
  frontend:
    build:
      context: ../apps/web
      dockerfile: ../../deploy/docker/frontend/Dockerfile.dev
    container_name: threadflow-frontend
    restart: always
    environment:
      VITE_API_URL: http://localhost:5000/api/v1
      VITE_KEYCLOAK_URL: http://localhost:8080
      VITE_KEYCLOAK_REALM: threadflow
      VITE_KEYCLOAK_CLIENT_ID: threadflow-web
    ports:
      - "3000:3000"
    volumes:
      - ../apps/web:/app
      - /app/node_modules
    depends_on:
      - backend

volumes:
  postgres_data:
    driver: local
  redis_data:
    driver: local
  minio_data:
    driver: local
```

---

## ৪. সিড ডেটা ও ইনিশিয়াল বুটস্ট্র্যাপ স্ট্র্যাটেজি (Bootstrap Strategy)

সিস্টেম প্রথমবার বুট করার সময় `DbInitializer` সার্ভিস নিম্নলিখিত মাস্টার ডেটা স্বয়ংক্রিয়ভাবে ইনজেক্ট করবে:

### ৪.১ ডিফল্ট অ্যাডমিন ও সিস্টেম রোলস
1. **সুপার অ্যাডমিন ইউজার**: `admin@threadflow.erp` (System Initializer)।
2. **কোর সিস্টেম রোলস**:
   - `GLOBAL_SUPERADMIN`: সম্পূর্ণ সিস্টেম ও কনফিগারেশন এক্সেস।
   - `MANAGING_DIRECTOR`: গ্রুপ-লেভেল সমস্ত কারখানা ও ফাইন্যান্সিয়াল রিপোর্ট এক্সেস।
   - `FACTORY_ADMIN`: সুনির্দিষ্ট ফ্যাক্টরির পূর্ণ প্রশাসনিক ক্ষমতা।
   - `MERCHANDISING_MANAGER`: স্টাইল, বিওএম, কস্টিং ও টিঅ্যান্ডএ অনুমোদন ক্ষমতা।
   - `IE_PLANNING_MANAGER`: ক্যাপাসিটি, লাইন ব্যালেন্সিং ও প্ল্যানিং মাস্টার।
   - `CUTTING_OFFICER`: কাটিং ও বান্ডিল কার্ড ইস্যুকারী।
   - `QUALITY_INSPECTOR`: অ্যান্ডন অ্যালার্ট ও লাইভ ডিএইচইউ ইন্সপেক্টর।

### ৪.২ বেস রেফারেন্স মাস্টার ডেটা
1. **কারেন্সি ক্যাটালগ**: `USD` ($), `BDT` (৳), `EUR` (€), `GBP` (£), `INR` (₹), `CNY` (¥)।
2. **স্ট্যান্ডার্ড UOM**: `PCS` (Pieces), `DZN` (Dozen), `YDS` (Yards), `MTR` (Meters), `KGS` (Kilograms), `CONE` (Thread Cones), `GROSS` (144 Pcs), `ROLL` (Fabric Rolls)।
3. **স্ট্যান্ডার্ড ইনকোটার্মস**: `FOB` (Free on Board), `CIF` (Cost, Insurance & Freight), `CFR` (Cost & Freight), `EXW` (Ex Works)।
4. **পোশাক ক্যাটাগরি**: `T-SHIRT`, `POLO_SHIRT`, `DENIM_PANTS`, `WOVEN_SHIRT`, `HOODIE_SWEATSHIRT`, `JACKET`, `UNDERWEAR`, `SWEATER`।
5. **ডিফল্ট মাল্টি-টেন্যান্ট অর্গানাইজেশন**:
   - রুট টেন্যান্ট (SaaS Client): `Apex Textile Holdings Ltd` (Code: `APEX_GROUP`, Subdomain: `apex`, Tier: `ENTERPRISE`)
   - কোম্পানি: `Apex Apparel Ltd` (Code: `APEX_APP`)
   - বিজনেস ইউনিট: `Apparel Manufacturing Division` (Code: `BU-APP`)
   - ফ্যাক্টরি: `Apex Apparel Unit 1` (Code: `FAC-1`)
   - বিল্ডিং: `Building A` (Code: `BLD-A`)
   - ফ্লোর: `Floor 2` (Code: `FLR-2`)
   - সেকশন: `Sewing Section` (Code: `SEC-SEW`)
   - প্রোডাকশন লাইন: `Line 01` (Code: `L-01`)

---

## ৫. কোডিং ফেজ শুরু করার পূর্বশর্ত সাইন-অফ (Sign-Off Readiness)

| ডোমেন ও চেকলিস্ট | দায়িত্বপ্রাপ্ত বিশেষজ্ঞ | স্ট্যাটাস |
| :--- | :--- | :--- |
| **ডোমেন বিজনেস ব্লুপ্রিন্ট (১২টি ডোমেন)** | 📋 নাবিলা (Lead BA) | ✅ 100% Complete |
| **টেক স্ট্যাক এসআরএস ও সিকিউরিটি** | 🏛️ তানভীর (Principal Architect) | ✅ 100% Complete |
| **ডেটাবেজ স্কিমা ও পার্টিশনিং** | 🗄️ ফাহিম (Database Architect) | ✅ 100% Complete |
| **ইউআই ডিজাইন সিস্টেম ও কালার প্যালেট** | 🎨 সজীব (Frontend Craftsman) | ✅ 100% Complete |
| **অথ ও ৭-লেয়ার ডেটা স্কোপ ইঞ্জিন** | ⚡ আসিফ (Backend Lead) | ✅ 100% Complete |
| **এপিআই কন্ট্রাক্ট ও এরর প্রোটোকল** | ⚡ আসিফ & 🎨 সজীব | ✅ 100% Complete |
| **ইনফ্রাস্ট্রাকচার ও ডকার অর্কেস্ট্রেশন** | 🚀 কবীর (DevOps Lead) | ✅ 100% Complete |
| **কোয়ালিটি গেট ও ভালনারেবিলিটি চেকলিস্ট**| 🛡️ মায়া (QA & Security) | ✅ 100% Complete |
| **টেকনিক্যাল ডকুমেন্টেশন ও রেফারেন্স** | 📚 শাকিল (Tech Writer) | ✅ 100% Complete |

> **👑 সিটিও (CTO / বস) অনুমোদনের পর সাথে সাথে কোডিং ফেইজে স্ক্যাফোল্ডিং শুরু করা যাবে।**
