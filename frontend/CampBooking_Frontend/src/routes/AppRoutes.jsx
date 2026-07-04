import { Navigate, Outlet, Route, Routes } from 'react-router-dom'
import { useSelector } from 'react-redux'
import AdminLayout from '../layouts/AdminLayout'
import UserLayout from '../layouts/UserLayout'
import AdminCampsPage from '../pages/AdminCampsPage'
import AdminDashboardPage from '../pages/AdminDashboardPage'
import AdminLoginPage from '../pages/AdminLoginPage'
import BookingPage from '../pages/BookingPage'
import CampDetailsPage from '../pages/CampDetailsPage'
import ConfirmationPage from '../pages/ConfirmationPage'
import DashboardPage from '../pages/DashboardPage'
import ManageBookingPage from '../pages/ManageBookingPage'
import MyBookingsPage from '../pages/MyBookingsPage'
import NotFoundPage from '../pages/NotFoundPage'
import RatingPage from '../pages/RatingPage'
import RegisterPage from '../pages/RegisterPage'
import UserLoginPage from '../pages/UserLoginPage'
import { ROLES } from '../constants/roles'

function UserRouteGuard() {
  const token = useSelector((state) => state.auth.token)

  if (!token) {
    return <Navigate to="/login" replace />
  }

  return <Outlet />
}

function UserOnlyRouteGuard() {
  const { token, role } = useSelector((state) => state.auth)

  if (!token) {
    return <Navigate to="/login" replace />
  }

  if (role === ROLES.admin) {
    return <Navigate to="/admin" replace />
  }

  return <Outlet />
}

function AdminRouteGuard() {
  const { token, role } = useSelector((state) => state.auth)

  if (!token) {
    return <Navigate to="/login" replace />
  }

  if (role !== ROLES.admin) {
    return <Navigate to="/" replace />
  }

  return <Outlet />
}

function AppRoutes() {
  return (
    <Routes>
      <Route element={<UserLayout />}>
        <Route path="/" element={<DashboardPage />} />
        <Route path="/camp/:id" element={<CampDetailsPage />} />
        <Route path="/login" element={<UserLoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        <Route element={<UserOnlyRouteGuard />}>
          <Route path="/book/:campId" element={<BookingPage />} />
          <Route path="/confirmation/:reference" element={<ConfirmationPage />} />
          <Route path="/my-bookings" element={<MyBookingsPage />} />
          <Route path="/manage-booking" element={<ManageBookingPage />} />
          <Route path="/rating" element={<RatingPage />} />
        </Route>
      </Route>

      <Route path="/admin" element={<AdminLayout />}>
        <Route element={<AdminRouteGuard />}>
          <Route index element={<AdminDashboardPage />} />
          <Route path="manage-camps" element={<AdminCampsPage />} />
        </Route>
      </Route>

      <Route path="/not-found" element={<NotFoundPage />} />
      <Route path="*" element={<Navigate to="/not-found" replace />} />
    </Routes>
  )
}

export default AppRoutes
