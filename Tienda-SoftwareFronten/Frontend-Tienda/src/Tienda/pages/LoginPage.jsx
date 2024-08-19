import  { useState } from "react";
import { addUser } from "../hooks/useUser";

export const LoginPage = () => {
  const { user, isLoading, addingUser } = addUser();
  const [name, setName] = useState("");
  const [securityCode, setSecurityCode] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    const userData = { name, securityCode };
    addingUser(userData);
  };

  return (
    <div className="container flex min-h-screen items-center justify-center bg-gray-900">
      <div className="mx-auto w-full max-w-md space-y-8">
        <div className="text-center text-white">
          <h1 className="text-3xl font-bold tracking-tight">Crear nuevo usuario</h1>
          <p className="text-gray-400">
            <a href="/loginpublisher" className="font-medium text-gray-200 underline underline-offset-4 hover:text-white">
              Login para desarrolladores
            </a>
          </p>
        </div>
        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label htmlFor="name" className="block text-sm font-medium text-gray-300">
              Name
            </label>
            <input
              id="name"
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              
              required
              className="mt-1 block w-full rounded-md border-gray-700 bg-gray-800 text-white shadow-lg focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
            />
          </div>
          <div>
            <label htmlFor="securityCode" className="block text-sm font-medium text-gray-300">
              Security Code
            </label>
            <input
              id="securityCode"
              type="text"
              value={securityCode}
              onChange={(e) => setSecurityCode(e.target.value)}
             
              required
              className="mt-1 block w-full rounded-md border-gray-700 bg-gray-800 text-white shadow-lg focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
            />
          </div>
          <button
            type="submit"
            className="w-full py-2 px-4 bg-gray-700 text-white rounded-md font-semibold hover:bg-gray-600 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-offset-2 shadow-md"
            disabled={isLoading}
          >
            {isLoading ? "Creating..." : "Create User"}
          </button>
          {user && <p className="text-green-500">User created successfully!</p>}
        </form>
      </div>
    </div>
  );
};
