import type { ShoppingListQueryResponse } from './communication/ShoppingListQueryResponse';
import { writable } from "svelte/store";
import type { GetCategoriesDetailsQueryDto } from "./communication/queries/ItemsQuery";

export const itemsStore = writable<GetCategoriesDetailsQueryDto[]>([]);

export const shoppingListStore = writable<ShoppingListQueryResponse>();