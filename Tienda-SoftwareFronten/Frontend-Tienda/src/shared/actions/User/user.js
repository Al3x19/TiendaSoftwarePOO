import { tiendaApi } from "../../../config/api/tiendaApi";

export const createUser = async (userData) => {
  try {
    const { data } = await tiendaApi.post(`/Users`, userData);
    return data;
  } catch (error) {
    console.error(error);
    return error.response;
  }
};