import { useMemo } from 'react'

export function usePagination(page, totalPages, setPage) {
  return useMemo(
    () => ({
      canGoPrev: page > 1,
      canGoNext: page < totalPages,
      goPrev: () => setPage(Math.max(1, page - 1)),
      goNext: () => setPage(Math.min(totalPages, page + 1)),
    }),
    [page, totalPages, setPage],
  )
}
