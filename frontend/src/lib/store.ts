import type { ShoppingListQueryResponse } from './communication/api/queries/ShoppingListQueryResponse';
import { writable } from "svelte/store";
import type { GetCategoriesDetailsQueryDto } from "./communication/api/queries/ItemsQueryResponse";

export const itemsStore = writable<GetCategoriesDetailsQueryDto[]>([]);

export const shoppingListStore = writable<ShoppingListQueryResponse>();