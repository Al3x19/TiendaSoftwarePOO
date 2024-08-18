import React from 'react';
import { Link } from 'react-router-dom';

export const LoginPage = () => {
  return (
    <div className="container flex min-h-screen items-center justify-center bg-gray-100">
      <div className="mx-auto w-full max-w-md space-y-8">
        <div className="text-center">
          <h1 className="text-3xl font-bold tracking-tight">Sign in to your account</h1>
          <p className="text-gray-600">
            Don't have an account?{" "}
            <Link to="/register" className="font-medium underline underline-offset-4">
              Register
            </Link>
          </p>
        </div>
        <form className="space-y-6">
          <div>
            <label htmlFor="email" className="block text-sm font-medium text-gray-700">
              Email address
            </label>
            <input
              id="email"
              type="email"
              placeholder="name@example.com"
              required
              className="mt-1 block w-full rounded-md border-gray-500 shadow-sm focus:border-gray-600 focus:ring-gray-500 sm:text-sm"
            />
          </div>
          <div>
            <div className="flex items-center justify-between">
              <label htmlFor="password" className="block text-sm font-medium text-gray-700">
                Password
              </label>
              <Link
                to="/forgot-password"
                className="text-sm font-medium underline underline-offset-4 text-gray-600 hover:text-gray-400"
              >
                Forgot password?
              </Link>
            </div>
            <input
              id="password"
              type="password"
              required
              className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
            />
          </div>
          <button
            type="submit"
            className="w-full py-2 px-4 bg-gray-600 text-white rounded-md font-semibold hover:bg-gray-500 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-offset-2"
          >
            Sign in
          </button>
        </form>
      </div>
    </div>
  );
}


