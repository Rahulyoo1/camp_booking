import { useEffect, useState } from 'react'
import { getAdminStats } from '../services/adminService'
import { useToast } from '../hooks/useToast'

function StatCard({ label, value }) {
  return (
    <div className="summary-card" style={{ textAlign: 'center' }}>
      <p className="muted" style={{ marginBottom: '0.4rem' }}>{label}</p>
      <p style={{ fontSize: '2rem', fontWeight: '700', color: 'var(--heading)', margin: 0 }}>{value}</p>
    </div>
  )
}

function AdminDashboardPage() {
  const { error } = useToast()
  const [stats, setStats] = useState(null)
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    async function loadStats() {
      setLoading(true)
      try {
        const data = await getAdminStats()
        setStats(data)
      } catch (err) {
        error(err)
      } finally {
        setLoading(false)
      }
    }
    loadStats()
  }, [error])

  return (
    <section className="page-shell">
      <h2>Admin Dashboard</h2>

      {loading ? <p>Loading statistics...</p> : null}

      {!loading && !stats ? (
        <p className="empty-state">Could not load statistics.</p>
      ) : null}

      {stats ? (
        <div className="camp-grid">
          <StatCard label="Total Camps" value={stats.totalCamps ?? stats.TotalCamps ?? 0} />
          <StatCard label="Total Bookings" value={stats.totalBookings ?? stats.TotalBookings ?? 0} />
          <StatCard label="Active Bookings" value={stats.activeBookings ?? stats.ActiveBookings ?? 0} />
          <StatCard label="Cancelled Bookings" value={stats.cancelledBookings ?? stats.CancelledBookings ?? 0} />
          <StatCard label="Average Rating" value={`${stats.averageRating ?? stats.AverageRating ?? 0} ★`} />
          <StatCard label="Registered Users" value={stats.totalUsers ?? stats.TotalUsers ?? 0} />
        </div>
      ) : null}
    </section>
  )
}

export default AdminDashboardPage

