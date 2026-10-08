# Enterprise RMG ERP — Technology Stack SRS
## Frontend, Backend, Database, Role & Permission, Policy & Data Scope Engine

- **Document Type**: Technology Stack Software Requirements Specification (SRS)
- **Target System**: Enterprise-level RMG ERP (**ThreadFlow ERP**)
- **Architecture**: Modular Monolith + Clean Architecture + DDD, with future Microservices readiness
- **Primary Backend**: ASP.NET Core 10 on .NET 10, C# 14 (Targeting .NET 8/9 LTS during baseline setup)
- **Primary Frontend**: React 19 + TypeScript + Vite (SPA)
- **Primary Database**: PostgreSQL 16+ (System of Record, UUIDv7, Native Range Partitioning)
- **Authentication**: Keycloak + OpenID Connect (OIDC) / OAuth 2.0
- **Authorization**: ASP.NET Core Policy-Based Authorization + DB-backed Fine-Grained Permissions + Custom Data Scope Engine
- **Deployment**: Linux + Docker, Kubernetes-ready
- **Document Status**: **Officially Locked Baseline Architecture**

---

## 1. Purpose & Scope

This SRS defines the mandatory technology stack and architectural requirements for an enterprise-level RMG (Ready-Made Garments) ERP system.

The system is engineered to natively support:
1. **Multiple Companies / Business Units** (Sister concerns, multi-tenant corporate groups).
2. **Multiple Factories & Archetypes** (`KNIT_DEDICATED`, `WOVEN_DEDICATED`, `DENIM_DEDICATED`, `SWEATER_DEDICATED`, `WASH_DEDICATED`, `EMBELLISHMENT_DEDICATED`, `COMPOSITE_MULTI`).
3. **Deep Organizational Hierarchy**: Buildings, floors, sections, and production lines.
4. **Order Management & Merchandising**: Inquiry, tech pack, costing, BOM, sample tracking, buyer POs, T&A.
5. **Fabric & Trims Inventory**: Multi-bin warehouse, shade segregation, roll barcode inspection (4-Point), inventory aging.
6. **Cutting Room Management**: CAD marker import, fabric relaxation logs, lay chart, piece card, and bundle ticketing.
7. **Embellishment & Subcontract**: Printing, embroidery, heat transfer, inter-departmental dispatch & receive.
8. **Sewing Floor Execution**: Hourly line tracking, RFID/barcode real-time scanning, line balancing, DHU/Defect logging.
9. **Industrial Washing & Wet Processing**: Batch recipe management, chemical titration, dry process, shade banding (A/B/C).
10. **Finishing, Packing & Buyer AQL**: Ironing, metal detector pass, cartooning, carton barcode scan, pre-shipment AQL audit.
11. **Quality Assurance**: Traffic light system, statistical process control, end-line QC, inline roving QC.
12. **Commercial & Logistics**: Export LC, Import Back-to-Back LC, Customs Bond Register, RGP/NRGP Gate Pass.
13. **Compliance & Approval Workflows**: Hierarchical configurable multi-tier approval matrix with segregation of duties.
14. **Reporting, Dashboards & Telemetry**: Sub-second operational grids, Dapper-accelerated executive summaries, and real-time shop-floor TV Andon displays.
15. **Full Auditability & Traceability**: Immutable historical transaction and security audit logs (`old_value` $\rightarrow$ `new_value`).
16. **Role-Based and Fine-Grained Access Control**: Module $\rightarrow$ Resource $\rightarrow$ Action permission taxonomy.
17. **Company/Factory/Floor/Line-Level Data Restriction**: Dynamic backend Data Scope Engine.
18. **Real-time Notifications**: WebSockets / ASP.NET Core SignalR alerts for line disruptions and approvals.
19. **Future AI & Scanner Integration**: pgvector semantic matching, edge tablet / handheld RFID/barcode integration.

---

## 2. Architectural Principles & High-Level Topology

### 2.1 Core Principles
- **Domain-Driven Design (DDD)**: Rich domain models encapsulating manufacturing business rules and invariants; anemic domain models are forbidden.
- **Clean Architecture**: Strict unidirectional dependencies: $\text{API} \rightarrow \text{Application} \rightarrow \text{Domain} \leftarrow \text{Infrastructure}$.
- **Modular Monolith**: Organized into distinct Bounded Contexts with in-process event communication; zero microservice operational overhead initially, but cleanly decoupled for future extraction.
- **API-First & Contract-First**: Strictly typed OpenAPI (Swagger) specifications with server-side validation.
- **Client-Side SPA Architecture**: Pure React 19 + Vite SPA for maximum internal factory back-office speed and memory efficiency.
- **Zero-Trust Server-Side Authorization**: Backend is the sole security authority. Frontend UI checks are strictly ergonomic controls, never security barriers.
- **Defense-in-Depth Security**: Keycloak Authentication $\rightarrow$ JWT Validation $\rightarrow$ Policy Authorization $\rightarrow$ Data Scope Filtering $\rightarrow$ Business Invariant Validation.

### 2.2 Recommended High-Level Architecture Diagram

```
                    ┌──────────────────────────────────────────────┐
                    │                 Web Browser                  │
                    │        React 19 + TypeScript + Vite          │
                    │  Tailwind 4 + shadcn/ui + AG Grid + ECharts  │
                    │         TanStack Query + Redux Toolkit       │
                    └──────────────────────┬───────────────────────┘
                                           │ HTTPS (WSS for SignalR)
                                           ▼
                    ┌──────────────────────────────────────────────┐
                    │        Reverse Proxy / Load Balancer         │
                    │                    Nginx                     │
                    └──────────────────────┬───────────────────────┘
                                           │
                                           ▼
              ┌────────────────────────────────────────────────────────┐
              │                  ASP.NET Core API                      │
              │             .NET 10 / C# 14 (.NET 8/9 LTS)             │
              │                                                        │
              │       API  ──>  Application  ──>  Domain  <──  Infra   │
              │                                                        │
              │   AuthN  ──>  AuthZ  ──>  Policy  ──>  Data Scope      │
              └───────┬──────────────────┬──────────────────┬──────────┘
                      │                  │                  │
                      ▼                  ▼                  ▼
                PostgreSQL             Redis              MinIO
              (Transactional)         (Cache)        (Object Store)
                      │                  │                  │
                      ▼                  ▼                  ▼
               Background Jobs      Distributed        Tech Packs,
                  Hangfire             Locks           Attachments
                      │
                      ▼
                 Integrations
        RabbitMQ / SignalR / AI / Scanners
```

---

## 3. Technology Stack Specification

| Layer | Technology | Purpose & Architectural Justification |
| :--- | :--- | :--- |
| **Frontend Framework** | **React 19** | Modern component-based web UI framework with optimized rendering and Transitions. |
| **Frontend Language** | **TypeScript** | Strict end-to-end type safety across DTOs, forms, states, and permissions. |
| **Build Tool** | **Vite** | Blazing-fast development server (ESBuild) and highly optimized production rollups. |
| **UI Styling** | **Tailwind CSS 4** | Next-gen CSS-first Oxide engine, custom design tokens, `@theme` configuration. |
| **UI Components** | **shadcn/ui + Radix UI** | Accessible, headless, fully customizable enterprise component primitives. |
| **Iconography** | **Lucide React** | Consistent, crisp, scalable enterprise iconography. |
| **Server State** | **TanStack Query v5** | API caching, auto-refetching, query invalidation, pagination, and mutations. |
| **Client State** | **Redux Toolkit (RTK)** | Global client-only context: active factory, user snapshot, sidebar, filter presets. |
| **Forms & Validation** | **React Hook Form + Zod**| Minimal re-render form state management with strict schema validation. |
| **Enterprise Data Grid**| **AG Grid (Enterprise)** | Virtualized tables, pin columns, excel export, cell editing for 50,000+ records. |
| **Charts & Analytics** | **Apache ECharts** | Canvas/SVG rendering for production efficiency, DHU trendlines, and capacity gauges. |
| **Client Routing** | **React Router v7** | Client-side SPA routing with permission-aware route guard metadata. |
| **Backend Framework** | **ASP.NET Core 10** | High-performance, cross-platform enterprise web API framework. |
| **Runtime & Language** | **.NET 10 / C# 14** | Primary language and runtime platform (built on .NET 8/9 LTS foundation). |
| **Primary ORM** | **EF Core 10** | Aggregate persistence, rich domain mappings, migrations, change tracking. |
| **High-Performance SQL**| **Dapper** | Raw micro-ORM for heavy read-only dashboards, KPI cards, and batch reports. |
| **Primary Database** | **PostgreSQL 16+** | Relational ACID database, Native Range Partitioning, GIN indexes, JSONB, UUIDv7. |
| **Distributed Cache** | **Redis 7.2 / Valkey** | Distributed permission cache, data scope cache, rate limiter, distributed locks. |
| **Authentication (IdP)** | **Keycloak** | OpenID Connect, OAuth 2.0, SSO, MFA, Centralized Identity & Session management. |
| **Authorization Engine** | **ASP.NET Core Policy** | Custom requirement handlers, dynamic DB permission provider, SoD evaluation. |
| **Data Scope Engine** | **Custom Engine** | 7-tier organizational data filter (Company $\rightarrow$ Factory $\rightarrow$ Building $\rightarrow$ Floor $\rightarrow$ Section $\rightarrow$ Line). |
| **Background Jobs** | **Hangfire** | Persistent background processing, cron jobs, heavy PDF/Excel generation. |
| **Real-Time Engine** | **ASP.NET Core SignalR** | WebSockets for live shop-floor production counters, defect alarms, supervisor alerts. |
| **Object Storage** | **MinIO (S3-Compatible)**| Secure on-premise/cloud object store for Tech Packs, lab dips, QC photos, PDFs. |
| **Message Broker** | **RabbitMQ** | Asynchronous decoupling for inter-module integration and background dispatch. |
| **Logging & Telemetry** | **Serilog + OpenTelemetry**| Structured JSON logging, distributed tracing (W3C TraceContext) across calls. |
| **Metrics & Monitoring**| **Prometheus + Grafana** | Real-time system telemetry, API latency (<300ms), DB query profiling (<100ms). |
| **Reverse Proxy** | **Nginx** | TLS termination, gzip/brotli compression, rate limiting, and static asset serving. |
| **Containers & DevOps** | **Docker & Docker Compose**| Consistent containerization across local development, staging, and production. |
| **Reporting Engines** | **QuestPDF + ClosedXML** | High-precision PDF document generation (invoices, challans) and Excel reporting. |

---

## 4. Frontend Architecture & Ergonomics (React 19 SPA)

### 4.1 Folder Structure Standard (`apps/web/src/`)
```
src/
├── app/                  # Application bootstrap, providers, root layouts
├── assets/               # Static images, brand logos, svgs
├── components/           # Atomic shadcn/ui and Radix UI design system primitives
│   ├── ui/               # Button, Input, Modal, Drawer, Combobox
│   ├── feedback/         # Loading skeletons, Alert, EmptyState
│   └── tables/           # AG Grid enterprise wrapper components
├── features/             # Business feature modules (Bounded Contexts)
│   ├── merchandising/    # Orders, Costing, BOM, T&A
│   ├── cutting/          # Lay Chart, Relaxation, Bundles
│   ├── sewing/           # Hourly Output, Defect Logging, Line Balancing
│   ├── inventory/        # Fabric rolls, Trims bins, GRN, Stock aging
│   └── admin/            # Users, Roles, Permissions, Data Scopes
├── layouts/              # AppLayout, AuthLayout, FullscreenKioskLayout
├── pages/                # Page route components (thin orchestration)
├── routes/               # React Router definitions with permission metadata
├── services/             # Axios / Fetch HTTP API client instances
├── store/                # Redux Toolkit slices (AuthContext, UIState, ScopeContext)
├── hooks/                # Custom utility hooks (usePermission, useDataScope, useSignalR)
├── schemas/              # Zod validation schemas matching backend contracts
├── permissions/          # Permission string constants, Can component, RouteGuards
├── types/                # Global TypeScript definitions & DTOs
├── utils/                # Date formatting, currency, barcode generators
└── main.tsx              # Application entry point
```

### 4.2 State Management Boundary
1. **Server State (TanStack Query v5)**:
   - All HTTP GET requests, pagination, server caching, optimistic mutations, refetch on window focus.
   - **Never store API entity payloads in Redux**.
2. **Client State (Redux Toolkit)**:
   - User identity snapshot: `{ userId, fullName, roles, email }`.
   - Active organizational scope context: `{ activeCompanyId, activeFactoryId }`.
   - Permission snapshot: `string[]` of granted permission keys.
   - UI layout state: `sidebarOpen`, `themeMode`, `activeTab`.

### 4.3 Enterprise Data Grid (AG Grid)
- Plain HTML tables (`<table>`) are **strictly banned** for business data lists.
- All transactional lists (Orders, POs, Bundles, Rolls, Inventory, Outputs) must use the standardized AG Grid wrapper.
- Standard capabilities enabled:
  - Virtualized DOM row and column rendering (smooth 60fps scrolling on 50,000+ items).
  - Multi-column sorting and filtering (Set filter, date range filter).
  - Floating pinned columns (e.g., Action buttons, Order Number pinned left).
  - Background async CSV/Excel export.
  - Custom cell renderers for status badges and permission-guarded actions.

### 4.4 Frontend Authorization Ergonomics
Frontend authorization exists **solely for user delight and interface clarity**, never as a security barrier:
```tsx
// Reusable UI Permission Guard Component
<Can permission="cutting.plan.approve">
  <Button variant="default" onClick={handleApprove}>
    Approve Lay Plan
  </Button>
</Can>
```

---

## 5. Backend Architecture & Clean DDD (.NET 10 / C# 14)

### 5.1 Project Layering Standard (`ThreadFlow.sln`)
```
backend/
├── src/
│   ├── ThreadFlow.Domain/            # Pure Business Domain (No dependencies)
│   │   ├── Common/                   # BaseEntity, ValueObject, IAggregateRoot, DomainEvent
│   │   ├── Entities/                 # Rich Aggregates (Order, CuttingPlan, Bundle, ProductionOutput)
│   │   ├── ValueObjects/             # Money, Dimensions, Barcode, GeoCoordinate
│   │   ├── Exceptions/               # DomainRuleViolationException
│   │   └── Specifications/           # ISpecification queries
│   │
│   ├── ThreadFlow.Application/       # Use Cases, CQRS & Interfaces
│   │   ├── Common/                   # Interfaces (IApplicationDbContext, ICurrentUser, ICache)
│   │   ├── Features/                 # Vertical Slice CQRS Handlers (MediatR)
│   │   │   ├── Orders/Commands/      # CreateOrderCommand, ApproveOrderCommand
│   │   │   └── Orders/Queries/       # GetOrderByIdQuery, GetOrdersListQuery (Dapper/EF)
│   │   ├── Behaviors/                # ValidationBehavior (FluentValidation), LoggingBehavior
│   │   └── Authorization/            # IPermissionService, IDataScopeEnforcer
│   │
│   ├── ThreadFlow.Infrastructure/    # External Concerns & Concrete Implementations
│   │   ├── Persistence/              # EF Core ApplicationDbContext, EntityConfigurations
│   │   ├── Repositories/             # Dapper Query Repositories
│   │   ├── Identity/                 # Keycloak JWT Handler, ClaimsTransformation
│   │   ├── Caching/                  # RedisDistributedCacheService
│   │   ├── Storage/                  # MinIOObjectStorageService
│   │   ├── Jobs/                     # Hangfire Job Registrations
│   │   └── Realtime/                 # SignalR ProductionHub
│   │
│   └── ThreadFlow.WebApi/            # API Endpoints, Middlewares & Host
│       ├── Controllers/              # Thin API Controllers
│       ├── Middlewares/              # ExceptionHandlingMiddleware, AuditLogMiddleware
│       ├── Authorization/            # Custom Requirement Handlers & Dynamic Policy Provider
│       └── Program.cs                # Dependency Injection & Pipeline Config
```

### 5.2 Hybrid Data Access Strategy: EF Core + Dapper
```
[Client HTTP Request]
         │
         ├─── Command (Write/Update) ───> MediatR Command Handler
         │                                      │
         │                               EF Core 10 ORM
         │                                      │
         │                               Domain Invariants & ACID
         │                                      ▼
         │                                 PostgreSQL
         │
         └─── Query (High-Speed Read) ──> MediatR Query Handler
                                                │
                                            Dapper SQL
                                                │
                                         Unbuffered / Fast DTO
                                                ▼
                                         Sub-50ms Response
```

---

---

## 6. The 7-Tier Security, Multi-Tenancy & Authorization Engine

### 6.0 Multi-Tenancy Architecture (SaaS-Ready Hybrid Model)
ThreadFlow ERP is engineered from Day 1 with a **SaaS-Ready Hybrid Multi-Tenant Architecture**. This design natively supports two deployment modalities using the exact same codebase:
1. **Public Cloud Multi-Tenant SaaS**: Multiple independent factory subscribers operate on `app.threadflow.erp` (or custom domains), isolated via `tenant_id` and EF Core Global Query Filters.
2. **Dedicated Enterprise On-Premise**: Deployed on a conglomerate's private cloud/datacenter as a single-tenant instance with zero configuration drift.

```
Tenant (Subscriber Organization / Client)
   │
   ▼
Company (Legal Entity / Conglomerate Sister Concern)
   │
   ▼
Business Unit (Division)
   │
   ▼
Factory (Production Campus)
   │
   ▼
Building ──► Floor ──► Section ──► Line
```

#### Tenant Resolution Flow:
1. **Subdomain / Host Header**: e.g., `apex.threadflow.erp` $\rightarrow$ Resolves `tenant_code = 'APEX'`.
2. **HTTP Header (API Calls)**: `X-Tenant-Id: <UUIDv7>` (Validated against caller's token claims).
3. **JWT Claims**: Keycloak Access Token contains `tenant_id` claim.
4. **EF Core Global Query Filter**:
   ```csharp
   modelBuilder.Entity<Company>().HasQueryFilter(e => e.TenantId == _tenantService.CurrentTenantId);
   modelBuilder.Entity<Order>().HasQueryFilter(e => e.TenantId == _tenantService.CurrentTenantId);
   ```

### 6.1 Authentication vs Authorization Separated

```
Authentication (Keycloak)
   │ "Who are you?" (Identifies User & Tenant Claim)
   ▼
ASP.NET Core JWT Middleware & Tenant Resolution Middleware
   │ Validates RS256 Signature, extracts UserId & TenantId
   ▼
ERP Authorization Engine (PostgreSQL + Redis)
   │ 0. Tenant Isolation: Ensure user belongs to active Tenant.
   │ 1. Permission Check: Can user perform this action? (<module>.<resource>.<action>)
   │ 2. Data Scope Check: Does record belong to user's assigned Factory/Floor/Line?
   │ 3. Workflow Policy: Is document in a state that permits this action?
   │ 4. Segregation of Duties: Is the approver different from the creator?
   │ 5. Approval Matrix: Does transaction exceed user's financial/quantity limit?
   │ 6. Audit Logging: Record immutable forensic trail in DB.
   ▼
[Execution Allowed or 403 Forbidden]
```

### 6.2 8-Tier Organizational & Spatial Data Scope Hierarchy
The system enforces strict multi-level spatial and organizational isolation:
$$\mathbf{Tenant} \longrightarrow \mathbf{Company} \longrightarrow \mathbf{Business \ Unit} \longrightarrow \mathbf{Factory} \longrightarrow \mathbf{Building} \longrightarrow \mathbf{Floor} \longrightarrow \mathbf{Section} \longrightarrow \mathbf{Line}$$

- A Sewing Line Supervisor assigned to `Factory-01 -> Building-B -> Floor-03 -> Line-05` will have database queries automatically constrained to that exact scope.
- In-memory checks and SQL filters ensure zero data leakage across different SaaS tenants, sister companies, distinct factory campuses, or different lines.

### 6.3 Permission String Taxonomy
All system permissions follow a strict three-segment machine-readable format:
$$\mathbf{\langle module\rangle.\langle resource\rangle.\langle action\rangle}$$

Examples:
- `merchandising.order.create`
- `merchandising.costing.approve`
- `cutting.layplan.approve`
- `cutting.bundle.transfer`
- `sewing.output.log`
- `quality.inspection.reject`
- `commercial.exportlc.view`
- `admin.role.assign`

### 6.4 Core Authorization & Multi-Tenancy Database Schema (`snake_case`)
```sql
-- Root Tenant Table (SaaS Layer)
CREATE TABLE tenants (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v7(),
    tenant_code VARCHAR(32) UNIQUE NOT NULL,
    tenant_name VARCHAR(256) NOT NULL,
    subdomain VARCHAR(64) UNIQUE NOT NULL,
    custom_domain VARCHAR(256) UNIQUE,
    subscription_tier VARCHAR(32) NOT NULL DEFAULT 'ENTERPRISE',
    subscription_status VARCHAR(32) NOT NULL DEFAULT 'ACTIVE',
    isolation_strategy VARCHAR(32) NOT NULL DEFAULT 'SHARED_ROW_LEVEL',
    dedicated_connection_string TEXT,
    max_factories INT NOT NULL DEFAULT 5,
    max_lines INT NOT NULL DEFAULT 50,
    max_active_users INT NOT NULL DEFAULT 200,
    primary_contact_name VARCHAR(128) NOT NULL,
    primary_contact_email VARCHAR(256) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT clock_timestamp()
);

-- Core Role & Permission Tables
CREATE TABLE roles (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v7(),
    tenant_id UUID REFERENCES tenants(id) ON DELETE CASCADE, -- NULL for Global System Roles
    code VARCHAR(64) NOT NULL,
    name VARCHAR(128) NOT NULL,
    description TEXT,
    is_system_role BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT clock_timestamp(),
    CONSTRAINT uq_roles_tenant_code UNIQUE (tenant_id, code)
);

CREATE TABLE permissions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v7(),
    code VARCHAR(128) UNIQUE NOT NULL,  -- e.g. 'cutting.plan.approve'
    module VARCHAR(64) NOT NULL,
    resource VARCHAR(64) NOT NULL,
    action VARCHAR(32) NOT NULL,
    description TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT clock_timestamp()
);

CREATE TABLE role_permissions (
    role_id UUID NOT NULL REFERENCES roles(id) ON DELETE CASCADE,
    permission_id UUID NOT NULL REFERENCES permissions(id) ON DELETE CASCADE,
    PRIMARY KEY (role_id, permission_id)
);

CREATE TABLE user_roles (
    tenant_id UUID NOT NULL REFERENCES tenants(id) ON DELETE CASCADE,
    user_id UUID NOT NULL,
    role_id UUID NOT NULL REFERENCES roles(id) ON DELETE CASCADE,
    assigned_at TIMESTAMPTZ NOT NULL DEFAULT clock_timestamp(),
    assigned_by UUID NOT NULL,
    PRIMARY KEY (user_id, role_id)
);

-- Data Scope Hierarchical Tables
CREATE TABLE user_data_scopes (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v7(),
    tenant_id UUID NOT NULL REFERENCES tenants(id) ON DELETE CASCADE,
    user_id UUID NOT NULL,
    company_id UUID NOT NULL,
    factory_id UUID,
    building_id UUID,
    floor_id UUID,
    section_id UUID,
    line_id UUID,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT clock_timestamp()
);

-- Immutable Forensic Audit Logs
CREATE TABLE audit_logs (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v7(),
    tenant_id UUID NOT NULL REFERENCES tenants(id) ON DELETE CASCADE,
    user_id UUID NOT NULL,
    action VARCHAR(64) NOT NULL,
    module VARCHAR(64) NOT NULL,
    resource_type VARCHAR(64) NOT NULL,
    resource_id UUID NOT NULL,
    old_value JSONB,
    new_value JSONB,
    ip_address VARCHAR(45) NOT NULL,
    user_agent TEXT,
    correlation_id VARCHAR(64) NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT clock_timestamp()
);
```

### 6.5 Dynamic ASP.NET Core Policy Handler Implementation
Instead of writing hundreds of static policy classes, ASP.NET Core uses a custom `DynamicPermissionPolicyProvider` and `PermissionRequirementHandler`:

```csharp
// Attaching the policy to any API Controller or Minimal API endpoint
[Authorize(Policy = "permission:cutting.plan.approve")]
[HttpPost("{id:guid}/approve")]
public async Task<IActionResult> ApproveCuttingPlan(Guid id, [FromBody] ApprovePlanCommand command)
{
    command.PlanId = id;
    var result = await _mediator.Send(command);
    return Ok(result);
}
```

---

## 7. Database Standards & Concurrency Strategy

1. **System of Record**: PostgreSQL 16+ is the definitive system of record. Redis is strictly a cache and coordinate layer; persisting master business records in Redis is strictly forbidden.
2. **Key Convention**:
   - Primary Keys: Time-ordered **UUIDv7** (`RFC 9562`) for monotonic index clustering and zero B-Tree index fragmentation.
   - Foreign Keys: Explicit referencing constraints (`company_id`, `factory_id`, `user_id`).
   - Timestamps: Always `TIMESTAMPTZ` in UTC.
3. **Database Constraints**: Integrity rules (unique style numbers, non-negative inventory balances, valid status values) must be enforced via `CHECK` and `UNIQUE` constraints in PostgreSQL, never relying solely on C# validation.
4. **Optimistic Concurrency**:
   - Concurrency tokens (`xmin` or `byte[] RowVersion`) must be enforced on high-contention records (e.g., Inventory Balances, Bundle Statuses) to prevent race conditions during concurrent line scans.

---

## 8. Real-Time, Background Jobs & Object Storage

### 8.1 Hangfire Background Job Processing
- Long-running tasks (>500ms) are strictly offloaded from the HTTP pipeline.
- Hangfire handles:
  - Buyer T&A recalculations upon order amendment.
  - Automated midnight inventory reconciliation and aging snapshots.
  - Heavy batch exports (QuestPDF invoices, ClosedXML order books).
  - Webhook delivery and external buyer EDI integrations.

### 8.2 ASP.NET Core SignalR WebSockets
- **Shop-Floor Andon Display**: Broadcasts real-time sewing line output counters, target vs actual efficiency, and red alert flags to 50-foot LED TVs across the factory floor.
- **Supervisor Notification Hub**: Instant alerts when a cutting lay check fails, fabric relaxation moisture test is out of tolerance, or critical machine breakdown occurs.

### 8.3 MinIO S3 Object Storage
- Files, images, and heavy binary blobs must never be stored directly in PostgreSQL columns.
- Storage Structure in MinIO:
  - `bucket-techpacks/{companyId}/{styleId}/{version}.pdf`
  - `bucket-labdips/{companyId}/{labdipId}/swatch.jpg`
  - `bucket-qc-defects/{factoryId}/{inspectionDate}/{defectId}.jpg`
- PostgreSQL stores only the metadata record: `{ id, file_name, object_key, content_type, size_bytes, sha256_hash, created_at }`.

---

## 9. Observability, DevOps & Deployment Baseline

### 9.1 Observability Triangle
1. **Logging**: Serilog structured JSON logging to Console & Seq/Elasticsearch. Includes `CorrelationId`, `UserId`, `FactoryId`, `ElapsedMilliseconds`.
2. **Tracing**: OpenTelemetry distributed tracing across HTTP requests, EF Core SQL queries, Redis cache lookups, and Hangfire background jobs.
3. **Metrics**: Prometheus scraper endpoint exposing API latency percentiles (P95, P99), active SignalR connections, database connection pool saturation, and GC memory pressure, visualized in Grafana dashboards.

### 9.2 Containerized Topology (`docker-compose.yml` Baseline)
```
threadflow-stack:
  ├── nginx (Reverse Proxy / TLS / Static Serve)
  ├── threadflow-web (React 19 Vite Production Build)
  ├── threadflow-api (ASP.NET Core Web API)
  ├── threadflow-postgres (PostgreSQL 16 with UUIDv7 & pgvector)
  ├── threadflow-redis (Redis 7.2 Distributed Cache)
  ├── threadflow-minio (MinIO S3-Compatible Object Storage)
  ├── threadflow-keycloak (Keycloak IAM & OIDC Provider)
  └── threadflow-hangfire (Background Worker Process)
```

---

## 10. Mandatory Architectural Rules & Governance

1. **Backend Authorization is King**: Frontend checks are convenience helpers; the backend ASP.NET Core API is the final security and compliance authority.
2. **Server-Side Data Scope Enforcement**: All data-fetching queries must be scoped to the authenticated user's organization context at the database query level.
3. **Thin Controllers, Rich Domain**: Business rules live strictly in Domain entities and Application command handlers, never in API controllers or frontend components.
4. **Deny by Default**: Any operation without an explicit matching permission grant is denied (`403 Forbidden`).
5. **No AI/Fluff File Naming**: Code files must strictly follow clean international naming standards (`OrderService.cs`, `useOrders.ts`, `CuttingPlanDto.cs`).
6. **Documentation Before Coding**: Architectural blueprints, database schemas, and API contracts must be completely documented and approved prior to launching implementation code.

---

## 11. Final Architecture Sign-Off

The baseline technology stack for **ThreadFlow Enterprise RMG ERP** is officially locked:

$$\mathbf{React\ 19\ (Vite\ SPA)\ +\ ASP.NET\ Core\ 10\ (C\#\ 14)\ +\ PostgreSQL\ 16\ +\ Redis\ +\ Keycloak\ +\ MinIO}$$

This stack provides enterprise-grade throughput, banking-grade data security, instant shop-floor ergonomics, and long-term maintainability for the world's most demanding garments manufacturing conglomerates.
