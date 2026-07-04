import { createSlice } from '@reduxjs/toolkit'

const initialState = {
  currentBooking: null,
  loading: false,
}

const bookingSlice = createSlice({
  name: 'booking',
  initialState,
  reducers: {
    setBookingLoading: (state, action) => {
      state.loading = action.payload
    },
    setCurrentBooking: (state, action) => {
      state.currentBooking = action.payload
    },
    clearCurrentBooking: (state) => {
      state.currentBooking = null
    },
  },
})

export const { setBookingLoading, setCurrentBooking, clearCurrentBooking } = bookingSlice.actions

export default bookingSlice.reducer
