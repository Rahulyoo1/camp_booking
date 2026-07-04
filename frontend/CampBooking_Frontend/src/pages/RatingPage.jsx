import { useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { createRating } from '../services/ratingService'
import { setLastSubmittedRating, setRatingLoading } from '../features/rating/ratingSlice'
import { useToast } from '../hooks/useToast'

function RatingPage() {
  const dispatch = useDispatch()
  const { error, success } = useToast()
  const loading = useSelector((state) => state.rating.loading)

  const [form, setForm] = useState({
    bookingId: '',
    stars: 5,
    review: '',
  })

  const onSubmit = async (event) => {
    event.preventDefault()
    dispatch(setRatingLoading(true))

    try {
      const payload = {
        bookingId: Number(form.bookingId),
        stars: Number(form.stars),
        review: form.review,
      }

      const response = await createRating(payload)
      dispatch(setLastSubmittedRating(response))
      success('Rating submitted successfully.')
      setForm({ bookingId: '', stars: 5, review: '' })
    } catch (err) {
      error(err)
    } finally {
      dispatch(setRatingLoading(false))
    }
  }

  return (
    <section className="page-shell narrow">
      <h2>Rate Your Stay</h2>
      <form className="stack-form" onSubmit={onSubmit}>
        <label>
          Booking Id
          <input
            type="number"
            min="1"
            value={form.bookingId}
            onChange={(event) => setForm({ ...form, bookingId: event.target.value })}
            required
          />
        </label>

        <label>
          Stars
          <input
            type="number"
            min="1"
            max="5"
            value={form.stars}
            onChange={(event) => setForm({ ...form, stars: event.target.value })}
            required
          />
        </label>

        <label>
          Review
          <textarea
            rows={4}
            value={form.review}
            onChange={(event) => setForm({ ...form, review: event.target.value })}
          />
        </label>

        <button type="submit" className="btn" disabled={loading}>
          {loading ? 'Submitting...' : 'Submit Rating'}
        </button>
      </form>
    </section>
  )
}

export default RatingPage
