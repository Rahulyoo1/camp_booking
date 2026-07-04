import { useCallback, useEffect, useMemo, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { cancelBooking, getMyBookings } from '../services/bookingService'
import { useToast } from '../hooks/useToast'

function normalizeStatus(value) {
  if (typeof value === 'number') {
    return value === 2 ? 'Cancelled' : 'Confirmed'
  }

  return String(value || 'Confirmed').toLowerCase().includes('cancel') ? 'Cancelled' : 'Confirmed'
}

function formatDisplayDate(value) {
  const date = new Date(value)
  if (Number.isNaN(date.getTime())) {
    return value || '-'
  }

  return date.toLocaleDateString('en-GB', {
    day: '2-digit',
    month: 'short',
  })
}

function toDateOnly(value) {
  const date = new Date(value)
  if (Number.isNaN(date.getTime())) {
    return null
  }

  date.setHours(0, 0, 0, 0)
  return date
}

function mapBooking(raw) {
  return {
    bookingId: raw.bookingId || raw.BookingId,
    bookingReference: raw.bookingReference || raw.BookingReference,
    campName: raw.campName || raw.CampName,
    checkInDate: raw.checkInDate || raw.CheckInDate,
    checkOutDate: raw.checkOutDate || raw.CheckOutDate,
    totalAmount: raw.totalAmount || raw.TotalAmount,
    status: normalizeStatus(raw.status || raw.Status),
  }
}

function MyBookingsPage() {
  const navigate = useNavigate()
  const { error, success } = useToast()
  const [loading, setLoading] = useState(false)
  const [bookings, setBookings] = useState([])

  const today = useMemo(() => {
    const now = new Date()
    now.setHours(0, 0, 0, 0)
    return now
  }, [])

  const loadBookings = useCallback(async () => {
    setLoading(true)

    try {
      const data = await getMyBookings()
      const items = Array.isArray(data) ? data : data?.items || data?.data || data?.Data || []
      setBookings(items.map(mapBooking))
    } catch (err) {
      error(err)
      setBookings([])
    } finally {
      setLoading(false)
    }
  }, [error])

  useEffect(() => {
    const timerId = setTimeout(() => {
      void loadBookings()
    }, 0)

    return () => clearTimeout(timerId)
  }, [loadBookings])

  const onCancel = async (bookingReference) => {
    if (!window.confirm('Are you sure you want to cancel this booking?')) {
      return
    }
    try {
      await cancelBooking(bookingReference)
      success('Booking cancelled successfully.')
      setBookings((prev) =>
        prev.map((b) =>
          b.bookingReference === bookingReference ? { ...b, status: 'Cancelled' } : b
        )
      )
    } catch (err) {
      error(err)
    }
  }

  return (
    <section className="page-shell">
      <h2>My Bookings</h2>

      {loading ? <p>Loading bookings...</p> : null}

      {!loading && bookings.length === 0 ? (
        <p className="empty-state">No bookings found.</p>
      ) : null}

      <div className="camp-grid">
        {bookings.map((booking) => {
          const checkIn = toDateOnly(booking.checkInDate)
          const checkOut = toDateOnly(booking.checkOutDate)
          const isCancelled = booking.status === 'Cancelled'
          const isFutureBooking = Boolean(checkIn && checkIn > today)
          const isCompletedStay = Boolean(checkOut && checkOut < today)
          const canCancel = booking.status === 'Confirmed' && isFutureBooking

          return (
            <article className="summary-card" key={booking.bookingId || booking.bookingReference}>
              <p>
                <strong>{booking.campName || 'Camp Booking'}</strong>
              </p>
              <p>Booking Ref : {booking.bookingReference}</p>
              <p>
                {formatDisplayDate(booking.checkInDate)} - {formatDisplayDate(booking.checkOutDate)}
              </p>
              <p>Total Amount : ₹{booking.totalAmount ?? '-'}</p>
              <p>Status : {booking.status}</p>

              <div className="action-row">
                <button
                  type="button"
                  className="btn ghost"
                  onClick={() => navigate(`/confirmation/${booking.bookingReference}`)}
                >
                  View Details
                </button>

                <button
                  type="button"
                  className="btn warning"
                  disabled={!canCancel || isCancelled}
                  onClick={() => onCancel(booking.bookingReference)}
                >
                  Cancel
                </button>

                {isCompletedStay ? (
                  <button
                    type="button"
                    className="btn"
                    onClick={() => navigate('/rating', { state: { bookingId: booking.bookingId } })}
                  >
                    Rate
                  </button>
                ) : null}
              </div>
            </article>
          )
        })}
      </div>
    </section>
  )
}

export default MyBookingsPage
