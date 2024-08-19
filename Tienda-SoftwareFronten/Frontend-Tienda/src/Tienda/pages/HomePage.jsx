import React from "react";
export { Header } from "../components";
import { useSoftware } from "../hooks/useSoftware";
import { useEffect, useState } from "react";
import { SoftwareItem } from "../components/SoftwareItem";
import { Loading, Pagination } from "../../shared/components";

export const HomePage = () => {
  const { Software, loadSoftware, isLoading } = useSoftware();

  const [fetching, setFetching] = useState(true);

  useEffect(() => {
    if (fetching) {
      loadSoftware();
      setFetching(false);
    }
  }, [fetching, loadSoftware]);

  return (
    <div className="bg-white w-full flex flex-col items-center content-center">
      <div className="w-full h-auto bg-gray-100 px-6 py-4 flex justify-center border-b-2 border-black">
        <div className="text-center mb-10">
          <h3 className="text-3xl sm:text-4xl leading-normal font-extrabold tracking-tight text-gray-900">
            Tienda <span className="text-gray-400">Software</span>
          </h3>
        </div>
      </div>
      <div className="w-full max-w-6xl bg-white rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 ease-in-out transform hover:scale-105 p-6">
        {isLoading ? (
          <Loading />
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
            {Software?.data?.items?.length ? (
              Software.data.items.map((software) => (
                <SoftwareItem key={software.id} software={software} />
              ))
            ) : (
              <p>No hay publicaciones disponibles</p>
            )}
          </div>
        )}
      </div>
    </div>
  );
};

