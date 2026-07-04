import { useCallback, useEffect, useState } from 'react'
import { createCamp, deleteCamp, getCamps, updateCamp } from '../services/campService'
import { useToast } from '../hooks/useToast'

const emptyForm = {
  id: null,
  name: '',
  location: '',
  description: '',
  capacity: '',
  weekdayRate: '',
  weekendRate: '',
  imageUrl: '',
}

function AdminCampsPage() {
  const { error, success } = useToast()
  const [camps, setCamps] = useState([])
  const [form, setForm] = useState(emptyForm)
  const [editingId, setEditingId] = useState(null)

  const loadCamps = useCallback(async () => {
    try {
      const data = await getCamps()
      setCamps(data.items || data || [])
    } catch (err) {
      error(err)
    }
  }, [error])

  useEffect(() => {
    const timerId = setTimeout(() => {
      void loadCamps()
    }, 0)

    return () => clearTimeout(timerId)
  }, [loadCamps])

  const onSubmit = async (event) => {
    event.preventDefault()

    const payload = {
      ...form,
      capacity: Number(form.capacity),
      weekdayRate: Number(form.weekdayRate),
      weekendRate: Number(form.weekendRate),
    }

const hasEmpty =
  !payload.name ||
  !payload.location ||
  !payload.description ||
  !payload.imageUrl ||
  payload.capacity <= 0 ||
  payload.weekdayRate <= 0 ||
  payload.weekendRate <= 0;


   if (hasEmpty) {
  error({
    response: {
      data: {
        message: 'All camp fields are mandatory and must be valid.'
      }
    }
  });
  return;
}
    try {
      if (editingId) {
        await updateCamp({ ...payload, id: editingId })
        success('Camp updated.')
      } else {
        await createCamp(payload)
        success('Camp added.')
      }

      setForm(emptyForm)
      setEditingId(null)
      loadCamps()
    } catch (err) {
      error(err)
    }
  }

  const editCamp = (camp) => {
    setEditingId(camp.id)
    setForm({
      id: camp.id,
      name: camp.name,
      location: camp.location,
      description: camp.description,
      capacity: camp.capacity,
      weekdayRate: camp.weekdayRate,
      weekendRate: camp.weekendRate,
      imageUrl: camp.imageUrl,
    })
  }

  const removeCamp = async (campId) => {
    try {
      await deleteCamp(campId)
      success('Camp deleted.')
      loadCamps()
    } catch (err) {
      error(err)
    }
  }

  return (
    <section className="page-shell">
      <h2>Manage Camps</h2>

      <form className="stack-form" onSubmit={onSubmit}>
        <div className="form-grid">
          <input
            type="text"
            placeholder="Camp Name"
            value={form.name}
            onChange={(event) => setForm({ ...form, name: event.target.value })}
          />
           <input
    type="text"
    name="location"
    value={form.location}
    onChange={(event) => setForm({ ...form, location: event.target.value })}
    placeholder="Camp Location"
  />
          <input
            type="text"
            placeholder="Capacity"
            value={form.capacity}
            onChange={(event) => setForm({ ...form, capacity: event.target.value })}
          />
          <input
            type="number"
            placeholder="Weekday Rate"
            value={form.weekdayRate}
            onChange={(event) => setForm({ ...form, weekdayRate: event.target.value })}
          />
          <input
            type="number"
            placeholder="Weekend Rate"
            value={form.weekendRate}
            onChange={(event) => setForm({ ...form, weekendRate: event.target.value })}
          />
          <input
            type="text"
            placeholder="Image URL"
            value={form.imageUrl}
            onChange={(event) => setForm({ ...form, imageUrl: event.target.value })}
          />
        </div>

        <textarea
          rows={4}
          placeholder="Description"
          value={form.description}
          onChange={(event) => setForm({ ...form, description: event.target.value })}
        />

        <div className="action-row">
          <button type="submit" className="btn">
            {editingId ? 'Update Camp' : 'Add Camp'}
          </button>
          {editingId ? (
            <button
              type="button"
              className="btn ghost"
              onClick={() => {
                setEditingId(null)
                setForm(emptyForm)
              }}
            >
              Cancel Edit
            </button>
          ) : null}
        </div>
      </form>

      <div className="table-wrap">
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Capacity</th>
              <th>Weekday</th>
              <th>Weekend</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {camps.map((camp) => (
              <tr key={camp.id}>
                <td>{camp.name}</td>
                <td>{camp.capacity}</td>
                <td>${camp.weekdayRate}</td>
                <td>${camp.weekendRate}</td>
                <td>
                  <div className="table-actions">
                    <button type="button" className="btn ghost" onClick={() => editCamp(camp)}>
                      Update
                    </button>
                    <button type="button" className="btn warning" onClick={() => removeCamp(camp.id)}>
                      Delete
                    </button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  )
}

export default AdminCampsPage
