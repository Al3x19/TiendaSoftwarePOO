import React from "react";
import img from "../assets/img.webp";
export { Header } from "../components";

export const HomePage = () => {
  return (
    <div className="bg-white w-full  flex flex-col items-center content-center ">
      <div className="w-full h-auto bg-gray-100 px-6 py-4 flex justify-center border-b-2 border-black ">
        <div className="text-center mb-10">
          <h3 className="text-3xl sm:text-4xl leading-normal font-extrabold tracking-tight text-gray-900">
            Tienda <span className="text-gray-400">Software</span>
          </h3>
        </div>
      </div>
      <div className="w-full h-auto bg-gray-100 px-4 py-10 flex justify-center">
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
    </div>
  );
};
