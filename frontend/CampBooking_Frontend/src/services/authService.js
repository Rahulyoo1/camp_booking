import apiClient from '../api/client'
import { API_ROUTES } from '../constants/apiRoutes'
import { unwrapApiData } from '../utils/apiResponse'

export async function registerUser(payload) {
  const response = await apiClient.post(API_ROUTES.auth.register, payload)
  return unwrapApiData(response.data)
}

export async function loginUser(payload) {
  const response = await apiClient.post(API_ROUTES.auth.login, payload)
  return unwrapApiData(response.data)
}
