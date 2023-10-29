import Axios from 'axios';
import { useNotificationStore } from '@src/stores/notifications'

export const API_URL = import.meta.env.VITE_API_SERVER;

function logError(error: any) {
    const message = error.response?.data?.message || error.message;
    useNotificationStore.addNotification({
        type: 'error',
        title: 'error',
        message
    });

    return Promise.reject(error);
}

export const axios = Axios.create({
    baseURL: API_URL
})

axios.interceptors.request.use((config) => {
    if (config.headers)
        config.headers.Accept = 'application/json';
    return config;
}, logError);

axios.interceptors.response.use(
    (response) => {
        return response.data;
    },
    logError
)