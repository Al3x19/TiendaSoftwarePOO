import React from "react";
import { Link } from "react-router-dom";
import img from "../assets/img.webp";
import { FaEye, FaPlus } from "react-icons/fa";

export const SoftwareItem = ({ software }) => {
  return (
    <div
      key={software.id}
      className="relative group max-w-4xl px-10 py-6 mx-auto bg-white rounded-lg shadow-md mb-5 overflow-hidden"
    >
      <img src={software.img} alt={`Producto ${software.name}`} className="w-full h-auto" />
      <h4 className="text-black font-bold px-2 py-2">{software.name}</h4>
      <p className="text-gray-700 px-2 pb-2">{software.description}</p>

      {/* Overlay for icons */}
      <div className="absolute inset-0 flex items-center justify-center bg-black bg-opacity-50 opacity-0 group-hover:opacity-100 transition-opacity duration-300">
        <div className="flex flex-col space-y-4">
          <Link
            to={`/softwareDescription/${software.id}`}
            className="text-white bg-gray-800 p-3 rounded-full hover:bg-gray-600 transition-colors"
          >
            <FaEye className="w-6 h-6" />
          </Link>
          <button
            onClick={() => console.log(`Producto ${software.name} agregado al carrito`)}
            className="text-white bg-gray-800 p-3 rounded-full hover:bg-gray-600 transition-colors"
          >
            <FaPlus className="w-6 h-6" />
          </button>
        </div>
      </div>
    </div>
  );
};
