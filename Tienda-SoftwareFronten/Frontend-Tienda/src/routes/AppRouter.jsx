import { Route, Routes } from "react-router-dom"
import { TiendaRoutes } from "../Tienda/routes"

export const AppRouter = () => {
  return (
    <Routes>
        <Route path="*" element={<TiendaRoutes />} />
    </Routes>
  )
}

