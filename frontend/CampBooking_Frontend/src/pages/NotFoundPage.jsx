import { Link } from 'react-router-dom'

function NotFoundPage() {
  return (
    <section className="page-shell">
      <h2>Page not found</h2>
      <p className="empty-state">The page you requested does not exist.</p>
      <Link to="/" className="btn">
        Back to Dashboard
      </Link>
    </section>
  )
}

export default NotFoundPage
