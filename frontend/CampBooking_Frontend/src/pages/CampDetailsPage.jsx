import { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import { getCampById } from '../services/campService'
import { useToast } from '../hooks/useToast'

function CampDetailsPage() {
  const params = useParams()
  const { error } = useToast()

  const [camp, setCamp] = useState(null)
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    async function loadCamp() {
      if (!params.id) {
        return
      }

      setLoading(true)

      try {
        const data = await getCampById(params.id)
        setCamp(data)
      } catch (err) {
        error(err)
      } finally {
        setLoading(false)
      }
    }

    loadCamp()
  }, [error, params.id])

  if (loading) {
    return (
      <section className="page-shell">
        <p>Loading camp details...</p>
      </section>
    )
  }

  if (!camp) {
    return (
      <section className="page-shell">
        <p className="empty-state">Camp details not found.</p>
      </section>
    )
  }

  return (
    <section className="page-shell">
      <h2>{camp.name}</h2>
      <img src={camp.imageUrl} alt={camp.name} className="camp-detail-image" />
      <div className="summary-card">
        <p>{camp.description}</p>
        <p>
          <strong>Capacity:</strong> {camp.capacity}
        </p>
        <p>
          <strong>Weekday Rate:</strong> ${camp.weekdayRate}
        </p>
        <p>
          <strong>Weekend Rate:</strong> ${camp.weekendRate}
        </p>
        <p>
          <strong>Average Rating:</strong> {camp.averageRating ?? 0}
        </p>
      </div>
      <Link to="/" className="btn ghost">
        Back to Dashboard
      </Link>
    </section>
  )
}

export default CampDetailsPage
