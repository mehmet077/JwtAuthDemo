# 🔐 JWT Authentication Demo (.NET)

Bu proje, **ASP.NET Core Web API** kullanılarak geliştirilmiş,  
**JWT (JSON Web Token) tabanlı kimlik doğrulama ve rol bazlı yetkilendirme** örneğidir.

Amaç; JWT mantığını **net, sade ve öğretici** bir şekilde göstermek ve
gerçek projelerde kullanılabilecek sağlam bir temel sunmaktır.

---

## 🚀 Özellikler

- JWT ile kullanıcı doğrulama
- Role-based authorization (`Admin`, `User` vb.)
- `appsettings.json` üzerinden merkezi JWT yönetimi
- Secure endpoint’ler
- Swagger üzerinden JWT ile test imkânı
- Clean ve anlaşılır kod yapısı

---

## 🛠️ Kullanılan Teknolojiler

- **.NET 8 / .NET 9**
- **ASP.NET Core Web API**
- **JWT (System.IdentityModel.Tokens.Jwt)**
- **Swagger (Swashbuckle)**
- **Authorization & Claims**

---

## 📁 Proje Yapısı
JwtAuthDemo
│
├── Controllers
│ ├── AuthController.cs // Token üretimi
│ └── SecureController.cs // Yetkili endpoint'ler
│
├── Models
│ └── LoginModel.cs // Login request modeli
│
├── appsettings.json // JWT ayarları
├── Program.cs // JWT konfigürasyonu
