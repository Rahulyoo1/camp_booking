import { configureStore } from '@reduxjs/toolkit'
import authReducer from '../features/auth/authSlice'
import bookingReducer from '../features/booking/bookingSlice'
import campReducer from '../features/camp/campSlice'
import ratingReducer from '../features/rating/ratingSlice'

export const store = configureStore({
  reducer: {
    auth: authReducer,
    camp: campReducer,
    booking: bookingReducer,
    rating: ratingReducer,
  },
})
