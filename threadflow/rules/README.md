# 📜 ThreadFlow ERP — সম্পূর্ণ ডেভেলপমেন্ট রুলস ও ইঞ্জিনিয়ারিং স্ট্যান্ডার্ডস (Development Ruleset Index)

> **উদ্দেশ্য**: একটি দীর্ঘমেয়াদী, শতভাগ এন্টারপ্রাইজ গ্রেড পোশাক শিল্প ইআরপির (Enterprise RMG-ERP) জন্য কোনো একক ফাইলে সব নিয়ম লেখা সম্ভব নয়। তাই প্রতিটি টেকনিক্যাল ডোমেন ও কনসার্নের জন্য আলাদা আলাদা ডেডিকেটেড রুলবুক প্রস্তুত করা হয়েছে।
> 
> **বাধ্যতামূলক নীতি**: প্রতিটি ডেভেলপার, কোডার এবং ইঞ্জিনিয়ার কোড লেখার সময় এই রুলবুকগুলো অক্ষরে অক্ষরে মেনে চলতে বাধ্য। কোনো রুল অমান্য করলে PR স্বয়ংক্রিয়ভাবে রিজেক্ট হবে।

---

## 🗂️ মডুলার ডেভেলপমেন্ট রুলবুক ইনডেক্স (Rules Directory Index)

| ক্রম | রুলবুকের নাম ও ফাইল | দায়িত্বপ্রাপ্ত লিড | মূল ফোকাস ও আওতা |
| :---: | :--- | :--- | :--- |
| **০১** | [01-architecture-and-modular-monolith.md](01-architecture-and-modular-monolith.md) | 🏛️ তানভীর (Architect) | ডোমেন-ড্রাইভেন ডিজাইন (DDD), মডুলার মনোলিথ বাউন্ডারি, জিরো ক্রস-ডোমেন ডিবি এক্সেস, ডোমেন ইভেন্টস |
| **০২** | [02-database-transactions-and-orm.md](02-database-transactions-and-orm.md) | 🗄️ ফাহিম (Database Lead) | পোস্টগ্রেস স্কিমা নেমিং, ACID ট্রানজ্যাকশন ব্লক, জিরো-ফ্লোট ডেসিমাল ম্যাথ, সফট-ডিলিট ও অডিট ট্রেইল |
| **০৩** | [03-backend-dotnet-and-clean-architecture.md](03-backend-dotnet-and-clean-architecture.md) | ⚡ আসিফ (Backend Lead) | ASP.NET Core 10, .NET 10 / C# 14, ক্লিন আর্কিটেকচার, MediatR, FluentValidation, ProblemDetails, পেজিনেশন |
| **০৪** | [04-frontend-react-vite-and-aggrid.md](04-frontend-react-vite-and-aggrid.md) | 🎨 সজীব (Frontend Lead) | React 19, Vite (SPA), AG Grid Enterprise, TanStack Query, Redux Toolkit, কীবোর্ড-ফার্স্ট এরগোনোমিক্স |
| **০৫** | [05-security-auth-and-rbac.md](05-security-auth-and-rbac.md) | 🛡️ মায়া (Security Lead) | Keycloak OIDC, পলিসি-বেসড অথোরাইজেশন, ৭-লেয়ার ডেটা স্কোপ ইঞ্জিন, SoD (Creator != Approver), অডিট ট্রেইল |
| **০৬** | [06-qa-testing-and-reliability-gates.md](06-qa-testing-and-reliability-gates.md) | 🛡️ মায়া (QA Lead) | ১০০% ম্যাথ ফর্মুলা টেস্ট কভারেজ, ইন্টিগ্রেশন টেস্ট, সার্কিট ব্রেকার রুল, সিআই/সিডি পিআর ব্লকিং রুলস |
| **০৭** | [07-floor-pwa-and-iot-andon-standards.md](07-floor-pwa-and-iot-andon-standards.md) | 📱 ইমরান (Mobile/Floor Lead) | অফলাইন-ফার্স্ট IndexedDB সিঙ্ক, ক্যামেরা কিউআর স্ক্যানিং, অ্যান্ডন স্ক্রিন ওয়েব-সকেট, লো-নেটওয়ার্ক রেজিলিয়েন্স |
| **০৮** | [08-devops-docker-and-git-workflow.md](08-devops-docker-and-git-workflow.md) | 🚀 কবীর (DevOps Lead) | ডকার-কম্পোজ স্ট্যান্ডার্ড, এনভায়রনমেন্ট সিকিউরিটি, কনভেনশনাল গিট কমিট, ব্রাঞ্চিং স্ট্র্যাটেজি |
| **০৯** | [09-typescript-strictness-and-coding-style.md](09-typescript-strictness-and-coding-style.md) | 🏛️ তানভীর & ⚡ আসিফ | স্ট্রিক্ট টাইপস্ক্রিপ্ট (`noImplicitAny`), ক্লিন কোড প্রিন্সিপাল, নেমিং কনভেনশন ও কোড ফরম্যাটিং |

---

> **সোর্স অব ট্রুথ অবস্থান**: এই রুলবুকগুলো একই সাথে `.agents/rules/` এবং `threadflow/rules/` ডিরেক্টরিতে স্থায়ীভাবে সক্রিয় ও সংরক্ষিত।
