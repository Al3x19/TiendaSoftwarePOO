import { tiendaApi } from "../../../config/api/tiendaApi";

export const getSoftwareList = async (searchTerm = "", page = 1) => {
  try {
    const { data } = await tiendaApi.get(`/softwares?searchTerm=${searchTerm}&page=${page}    
        `);
    return data;
  } catch (error) {
    console.error(error)
    return error.response;
  }
};

export const getSoftware = async (id) => {
  try {
    const { data } = await tiendaApi.get(`/softwares/${id}`); 
    return data;
  } catch (error) {
    console.error("womp womp",error);
    return error.response;
  }
};