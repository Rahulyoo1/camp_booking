import axios from 'axios'
import { STORAGE_KEYS } from '../constants/storageKeys'

const apiClient = axios.create({                         // axios instance is created with default configuration
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 15000,
  headers: {
    'Content-Type': 'application/json',
    Accept: 'application/json',
  },
})

apiClient.interceptors.request.use((config) => {                      // request interceptor is added to the axios instance 
  const token = localStorage.getItem(STORAGE_KEYS.token)
  const requestUrl = String(config.url || '').toLowerCase()
  const isAuthRequest = requestUrl.includes('/auth/login') || requestUrl.includes('/auth/register')

  if (token && !isAuthRequest) {
    config.headers.Authorization = `Bearer ${token}`
  }

  return config
})

apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error?.response?.status === 401) {
      localStorage.removeItem(STORAGE_KEYS.token)
      localStorage.removeItem(STORAGE_KEYS.email)
      localStorage.removeItem(STORAGE_KEYS.fullName)
      window.dispatchEvent(new CustomEvent('auth:unauthorized'))
    }

    return Promise.reject(error)
  },
)

export default apiClient
