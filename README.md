📌 Genel Bakış
Bu API, şirketlerde personel masraflarının takibi ve yönetimi için çözüm sunar. Temel özellikler:

JWT kimlik doğrulama

Rol bazlı yetkilendirme (Admin/Personel)

Masraf talebi yönetimi


🚀 Kullanılan Teknolojiler
.NET 8 Web API

Entity Framework Core (ORM)

Microsoft SQL Server (Veritabanı)

JWT Bearer Kimlik Doğrulama

MediatR (CQRS Pattern)

Swagger API Dokümantasyonu

🔧 Kurulum
Önkoşullar
.NET 8 SDK

SQL Server 2019+

SQL Server Management Studio (SSMS)

Visual Studio 2022 veya VS Code

Kurulum Adımları
Depoyu klonlayın:
git clone https://github.com/Nilozkan/personnel-expense-system.git

appsettings.json dosyasında veritabanı bağlantısını yapılandırın:

"ConnectionStrings": {
  "MsSqlConnection": "Server=sunucu-adınız;Database=.\\SQLEXPRESS; Database=Personnel_expense_database;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
}

Veritabanı migration'larını uygulayın:

dotnet ef database update --project WebAPI
Uygulamayı çalıştırın:

dotnet run --project WebAPI

🔐 Kimlik Doğrulama
(JWT kimlik doğrulama hatalı)
API, JWT kimlik doğrulama kullanır. Örnek giriş akışı:

Token alın:

POST /api/user/login
Content-Type: application/json

{
  "email": "admin@example.com",
  "password": "Admin123!"
}

İsteklerde token'ı kullanın:

Authorization: Bearer {token_bilginiz}

📋 API Endpoint'leri
Kullanıcılar

POST	/api/user	Kullanıcı oluştur	Admin
GET	/api/user	Kullanıcıları listele	Admin
GET	/api/user/{id}	Kullanıcı detaylarını getir	Admin

Masraf Talepleri

POST	/api/expenserequest	Talep oluştur	Personel
PUT	/api/expenserequest/{id}/approve	Onayla/reddet	Admin
🛠 Veritabanı Yapılandırması
SQL Server kurulum gereksinimleri:

Gerekli tablolar EF Core migration'ları ile otomatik oluşturulacaktır

SQL Server servisinin çalıştığından emin olun

Varsayılan admin bilgileri (SeedData.cs içinde):

Email: admin@example.com

Şifre: Admin123!

📊 Örnek Veritabanı Şeması (MS SQL)

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Email NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    RoleId INT NOT NULL
);

CREATE TABLE ExpenseRequests (
    Id INT PRIMARY KEY IDENTITY,
    Amount DECIMAL(18,2) NOT NULL,
    Status INT NOT NULL,
    UserId INT NOT NULL
);
🌟 Dağıtım Notları
Prodüksiyon dağıtımı için:

Bağlantı dizesini SQL kimlik bilgileri ile güncelleyin

appsettings.json'da uygun JWT secret ayarlayın

Prodüksiyonda HTTPS yapılandırması yapın.