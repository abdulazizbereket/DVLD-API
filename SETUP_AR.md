# DVLD System API - تعليمات الإعداد والتشغيل

## 📋 المتطلبات

### قبل البدء، تأكد من وجود:

1. **.NET 7.0 SDK أو أحدث**
   ```bash
   dotnet --version
   ```
   اذا ما عندك، حمله من: https://dotnet.microsoft.com/download

2. **SQL Server** (Local أو Remote)
   - SQL Server 2016 أو أحدث
   - أو SQL Server Express (مجاني)

3. **Visual Studio 2022** أو **VS Code**

---

## 🔧 خطوات الإعداد

### الخطوة 1: استنساخ المشروع

```bash
git clone https://github.com/abdulazizbereket/DVLD-API.git
cd DVLD-API
```

---

### الخطوة 2: استعادة الـ NuGet Packages

```bash
dotnet restore
```

---

### الخطوة 3: تحديث Connection String

افتح ملف `appsettings.json` وحدّث:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=DVLD;Integrated Security=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**أمثلة Connection Strings:**

- **Local SQL Server:**
  ```
  Server=.;Database=DVLD;Integrated Security=true;
  ```

- **Named Instance:**
  ```
  Server=.\SQLEXPRESS;Database=DVLD;Integrated Security=true;
  ```

- **Remote Server:**
  ```
  Server=192.168.1.100;Database=DVLD;User Id=sa;Password=YourPassword;
  ```

---

### الخطوة 4: التأكد من وجود Database

تأكد من أن قاعدة البيانات **DVLD** موجودة على SQL Server مع الجداول التالية:

```sql
CREATE TABLE Countries (
    CountryID INT PRIMARY KEY,
    CountryName NVARCHAR(100) NOT NULL
);

CREATE TABLE People (
    PersonID INT PRIMARY KEY IDENTITY(1,1),
    NationalNo NVARCHAR(20) NOT NULL UNIQUE,
    FirstName NVARCHAR(100) NOT NULL,
    SecondName NVARCHAR(100) NOT NULL,
    ThirdName NVARCHAR(100),
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATETIME NOT NULL,
    Gendor SMALLINT,
    Address NVARCHAR(500),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    NationalityCountryID INT NOT NULL FOREIGN KEY REFERENCES Countries(CountryID)
);

CREATE TABLE ApplicationTypes (
    ApplicationTypeID INT PRIMARY KEY,
    ApplicationTypeTitle NVARCHAR(100),
    ApplicationFee DECIMAL(10, 2)
);

CREATE TABLE Applications (
    ApplicationID INT PRIMARY KEY IDENTITY(1,1),
    PersonID INT NOT NULL FOREIGN KEY REFERENCES People(PersonID),
    ApplicationDate DATETIME NOT NULL,
    ApplicationTypeID INT NOT NULL FOREIGN KEY REFERENCES ApplicationTypes(ApplicationTypeID),
    ApplicationStatus INT,
    LastStatusDate DATETIME,
    PaidFees DECIMAL(10, 2),
    UserID INT
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    PersonID INT NOT NULL FOREIGN KEY REFERENCES People(PersonID),
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    IsActive BIT
);
```

---

### الخطوة 5: بناء المشروع

```bash
dotnet build
```

---

### الخطوة 6: تشغيل المشروع

```bash
dotnet run
```

سيظهر شيء مثل:
```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to exit.
```

---

## 🌐 الوصول إلى الـ API

بعد التشغيل:

### 1. Swagger UI (التوثيق التفاعلي)
```
https://localhost:5001/swagger
```
أو
```
http://localhost:5000/swagger
```

### 2. API Base URL
```
http://localhost:5000/api
```

---

## ✅ اختبار الـ API

### استخدام Swagger UI:

1. افتح `http://localhost:5000/swagger`
2. اختر أي endpoint (مثلاً `GET /api/person`)
3. اضغط **Try it out**
4. اضغط **Execute**

### استخدام cURL:

```bash
# جلب جميع الأشخاص
curl -X GET "http://localhost:5000/api/person" \
  -H "accept: application/json"

# إضافة شخص جديد
curl -X POST "http://localhost:5000/api/person" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "أحمد",
    "secondName": "محمد",
    "thirdName": "علي",
    "lastName": "الشامي",
    "nationalNo": "1234567890",
    "dateOfBirth": "1990-01-15",
    "gendor": 0,
    "email": "ahmad@example.com",
    "phone": "0912345678",
    "address": "دمشق",
    "nationalityCountryID": 183
  }'
```

### استخدام Postman:

1. افتح Postman
2. أنشئ request جديد
3. اختر HTTP Method (GET, POST, PUT, DELETE)
4. أدخل URL: `http://localhost:5000/api/person`
5. أضف headers: `Content-Type: application/json`
6. أضف body (للـ POST و PUT)
7. اضغط Send

---

## 🐛 استكشاف الأخطاء

### خطأ: "Cannot open database requested"

**السبب:** Connection String غير صحيح أو Database غير موجود

**الحل:**
1. تأكد من صحة Server name
2. تأكد من وجود Database
3. اختبر الاتصال مباشرة من SQL Server

---

### خطأ: "Timeout expired"

**السبب:** Server بطيء أو معطوب

**الحل:**
1. تأكد من تشغيل SQL Server
2. زد `Connection Timeout` في Connection String:
   ```
   Server=.;Database=DVLD;Integrated Security=true;Connection Timeout=30;
   ```

---

### خطأ: "Port 5000 already in use"

**السبب:** هناك برنامج آخر يستخدم الـ port

**الحل:**
```bash
# غيّر الـ port في launchSettings.json
# أو شغل على port مختلف:
dotnet run --urls="http://localhost:5002"
```

---

## 📚 الملفات المهمة

```
DVLD-API/
├── appsettings.json          # إعدادات الـ Application
├── Program.cs                # نقطة البداية
├── Controllers/              # API Endpoints
├── DTOs/                     # Data Transfer Objects
├── BusinessLayer/            # Business Logic
├── DataAccessLayer/          # Database Access
└── Properties/               # Project Properties
```

---

## 🎯 الخطوات التالية

1. ✅ استنسخ المشروع
2. ✅ حدّث Connection String
3. ✅ تأكد من وجود Database والجداول
4. ✅ شغل `dotnet restore`
5. ✅ شغل `dotnet build`
6. ✅ شغل `dotnet run`
7. ✅ افتح `http://localhost:5000/swagger`
8. ✅ اختبر الـ Endpoints

---

## 📞 للمساعدة

إذا واجهت مشاكل:

1. اقرأ Error Message بعناية
2. تحقق من Connection String
3. تأكد من تشغيل SQL Server
4. افتح Issue على GitHub

---

**مبروك! المشروع جاهز للعمل! 🚀**
