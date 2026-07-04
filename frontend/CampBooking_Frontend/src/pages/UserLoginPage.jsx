import { useState } from 'react'
import { useDispatch } from 'react-redux'
import { Link, Navigate, useNavigate } from 'react-router-dom'
import { loginUser } from '../services/authService'
import { setCredentials } from '../features/auth/authSlice'
import { useSelector } from 'react-redux'
import { ROLES } from '../constants/roles'
import { useToast } from '../hooks/useToast'

function UserLoginPage() {
  const dispatch = useDispatch()
  const navigate = useNavigate()
  const { success, error } = useToast()
  const { token, role: existingRole } = useSelector((state) => state.auth)

  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [loading, setLoading] = useState(false)

  const onSubmit = async (event) => {
    event.preventDefault()

    if (!email || !password) {
      error({ response: { data: { message: 'Email and password are required.' } } })
      return
    }

    setLoading(true)

    try {
      const result = await loginUser({ email, password })

      const role = result.role || result.Role || ROLES.user

      dispatch(
        setCredentials({
          token: result.token || result.Token,
          email: result.email || result.Email,
          fullName: result.fullName || result.FullName,
          role,
        }),
      )

      success('Login successful.')
      navigate(role === ROLES.admin ? '/admin' : '/')
    } catch (err) {
      error(err)
    } finally {
      setLoading(false)
    }
  }

  if (token) {
    return <Navigate to={existingRole === ROLES.admin ? '/admin' : '/'} replace />
  }

  return (
    <section className="page-shell narrow">
      <h2>Login</h2>
      <form className="stack-form" onSubmit={onSubmit}>
        <label>
          Email
          <input type="email" value={email} onChange={(event) => setEmail(event.target.value)} required />
        </label>

        <label>
          Password
          <input
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            required
          />
        </label>

        <button type="submit" className="btn" disabled={loading}>
          {loading ? 'Signing in...' : 'Login'}
        </button>
      </form>

      <p className="auth-footnote">
        New here? <Link to="/register">Create an account</Link>
      </p>
    </section>
  )
}

export default UserLoginPage
