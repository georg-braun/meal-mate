import type { ShoppingListQueryResponse } from './communication/dtos/ShoppingListQueryResponse';
import { writable } from "svelte/store";
import type { GetCategoriesDetailsQueryDto } from "./communication/dtos/GetCategoriesDetailsQuery";

export const categoriesWithItemsStore = writable<GetCategoriesDetailsQueryDto[]>([]);

export const shoppingListStore = writable<ShoppingListQueryResponse>();