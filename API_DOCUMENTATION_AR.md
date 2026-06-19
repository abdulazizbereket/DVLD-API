# API Documentation - DVLD System

## نظرة عامة

هذا API للنظام المركزي لإدارة رخص القيادة المحلية (DVLD) مبني على ASP.NET Core.

---

## المتطلبات

- .NET 7.0 أو أحدث
- SQL Server
- Visual Studio 2022 أو VS Code

---

## الإعداد الأولي

### 1. تحديث Connection String

في ملف `appsettings.json`، قم بتحديث:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DVLD;Integrated Security=true;"
}
```

### 2. تشغيل المشروع

```bash
dotnet run
```

سيظهر في المتصفح:
```
https://localhost:5000
```

### 3. الوصول إلى Swagger

```
https://localhost:5000/swagger
```

---

## Endpoints - النقاط النهائية

### 📌 Person Endpoints (الأشخاص)

#### 1. جلب جميع الأشخاص
```
GET /api/person
```

**Response:**
```json
{
  "success": true,
  "message": "تم جلب البيانات بنجاح",
  "data": [
    {
      "personID": 1,
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
      "nationalityCountryID": 183,
      "countryName": "Syria"
    }
  ]
}
```

---

#### 2. جلب شخص بالمعرف
```
GET /api/person/{id}
```

**مثال:**
```
GET /api/person/1
```

---

#### 3. جلب شخص برقم الهوية
```
GET /api/person/national/{nationalNo}
```

**مثال:**
```
GET /api/person/national/1234567890
```

---

#### 4. إضافة شخص جديد
```
POST /api/person
Content-Type: application/json
```

**Request Body:**
```json
{
  "firstName": "محمد",
  "secondName": "علي",
  "thirdName": "أحمد",
  "lastName": "الكردي",
  "nationalNo": "9876543210",
  "dateOfBirth": "1992-05-20",
  "gendor": 0,
  "email": "mohammad@example.com",
  "phone": "0998765432",
  "address": "حلب",
  "nationalityCountryID": 183
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "تمت إضافة الشخص بنجاح",
  "data": {
    "personID": 2,
    "firstName": "محمد",
    "secondName": "علي",
    "thirdName": "أحمد",
    "lastName": "الكردي",
    "nationalNo": "9876543210",
    "dateOfBirth": "1992-05-20",
    "gendor": 0,
    "email": "mohammad@example.com",
    "phone": "0998765432",
    "address": "حلب",
    "nationalityCountryID": 183
  }
}
```

---

#### 5. تحديث بيانات الشخص
```
PUT /api/person/{id}
Content-Type: application/json
```

**Request Body:** (نفس بيانات POST)

```json
{
  "firstName": "محمد",
  "secondName": "علي",
  "thirdName": "أحمد",
  "lastName": "الكردي",
  "nationalNo": "9876543210",
  "dateOfBirth": "1992-05-20",
  "gendor": 0,
  "email": "mohammad.updated@example.com",
  "phone": "0998765432",
  "address": "دمشق",
  "nationalityCountryID": 183
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "تم تحديث بيانات الشخص بنجاح",
  "data": {...}
}
```

---

#### 6. حذف الشخص
```
DELETE /api/person/{id}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "تم حذف الشخص بنجاح",
  "data": null
}
```

---

### 📌 Country Endpoints (الدول)

#### 1. جلب جميع الدول
```
GET /api/country
```

#### 2. جلب دولة بالمعرف
```
GET /api/country/{id}
```

#### 3. جلب دولة باسم الدولة
```
GET /api/country/name/{countryName}
```

---

### 📌 Application Endpoints (التطبيقات)

#### 1. جلب التطبيق
```
GET /api/application/{id}
```

#### 2. إضافة تطبيق جديد
```
POST /api/application
Content-Type: application/json
```

**Request Body:**
```json
{
  "personID": 1,
  "applicationDate": "2026-06-19",
  "applicationTypeID": 1,
  "applicationStatus": 1,
  "paidFees": 20,
  "userID": 1
}
```

#### 3. تحديث التطبيق
```
PUT /api/application/{id}
```

#### 4. حذف التطبيق
```
DELETE /api/application/{id}
```

---

### 📌 User Endpoints (المستخدمون)

#### 1. جلب المستخدم بالمعرف
```
GET /api/user/{id}
```

#### 2. جلب المستخدم باسم المستخدم
```
GET /api/user/username/{username}
```

#### 3. إضافة مستخدم جديد
```
POST /api/user
Content-Type: application/json
```

**Request Body:**
```json
{
  "personID": 1,
  "userName": "ahmad_user",
  "password": "SecurePassword123",
  "isActive": true
}
```

#### 4. تحديث المستخدم
```
PUT /api/user/{id}
```

#### 5. حذف المستخدم
```
DELETE /api/user/{id}
```

---

## رموز الحالة (Status Codes)

| الكود | المعنى |
|------|--------|
| 200 | OK - العملية نجحت |
| 201 | Created - تم إنشاء مورد جديد |
| 400 | Bad Request - بيانات غير صحيحة |
| 404 | Not Found - المورد غير موجود |
| 500 | Internal Server Error - خطأ في الخادم |

---

## أمثلة استخدام (cURL)

### جلب جميع الأشخاص
```bash
curl -X GET "https://localhost:5000/api/person" \
  -H "accept: application/json"
```

### إضافة شخص جديد
```bash
curl -X POST "https://localhost:5000/api/person" \
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

### تحديث شخص
```bash
curl -X PUT "https://localhost:5000/api/person/1" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "أحمد",
    "secondName": "محمد",
    "thirdName": "علي",
    "lastName": "الشامي",
    "nationalNo": "1234567890",
    "dateOfBirth": "1990-01-15",
    "gendor": 0,
    "email": "ahmad.updated@example.com",
    "phone": "0912345678",
    "address": "دمشق",
    "nationalityCountryID": 183
  }'
```

### حذف شخص
```bash
curl -X DELETE "https://localhost:5000/api/person/1"
```

---

## ملاحظات مهمة

1. **Gendor Field**: 
   - 0 = ذكر (Male)
   - 1 = أنثى (Female)

2. **ApplicationStatus**:
   - 1 = New (جديد)
   - 2 = Canceled (ملغى)
   - 3 = Completed (مكتمل)

3. **ApplicationType**:
   - 1 = Local License
   - 2 = Renew License
   - 3 = Replacement Lost
   - 4 = Replacement Damaged
   - 5 = Release
   - 6 = New International
   - 7 = Retake Test

4. **خطأ التحقق من الصحة**: يجب التأكد من:
   - أن الأسماء لا تكون فارغة (FirstName و LastName مطلوبان)
   - أن رقم الهوية فريد (لا يتكرر في قاعدة البيانات)
   - أن تاريخ الميلاد معقول
   - أن البريد الإلكتروني صحيح (إن وجد)

---

## الأخطاء الشائعة

### 1. Connection String غير صحيح
```
Error: Cannot open database requested in login
```
**الحل**: تحديث `appsettings.json` بـ server و database الصحيح

### 2. بيانات فارغة
```json
{
  "success": false,
  "message": "الاسم الأول والأخير مطلوبان",
  "data": null
}
```
**الحل**: إرسال جميع الحقول المطلوبة

### 3. شخص مكرر برقم هوية
```json
{
  "success": false,
  "message": "هذا الشخص موجود بالفعل برقم الهوية هذا",
  "data": null
}
```
**الحل**: استخدام رقم هوية فريد

---

## الدعم والمساهمة

للمساهمة في المشروع:

1. Fork المستودع
2. أنشئ branch جديد
3. اعمل على التحسينات
4. أرسل Pull Request

---

**آخر تحديث**: 2026-06-19
