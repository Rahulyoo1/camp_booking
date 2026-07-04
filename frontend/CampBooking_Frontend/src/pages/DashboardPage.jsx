import { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import CampCard from '../components/CampCard'
import Pagination from '../components/Pagination'
import SearchBar from '../components/SearchBar'
import { getCamps, searchCamps } from '../services/campService'
import { setCampLoading, setCampPageNumber, setCampSearchResult } from '../features/camp/campSlice'
import { useToast } from '../hooks/useToast'

function DashboardPage() {
  const dispatch = useDispatch()
  const { error } = useToast()
  const campState = useSelector((state) => state.camp)

  const [camps, setCamps] = useState(campState.camps)
  const [totalPages, setTotalPages] = useState(campState.totalPages)
  const loading = campState.loading

  useEffect(() => {
    async function loadCamps() {
      dispatch(setCampLoading(true))

      try {
        let items = []
        let resolvedTotalPages = 1

        if (campState.hasAppliedSearch) {
          const data = await searchCamps({
            CheckInDate: campState.filters.checkInDate,
            CheckOutDate: campState.filters.checkOutDate,
            Capacity: campState.filters.capacity ? Number(campState.filters.capacity) : null,
            PageNumber: campState.filters.pageNumber,
            PageSize: campState.filters.pageSize,
          })

          items = data.items || data.data || data.Data || []
          resolvedTotalPages = data.totalPages || data.TotalPages || 1
        } else {
          const data = await getCamps()
          items = Array.isArray(data) ? data : data.items || data.data || data.Data || []
        }

        setCamps(items)
        setTotalPages(resolvedTotalPages)
        dispatch(setCampSearchResult({ items, totalPages: resolvedTotalPages }))
      } catch (err) {
        error(err)
        setCamps([])
        setTotalPages(1)
        dispatch(setCampSearchResult({ items: [], totalPages: 1 }))
      } finally {
        dispatch(setCampLoading(false))
      }
    }

    loadCamps()
  }, [campState.filters, campState.hasAppliedSearch, dispatch, error])

  return (
    <section className="page-shell">
      <div className="hero-banner">
        <h1>Book your next camp in minutes</h1>
        <p>Search by date and capacity, then reserve instantly.</p>
      </div>

      <SearchBar />

      {loading ? <p>Loading camps...</p> : null}

      {!loading && camps.length === 0 ? (
        <p className="empty-state">
          {campState.hasAppliedSearch ? 'No camps found for selected dates.' : 'No camps available right now.'}
        </p>
      ) : null}

      <div className="camp-grid">
        {camps.map((camp) => (
          <CampCard
            key={camp.id || camp.campId || camp.Id}
            camp={camp}
            checkIn={campState.filters.checkInDate}
            checkOut={campState.filters.checkOutDate}
          />
        ))}
      </div>

      {campState.hasAppliedSearch ? (
        <Pagination
          page={campState.filters.pageNumber}
          totalPages={totalPages}
          onPageChange={(nextPage) => dispatch(setCampPageNumber(nextPage))}
        />
      ) : null}
    </section>
  )
}

export default DashboardPage
