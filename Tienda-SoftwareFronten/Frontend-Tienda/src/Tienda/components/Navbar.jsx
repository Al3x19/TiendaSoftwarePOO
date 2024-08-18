import { useState } from "react";
import { Link } from "react-router-dom";

export const Navbar = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const handleMenuToggle = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <nav className="flex items-center gap-3 font-medium text-primary ">
      <div className="container flex flex-col mx-auto md:flex-row md:items-center md:justify-between border-b-2 border-black">
        <div className="flex items-center justify-between">
          <div>
            <Link
              to="/home"
              className="text-xl font-bold text-black md:text-2xl"
            >
              Tienda-Software
            </Link>
          </div>
          <div>
            <button
              type="button"
              onClick={handleMenuToggle}
              className="block text-xl font-bold text-black hover:text-gray-500"
            >
              <svg viewBox="0 0 24 24" className="w-6 h-6 fill-current">
                <path d="M4 5h16a1 1 0 0 1 0 2H4a1 1 0 1 1 0-2zm0 6h16a1 1 0 0 1 0 2H4a1 1 0 0 1 0-2zm0 6h16a1 1 0 0 1 0 2H4a1 1 0 1 1 0-2z"></path>
              </svg>
            </button>
          </div>
        </div>
        <div
          className={`${
            isMenuOpen ? "flex" : "hidden"
          } flex-col md:flex md:flex-row md:mx-4`}
        >
          <Link
            to="/home"
            className="my-1 text-black hover:text-gray-500 md:mx-4 md:my-0"
          >
            Inicio
          </Link>
          <Link
            to="/software"
            className="my-1 text-black hover:text-gray-500 md:mx-4 md:my-0"
          >
            Software
          </Link>
          <Link
            to="/login"
            className="my-1 text-black hover:text-gray-500 md:mx-4 md:my-0"
          >
            Login
          </Link> <Link
            to="/nosotros"
            className="my-1 text-black hover:text-gray-500 md:mx-4 md:my-0"
          >
            Sobre-Nosotros
          </Link>
        </div>
      </div>
    </nav>
  );
};
