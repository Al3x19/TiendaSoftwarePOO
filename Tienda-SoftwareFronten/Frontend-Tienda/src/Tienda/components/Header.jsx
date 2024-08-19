import React from "react";
import { Navbar } from "./Navbar";
import { BsBag } from "react-icons/bs";

export const Header = () => {
  return (
    <div className="w-full h-20 px-4 py-4 border-b-[4px] border-solid border-black">
      <header className="flex items-center justify-between mx-auto ">
        <div className="text-gray-500 font-bold text-left text-2xl mx-2">
          <p className="w-full">Tienda Software</p>
        </div>
        <div className="container flex flex-col items-center ml-auto ">
          <Navbar />
        </div>
        <div
          className="cursor-pointer flex relative"
          onClick={() => setIsOpen(!isOpen)}
        >
          <BsBag className="text-2xl" />
          <div className="bg-red-500 absolute -right-2 -bottom-2 text-[12px] w-[18px] h-[18px] text-white rounded-full flex justify-center items-center">
            {itemAmount}
          </div>
        </div>
      </header>
    </div>
  );
};
