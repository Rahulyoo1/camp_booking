import { createSlice } from '@reduxjs/toolkit'
import { STORAGE_KEYS } from '../../constants/storageKeys'

const storedToken = localStorage.getItem(STORAGE_KEYS.token)
const storedEmail = localStorage.getItem(STORAGE_KEYS.email)
const storedFullName = localStorage.getItem(STORAGE_KEYS.fullName)
const storedRole = localStorage.getItem(STORAGE_KEYS.role)

const initialState = {
  token: storedToken || '',
  email: storedEmail || '',
  fullName: storedFullName || '',
  role: storedRole || '',
}

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setCredentials: (state, action) => {
      const { token, email, fullName, role = '' } = action.payload

      state.token = token
      state.email = email
      state.fullName = fullName
      state.role = role

      localStorage.setItem(STORAGE_KEYS.token, token)
      localStorage.setItem(STORAGE_KEYS.email, email)
      localStorage.setItem(STORAGE_KEYS.fullName, fullName)
      localStorage.setItem(STORAGE_KEYS.role, role)
    },
    clearCredentials: (state) => {
      state.token = ''
      state.email = ''
      state.fullName = ''
      state.role = ''

      localStorage.removeItem(STORAGE_KEYS.token)
      localStorage.removeItem(STORAGE_KEYS.email)
      localStorage.removeItem(STORAGE_KEYS.fullName)
      localStorage.removeItem(STORAGE_KEYS.role)
    },
  },
})

export const { setCredentials, clearCredentials } = authSlice.actions

export default authSlice.reducer
