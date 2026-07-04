# Camp Booking Frontend Complete Guide

This file explains everything done in this frontend in simple language.

## 1. What this frontend does
This frontend is built for the Camp Booking application.

It supports:
- User registration and login
- Camp search by dates and capacity
- Camp booking
- Booking confirmation
- Manage booking by reference
- Booking cancellation (future booking only)
- Rating after stay date
- Admin login
- Admin camp management (add, update, delete)

## 2. Technology used
- React (functional components + hooks)
- Vite (frontend build tool)
- React Router (page navigation)
- Redux Toolkit + React Redux (state management)
- Axios (API calls)
- React Toastify (toast messages)
- Responsive CSS

## 3. Authentication flow (important)
Current flow is:
1. User opens app
2. If not logged in, user cannot access dashboard directly
3. User goes to Register page or Login page
4. If new user: register first
5. Then login
6. After login, dashboard opens and user can use features

Route protection:
- UserProtectedRoute: protects user pages
- ProtectedRoute: protects admin pages

## 4. Pages created and purpose
- /register
  - Create user account
  - Fields: Name, Email, Phone Number, Password, Confirm Password
- /login
  - User login
- /
  - Dashboard (only after login)
  - Search/filter camps
  - Pagination
  - Book button
- /book/:campId
  - Booking form
  - Calculates total amount (rate per night x nights)
- /confirmation/:reference
  - Shows booking details and booking reference
- /manage-booking
  - Search booking by reference
  - Cancel booking (future only)
  - Add/edit rating (after stay date)
- /admin/login
  - Admin login
- /admin/camps
  - Admin camp CRUD
- /not-found
  - Fallback page

## 5. Main business flow
### A) User flow
1. Register account
2. Login
3. Open dashboard
4. Select check-in, check-out, capacity
5. Click Search
6. Available camps appear
7. Click Book Camp
8. Fill billing address and phone
9. Confirm booking
10. See confirmation with reference
11. Later use Manage Booking using reference

### B) Manage booking flow
1. Open Manage Booking
2. Enter 8-character booking reference
3. View booking details
4. If check-in is in future, cancellation is allowed
5. If stay date is over, rating can be added/updated

### C) Admin flow
1. Open Admin Login
2. Login with admin account (isAdmin should be true from backend)
3. Open Manage Camps
4. Add camp (all fields required)
5. Update or delete existing camps

## 6. Validations implemented
### Register page
- All fields required
- Password minimum 6 characters
- Confirm password must match
- Phone number must be exactly 10 digits

### Login page
- Email and password required

### Search section
- Check-in and check-out required
- Check-out must be greater than check-in
- Capacity filter supported (Any, 1, 2, 4, 8)

### Booking page
- Billing address required
- Cell phone required (10 digits)

### Manage booking page
- Reference length check
- Cancel allowed only for future booking
- Rating allowed only after stay date

### Admin camps page
- All camp fields required
- Capacity and rate must be valid positive numbers

## 7. State management
Redux store is used with two slices:
- auth slice
  - Stores logged in user and token
  - Saves data in localStorage
  - Handles logout
- search slice
  - Stores check-in, check-out, capacity, page, pageSize
  - Default values:
    - check-in = today
    - check-out = today + 1 day
    - capacity = Any

## 8. Toast notifications
Toast messages are shown for important actions:
- Registration success/failure
- Login success/failure
- Booking success/failure
- Booking search success/failure
- Cancellation success/failure
- Rating save success/failure
- Camp add/update/delete success/failure
- API loading errors

## 9. API endpoints expected from backend
Frontend currently calls these endpoints:
- POST /auth/register
- POST /auth/login
- GET /camps/availability
- POST /bookings
- GET /bookings/{reference}
- PATCH /bookings/{reference}/cancel
- PATCH /bookings/{reference}/rating
- GET /admin/camps
- POST /admin/camps
- PUT /admin/camps/{campId}
- DELETE /admin/camps/{campId}

If your backend endpoint names are different, update file: src/api/services.js

## 10. Environment setup
Create .env in project root with:
VITE_API_BASE_URL=http://localhost:5000/api

If not provided, frontend uses this default URL:
http://localhost:5000/api

## 11. How to run
1. npm install
2. npm run dev
3. Open app in browser

Useful commands:
- npm run lint
- npm run build
- npm run preview

## 12. Important files map
- src/main.jsx
  - App startup, providers
- src/app/store.js
  - Redux store
- src/App.jsx
  - Route configuration
- src/api/client.js
  - Axios client and token interceptor
- src/api/services.js
  - All API methods
- src/features/auth/authSlice.js
  - Auth state
- src/features/search/searchSlice.js
  - Search filter state
- src/components/UserProtectedRoute.jsx
  - User route protection
- src/components/ProtectedRoute.jsx
  - Admin route protection
- src/components/Header.jsx
  - Navbar links and logout
- src/pages/RegisterPage.jsx
  - User registration
- src/pages/UserLoginPage.jsx
  - User login
- src/pages/DashboardPage.jsx
  - Search + camp listing + pagination
- src/pages/BookingPage.jsx
  - Booking form and amount calculation
- src/pages/ConfirmationPage.jsx
  - Booking summary
- src/pages/ManageBookingPage.jsx
  - Search/cancel/rate booking
- src/pages/AdminLoginPage.jsx
  - Admin login
- src/pages/AdminCampsPage.jsx
  - Camp management
- src/App.css and src/index.css
  - Full UI styling and responsive layout

## 13. Notes and assumptions
- Booking reference is generated in frontend as 8-character alphanumeric value.
- Admin access is controlled using user.isAdmin from login response.
- Camp image has fallback image if URL fails.
- Dashboard is not open for guests now. Login is required first.

## 14. Quick testing checklist
- Register with valid data including phone number
- Login with same user
- Dashboard opens
- Search camps with different capacities
- Create booking and check confirmation page
- Find booking in Manage Booking
- Cancel future booking
- Add rating only after stay date
- Login as admin and test camp CRUD

This completes the frontend setup and flow implementation.

## 15. Detailed explanation of every frontend file created

Below is a simple explanation of each file created for this frontend setup.

### Root level files

**package.json**
- Holds project metadata, scripts, and dependencies.
- Main scripts used:
  - `npm run dev`: starts app in development mode.
  - `npm run build`: creates production build.
  - `npm run lint`: checks code quality.
- Dependencies added for this project:
  - `react-router-dom` for routing
  - `@reduxjs/toolkit` and `react-redux` for state
  - `axios` for API calls
  - `react-toastify` for toast messages

**README.md**
- Short project-level guide.
- Includes run steps and backend endpoint contract.

**FRONTEND_COMPLETE_GUIDE.md**
- This complete, detailed documentation file.
- Includes flow, setup, validation, and file explanations.

### Entry and app shell files

**src/main.jsx**
- Entry point of React app.
- Wraps app with:
  - `Provider` (Redux store)
  - `BrowserRouter` (routing)
- Imports toast stylesheet.

**src/App.jsx**
- Main route configuration file.
- Defines all routes for user/admin pages.
- Adds route protection:
  - `UserProtectedRoute` for logged-in users
  - `ProtectedRoute` for admin-only pages
- Adds global `ToastContainer`.

**src/index.css**
- Global CSS variables and base styling.
- Sets fonts, background, base text style, and heading style.

**src/App.css**
- Main UI styling for all pages and components.
- Includes layout, cards, forms, tables, buttons, responsive breakpoints.

### API layer files

**src/api/client.js**
- Creates shared Axios instance.
- Sets base API URL using env variable `VITE_API_BASE_URL`.
- Adds token to request header automatically using interceptor.

**src/api/services.js**
- Central place for all frontend API functions.
- Keeps UI code clean by moving HTTP calls out of pages.
- Includes methods for:
  - user register/login
  - camp listing
  - booking create/get/cancel/rate
  - admin camp CRUD

### Redux files

**src/app/store.js**
- Configures Redux store.
- Combines auth and search reducers.

**src/features/auth/authSlice.js**
- Stores logged-in user and JWT token.
- Reads initial auth data from localStorage.
- Exposes actions:
  - `setCredentials`
  - `logout`
- Saves/removes auth data in localStorage.

**src/features/search/searchSlice.js**
- Stores dashboard search filters and pagination.
- Default values:
  - check-in = today
  - check-out = today + 1 day
  - capacity = Any
- Exposes actions:
  - `setFilters`
  - `setPage`

### Utility files

**src/utils/date.js**
- Helper functions for date operations:
  - format date to ISO
  - add days
  - calculate nights between dates
  - check whether a date is future date

**src/utils/booking.js**
- Booking helper functions:
  - generate 8-character booking reference
  - normalize capacity filter value

### Reusable component files

**src/components/Header.jsx**
- Top navigation bar.
- Shows different menu links based on login status.
- Shows logged-in user info and logout button.
- Shows admin manage link when `isAdmin` is true.

**src/components/UserProtectedRoute.jsx**
- Protects user routes.
- If user is not logged in, redirects to `/login`.

**src/components/ProtectedRoute.jsx**
- Protects admin routes.
- If user is not admin, redirects to `/admin/login`.

**src/components/SearchBar.jsx**
- Dashboard filter form:
  - check-in date
  - check-out date
  - capacity
- Validates date inputs and dispatches filter update to Redux.

**src/components/CampCard.jsx**
- Displays each camp card in dashboard list.
- Shows camp image, location, capacity, price, rating.
- Contains Book button and passes selected data to booking page.

**src/components/Pagination.jsx**
- Reusable pagination UI.
- Handles previous/next page navigation.

**src/components/StarRating.jsx**
- Reusable 1-to-5 star rating selector.
- Used in Manage Booking page for rating submission.

### Page files (screen-level)

**src/pages/RegisterPage.jsx**
- User registration screen.
- Fields:
  - full name
  - email
  - phone number
  - password
  - confirm password
- Validates user input.
- Calls register API and redirects to login on success.

**src/pages/UserLoginPage.jsx**
- User login screen.
- Takes email and password.
- Calls login API and stores token/user in Redux + localStorage.
- Redirects to dashboard on success.

**src/pages/DashboardPage.jsx**
- Main user dashboard after login.
- Uses filters from Redux.
- Calls availability API.
- Renders camp list and pagination.

**src/pages/BookingPage.jsx**
- Booking form for selected camp.
- Calculates total amount using rate x nights.
- Validates billing address and phone number.
- Creates booking and redirects to confirmation page.

**src/pages/ConfirmationPage.jsx**
- Shows booking confirmation details.
- Displays booking reference and stay details.
- If needed, can fetch booking using reference from URL.

**src/pages/ManageBookingPage.jsx**
- Lets user find booking using reference.
- Shows booking details.
- Supports:
  - future booking cancellation
  - rating submission after stay date

**src/pages/AdminLoginPage.jsx**
- Admin login screen.
- Uses same login endpoint.
- Allows access only when backend returns `user.isAdmin = true`.

**src/pages/AdminCampsPage.jsx**
- Admin camp management screen.
- Supports:
  - Add new camp
  - Update existing camp
  - Delete camp
- Validates all mandatory fields.

**src/pages/NotFoundPage.jsx**
- Fallback route page.
- Shown when user navigates to an invalid URL.

## 16. Final quick flow summary

1. User opens app.
2. If not logged in, goes to Register or Login.
3. After login, dashboard opens.
4. User searches camps and books.
5. User can manage booking using reference.
6. Admin logs in separately and manages camps.

## 17. Code-level explanation of every file

This section explains how code works inside each file (not just purpose).

### 17.1 Root and configuration files

**package.json (code logic)**
- `scripts.dev` runs Vite dev server with hot reload.
- `scripts.build` runs production bundling.
- `scripts.lint` checks ESLint rules including React hooks rules.
- Dependency list controls what gets imported in code:
  - Router code comes from `react-router-dom`.
  - Global state code comes from `@reduxjs/toolkit` and `react-redux`.
  - API service code uses `axios`.
  - Notification code uses `react-toastify`.

### 17.2 App bootstrap files

**src/main.jsx (code logic)**
- `createRoot(document.getElementById('root'))` mounts React app.
- `StrictMode` wraps app for development checks.
- `Provider store={store}` makes Redux state available globally.
- `BrowserRouter` enables URL-based routing.
- Toast CSS import is required so toasts render with proper styles.

**src/App.jsx (code logic)**
- Uses `Routes` and `Route` to map URL path to page component.
- Wraps user pages with `UserProtectedRoute`:
  - If no logged-in user, redirect happens to `/login`.
- Wraps admin page with `ProtectedRoute`:
  - If user is not admin, redirect to `/admin/login`.
- Uses wildcard route `*` to redirect invalid paths to `/not-found`.
- `ToastContainer` is placed once at app root, so any page can call `toast.success/error`.

### 17.3 API files

**src/api/client.js (code logic)**
- Creates one Axios instance so all requests share config.
- `baseURL` reads `VITE_API_BASE_URL`; if missing, fallback URL is used.
- Request interceptor runs before every request:
  - Reads token from localStorage (`camp_token`).
  - Adds `Authorization: Bearer <token>` header if token exists.

**src/api/services.js (code logic)**
- Each function is a thin wrapper around one backend endpoint.
- Returns `response.data` directly so pages receive clean payload.
- Keeps HTTP details out of UI components.
- Current grouping:
  - Auth: `registerUser`, `loginUser`, `adminLogin`
  - Camp query: `fetchAvailableCamps`
  - Booking flow: `createBooking`, `getBookingByReference`, `cancelBooking`, `saveRating`
  - Admin camps: `fetchAdminCamps`, `addCamp`, `updateCamp`, `deleteCamp`

### 17.4 Redux files

**src/app/store.js (code logic)**
- Calls `configureStore` and registers reducers:
  - `auth`
  - `search`
- This creates a single source of truth used by `useSelector` and `useDispatch`.

**src/features/auth/authSlice.js (code logic)**
- Reads stored token/user once during initial load.
- `setCredentials` reducer:
  - Updates Redux state with token/user.
  - Persists both values in localStorage.
- `logout` reducer:
  - Clears token/user in state.
  - Removes localStorage values.
- This keeps login status persistent after page refresh.

**src/features/search/searchSlice.js (code logic)**
- Initial state sets assignment defaults:
  - check-in = today
  - check-out = tomorrow
  - capacity = Any
  - pagination defaults
- `setFilters` updates selected filters and resets page to 1.
- `setPage` changes current page only.

### 17.5 Utility files

**src/utils/date.js (code logic)**
- `toIsoDate(input)` normalizes date to local `YYYY-MM-DD`.
- `addDays(isoDate, days)` adds days and returns ISO date.
- `nightsBetween(checkIn, checkOut)` returns number of nights used for total amount.
- `isFutureDate(isoDate)` compares against today midnight to decide cancellation/rating rules.

**src/utils/booking.js (code logic)**
- `generateBookingReference()` creates 8-character reference using uppercase letters and digits.
- `normalizeCapacity(value)` converts `Any` to `null`, and numeric strings to numbers.

### 17.6 Shared components

**src/components/Header.jsx (code logic)**
- Reads auth state with `useSelector`.
- Conditionally renders nav links:
  - Logged-in user sees dashboard + manage booking.
  - Logged-out user sees login + register.
  - Admin user sees manage camps.
- `handleLogout` dispatches logout and navigates to `/`.

**src/components/UserProtectedRoute.jsx (code logic)**
- Reads `state.auth.user`.
- If user is missing, returns `<Navigate to="/login" replace />`.
- Otherwise renders children page.

**src/components/ProtectedRoute.jsx (code logic)**
- Reads `state.auth.user`.
- If `user?.isAdmin` is false, redirects to admin login.
- Else renders children (admin-only content).

**src/components/SearchBar.jsx (code logic)**
- Uses local form state for date/capacity controls.
- On check-in change, auto-corrects check-out if invalid.
- On submit:
  - validates both dates
  - checks check-out > check-in
  - dispatches `setFilters`
- This triggers dashboard data reload through Redux state change.

**src/components/CampCard.jsx (code logic)**
- Receives camp data as props.
- Shows fallback image if `imageUrl` fails to load.
- `Link` navigates to booking route and passes `camp`, `checkIn`, `checkOut` via route state.

**src/components/Pagination.jsx (code logic)**
- Renders only when `totalPages > 1`.
- Previous and next buttons call `onPageChange`.
- Disables buttons at first/last page boundaries.

**src/components/StarRating.jsx (code logic)**
- Renders 5 buttons for star values.
- Star style changes based on `value`.
- Clicking star triggers `onChange(star)` unless `readOnly` is true.

### 17.7 Page files

**src/pages/RegisterPage.jsx (code logic)**
- Local state object stores all fields.
- `onSubmit` validation order:
  - required fields check
  - 10-digit phone check
  - minimum password length
  - password confirmation check
- Calls `registerUser` API.
- On success: toast + navigate to `/login`.
- If already logged in, redirects to `/`.

**src/pages/UserLoginPage.jsx (code logic)**
- Takes email/password from controlled inputs.
- On submit calls `loginUser`.
- On success dispatches `setCredentials({ token, user })`.
- Then redirects to dashboard.
- If user already exists in state, redirects to `/`.

**src/pages/DashboardPage.jsx (code logic)**
- Reads search filters from Redux.
- `useEffect` depends on full `search` object, so any filter/page change reloads camps.
- Calls `fetchAvailableCamps` with normalized params.
- Supports flexible response shape (`items` / `data` / `camps`).
- Computes and stores total pages.
- Renders loading, empty state, or camp grid.

**src/pages/BookingPage.jsx (code logic)**
- Receives selected camp and dates from router state.
- Uses memoized calculations:
  - `nightsBetween`
  - `totalAmount = ratePerNight * nights`
- Validates billing address and phone before API call.
- Generates booking reference in frontend.
- Calls `createBooking` and navigates to confirmation page with booking state.

**src/pages/ConfirmationPage.jsx (code logic)**
- Tries to use booking object from navigation state first.
- If missing, fetches booking by reference from URL params.
- Displays key booking fields in summary card.

**src/pages/ManageBookingPage.jsx (code logic)**
- Searches booking by reference input.
- Stores fetched booking and current rating value.
- `handleCancel`:
  - allows cancel only if check-in is future date
  - calls cancel API and updates local booking status
- `submitRating`:
  - allows rating only after check-out date
  - calls rating API

**src/pages/AdminLoginPage.jsx (code logic)**
- Reuses login endpoint through `adminLogin`.
- After response, checks `result.user?.isAdmin`.
- Non-admin user is blocked with error toast.
- Admin credentials are saved in auth slice and route goes to `/admin/camps`.

**src/pages/AdminCampsPage.jsx (code logic)**
- Maintains camp list, form state, and optional `editingId`.
- `loadCamps` fetches all camps for admin table.
- `onSubmit` handles both create and update:
  - converts `capacity` and `ratePerNight` to numbers
  - validates all fields
  - if `editingId` exists -> update API
  - else -> add API
- `editCamp` fills form with selected row data.
- `removeCamp` calls delete API and refreshes list.

**src/pages/NotFoundPage.jsx (code logic)**
- Simple fallback UI with a link back to dashboard.
- Used by wildcard route.

### 17.8 Styling files

**src/index.css (code logic)**
- Defines design tokens in `:root`.
- Applies base font, color, background layers.
- Sets default heading font and global box-sizing.

**src/App.css (code logic)**
- Contains component-level class styles used across pages.
- Handles:
  - header layout
  - hero section
  - search panel grid
  - cards/tables/forms/buttons
  - mobile responsiveness via media queries

### 17.9 End-to-end data flow in code

1. User enters data in form component.
2. Form validates and dispatches action or calls service.
3. Service function uses Axios client to call backend.
4. Axios client automatically attaches auth token if available.
5. Response data updates local component state or Redux state.
6. UI re-renders and toast messages show success/failure.

## 18. How to connect backend with frontend (complete setup)

This section explains exactly how to make frontend + backend work as one complete application.

### 18.1 Step 1: Run backend and confirm base URL

Start your backend first and note:
- Host (example: `http://localhost`)
- Port (example: `5000`)
- API base path (example: `/api`)

Final base URL example:
- `http://localhost:5000/api`

You can quickly test backend is running by opening Swagger or calling one endpoint.

### 18.2 Step 2: Configure API URL in frontend

In frontend root, create file `.env` and add:

`VITE_API_BASE_URL=http://localhost:5000/api`

How this is used in code:
- File: `src/api/client.js`
- Code reads `import.meta.env.VITE_API_BASE_URL`
- If missing, fallback URL is `http://localhost:5000/api`

After changing `.env`, restart frontend dev server.

### 18.3 Step 3: Make sure backend allows CORS

Frontend and backend usually run on different ports, so backend must allow frontend origin.

If frontend runs on Vite default port, allow:
- `http://localhost:5173`

Backend should allow:
- origin
- headers
- methods
- credentials (if your auth setup needs it)

If CORS is not configured, API calls will fail in browser even if backend is running.

### 18.4 Step 4: Match API endpoints with frontend services

Frontend API mapping is in:
- `src/api/services.js`

Expected endpoints:
- `POST /auth/register`
- `POST /auth/login`
- `GET /camps/availability`
- `POST /bookings`
- `GET /bookings/{reference}`
- `PATCH /bookings/{reference}/cancel`
- `PATCH /bookings/{reference}/rating`
- `GET /admin/camps`
- `POST /admin/camps`
- `PUT /admin/camps/{campId}`
- `DELETE /admin/camps/{campId}`

If your backend uses different names (example `/camp/getAvailable`), update `src/api/services.js` accordingly.

### 18.5 Step 5: Match request payload fields

Frontend sends specific payload shapes. Backend should accept same keys, or you should adapt frontend.

Main payload examples:

Register:
- `{ name, email, phoneNumber, password }`

Login:
- `{ email, password }`

Create booking:
- `{ campId, checkIn, checkOut, billingAddress, phoneNumber, bookingReference }`

Save rating:
- `{ rating }`

Add/Update camp:
- `{ name, location, description, capacity, ratePerNight, imageUrl }`

If backend key names differ, map them before API call or transform backend DTO.

### 18.6 Step 6: Match response shape used by UI

Some pages expect these response fields:

Login response:
- `token`
- `user` object with at least `name/email` and `isAdmin`

Availability response:
- preferred: `{ items, totalPages, totalCount }`
- code also supports `data` or `camps` as list key

Booking response:
- should include `bookingReference` (or frontend falls back to generated one)

Booking details response:
- fields used in UI: `bookingReference`, `checkIn`, `checkOut`, `billingAddress`, `phoneNumber`, `camp` or `campName`, `status`, `rating`

If response fields are different, update page mapping in related file.

### 18.7 Step 7: JWT token and admin logic

Frontend auth token flow:
1. Login API returns token.
2. Token stored in Redux + localStorage by `authSlice`.
3. Axios interceptor in `src/api/client.js` adds `Authorization: Bearer <token>`.

Admin access logic:
- Frontend checks `user.isAdmin` from login response.
- If `isAdmin` is true, admin pages open.
- If false, user is blocked from admin routes.

So backend login response must include correct `isAdmin` flag.

### 18.8 Step 8: Backend validation and business rules alignment

Frontend already validates many inputs, but backend must still enforce rules.

Backend should validate:
- no overlapping bookings
- availability by date range
- capacity filtering
- cancel only future booking
- rating allowed after stay
- required fields and data types

Frontend assumes backend enforces final business logic.

### 18.9 Step 9: End-to-end run process

Run in this order:
1. Start backend server
2. Confirm backend database is connected and seeded
3. Start frontend (`npm run dev`)
4. Open app
5. Register -> Login -> Search -> Book -> Confirm -> Manage booking
6. Login as admin -> Manage camps

### 18.10 Step 10: Common issues and quick fixes

Issue: `Network Error` in frontend
- Check backend is running and URL in `.env` is correct.

Issue: `CORS` error in browser console
- Fix backend CORS policy for frontend origin.

Issue: Always redirected to login
- Check login response has valid `token` and `user` object.

Issue: Admin page not opening
- Check login response includes `user.isAdmin = true` for admin user.

Issue: Camp list not rendering
- Check availability endpoint response key (`items`/`data`/`camps`).

Issue: 401/403 for protected APIs
- Check token generation in backend and header acceptance.

### 18.11 Minimal integration checklist (must pass)

- Backend running and reachable
- `.env` has correct `VITE_API_BASE_URL`
- CORS configured
- Endpoint paths match `src/api/services.js`
- Request/response fields match UI usage
- Login returns `token` and `user.isAdmin`
- Protected APIs accept Bearer token
- Booking and admin CRUD work end-to-end

If all above are correct, frontend and backend will behave like one complete application.
