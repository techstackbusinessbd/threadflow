# 📝 ডিসকাশন নং ০১৭: টেক স্ট্যাক আধুনিকায়ন ও বাস্তব আরএমজি ফ্লো অডিট
### *Tech Stack Modernization (Tailwind v4, Serwist, Zod) & RMG Process Audit*

- **তারিখ**: ০৭ অক্টোবর, ২০২৬
- **টাইপ**: টেক স্ট্যাক আধুনিকায়ন, লাইব্রেরি অডিট ও ডোমেন গ্যাপ রিকনসিলিয়েশন
- **উপস্থিতি**:
  - 👑 বস (Founder / CTO / Head of Engineering)
  - 🏛️ তানভীর (Principal Architect & Tech Lead)
  - 🎨 সজীব (Frontend Craftsman)
  - ⚡ আসিফ (Senior Backend Engineer)
  - 📱 ইমরান (Mobile & Floor PWA Engineer)
  - 🗄️ ফাহিম (Database Architect)
  - 📋 নাবিলা (Lead Business Analyst & RMG Specialist)
  - 🛡️ মায়া (QA & Security Specialist)
  - 📚 শাকিল (Documentation Specialist)

---

## ১. মিটিংয়ের প্রেক্ষাপট ও বসের নির্দেশনা
বসের সুনির্দিষ্ট পর্যবেক্ষণ: *"Tailwind CSS 3.4+ eta old, new 4... e rokom missing r asa kina check koro."*
এই নির্দেশনার আলোকে পুরো টিম টেক স্ট্যাকের প্রতিটি লাইব্রেরি এবং আরএমজি ফ্লোর প্রসেসের খুঁটিনাটি অডিট করে।

---

## ২. গৃহীত সিদ্ধান্তসমূহ (Decisions Approved by CTO)

### সিদ্ধান্ত ১: টেক স্ট্যাকের আধুনিকতম ভার্সন ও লাইব্রেরি স্ট্যান্ডার্ড
1. **Tailwind CSS v4 (Oxide Rust Engine)**:
   - পুরোনো ৩.৪ এবং `tailwind.config.js` সম্পূর্ণ পরিহার করা হলো।
   - লেটেস্ট Rust-ভিত্তিক Oxide ইঞ্জিন ব্যবহার হবে, যা ১০ গুণ দ্রুত কম্পাইল হয় এবং সরাসরি CSS `@theme` দিয়ে ডিজাইন সিস্টেম টোকেন কনফিগার করে।
2. **Serwist (`@serwist/next`) ফ্লোর PWA-এর জন্য**:
   - অবহেলিত পুরোনো `next-pwa` বাতিল করা হলো।
   - Next.js 15 App Router ও React 19-এর জন্য আধুনিক অফিসিয়াল স্ট্যান্ডার্ড **Serwist** চূড়ান্ত করা হলো।
3. **End-to-End Zod Schema Validation**:
   - ব্যাকএন্ড ও ফ্রন্টএন্ডের জন্য একক Zod স্কিমা ডিফাইন করা হবে (`packages/contracts`)।
   - ব্যাকএন্ডে `zod-nestjs` এবং ফ্রন্টএন্ডে `react-hook-form` + `@hookform/resolvers/zod` একই স্কিমা ব্যবহার করবে।
4. **TanStack Query v5 (React Query)**:
   - ফ্লোর থেকে কোনো কাটিং বা সুইং এন্ট্রি হলে মার্চেন্ডাইজারদের পেজে পেজ রিলোড ছাড়াই রিয়েল-টাইম ক্যাশ আপডেট ও অপটিমিস্টিক আপডেট হবে।
5. **Valkey 8.0 / Redis 7.2**:
   - লিনাক্স ফাউন্ডেশনের ওপেন-সোর্স স্ট্যান্ডার্ড Valkey 8.0 অথবা Redis 7.2 মেমোরি ক্যাশ হিসেবে ব্যবহৃত হবে।

---

### সিদ্ধান্ত ২: বাস্তব আরএমজি ফ্যাক্টরির ৩টি মিসিং প্রসেস সংযোজন
1. **ডোমেন ০২: কম্পোজিট টেক্সটাইল মিল ফ্লো**:
   - নিট কম্পোজিট কারখানার জন্য ইয়ার্ন রিসিভিং $\rightarrow$ সার্কুলার/ফ্ল্যাট নিটিং $\rightarrow$ ল্যাব ডিপ (Lab Dip) কালার স্পেক্ট্রোফোটোমিটার অ্যাপ্রুভাল $\rightarrow$ ডাইং ও ফিনিশিং ব্যাচ কার্ড।
2. **ডোমেন ০৪: ঝুট ও কাটিং স্ক্র্যাপ অডিট (Jhut & Scrap Wastage)**:
   - কাটিং এন্ড-কাটস ও ঝুট ওজন (KG), কাস্টমস বন্ড অনুমোদিত ৭-৯% ওয়েস্টেজ রিকনসিলিয়েশন এবং ভ্যাট মূসক-৬.৩ চালানে ঝুট সেলস।
3. **গেটপাস সিকিউরিটি কন্ট্রোল (RGP / NRGP)**:
   - Returnable Gate Pass (RGP) ও Non-Returnable Gate Pass (NRGP) ট্র্যাকিং।

---

> **রেকর্ডার**: শাকিল (Documentation Specialist) & তানভীর (Tech Lead)  
> **অনুমোদনকারী**: বস (Founder / CTO / Head of Engineering)
