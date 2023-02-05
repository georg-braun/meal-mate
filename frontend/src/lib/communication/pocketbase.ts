
import PocketBase from "pocketbase";
import { writable } from "svelte/store";

export const pb = new PocketBase("http://127.0.0.1:8090");

export const CategoriesCollectionName: string = "categories";
export const ItemsCollectionName: string = "items";
export const ShoppingListsCollectionName: string = "shopping_lists";
export const UserShoppingListsCollectionName: string = "user_shopping_lists";


export const UserShoppingListsConnectionCollectionName: string = "user_shoppinglists_connection";


export const currentUser = writable(pb.authStore.model);

pb.authStore.onChange((auth) => {
    console.log('authStore changed', auth);
    currentUser.set(pb.authStore.model);
});