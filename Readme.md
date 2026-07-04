# Camp Booking Application

Full-stack camp reservation platform with role-based access for Users and Admins.

- Users can register, log in, search camps, book, cancel future bookings, and rate completed stays.
- Admins can log in, view dashboard statistics, and manage camps.

## Project Overview

This repository contains:

- Backend: ASP.NET Core Web API (.NET 8), layered architecture, JWT auth, Identity, Entity Framework Core
- Frontend: React + Vite, Redux Toolkit, React Router, Axios
- Tests: xUnit, Moq, FluentAssertions

Solution file:

- CampBookingSolution.slnx

## Tech Stack

### Backend

- ASP.NET Core Web API (.NET 8)
- ASP.NET Core Identity
- JWT Authentication
- Entity Framework Core
- Npgsql provider (PostgreSQL)
- AutoMapper
- FluentValidation
- Swagger

### Frontend

- React 19
- Vite 8
- Redux Toolkit
- React Router
- Axios
- React Toastify

### Testing

- xUnit
- Moq
- FluentAssertions
- coverlet

## Architecture

### Backend Projects

- API layer: CampBooking.API
- Business layer: CampBooking.Business
- Data layer: CampBooking.Data
- Domain layer: CampBooking.Domain
- Test project: CampBooking.Tests

### Frontend Project

- CampBooking_Frontend

## Current Runtime Configuration

Backend bootstrapping and middleware:

- Program.cs

Key points:

- CORS policy allows frontend origin http://localhost:5173
- JWT authentication is enabled
- Identity is configured
- DbSeeder runs on startup to seed roles and admin user
- Swagger is enabled

Backend launch URLs:

- launchSettings.json
- HTTP profile: http://localhost:5293
- HTTPS profile: https://localhost:7263 and http://localhost:5293

Frontend API configuration:

- .env.development uses /api
- vite.config.js proxies /api to http://localhost:5293
- .env currently points to https://localhost:7263/api for non-dev behavior

## Authentication and Roles

Backend auth endpoints:

- AuthController.cs

Login response DTO includes:

- Token
- Email
- FullName
- Role
- AuthResponseDto.cs

Role is fetched from Identity in:

- AuthService.cs

Seeded roles and admin:

- DbSeeder.cs
- Roles: Admin, User
- Seeded admin email: admin@campbooking.com
- Seeded admin password: Admin@123

Frontend auth state:

- authSlice.js
- Persists token, email, fullName, role in localStorage

Frontend login page:

- UserLoginPage.jsx

Redirect logic:

- Admin -> /admin
- User -> /

## Role-based Navigation and Routing

Top navbar:

- Header.jsx

Guest navbar:

- Dashboard
- Login
- Register

User navbar:

- Dashboard
- My Bookings
- Manage Booking
- Rating
- Logout

Admin navbar:

- Dashboard
- Admin Dashboard
- Logout

Frontend route guards:

- AppRoutes.jsx

Guards:

- UserOnlyRouteGuard blocks Admin from user booking pages
- AdminRouteGuard allows only Admin role to /admin routes

Admin nested routes:

- /admin -> dashboard overview
- /admin/manage-camps -> camp management

Admin layout with sidebar:

- AdminLayout.jsx

Sidebar links:

- Dashboard
- Manage Camps

Active highlighting uses proper NavLink behavior with end for Dashboard.

## API Surface

Base path:

- /api

### Auth

- POST /api/Auth/register
- POST /api/Auth/login

### Camp

- GET /api/Camp
- GET /api/Camp/{id}
- GET /api/Camp/search
- POST /api/Camp (Admin only)
- PUT /api/Camp/{id} (Admin only)
- DELETE /api/Camp/{id} (Admin only)

### Booking

- POST /api/Booking (User only)
- GET /api/Booking/{bookingReference} (Owner or Admin)
- PUT /api/Booking/Cancel/{bookingReference} (User only)
- GET /api/Booking/MyBookings (User only)

### Rating

- POST /api/Rating (User only)

### Admin

- GET /api/Admin/stats (Admin only)

Controller references:

- CampController.cs
- BookingController.cs
- RatingController.cs
- AdminController.cs

## Frontend Functional Pages

Main pages:

- Dashboard: DashboardPage.jsx
- Camp Details: CampDetailsPage.jsx
- Booking: BookingPage.jsx
- Confirmation: ConfirmationPage.jsx
- My Bookings: MyBookingsPage.jsx
- Manage Booking: ManageBookingPage.jsx
- Rating: RatingPage.jsx
- Login: UserLoginPage.jsx
- Register: RegisterPage.jsx
- Admin Dashboard: AdminDashboardPage.jsx
- Admin Camps: AdminCampsPage.jsx

API client and services:

- client.js
- authService.js
- campService.js
- bookingService.js
- ratingService.js
- adminService.js

## Security Rules Implemented

- JWT required for protected APIs

Role restrictions:

- User-only for booking creation, cancellation, my bookings, ratings
- Admin-only for camp management and stats

Booking ownership protection:

- Non-admin cannot fetch another user's booking by reference
- API returns 403 Forbidden

- Frontend route-level role guards for user and admin areas
- Token auto-attached to requests via Axios interceptor

## Setup and Run

### Prerequisites

- .NET SDK 8
- Node.js 18+
- npm
- PostgreSQL-compatible database access (current config uses Supabase Postgres)

### Backend Run

1. Go to CampBookingSolution
2. Restore and build
3. Run API from CampBooking.API
4. Open Swagger at:
	- http://localhost:5293/swagger
	- https://localhost:7263/swagger

### Frontend Run

1. Go to CampBooking_Frontend
2. Install dependencies
3. Run dev server
4. Open URL printed by Vite (usually 5173 or fallback like 5174)

## Environment Variables

### Frontend

- VITE_API_BASE_URL

Current dev behavior:

- .env.development uses /api
- Vite proxy forwards to backend on 5293

### Backend

Connection string and JWT settings in:

- appsettings.json

Important security note:

- Move secrets out of appsettings.json into user secrets or environment variables for production.

## Testing

Test project:

- CampBooking.Tests

Frameworks:

- xUnit
- Moq
- FluentAssertions

Recommended command:

- dotnet test from CampBookingSolution

## Common Issues and Fixes

### Frontend 502 Bad Gateway during local dev

- Usually caused by proxy target mismatch
- Ensure backend is running on http://localhost:5293
- Confirm vite.config.js target matches backend

### Port already in use

- If backend fails to start with address in use, stop older process using that port

### Unauthorized or auto logout

- 401 responses clear stored auth and trigger global unauthorized event in axios client

## Project Scripts

Frontend scripts from package.json:

- dev
- build
- lint
- preview
