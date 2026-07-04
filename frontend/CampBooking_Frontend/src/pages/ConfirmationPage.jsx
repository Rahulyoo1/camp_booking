import { useEffect, useState } from 'react'
import { useLocation, useParams } from 'react-router-dom'
import { getBookingByReference } from '../services/bookingService'
import { useToast } from '../hooks/useToast'

function ConfirmationPage() {
  const location = useLocation()
  const params = useParams()
  const { error } = useToast()

  const [booking, setBooking] = useState(location.state?.booking || null)

  useEffect(() => {
    async function loadBooking() {
      if (booking || !params.reference) {
        return
      }

      try {
        const result = await getBookingByReference(params.reference)
        setBooking(result)
      } catch (err) {
        if (err?.response?.status === 403) {
          error({ response: { data: { message: 'You are not authorized to view this booking.' } } })
        } else {
          error(err)
        }
      }
    }

    loadBooking()
  }, [booking, error, params.reference])

  if (!booking) {
    return (
      <section className="page-shell">
        <p className="empty-state">Booking confirmation not found.</p>
      </section>
    )
  }

  return (
    <section className="page-shell">
      <h2>Booking Confirmed</h2>
      <div className="summary-card">
        <p>
          <strong>Reference:</strong> {booking.bookingReference || booking.BookingReference}
        </p>
        <p>
          <strong>Camp:</strong> {booking.campName || booking.CampName}
        </p>
        <p>
          <strong>Stay:</strong> {booking.checkInDate || booking.CheckInDate} to {booking.checkOutDate || booking.CheckOutDate}
        </p>
        <p>
          <strong>Billing Address:</strong> {booking.billingAddress}
        </p>
        <p>
          <strong>Phone:</strong> {booking.phoneNumber}
        </p>
        <p>
          <strong>Status:</strong> {booking.status || booking.Status}
        </p>
        <p>
          <strong>Total Amount:</strong> ${booking.totalAmount || booking.TotalAmount}
        </p>
      </div>
    </section>
  )
}

export default ConfirmationPage
