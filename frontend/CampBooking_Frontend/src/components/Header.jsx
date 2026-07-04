import { Link, NavLink, useNavigate } from 'react-router-dom'
import { useDispatch, useSelector } from 'react-redux'
import { clearCredentials } from '../features/auth/authSlice'
import { ROLES } from '../constants/roles'

function Header() {
  const navigate = useNavigate()
  const dispatch = useDispatch()
  const auth = useSelector((state) => state.auth)

  const isAuthenticated = Boolean(auth.token)
  const isAdmin = auth.role === ROLES.admin

  const handleLogout = () => {
    dispatch(clearCredentials())
    navigate('/')
  }

  return (
    <header className="site-header">
      <Link to="/" className="logo-mark">
        CampOrbit
      </Link>
      <nav className="nav-links">
        <NavLink to="/">Dashboard</NavLink>
        {isAuthenticated && !isAdmin ? <NavLink to="/my-bookings">My Bookings</NavLink> : null}
        {isAuthenticated && !isAdmin ? <NavLink to="/manage-booking">Manage Booking</NavLink> : null}
        {isAuthenticated && !isAdmin ? <NavLink to="/rating">Rating</NavLink> : null}
        {isAdmin ? <NavLink to="/admin">Admin Dashboard</NavLink> : null}
        {!isAuthenticated ? <NavLink to="/login">Login</NavLink> : null}
        {!isAuthenticated ? <NavLink to="/register">Register</NavLink> : null}
      </nav>
      <div className="header-side">
        {isAuthenticated ? <span className="user-pill">{auth.fullName || auth.email}</span> : null}
        {isAuthenticated ? (
          <button type="button" className="btn ghost" onClick={handleLogout}>
            Logout
          </button>
        ) : null}
      </div>
    </header>
  )
}

export default Header
