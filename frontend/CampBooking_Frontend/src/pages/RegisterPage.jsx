import { useState } from 'react'
import { Link, Navigate, useNavigate } from 'react-router-dom'
import { useSelector } from 'react-redux'
import { registerUser } from '../services/authService'
import { useToast } from '../hooks/useToast'

function RegisterPage() {
  const navigate = useNavigate()
  const token = useSelector((state) => state.auth.token)
  const { success, error } = useToast()

  const [form, setForm] = useState({
    fullName: '',
    email: '',
    phoneNumber: '',
    password: '',
    confirmPassword: '',
  })
  const [loading, setLoading] = useState(false)

  const onSubmit = async (event) => {
    event.preventDefault()

    if (!form.fullName || !form.email || !form.phoneNumber || !form.password || !form.confirmPassword) {
      error({ response: { data: { message: 'All fields are required.' } } })
      return
    }

    if (!/^\d{10}$/.test(form.phoneNumber)) {
      error({ response: { data: { message: 'Phone number must be 10 digits.' } } })
      return
    }

    if (form.password.length < 6) {
      error({ response: { data: { message: 'Password must be at least 6 characters.' } } })
      return
    }

    if (form.password !== form.confirmPassword) {
      error({ response: { data: { message: 'Passwords do not match.' } } })
      return
    }

    setLoading(true)

    try {
      await registerUser({
        fullName: form.fullName,
        email: form.email,
        phoneNumber: form.phoneNumber,
        password: form.password,
        confirmPassword: form.confirmPassword,
      })

      success('Registration successful. Please login.')
      navigate('/login')
    } catch (err) {
      error(err)
    } finally {
      setLoading(false)
    }
  }

  if (token) {
    return <Navigate to="/" replace />
  }

  return (
    <section className="page-shell narrow">
      <h2>Create Account</h2>
      <form className="stack-form" onSubmit={onSubmit}>
        <label>
          Full Name
          <input
            type="text"
            value={form.fullName}
            onChange={(event) => setForm({ ...form, fullName: event.target.value })}
            required
          />
        </label>

        <label>
          Email
          <input
            type="email"
            value={form.email}
            onChange={(event) => setForm({ ...form, email: event.target.value })}
            required
          />
        </label>

        <label>
          Phone Number
          <input
            type="tel"
            value={form.phoneNumber}
            onChange={(event) =>
              setForm({ ...form, phoneNumber: event.target.value.replace(/\D/g, '').slice(0, 10) })
            }
            required
          />
        </label>

        <label>
          Password
          <input
            type="password"
            value={form.password}
            onChange={(event) => setForm({ ...form, password: event.target.value })}
            required
          />
        </label>

        <label>
          Confirm Password
          <input
            type="password"
            value={form.confirmPassword}
            onChange={(event) => setForm({ ...form, confirmPassword: event.target.value })}
            required
          />
        </label>

        <button type="submit" className="btn" disabled={loading}>
          {loading ? 'Creating...' : 'Register'}
        </button>
      </form>

      <p className="auth-footnote">
        Already registered? <Link to="/login">Login here</Link>
      </p>
    </section>
  )
}

export default RegisterPage
