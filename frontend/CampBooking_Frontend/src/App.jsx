import { useEffect } from 'react'
import { useDispatch } from 'react-redux'
import { ToastContainer } from 'react-toastify'
import './App.css'
import { clearCredentials } from './features/auth/authSlice'
import AppRoutes from './routes/AppRoutes'

function App() {
  const dispatch = useDispatch()

  useEffect(() => {
    const handleUnauthorized = () => {
      dispatch(clearCredentials())
    }

    window.addEventListener('auth:unauthorized', handleUnauthorized)

    return () => {
      window.removeEventListener('auth:unauthorized', handleUnauthorized)
    }
  }, [dispatch])

  return (
    <>
      <AppRoutes />
      <ToastContainer position="top-right" autoClose={2200} />
    </>
  )
}

export default App
