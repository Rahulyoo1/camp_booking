import apiClient from '../api/client'
import { API_ROUTES } from '../constants/apiRoutes'
import { unwrapApiData } from '../utils/apiResponse'

export async function getAdminStats() {
  const response = await apiClient.get(API_ROUTES.admin.stats)
  return unwrapApiData(response.data)
}
