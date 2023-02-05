import { get, type Writable } from "svelte/store";
import { CategoriesCollectionName, currentUser, ItemsCollectionName, ShoppingListsCollectionName, UserShoppingListsConnectionCollectionName } from "../communication/pocketbase";
import { loadInitialData, loadUserShoppingLists } from "../communication/queries";
import type { BaseItem } from "../domain/BaseItem";
import { Category } from "../domain/item/Category";
import { Item } from "../domain/item/Item";
import { ShoppingList } from "../domain/shopping-list/ShoppingList";
import { ShoppingListEntry } from "../domain/shopping-list/ShoppingListEntry";
import { findbyId, removeEntryWithId } from "../utilities/array";
import { categoryStore, itemStore, shoppingListStore } from "./stores";

/**,,,,,
 *  Save the data in this repository. But also reflect the changes to the gloval svelte stores which can be consumed by the UI components.
 */
export class ItemRepository {


    public categories: Category[] = []
    public items: Item[] = []
    public shoppingList: ShoppingList[] = []

    /* 
    Todo: Following are some commands that don't modify the repository. They just trigger something.
    The modification happens after a processing by the backend.
    I think they should be moved to some command module
    */


    public findShoppingList(listId: string): ShoppingList {
        const index = this.shoppingList.findIndex(_ => _.id == listId);
        return (index < 0) ? undefined : this.shoppingList[index];
    }

    private getStore(element: BaseItem): any {

        return this.getStoreByTypeName(element.type)
    }

    private getStoreByTypeName(typeName: string): Writable<BaseItem[]> {

        // This feels super ugly. I would like to have a more generic solution.
        switch (typeName) {
            case Category.typeName: return categoryStore;
            case Item.typeName: return itemStore;
            case ShoppingList.typeName: return shoppingListStore;
            default:
                console.log(`CAUTION. Store not found for type ${typeof (typeName)}`);
                break;
        }
    }

    private getLocalStorage(element: BaseItem): any {

        return this.getLocalStorageByTypeName(element.type)
    }

    private getLocalStorageByTypeName(typeName: string): BaseItem[] {

        // This feels super ugly. I would like to have a more generic solution.
        switch (typeName) {
            case Category.typeName: return this.categories;
            case Item.typeName: return this.items;
            case ShoppingList.typeName: return this.shoppingList;
            default:
                console.log(`CAUTION. Local storage not found for type ${typeof (typeName)}`);
                break;
        }
    }

    public add<T extends BaseItem>(element: T) {
        // update the subscribable store
        const store: Writable<T[]> = this.getStore(element)
        store.update(_ => {
            _.push(element);
            return _;
        });

        // but also the local storage
        const localStorage: T[] = this.getLocalStorage(element)
        localStorage.push(element)
    }

    public update<T extends BaseItem>(data: BaseItem){
        const typeName = data.type;
        const localStorage = this.getLocalStorageByTypeName(typeName);
        const store = this.getStoreByTypeName(typeName)

        // update the local storage
        const index = localStorage.findIndex(_ => _.id == data.id)

        if(index < 0){
            console.log(`Could not update element of type ${typeName} with id ${data.id} because could not find the origin dataset.`)
            return;
        }

        localStorage.splice(index, 1, data)

         // update the subscribable store
         store.set(localStorage)
    }


    public clear<T extends BaseItem>(typeName: string) {

        const store: Writable<T[]> = this.getStoreByTypeName(typeName)
        store.set([])

        // but also the local storage
        const localStorage: T[] = this.getLocalStorageByTypeName(typeName)
        localStorage.splice(0, localStorage.length)
    }

    public async loadInitialData<T extends BaseItem>(collectionName: string, typeName: string): Promise<void> {
        // remove existing data
        this.clear(typeName)

        // get data from the server
        const data = await loadInitialData<T>(collectionName, typeName);

        // and add this data to the store
        data.forEach(element => {
            this.add(element);
        });
    }

    public async loadShoppingLists(): Promise<void> {
        // remove existing data
        this.clear(ShoppingList.typeName)

        // get the available shopping lists for the user
        const data = await loadUserShoppingLists();
        
        // and add this data to the store
        data.forEach(element => {
            this.add(element);
        });
    }

    public delete(typeName: string, id: string) {
        console.log(`Try to delete element of type ${typeName} with id ${id}.`)

        const localStorage = this.getLocalStorageByTypeName(typeName);
        const store = this.getStoreByTypeName(typeName);

        if (localStorage == undefined) {
            console.log(`Can't delete because could not find the local storage for ${typeName}`)
            return;
        }

        if (store == undefined) {
            console.log(`Can't delete because could not find the store for ${typeName}`)
            return;
        }

        const modified = removeEntryWithId(localStorage, id);
        localStorage.splice(0, localStorage.length)
        localStorage.push(...modified)
        store.set(modified);
    }

    public getCategoryTitle(id: string): string {
        if (id == undefined || id == "") return "uncategorized"

        const category = this.getItemById(this.categories, id);

        if (category == undefined) {
            console.log(`Could not find the category with id ${id}`)
            return `Category id: ${id}`;
        }

        return category.title;
    }

    public getCategoryTitleOfShoppingListEntry(listId: string, entryId: string) : string{
        const list = this.find<ShoppingList>(ShoppingList.typeName, listId);
        if (list == undefined) return;

        const entry = findbyId(list.items, entryId);
        if (entry == undefined) return;

        const item = this.find<Item>(Item.typeName, entry.itemId)
        if (item == undefined) return;

        return this.getCategoryTitle(item.categoryId)
    }

    public getItemTitle(id: string): string {
        if (id == undefined || id == "") return "unkown item"

        const item = this.getItemById(this.items, id);

        if (item == undefined) {
            console.log(`Could not find the item with id ${id}`)
            return `Item with id ${id}`;
        }

        return item.title;
    }

    public find<T extends BaseItem>(typeName: string, id: string): T{
        const data = this.getLocalStorageByTypeName(typeName);
        return this.getItemById(data, id)
    }

    private getItemById<T extends BaseItem>(data: T[], id: string): T {
        const index = data.findIndex(_ => _.id == id);
        return index >= 0 ? data.at(index) : undefined;
    }

    

}