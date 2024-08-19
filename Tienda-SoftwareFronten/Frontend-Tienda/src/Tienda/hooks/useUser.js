import { useState } from "react";
import { createUser } from "../../shared/actions/User/user";


export const addUser = () => {
  const [User, setUser] = useState();
  const [isloading, setIsLoading] = useState(false);

  const addingUser = async (userData) => {
    setIsLoading(true);
    const result = await createUser(userData);
    setUser(result);
    setIsLoading(false);
  };

  return {
    User,
    isloading,

    addingUser,
  };
};
