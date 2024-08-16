import React from "react";
import img from "./assets/img.webp";

function App() {
  return (
    <div className="bg-white w-full  flex flex-col items-center content-center ">
      {/* Inicio del Header */},
      <div className="w-full h-20 px-4 py-4 border-b-[4px] border-solid border-black">
        <header className="flex items-center justify-between mx-auto ">
          <div className="text-gray-500 font-bold text-left text-2xl mx-2">
            <p className="w-full">Tienda-Software</p>
          </div>
          <div className="container flex flex-col items-center ml-auto ">
            <nav className="space-y-10 ml-auto hover:bg-white">
              <ul>
                <li>
                  <a href="#" className="px-2 ">
                    Software free
                  </a>
                  <a href="#" className="px-2 ">
                    contactenos
                  </a>
                  <a href="#" className="px-2 ">
                    messi
                  </a>
                </li>
              </ul>
            </nav>
          </div>
        </header>
      </div>
      {/* Fin del Header */},{/*contenido */}
      <div className="w-full h-auto bg-gray-100 px-4 py-10 flex justify-center">
        <div className="max-w-6xl grid grid-cols-1 md:grid-cols-3 gap-8">
          
        </div>
        <div className="max-w-6xl grid grid-cols-1 md:grid-cols-3 gap-8">
          <div className="bg-white rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 ease-in-out transform hover:scale-105 p-6">
            <img
              className="w-full h-48 object-cover rounded-t-lg"
              src={img}
              alt="Card 1"
            />
            <div className="p-4">
              <h3 className="text-xl font-semibold mb-2">Card Title 1</h3>
              <p className="text-gray-600">
                Lorem, ipsum dolor sit amet consectetur adipisicing elit.
                Perspiciatis, molestias veniam. Nesciunt praesentium
                exercitationem suscipit impedit saepe facere, placeat id
                possimus libero. Nobis alias hic ea aut numquam ratione esse!
              </p>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 ease-in-out transform hover:scale-105 p-6">
            <img
              className="w-full h-48 object-cover rounded-t-lg"
              src={img}
              alt="Card 2"
            />
            <div className="p-4">
              <h3 className="text-xl font-semibold mb-2">Card Title 2</h3>
              <p className="text-gray-600">
                Lorem, ipsum dolor sit amet consectetur adipisicing elit.
                Perspiciatis, molestias veniam. Nesciunt praesentium
                exercitationem suscipit impedit saepe facere, placeat id
                possimus libero. Nobis alias hic ea aut numquam ratione esse!
              </p>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 ease-in-out transform hover:scale-105 p-6">
            <img
              className="w-full h-48 object-cover rounded-t-lg"
              src={img}
              alt="Card 3"
            />
            <div className="p-4">
              <h3 className="text-xl font-semibold mb-2">Card Title 3</h3>
              <p className="text-gray-600">
                Lorem, ipsum dolor sit amet consectetur adipisicing elit.
                Perspiciatis, molestias veniam. Nesciunt praesentium
                exercitationem suscipit impedit saepe facere, placeat id
                possimus libero. Nobis alias hic ea aut numquam ratione esse!
              </p>
            </div>
          </div>
        </div>
      </div>
      {/*Inicio del footer */}
      <div>
        <footer className="px-10 py-10 bg-white">
          <div className="container flex flex-col items-center justify-between mx-auto md:flex-row">
            <a
              className="text-xl font-bold text-gray-500 hover:text-black "
              href=""
            >
              Tienda Software
            </a>
            `
            <p className="mt-2 text-sm text-gray-400 md:mt-0">
              Todos los derechos reservados 2024
            </p>
            `
          </div>
        </footer>
      </div>
      {/*Fin del footer */}
    </div>
  );
}

export default App;
