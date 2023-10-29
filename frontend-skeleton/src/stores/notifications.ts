import { writable } from "svelte/store";

let notificationStore = writable<Notification>();

export type Notification = {
    type: 'info' | 'warning' | 'success' | 'error';
    title: string;
    message?: string;
}

export const useNotificationStore = {
    
    addNotification: (notification: Notification) => {
        notificationStore.set(notification);
    }
}