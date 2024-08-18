import React from "react";
import img from "../assets/img.webp";
import { CartContext } from "../contexts/CartContext"; 

export const SoftwarePage = () => {
  return (
    <section className="mt-[120px] sm:mt-10 max-w-[1100px] mx-auto py-16 justify-content: space-between">
      <div className="container mx-auto ">
        <div className="text-center mb-10">
          <h3 className="text-3xl sm:text-4xl leading-normal font-extrabold tracking-tight text-gray-900">
            Todos Nue<span className="text-gray-400">stros Productos</span>
          </h3>
        </div>
        <div className="grid grid-cols-1 gap-5 md:grid-cols-2 lg:grid-cols-4  max-w-sm mx-auto md:max-w-none md:mx-0">
          <div className="border border-black">
            <img src={img} alt="" />
            <title className="text-black font-bold px-2 py-2">Software-Libre</title>
          </div>
          <div className="border border-black">
            <img src={img} alt="" />
          </div>
          <div className="border border-black">
            <img src={img} alt="" />
          </div>
          <div className="border border-black">
            <img src={img} alt="" />
          </div>
          <div className="border border-black">
            <img src={img} alt="" />
          </div>
          <div className="border border-black">
            <img src={img} alt="" />
          </div>
          <div className="border border-black">
            <img src={img} alt="" />
          </div>
          <div className="border border-black">
            <img src={img} alt="" />
          </div>
        </div>
      </div>
    </section>
  );
};
