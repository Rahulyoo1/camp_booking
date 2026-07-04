import { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import { useLocation, useNavigate, useParams } from 'react-router-dom'
import { createBooking } from '../services/bookingService'
import { getCampById } from '../services/campService'
import { setBookingLoading, setCurrentBooking } from '../features/booking/bookingSlice'
import { useToast } from '../hooks/useToast'

function BookingPage() {
  const dispatch = useDispatch()
  const navigate = useNavigate()
  const params = useParams()
  const location = useLocation()
  const { error, success } = useToast()

  const { checkIn, checkOut } = location.state || {}
  const [camp, setCamp] = useState(location.state?.camp || null)

  const [billingAddress, setBillingAddress] = useState('')
  const [phoneNumber, setPhoneNumber] = useState('')
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    async function loadCamp() {
      if (camp || !params.campId) {
        return
      }

      try {
        const result = await getCampById(params.campId)
        setCamp(result)
      } catch (err) {
        error(err)
      }
    }

    loadCamp()
  }, [camp, error, params.campId])

  if (!camp) {
    return (
      <section className="page-shell">
        <p className="empty-state">Please select a camp from dashboard.</p>
      </section>
    )
  }

  const onSubmit = async (event) => {
    event.preventDefault()

    if (!billingAddress.trim()) {
      error({ response: { data: { message: 'Billing address is required.' } } })
      return
    }

    if (!/^\d{10}$/.test(phoneNumber)) {
      error({ response: { data: { message: 'Cell phone must be 10 digits.' } } })
      return
    }

    setLoading(true)
    dispatch(setBookingLoading(true))

    try {
      const campId = camp.id || camp.campId || camp.Id

      const payload = {
        campId,
        checkInDate: checkIn,
        checkOutDate: checkOut,
        billingAddress,
        phoneNumber,
      }

      const booking = await createBooking(payload)
      dispatch(setCurrentBooking(booking))

      success('Booking created successfully.')
      navigate(`/confirmation/${booking.bookingReference || booking.BookingReference}`, {
        state: { booking },
      })
    } catch (err) {
      error(err)
    } finally {
      setLoading(false)
      dispatch(setBookingLoading(false))
    }
  }

  return (
    <section className="page-shell">
      <h2>Booking Details</h2>
      <div className="summary-card">
        <p>
          <strong>{camp.name}</strong> | {checkIn} to {checkOut}
        </p>
        <p className="muted">Final amount is calculated and returned by backend.</p>
      </div>

      <form className="stack-form" onSubmit={onSubmit}>
        <label>
          Billing Address
          <textarea
            rows={4}
            value={billingAddress}
            onChange={(event) => setBillingAddress(event.target.value)}
            required
          />
        </label>

        <label>
          Cell Phone Number
          <input
            type="tel"
            value={phoneNumber}
            onChange={(event) => setPhoneNumber(event.target.value.replace(/\D/g, '').slice(0, 10))}
            required
          />
        </label>

        <button type="submit" className="btn" disabled={loading}>
          {loading ? 'Confirming...' : 'Confirm Booking'}
        </button>
      </form>
    </section>
  )
}

export default BookingPage
