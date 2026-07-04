import apiClient from '../api/client'
import { API_ROUTES } from '../constants/apiRoutes'
import { unwrapApiData } from '../utils/apiResponse'

export async function createBooking(payload) {
  const response = await apiClient.post(API_ROUTES.booking.base, payload)
  return unwrapApiData(response.data)
}

export async function getBookingByReference(bookingReference) {
  const response = await apiClient.get(API_ROUTES.booking.byReference(bookingReference))
  return unwrapApiData(response.data)
}

export async function cancelBooking(bookingReference) {
  const response = await apiClient.put(API_ROUTES.booking.cancel(bookingReference))
  return unwrapApiData(response.data)
}

export async function getMyBookings() {
  const response = await apiClient.get('/Booking/MyBookings')
  return unwrapApiData(response.data)
}
