import apiClient from '../api/client'
import { API_ROUTES } from '../constants/apiRoutes'
import { unwrapApiData } from '../utils/apiResponse'

export async function createRating(payload) {
  const response = await apiClient.post(API_ROUTES.rating.base, payload)
  return unwrapApiData(response.data)
}
