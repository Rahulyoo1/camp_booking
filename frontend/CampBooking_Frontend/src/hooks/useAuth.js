import { useMemo } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { clearCredentials } from '../features/auth/authSlice'

export function useAuth() {
  const dispatch = useDispatch()
  const auth = useSelector((state) => state.auth)

  const isAuthenticated = Boolean(auth.token)
  const isAdmin = auth.role === 'Admin'

  return useMemo(
    () => ({
      ...auth,
      isAuthenticated,
      isAdmin,
      logout: () => dispatch(clearCredentials()),
    }),
    [auth, dispatch, isAuthenticated, isAdmin],
  )
}
