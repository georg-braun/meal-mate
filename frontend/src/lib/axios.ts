import Axios, { AxiosRequestConfig } from 'axios';

export const API_URL = import.meta.env.VITE_API_SERVER;


function authRequestInterceptor(config: AxiosRequestConfig) {
  config.headers.Accept = 'application/json';
  return config;
}

export const axios = Axios.create({
  baseURL: API_URL,
});

axios.interceptors.request.use(authRequestInterceptor);
axios.interceptors.response.use(
  (response) => {
    return response.data;
  },
  (error) => {
    const message = error.response?.data?.message || error.message;
    console.log(message);

    return Promise.reject(error);
  }
);