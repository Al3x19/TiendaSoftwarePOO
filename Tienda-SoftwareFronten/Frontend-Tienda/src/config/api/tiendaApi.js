import axios from "axios";

const API_URL = 'https://localhost:7271/api'

const tiendaApi = axios.create({
    baseURL: API_URL,
    headers: {
        "Content-Type": "application/json"
    }
});

export{
    tiendaApi,
    API_URL
}