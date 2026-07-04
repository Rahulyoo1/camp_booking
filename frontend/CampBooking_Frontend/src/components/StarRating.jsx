function StarRating({ value, onChange, readOnly = false }) {
  const stars = [1, 2, 3, 4, 5]

  return (
    <div className="star-rating" role="radiogroup" aria-label="Star Rating">
      {stars.map((star) => (
        <button
          key={star}
          type="button"
          className={star <= value ? 'active' : ''}
          onClick={() => onChange(star)}
          disabled={readOnly}
          aria-label={`Rate ${star}`}
        >
          ★
        </button>
      ))}
    </div>
  )
}

export default StarRating
