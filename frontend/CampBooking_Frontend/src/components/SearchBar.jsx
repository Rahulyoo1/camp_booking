import { useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { toast } from 'react-toastify'
import { setCampFilters, setCampSearchApplied } from '../features/camp/campSlice'
import { addDays, toIsoDate } from '../utils/date'

function SearchBar() {
  const dispatch = useDispatch()
  const current = useSelector((state) => state.camp.filters)

  const [checkIn, setCheckIn] = useState(current.checkInDate)
  const [checkOut, setCheckOut] = useState(current.checkOutDate)
  const [capacity, setCapacity] = useState(current.capacity)

  const handleCheckInChange = (value) => {
    setCheckIn(value)

    if (new Date(checkOut) <= new Date(value)) {
      setCheckOut(addDays(value, 1))
    }
  }

  const onSubmit = (event) => {
    event.preventDefault()

    if (!checkIn || !checkOut) {
      toast.error('Please select both dates.')
      return
    }

    if (new Date(checkOut) <= new Date(checkIn)) {
      toast.error('Check-out must be after check-in.')
      return
    }

    dispatch(
      setCampFilters({
        checkInDate: checkIn,
        checkOutDate: checkOut,
        capacity,
      }),
    )
    dispatch(setCampSearchApplied(true))
  }

  return (
    <form className="search-panel" onSubmit={onSubmit}>
      <label>
        Check-in
        <input
          type="date"
          value={checkIn}
          min={toIsoDate(new Date())}
          onChange={(event) => handleCheckInChange(event.target.value)}
        />
      </label>
      <label>
        Check-out
        <input
          type="date"
          value={checkOut}
          min={addDays(checkIn, 1)}
          onChange={(event) => setCheckOut(event.target.value)}
        />
      </label>
      <label>
        Capacity
        <input
          type="number"
          min="1"
          placeholder="Enter capacity"
          value={capacity}
          onChange={(event) => setCapacity(event.target.value)}
        />
      </label>
      <button type="submit" className="btn">
        Search
      </button>
    </form>
  )
}

export default SearchBar
