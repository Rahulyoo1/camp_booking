import { useState } from 'react'
import StarRating from '../components/StarRating'
import { cancelBooking, getBookingByReference } from '../services/bookingService'
import { createRating } from '../services/ratingService'
import { useToast } from '../hooks/useToast'

function ManageBookingPage() {
  const { error, success } = useToast()
  const [reference, setReference] = useState('')
  const [booking, setBooking] = useState(null)
  const [loading, setLoading] = useState(false)
  const [rating, setRating] = useState(0)
  const [review, setReview] = useState('')

  const searchBooking = async (event) => {
    event.preventDefault()

    setLoading(true)

    try {
      const result = await getBookingByReference(reference.trim())
      setBooking(result)
      setRating(result.stars || result.Stars || 0)
      success('Booking found.')
    } catch (err) {
      if (err?.response?.status === 403) {
        error({ response: { data: { message: 'You are not authorized to view this booking.' } } })
      } else {
        error(err)
      }
      setBooking(null)
    } finally {
      setLoading(false)
    }
  }

  const handleCancel = async () => {
    if (!booking) {
      return
    }

    try {
      await cancelBooking(booking.bookingReference || booking.BookingReference)
      setBooking({ ...booking, status: 'Cancelled' })
      success('Booking cancelled successfully.')
    } catch (err) {
      error(err)
    }
  }

  const submitRating = async () => {
    if (!booking) {
      return
    }

    try {
      await createRating({
        bookingId: booking.bookingId || booking.BookingId,
        stars: rating,
        review,
      })
      success('Rating saved.')
    } catch (err) {
      error(err)
    }
  }

  return (
    <section className="page-shell">
      <h2>Manage Booking</h2>

      <form className="search-by-reference" onSubmit={searchBooking}>
        <input
          type="text"
          placeholder="Booking Reference"
          value={reference}
          onChange={(event) => setReference(event.target.value.toUpperCase())}
          maxLength={8}
        />
        <button type="submit" className="btn" disabled={loading}>
          {loading ? 'Searching...' : 'Find Booking'}
        </button>
      </form>

      {booking ? (
        <div className="summary-card">
          <p>
            <strong>Camp:</strong> {booking.camp?.name || booking.campName}
          </p>
          <p>
            <strong>Dates:</strong> {booking.checkInDate || booking.CheckInDate} to {booking.checkOutDate || booking.CheckOutDate}
          </p>
          <p>
            <strong>Status:</strong> {booking.status || booking.Status || 'Confirmed'}
          </p>

          <div className="action-row">
            <button type="button" className="btn warning" onClick={handleCancel}>
              Cancel Booking
            </button>
          </div>

          <div className="rating-block">
            <p>Rate your stay</p>
            <StarRating value={rating} onChange={setRating} />
            <textarea
              rows={3}
              placeholder="Review"
              value={review}
              onChange={(event) => setReview(event.target.value)}
            />
            <button type="button" className="btn" onClick={submitRating} disabled={!rating}>
              Save Rating
            </button>
          </div>
        </div>
      ) : null}
    </section>
  )
}

export default ManageBookingPage
