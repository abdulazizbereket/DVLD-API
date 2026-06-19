# DVLD System - API

تطبيق API مبني على ASP.NET Core لنظام إدارة رخص القيادة المحلية (DVLD).

## المميزات

- ✅ RESTful API
- ✅ CRUD Operations للأشخاص
- ✅ إدارة الطلبات
- ✅ إدارة المستخدمين
- ✅ دعم الدول والجنسيات
- ✅ Error Handling شامل
- ✅ Swagger Documentation

## المتطلبات

- .NET 7.0 أو أحدث
- SQL Server
- Visual Studio 2022 أو أي IDE أخرى

## التثبيت

1. استنساخ المستودع:
```bash
git clone https://github.com/abdulazizbereket/DVLD-API.git
```

2. تحديث Connection String في `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DVLD;Integrated Security=true;"
}
```

3. تشغيل التطبيق:
```bash
dotnet run
```

## API Endpoints

### Person Endpoints
- `GET /api/person` - جلب جميع الأشخاص
- `GET /api/person/{id}` - جلب شخص بالمعرف
- `GET /api/person/national/{nationalNo}` - جلب شخص برقم الهوية
- `POST /api/person` - إضافة شخص جديد
- `PUT /api/person/{id}` - تحديث بيانات الشخص
- `DELETE /api/person/{id}` - حذف الشخص

### Country Endpoints
- `GET /api/country` - جلب جميع الدول
- `GET /api/country/{id}` - جلب دولة بالمعرف
- `GET /api/country/name/{countryName}` - جلب دولة بالاسم

## Swagger Documentation

بعد تشغيل التطبيق، يمكنك الوصول إلى Swagger UI على:
```
http://localhost:5000/swagger
```

## المتغيرات البيئية

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server connection string here"
  }
}
```

## التطوير

للمساهمة في المشروع:

1. عمل Fork للمستودع
2. إنشاء branch جديد (`git checkout -b feature/AmazingFeature`)
3. عمل Commit للتغييرات (`git commit -m 'Add some AmazingFeature'`)
4. عمل Push للـ branch (`git push origin feature/AmazingFeature`)
5. فتح Pull Request

## الترخيص

هذا المشروع تحت ترخيص MIT.
