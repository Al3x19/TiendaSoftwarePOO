import { Navigate, Route, Routes } from 'react-router-dom';
//paginas
import {HomePage, LoginPage, SoftwarePage} from "../pages"
// componentes 
import { Navbar, Footer } from "../components"
import React from 'react'

export const TiendaRoutes = () => {
  return (
    <div className="overflow-x-hidden bg-white w-screen h-screen  bg-no-repeat bg-cover">
    <Navbar/>
    <div className="px-6 py-8">
      <div className="container flex justify-between mx-auto">
        <Routes>
          <Route path='/home' element={<HomePage />} />
          <Route path='/software' element={<SoftwarePage />} />
          <Route path='/login' element={<LoginPage />} />
          <Route path='/*' element={<Navigate to={"/home"} />} />
        </Routes>
      </div>
    </div>
    <Footer />
  </div>
  )
}