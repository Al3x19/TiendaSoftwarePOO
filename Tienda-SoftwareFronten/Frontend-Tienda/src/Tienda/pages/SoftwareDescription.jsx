import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useSoftware } from "../hooks/useSoftware";

export const SoftwareDescription = () => {
  const { id } = useParams();
  const { Software, loadSingleSoftware, isLoading } = useSoftware();
  const [loaded, setLoaded] = useState(false); // State to track if data has been loaded
  const [fetchAgain, setFetchAgain] = useState(false); // State to trigger re-fetch

  useEffect(() => {
    if (!loaded) {
      // Load software data after the initial render
      loadSingleSoftware(id);
       setLoaded(true); // Ensure setLoaded is called after loadSingleSoftware
    }
  }, [id, loadSingleSoftware, loaded, fetchAgain]);

 


  if (isLoading && !loaded) return <p>Loading...</p>;

  if (!Software) return <p>Software not found</p>;

  return (
    <main className="flex flex-col items-center justify-center p-8 md:flex-row md:justify-between md:p-16">
      <div className="w-full max-w-sm">
        <img
          src={Software.data.img}
          alt={Software.name || "Product Image"}
          className="w-full h-auto"
          width="400"
          height="400"
          style={{ aspectRatio: "400/400", objectFit: "cover" }}
        />
      </div>
      <div className="w-full max-w-lg space-y-4">
        <h1 className="text-3xl font-bold">{Software.data.name}</h1>
        <p className="text-2xl text-red-600">{Software.data.price}</p>
        <p className="text-gray-700">{Software.data.description}</p>
      </div>

    </main>
  );
};
