import { createSlice } from '@reduxjs/toolkit'
import { addDays, toIsoDate } from '../../utils/date'

const today = toIsoDate(new Date())

const initialState = {
  filters: {
    checkInDate: today,
    checkOutDate: addDays(today, 1),
    capacity: '',
    pageNumber: 1,
    pageSize: 6,
  },
  camps: [],
  totalPages: 1,
  loading: false,
  hasAppliedSearch: false,
}

const campSlice = createSlice({
  name: 'camp',
  initialState,
  reducers: {
    setCampFilters: (state, action) => {
      state.filters = {
        ...state.filters,
        ...action.payload,
        pageNumber: 1,
      }
    },
    setCampPageNumber: (state, action) => {
      state.filters.pageNumber = action.payload
    },
    setCampLoading: (state, action) => {
      state.loading = action.payload
    },
    setCampSearchApplied: (state, action) => {
      state.hasAppliedSearch = action.payload
      if (!action.payload) {
        state.filters.pageNumber = 1
      }
    },
    setCampSearchResult: (state, action) => {
      state.camps = action.payload.items
      state.totalPages = action.payload.totalPages
    },
  },
})

export const { setCampFilters, setCampPageNumber, setCampLoading, setCampSearchApplied, setCampSearchResult } = campSlice.actions

export default campSlice.reducer
