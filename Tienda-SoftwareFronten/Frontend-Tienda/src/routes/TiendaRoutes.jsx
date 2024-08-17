import{BrowserRouter as Router, Routes, Route }from "react-router-dom"
//paginas
import {HomePage, LoginPage, SoftwarePage} from "../pages"
// componentes 
import { Header, Container, Navbar  } from "../components"

import React from 'react'

export const TiendaRoutes = () => {
  return (
    <div className="overflow-x-hidden bg-gray-100 w-screen h-screen bg-hero-pattern bg-no-repeat bg-cover">
    <Nav />
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