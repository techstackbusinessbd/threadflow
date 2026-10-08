# 📝 ডিসকাশন নং ০১৯: এন্টারপ্রাইজ টেকনোলজি স্ট্যাক এসআরএস ও ডেটা স্কোপ ইঞ্জিন সাইন-অফ
### *Enterprise RMG ERP Technology Stack SRS & 6-Tier Security Governance*

- **তারিখ**: ০৮ অক্টোবর, ২০২৬
- **টাইপ**: টেকনোলজি স্ট্যাক এসআরএস অনুমোদন ও পলিসি লক
- **উপস্থিতি**:
  - 👑 বস (Founder / CTO / Head of Engineering)
  - 🏛️ তানভীর (Principal Architect & Tech Lead)
  - 🎯 রাফি (Product & Delivery Lead)
  - 📋 নাবিলা (Lead Business Analyst & RMG Specialist)
  - ⚡ আসিফ (Senior Backend & API Lead)
  - 🎨 সজীব (Frontend & Product Experience Craftsman)
  - 🗄️ ফাহিম (Database Architect)
  - 🚀 কবীর (DevOps & SRE)
  - 🛡️ মায়া (QA & Security Specialist)
  - 📚 শাকিল (Documentation Specialist)

---

## ১. মিটিংয়ের প্রেক্ষাপট ও বসের সুস্পষ্ট ডিরেক্টিভ
সিটিও / বস অফিসিয়াল **Technology Stack SRS** উপস্থাপন করেছেন এবং ইঞ্জিনিয়ারিং টিমকে তা পুঙ্খানুপুঙ্খভাবে বিশ্লেষণ করে ডকুমেন্টেশন চূড়ান্ত করার পর কোডিংয়ে যাওয়ার কঠোর নির্দেশনা দিয়েছেন:
> *"আপনি গ্রিন সিগন্যাল দিলে আমরা এই মাস্টার স্পেসিফিকেশনটি ফাইলে সেভ koro but coding akhon na documentation final kore then coding"*

পুরো টিম এই ডিরেক্টিভ সম্পূর্ণ গ্রহণ করেছে। **আগে ডকুমেন্টেশন ১০০% নির্ভুল ও চূড়ান্ত হবে, তারপর কোডিং শুরু হবে।** কোনো তাড়াহুড়ো বা অনুমানভিত্তিক কোডিং করা হবে না।

---

## ২. টিমের চূড়ান্ত ঐক্যমত্য ও আর্কিটেকচারাল সিদ্ধান্ত

1. **🏛️ তানভীর (Principal Architect)**:
   - "ফ্রন্টএন্ডের জন্য **React 19 + TypeScript + Vite (SPA)** এবং ব্যাকএন্ডে **ASP.NET Core (Clean Architecture + DDD + Modular Monolith)** লক করা হলো। ব্যাকঅফিস এন্টারপ্রাইজ সিস্টেমে এটি আল্ট্রা-ফাস্ট রেসপন্স নিশ্চিত করবে।"
2. **🎨 সজীব (Frontend Craftsman)**:
   - "টেবিল সিস্টেমে **AG Grid Enterprise** ফাইনাল করা হয়েছে। হাজার হাজার কাটিং বান্ডেল ও সাইজ ব্রেকডাউনের রো ভার্চুয়ালাইজেশনে কোনো ল্যাগ থাকবে না। স্টেট ম্যানেজমেন্টে **TanStack Query v5 (সার্ভার স্টেট)** এবং **Redux Toolkit (ক্লায়েন্ট কনটেক্সট)** কার্যকর থাকবে।"
3. **⚡ আসিফ (Backend Lead)**:
   - "কমান্ড পাথ ও এগ্রিগেট রাইটে **EF Core 10** এবং ভারী রিপোর্ট ও লাইভ ড্যাশবোর্ড কোয়েরিতে **Dapper** ব্যবহার হবে। সিগন্যালআর দিয়ে রিয়েল-টাইম অ্যান্ডন অ্যালার্ট এবং হ্যাংফায়ার দিয়ে ব্যাকগ্রাউন্ড জব পরিচালিত হবে।"
4. **📋 নাবিলা (Lead BA)**:
   - "গার্মেন্টস ফ্যাক্টরির ৭-লেয়ার ডেটা স্কোপ ($\text{Company} \rightarrow \text{Factory} \rightarrow \text{Building} \rightarrow \text{Floor} \rightarrow \text{Section} \rightarrow \text{Line}$) এবং Segregation of Duties (Creator $\neq$ Approver) ব্যাকএন্ডে মডেল করা হয়েছে।"
5. **🛡️ মায়া (QA & Security)**:
   - "আইডেন্টিটি থাকবে **Keycloak**-এ, কিন্তু ফাইন-গ্রেইন্ড পারমিশন ও ডেটা স্কোপ থাকবে **ERP PostgreSQL**-এ। ফ্রন্টএন্ড কোনো সিকিউরিটি দেয় না; ব্যাকএন্ডে Deny-by-default জিরো-ট্রাস্ট পলিসি চালু থাকবে।"
6. **🗄️ ফাহিম (Database Architect)**:
   - "পোস্টগ্রেসকিউএলে টাইম-অর্ডার্ড **UUIDv7**, `snake_case`, UTC টাইমস্ট্যাম্প এবং অডিট লগে `old_value` ও `new_value` ট্র্যাকিং বাধ্যতামূলক করা হয়েছে।"
7. **🚀 কবীর (DevOps Lead)**:
   - "ডকারাইজড স্ট্যাক (Postgres, Redis, MinIO, Keycloak, Nginx) এবং সেরিয়ার্লগ/ওপেনটেলিমেট্রি মনিটরিং কনফিগারেশনের এসআরএস স্পেক প্রস্তুত।"

---

## ৩. পরবর্তী ডকুমেন্টেড রোডম্যাপ (Documentation-First Strategy)
1. `threadflow/docs/10-technology-stack-srs-and-authorization-engine.md` ফাইলে মাস্টার এসআরএস স্থায়ীভাবে সেভ করা হয়েছে।
2. `.agents/ARCHITECTURE.md` এবং `.agents/TASKS.md` ফাইলে আপডেট সিঙ্ক করা হয়েছে।
3. বসের নির্দেশ অনুযায়ী কোডিং আপাতত হোল্ড থাকবে; পরবর্তী স্পেসিফিকেশন ও স্কিমা ডকুমেন্টেশন বসের পূর্ণাঙ্গ সাইন-অফের পরই কেবল কোড জেনারেশনে যাওয়া হবে।

---

> **অফিসিয়াল ডকুমেন্ট**: `threadflow/docs/10-technology-stack-srs-and-authorization-engine.md`  
> **অনুমোদনকারী**: বস (Founder / CTO / Head of Engineering)
