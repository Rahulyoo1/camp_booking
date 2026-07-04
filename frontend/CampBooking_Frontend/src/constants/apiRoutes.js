export const API_ROUTES = {
  auth: {
    register: '/Auth/register',
    login: '/Auth/login',
  },
  camp: {
    base: '/Camp',
    search: '/Camp/Search',
  },
  booking: {
    base: '/Booking',
    cancel: (bookingReference) => `/Booking/Cancel/${bookingReference}`,
    byReference: (bookingReference) => `/Booking/${bookingReference}`,
  },
  rating: {
    base: '/Rating',
  },
  admin: {
    stats: '/Admin/stats',
  },
}
