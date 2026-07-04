export function generateBookingReference() {
  const alphabet = 'ABCDEFGHJKLMNPQRSTUVWXYZ23456789'
  let result = ''

  for (let i = 0; i < 8; i += 1) {
    const index = Math.floor(Math.random() * alphabet.length)
    result += alphabet[index]
  }

  return result
}

export function normalizeCapacity(value) {
  if (!value || value === 'Any') {
    return null
  }

  const parsed = Number(value)
  return Number.isNaN(parsed) ? null : parsed
}
