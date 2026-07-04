export function toIsoDate(input) {
  const date = new Date(input)
  if (Number.isNaN(date.getTime())) {
    return ''
  }

  const offsetMs = date.getTimezoneOffset() * 60 * 1000
  const localDate = new Date(date.getTime() - offsetMs)
  return localDate.toISOString().slice(0, 10)
}

export function addDays(isoDate, days) {
  const base = new Date(isoDate)
  base.setDate(base.getDate() + days)
  return toIsoDate(base)
}

export function nightsBetween(checkIn, checkOut) {
  const start = new Date(checkIn)
  const end = new Date(checkOut)
  const diffMs = end.getTime() - start.getTime()
  return Math.max(0, Math.ceil(diffMs / (1000 * 60 * 60 * 24)))
}

export function isFutureDate(isoDate) {
  const date = new Date(isoDate)
  const today = new Date()
  today.setHours(0, 0, 0, 0)
  return date.getTime() > today.getTime()
}
