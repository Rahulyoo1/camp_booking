import { Navigate } from 'react-router-dom'
import { useSelector } from 'react-redux'
import { ROLES } from '../constants/roles'

function ProtectedRoute({ children }) {
  const auth = useSelector((state) => state.auth)

  if (!auth.token || auth.role !== ROLES.admin) {
    return <Navigate to="/admin/login" replace />
  }

  return children
}

export default ProtectedRoute
