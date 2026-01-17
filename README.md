# 🐝 BeeFocus - Çalışma Takip Uygulaması

<p align="center">
  <img src="https://private-user-images.githubusercontent.com/156241732/537160337-7cdc875f-d6af-4940-95e6-78ab5b71691d.png?jwt=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3Njg2NjY0MjksIm5iZiI6MTc2ODY2NjEyOSwicGF0aCI6Ii8xNTYyNDE3MzIvNTM3MTYwMzM3LTdjZGM4NzVmLWQ2YWYtNDk0MC05NWU2LTc4YWI1YjcxNjkxZC5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjYwMTE3JTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI2MDExN1QxNjA4NDlaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT0zOWUxZGUzOWE2ZGMwNmQ3NzUzY2Y1MTBjYTMyYjg4MWZkZDdjODVjYWU0YWRmYTBlZTUyOWMwYzZmNGY4YTMxJlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.u3ZvZqIL5TZB-f7atWObCHVyLYggRwWk45kmTUwru8k" alt="BeeFocus Logo" width="120"/>
</p>
Proje tanıtım videosu: https://drive.google.com/file/d/10qAwRobKwJIrgELTKM8G5y4CXc6820kJ/view?usp=sharing
> **YKS öğrencileri için geliştirilmiş, çalışma sürelerini takip eden ve analiz eden tam kapsamlı bir uygulama.**

BeeFocus, TYT/AYT/YDT sınavlarına hazırlanan öğrencilerin ders çalışma sürelerini takip etmelerine, pomodoro tekniği ile verimli çalışmalarına ve detaylı istatistiklerle ilerlemelerini görmelerine olanak tanır.

---

## 📁 Proje Yapısı

Bu repo, 3 ayrı projeden oluşmaktadır:

```

├── BeeFocus/          # 🔧 Backend API (.NET 8)
├── BeeFocusClient/    # 📱 Mobil Uygulama (Flutter)
└── BeeFocusWeb/       # 🌐 Web Dashboard (React + Vite)
```

---

## 🔧 Backend (BeeFocus)

### Teknolojiler

| Teknoloji | Açıklama |
|-----------|----------|
| **.NET 8** | Modern, yüksek performanslı framework |
| **Minimal API** | Hafif ve hızlı REST API |
| **Entity Framework Core** | ORM - Veritabanı erişimi |
| **PostgreSQL** | İlişkisel veritabanı |
| **MediatR** | CQRS pattern implementasyonu |
| **FluentValidation** | Input validasyonu |
| **JWT Bearer** | Token tabanlı kimlik doğrulama |
| **Swagger/OpenAPI** | API dokümantasyonu |

### Mimari: Clean Architecture + Modular Monolith

```
BeeFocus/
├── BeeFocus.API/           # Ana giriş noktası, endpoint routing
├── BeeFocus.Auth/          # Kimlik doğrulama (Login, Register, JWT)
├── BeeFocus.Users/         # Kullanıcı yönetimi
├── BeeFocus.Subjects/      # Dersler (TYT/AYT/YDT kategorileri)
├── BeeFocus.StudySessions/ # Çalışma oturumları (Pomodoro/Stopwatch)
├── BeeFocus.Analytics/     # İstatistik ve raporlama
└── BeeFocus.Shared/        # Ortak bileşenler (BaseEntity, Interfaces)
```

### Her Modülün Katmanları (Clean Architecture)

```
BeeFocus.(Modül)/
├── Domain/           # Entity'ler ve iş kuralları
├── Application/      # CQRS (Commands/Queries), DTOs, Validators
├── Infrastructure/   # Repository implementasyonları, servisler
├── Persistence/      # DbContext, EF Core yapılandırmaları
├── Endpoints/        # Minimal API endpoint tanımları
└── Extensions/       # Dependency Injection konfigürasyonları
```

### API Endpoints

#### 🔐 Authentication

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| POST | `/api/auth/register` | Yeni kullanıcı kaydı |
| POST | `/api/auth/login` | Giriş (JWT token döner) |
| POST | `/api/auth/refresh` | Token yenileme |
| GET | `/api/auth/me` | Mevcut kullanıcı bilgisi |

#### 📚 Subjects (Dersler)

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| GET | `/api/subjects` | Tüm dersler |
| GET | `/api/subjects/category/{category}` | TYT/AYT/YDT'ye göre |
| GET | `/api/subjects/field/{field}` | Sayısal/Sözel/EA/Dil'e göre |

#### ⏱️ Study Sessions

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| POST | `/api/sessions/start` | Oturum başlat |
| POST | `/api/sessions/{id}/finish` | Oturumu bitir |
| DELETE | `/api/sessions/{id}` | Oturumu sil |
| GET | `/api/sessions/today` | Bugünkü oturumlar |
| GET | `/api/sessions/date/{date}` | Tarihe göre oturumlar |
| GET | `/api/sessions/subject/{id}` | Derse göre oturumlar |

#### 📊 Analytics

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| GET | `/api/analytics/all-time` | Tüm zamanlar istatistiği |
| GET | `/api/analytics/weekly` | Haftalık istatistik |
| GET | `/api/analytics/daily-breakdown` | Günlük dağılım |
| GET | `/api/analytics/subjects` | Ders bazlı dağılım |

### Çalıştırma

```bash
cd BeeFocus
dotnet run --project src/BeeFocus.API
# Swagger UI: http://localhost:5000/swagger
```

---

## 📱 Mobil Uygulama (BeeFocusClient)

### Teknolojiler

| Teknoloji | Açıklama |
|-----------|----------|
| **Flutter 3.x** | Cross-platform mobil framework |
| **Dart 3.9+** | Programlama dili |
| **Riverpod** | State management |
| **GoRouter** | Declarative routing |
| **Dio** | HTTP client |
| **Isar** | Offline-first yerel veritabanı |
| **Flutter Secure Storage** | Güvenli token saklama |

### Mimari: Feature-First Architecture

```
lib/
├── main.dart
├── app.dart
├── core/
│   ├── auth/           # Token yönetimi, auth providers
│   ├── router/         # GoRouter yapılandırması
│   ├── theme/          # Tema ve renkler
│   └── network/        # Dio client, API base
├── features/
│   ├── auth/           # Login sayfası
│   ├── timer/          # ⏱️ Ana özellik: Pomodoro/Kronometre
│   │   ├── data/       # API, Repository, Models
│   │   ├── presentation/
│   │   │   ├── pages/  # TimerPage
│   │   │   ├── state/  # TimerNotifier, TimerState
│   │   │   └── widgets/# TimerRing, TimerControls, ModeSelector
│   ├── subjects/       # Ders seçimi
│   ├── reports/        # 📊 İstatistik ve raporlar
│   ├── profile/        # Profil sayfası
│   ├── settings/       # Ayarlar
│   └── shared/         # Ortak widget'lar (AppDrawer)
```

### Özellikler

#### ⏱️ Timer (Ana Özellik)
- **Pomodoro Modu**: Geri sayım ile çalışma (25dk, 50dk vb.)
- **Kronometre Modu**: Serbest süre çalışma
- **Ders Seçimi**: Hangi ders için çalışıldığını kaydetme

#### 📊 Raporlar
- Toplam çalışma süresi
- Haftalık istatistikler
- Günlük dağılım (bar chart)
- Ders bazlı breakdown
- Haftalık hedef takibi (donut chart)

#### 🔄 Offline-First
- Isar veritabanı ile yerel kayıt
- İnternet olmadan çalışma
- Bağlantı geldiğinde otomatik senkronizasyon

### Çalıştırma

```bash
cd BeeFocusClient/beefocus
flutter pub get
flutter run
```

---

## 🌐 Web Dashboard (BeeFocusWeb)

### Teknolojiler

| Teknoloji | Açıklama |
|-----------|----------|
| **React 19** | UI framework |
| **Vite 7** | Build tool |
| **React Router DOM** | Routing |
| **CSS Modules** | Styling |

### Yapı

```
beefocus-web/
├── src/
│   ├── App.jsx
│   ├── main.jsx
│   ├── services/
│   │   └── api.js      # Auth, Analytics, Subjects API
│   └── pages/
│       ├── Login.jsx   # Giriş sayfası
│       └── Reports.jsx # 📊 Raporlar dashboard
```

### Özellikler

#### 🔐 Authentication
- JWT token ile giriş
- Refresh token desteği
- Otomatik token yenileme

#### 📊 Reports Dashboard
- **Toplam Çalışma**: Tüm zamanlar istatistiği
- **Bu Hafta**: Haftalık özet kartları
- **Günlük Dağılım**: Bar chart ile görselleştirme
- **Ders Bazında**: Renk kodlu ders istatistikleri

### Çalıştırma

```bash
cd BeeFocusWeb/beefocus-web
npm install
npm run dev
# http://localhost:5173
```

---

## 🔗 Sistem Entegrasyonu

```
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│   📱 Flutter    │     │   🌐 React Web  │     │                 │
│   Mobile App    │────▶│    Dashboard    │────▶│   🔧 .NET 8     │
│                 │     │                 │     │   Backend API   │
│  - Timer        │     │  - Reports      │     │                 │
│  - Subjects     │     │  - Login        │     │  - PostgreSQL   │
│  - Reports      │     │                 │     │  - JWT Auth     │
│  - Offline DB   │     │                 │     │  - CQRS         │
└─────────────────┘     └─────────────────┘     └─────────────────┘
         │                       │                       │
         └───────────────────────┴───────────────────────┘
                              REST API
                         (JSON over HTTP)
```

---

## 🚀 Kurulum

### Gereksinimler
- .NET 8 SDK
- Flutter 3.x
- Node.js 18+
- PostgreSQL 14+

### 1. Backend

```bash
cd BeeFocus
# appsettings.json'da PostgreSQL connection string'i ayarla
dotnet ef database update --project src/BeeFocus.Auth
dotnet ef database update --project src/BeeFocus.Users
dotnet ef database update --project src/BeeFocus.Subjects
dotnet ef database update --project src/BeeFocus.StudySessions
dotnet run --project src/BeeFocus.API
```

### 2. Mobil

```bash
cd BeeFocusClient/beefocus
# lib/core/network/ içinde API_BASE_URL'i ayarla
flutter pub get
flutter run
```

### 3. Web

```bash
cd BeeFocusWeb/beefocus-web
# src/services/api.js içinde API_BASE_URL'i ayarla
npm install
npm run dev
```

---

## 👨‍💻 Geliştirici

**İbrahim** - Ağ Tabanlı Programlama Dersi Projesi (2026)

---

## 📄 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

---

## 🎯 Proje Özellikleri Özeti

✅ **Backend**: Clean Architecture, CQRS, JWT Auth, Modular Monolith  
✅ **Mobil**: Feature-First, Riverpod, Offline-First, Isar DB  
✅ **Web**: React 19, Vite, Modern Dashboard  
✅ **Entegrasyon**: REST API, Token-based Auth, Real-time Sync
