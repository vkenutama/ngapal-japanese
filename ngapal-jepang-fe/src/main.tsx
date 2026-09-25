import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import DeckScreenPage from './pages/DeckScreenPage'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <DeckScreenPage/>
  </StrictMode>,
)
