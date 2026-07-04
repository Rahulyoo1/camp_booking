import { Link } from 'react-router-dom'
import { useSelector } from 'react-redux'
import { ROLES } from '../constants/roles'

function CampCard({ camp, checkIn, checkOut }) {
  const campId = camp.id || camp.campId || camp.Id
  const role = useSelector((state) => state.auth.role)
  const isAdmin = role === ROLES.admin

  return (
    <article className="camp-card">
      <img
        src={camp.imageUrl || camp.ImageUrl}
        alt={camp.name || camp.Name}
        onError={(event) => {
          event.currentTarget.src = 'https://images.unsplash.com/photo-1523987355523-c7b5b0723c6a?auto=format&fit=crop&w=900&q=80'
        }}
      />
      <div className="camp-content">
        <h3>{camp.name || camp.Name}</h3>
        <p className="muted">Capacity: {camp.capacity || camp.Capacity}</p>
        <p className="price">Weekday Rate: ${camp.weekdayRate ?? camp.WeekdayRate}</p>
        <p className="price">Weekend Rate: ${camp.weekendRate ?? camp.WeekendRate}</p>
        <p className="rating-line">Average Rating: {camp.averageRating ?? camp.AverageRating ?? 0}</p>
        <p className="muted">{camp.description || camp.Description}</p>
        <Link to={`/camp/${campId}`} className="btn ghost">
          View Details
        </Link>
        {!isAdmin ? (
          <Link to={`/book/${campId}`} state={{ camp, checkIn, checkOut }} className="btn">
            Book Camp
          </Link>
        ) : null}
      </div>
    </article>
  )
}

export default CampCard
