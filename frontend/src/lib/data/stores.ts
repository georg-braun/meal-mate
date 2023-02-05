import { writable, type Writable } from "svelte/store";
import type { Category } from "../domain/item/Category";
import type { Item } from "../domain/item/Item";
import type { ShoppingList } from "../domain/shopping-list/ShoppingList";
import type { ItemRepository } from "./ItemRepository";



export const itemRepositoryStore: Writable<ItemRepository> = writable<ItemRepository>()
export const categoryStore: Writable<Category[]> = writable<Category[]>([])
export const itemStore: Writable<Item[]> = writable<Item[]>([])
export const shoppingListStore: Writable<ShoppingList[]> = writable<ShoppingList[]>([])