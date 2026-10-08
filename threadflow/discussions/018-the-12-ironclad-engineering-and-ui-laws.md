# 📝 ডিসকাশন নং ০১৮: বসের ১২টি অলঙ্ঘনীয় মূলনীতি — ডায়নামিক আর্কিটেকচার, জিরো হার্ডকোডিং ও প্রিমিয়াম হিউম্যান UI/UX
### *The 12 Ironclad Directives of the Founder / CTO*

- **তারিখ**: ০৭ অক্টোবর, ২০২৬
- **টাইপ**: প্রাতিষ্ঠানিক সংবিধান ও আর্কিটেকচারাল পলিসি লক
- **উপস্থিতি**:
  - 👑 বস (Founder / CTO / Head of Engineering)
  - 🏛️ তানভীর (Principal Architect & Tech Lead)
  - 🎨 সজীব (Frontend & Product Experience Craftsman)
  - ⚡ আসিফ (Senior Backend & API Lead)
  - 🗄️ ফাহিম (Database Architect)
  - 📋 নাবিলা (Lead Business Analyst & RMG Specialist)
  - 📱 ইমরান (Mobile & Floor Engineer)
  - 🚀 কবীর (DevOps & SRE)
  - 🛡️ মায়া (QA & Security Specialist)
  - 📚 শাকিল (Documentation Specialist)

---

## ১. মিটিংয়ের প্রেক্ষাপট ও বসের ঐতিহাসিক ডিরেক্টিভ
প্রতিষ্ঠাতা ও সিটিও (বস) পুরো ইঞ্জিনিয়ারিং টিমকে থ্রেডফ্লো ইআরপির আর্কিটেকচার, কোড কোয়ালিটি ও ইউজার ইন্টারফেসের বিষয়ে ১২টি সুস্পষ্ট নির্দেশনা প্রদান করেছেন:
*"full system dynamic hoba, kono hardcode use hoba na, code e kono syntex error, warning thakbena, code write er somoy must technology er official documentation follow korte hoba, full system adimin panel thaka configureable hoba. admin chaila ui/ux er button, input, label er name change korte parba but database er na. must datatable use hoba, normal table use hoba na, serverside validation hoba html validation na, ui/ux e unnecessary kono text or content or data hoba na, ui/ux e j text use hoba ta must human readable & easy hoba kono complex word use hoba na, robotic or ai vibe hoba na, file & folder name e robotic or ai vibe hoba na. ui/ux need & clean hoba. full software must user friendly hoba."*

---

## ২. ইঞ্জিনিয়ারিং টিমের প্রতিশ্রুতি ও বাস্তবায়ন পরিকল্পনা

1. **🏛️ তানভীর (Tech Lead)**: "জিরো হার্ডকোডিং এবং অফিশিয়াল ডকুমেন্টেশন কঠোরভাবে মেনে চলার জন্য আমি টাইপস্ক্রিপ্ট স্ট্রিক্ট কনফিগারেশন এবং আর্কিটেকচারাল বাউন্ডারি লক করেছি।"
2. **🎨 সজীব (Frontend)**: "সাধারণ কোনো HTML টেবিল সিস্টেমে থাকবে না; প্রতিটি গ্রিড হবে TanStack DataTable v8। এবং আমরা বসের নির্দেশে `ui_label_dictionary` তৈরি করেছি যাতে অ্যাডমিন যেকোনো বাটন ও লেবেলের নাম এক ক্লিকে পরিবর্তন করতে পারে কিন্তু ডাটাবেজ কলাম ১০০% অপরিবর্তিত থাকে।"
3. **⚡ আসিফ (Backend)**: "এইচটিএমএল ভ্যালিডেশনের ওপর কোনো ভরসা নয়; প্রতিটি রিকোয়েস্ট সার্ভারসাইডে Zod স্কিমা দিয়ে কঠোরভাবে ভ্যালিডেট হবে।"
4. **🛡️ মায়া (QA & Security)**: "কোডে একটি সিঙ্গেল সিনট্যাক্স এরর বা আনইউজড ওয়ার্নিং থাকলে পিআর পাস হবে না।"
5. **📚 শাকিল (Tech Writer)**: "কোনো রোবোটিক বুলি বা এআই ক্লিশে থাকবে না; সহজবোধ্য মানুষের ভাষায় সফটওয়্যারের প্রতিটি শব্দ ও ডকুমেন্টেশন লেখা থাকবে।"
6. **🎨 সজীব (Frontend)**: "পেজ বনাম মোডালের ক্ষেত্রে—দীর্ঘমেয়াদী কাজে ফুল পেজ, কুইক অ্যাকশনে ফোকাসড মোডাল (<৭০% হাইট), এবং রো প্রিভিউতে স্লাইড-ওভার ড্রয়ার ব্যবহার হবে।"
7. **👑 বসের অতিরিক্ত ডিরেক্টিভ**: "UI/UX, সিস্টেম মেসেজ, এরর রেসপন্স, নোটিফিকেশন ও রিপোর্ট ১০০% প্রফেশনাল আন্তর্জাতিক ইংরেজিতে হতে হবে।"

---

> **অফিসিয়াল ডকুমেন্ট**: `threadflow/docs/05-dynamic-system-architecture-and-ui-governance.md`  
> **অনুমোদনকারী**: বস (Founder / CTO / Head of Engineering)
