import { createSlice } from '@reduxjs/toolkit'

const initialState = {
  loading: false,
  lastSubmittedRating: null,
}

const ratingSlice = createSlice({
  name: 'rating',
  initialState,
  reducers: {
    setRatingLoading: (state, action) => {
      state.loading = action.payload
    },
    setLastSubmittedRating: (state, action) => {
      state.lastSubmittedRating = action.payload
    },
  },
})

export const { setRatingLoading, setLastSubmittedRating } = ratingSlice.actions

export default ratingSlice.reducer
