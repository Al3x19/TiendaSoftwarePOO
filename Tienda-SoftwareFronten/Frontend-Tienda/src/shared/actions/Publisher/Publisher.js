import { tiendaApi } from "../../../config/api/tiendaApi";

export const createPublisher = async (PublisherData) => {
  try {
    const { data } = await tiendaApi.post(`/Publishers`, PublisherData);
    return data;
  } catch (error) {
    console.error(error);
    return error.response;
  }
};