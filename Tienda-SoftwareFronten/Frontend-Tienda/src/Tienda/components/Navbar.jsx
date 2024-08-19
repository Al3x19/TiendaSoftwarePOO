import { useState } from "react";
import { Link } from "react-router-dom";
import { SiPaloaltosoftware } from "react-icons/si";
import { FaShoppingCart } from "react-icons/fa";

export const Navbar = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const handleMenuToggle = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <nav className="px-6 py-4 bg-gray-100 shadow-lg">
      <div className="container flex flex-col mx-auto md:flex-row md:items-center md:justify-between">
        <div className="flex items-center justify-between w-full md:w-auto">
          <Link
            to="/home"
            className="flex items-center text-xl font-bold text-gray-800 md:text-2xl space-x-2"
          >
            <SiPaloaltosoftware />
            <span>Software Store</span>
          </Link>
          <button
            type="button"
            onClick={handleMenuToggle}
            className="block text-gray-800 hover:text-gray-600 md:hidden"
          >
            <svg viewBox="0 0 24 24" className="w-6 h-6 fill-current">
              <path d="M4 5h16a1 1 0 0 1 0 2H4a1 1 0 1 1 0-2zm0 6h16a1 1 0 0 1 0 2H4a1 1 0 1 1 0-2zm0 6h16a1 1 0 0 1 0 2H4a1 1 0 1 1 0-2z"></path>
            </svg>
          </button>
        </div>
        <div
          className={`${
            isMenuOpen ? "flex" : "hidden"
          } flex-col md:flex md:flex-row md:mx-4`}
        >
          <Link
            to="/home"
            className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
          >
            Inicio
          </Link>
          <Link
            to="/software"
            className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
          >
            Software
          </Link>
          <Link
            to="/login"
            className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
          >
            Login 
          </Link>
          
          <Link
            to="./cartitem.jsx"
            className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0 flex items-center"
          >
            <FaShoppingCart className="w-5 h-5" />
          </Link>
        </div>
      </div>
    </nav>
  );
};
