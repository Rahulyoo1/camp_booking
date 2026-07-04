# Camp Booking Frontend

React frontend for the Camp Booking assignment.

## Stack
- React + Vite
- React Router
- Redux Toolkit
- Axios
- React Toastify

## Run
1. Install dependencies:
   npm install
2. Configure backend URL (optional, default shown):
   - Create `.env`
   - Add: `VITE_API_BASE_URL=http://localhost:5000/api`
3. Start app:
   npm run dev

## Features implemented
- Dashboard with search filters (check-in, check-out, capacity)
- Available camps listing with pagination
- Booking screen with total amount calculation
- Booking confirmation screen with reference display
- Manage booking by reference
- Cancel booking (future bookings)
- Star rating update (after stay date)
- Admin login
- Admin camp CRUD page
- Responsive UI and toast notifications

## Expected backend endpoints
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

If your backend routes differ, update calls in `src/api/services.js`.
