import { NavLink, Outlet } from 'react-router-dom'
import Header from '../components/Header'

function AdminLayout() {
  return (
    <div className="app-frame">
      <Header />
      <main className="main-content">
        <section className="page-shell admin-layout">
          <aside className="admin-sidebar">
            <NavLink to="/admin" end>Dashboard</NavLink>
            <NavLink to="/admin/manage-camps">Manage Camps</NavLink>
          </aside>
          <div className="admin-main">
            <Outlet />
          </div>
        </section>
      </main>
    </div>
  )
}

export default AdminLayout
