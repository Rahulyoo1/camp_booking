
import apiClient from '../api/client'
import { API_ROUTES } from '../constants/apiRoutes'
import { unwrapApiData } from '../utils/apiResponse'

export async function getCamps() {
  const response = await apiClient.get(API_ROUTES.camp.base)
  return unwrapApiData(response.data)
}

export async function getCampById(id) {
  const response = await apiClient.get(`${API_ROUTES.camp.base}/${id}`)
  return unwrapApiData(response.data)
}

export async function searchCamps(params) {
  const response = await apiClient.get(API_ROUTES.camp.search, { params })
  return response.data
}

export async function createCamp(payload) {
  const response = await apiClient.post(API_ROUTES.camp.base, payload)
  return unwrapApiData(response.data)
}

export async function updateCamp(payload) {
  const id = payload.id || payload.Id;

  const response = await apiClient.put(
    `${API_ROUTES.camp.base}/${id}`,
    payload
  );

  return unwrapApiData(response.data);
}

export async function deleteCamp(id) {
  const response = await apiClient.delete(`${API_ROUTES.camp.base}/${id}`)
  return unwrapApiData(response.data)
}
