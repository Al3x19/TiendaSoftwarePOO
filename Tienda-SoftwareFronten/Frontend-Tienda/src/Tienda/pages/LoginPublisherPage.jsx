import { useState } from "react";
import { addPublisher } from "../hooks/usePublisher";

export const LoginPublisherPage = () => {
  const { publisher, isLoading, addingPublisher } = addPublisher();
  const [name, setName] = useState("");
  const [securityCode, setSecurityCode] = useState("");
  const [description, setDescription] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    const publisherData = { name, securityCode, description };
    addingPublisher(publisherData);
  };

  return (
    <div className="container flex min-h-screen items-center justify-center bg-gray-900">
      <div className="mx-auto w-full max-w-md space-y-8">
        <div className="text-center text-white">
          <h1 className="text-3xl font-bold tracking-tight">Create New Publisher</h1>
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
          <div>
            <label htmlFor="description" className="block text-sm font-medium text-gray-300">
              Description
            </label>
            <input
              id="description"
              type="text"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Enter a description"
              required
              className="mt-1 block w-full rounded-md border-gray-700 bg-gray-800 text-white shadow-lg focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
            />
          </div>
          <button
            type="submit"
            className="w-full py-2 px-4 bg-gray-700 text-white rounded-md font-semibold hover:bg-gray-600 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-offset-2 shadow-md"
            disabled={isLoading}
            
          >
          
            {isLoading ? "Creating..." : "Create Publisher"}
          </button>
          {publisher && <p className="text-green-500">Publisher created successfully!</p>}
        </form>
      </div>
    </div>
  );
};
