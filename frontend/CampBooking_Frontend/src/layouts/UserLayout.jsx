import { Outlet } from 'react-router-dom'
import Header from '../components/Header'

function UserLayout() {
  return (
    <div className="app-frame">
      <Header />
      <main className="main-content">
        <Outlet />
      </main>
      <footer className="page-shell">Camp Booking</footer>
    </div>
  )
}

export default UserLayout
