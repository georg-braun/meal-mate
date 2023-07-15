import type { ShoppingListQueryResponse } from '../api/queries/ShoppingListQueryResponse';
import { writable } from "svelte/store";
import type { GetCategoriesDetailsQueryDto } from "../api/queries/ItemsQueryResponse";

export const itemsStore = writable<GetCategoriesDetailsQueryDto[]>([]);

export const shoppingListStore = writable<ShoppingListQueryResponse>();