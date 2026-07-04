import { useCallback, useMemo } from 'react'
import { toast } from 'react-toastify'

export function getErrorMessage(error) {
  const data = error?.response?.data
  const backendMessage = data?.message || data?.Message

  if (backendMessage && typeof backendMessage === 'string') {
    return backendMessage
  }

  if (Array.isArray(data?.errors) && data.errors.length > 0) {
    return String(data.errors[0])
  }

  if (data?.errors && typeof data.errors === 'object') {
    const firstErrorList = Object.values(data.errors).find((value) => Array.isArray(value) && value.length > 0)
    if (firstErrorList) {
      return String(firstErrorList[0])
    }
  }

  if (typeof error?.message === 'string' && error.message.trim()) {
    return error.message
  }

  return 'Something went wrong'
}

export function useToast() {
  const success = useCallback((message) => {
    toast.success(message)
  }, [])

  const error = useCallback((err) => {
    toast.error(getErrorMessage(err))
  }, [])

  const info = useCallback((message) => {
    toast.info(message)
  }, [])

  return useMemo(
    () => ({
      success,
      error,
      info,
    }),
    [error, info, success],
  )
}
