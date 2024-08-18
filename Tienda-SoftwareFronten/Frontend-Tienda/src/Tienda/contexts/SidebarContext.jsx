import React, { useState, createContext } from "react";

//distribuir el estado y las funciones de la barra createContext
export const SidebarContext = createContext();

const sidebarProvider = ({ children }) => {
  conts[(isOpen, setIsopen)] = useState(false);

  const handleCloset = () => {
    setIsopen(false);
  };

  return (
    //<SidebarContext.Provider>: Este es el componente que provee los valores del contexto a los componentes hijos.
    <SidebarContext.Provider value={{ isOpen, setIsopen, handleCloset }}>
      {children}
    </SidebarContext.Provider>
  );
};

export default sidebarProvider;
