export function unwrapApiData(payload) {
  if (!payload || typeof payload !== 'object') {
    return payload
  }

  if ('data' in payload && payload.data != null) {
    return payload.data
  }

  if ('Data' in payload && payload.Data != null) {
    return payload.Data
  }

  return payload
}
