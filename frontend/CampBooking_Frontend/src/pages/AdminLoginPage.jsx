import { Navigate } from 'react-router-dom'

// Admin login is now handled by the shared Login page.
// This file is kept to avoid breaking imports; it simply redirects.
function AdminLoginPage() {
  return <Navigate to="/login" replace />
}

export default AdminLoginPage
