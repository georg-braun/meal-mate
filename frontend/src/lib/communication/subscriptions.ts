import { get } from "svelte/store";
import type { ItemRepository } from "../data/ItemRepository";
import { shoppingListStore } from "../data/stores";
import type { BaseItem } from "../domain/BaseItem";
import { Category } from "../domain/item/Category";
import { Item } from "../domain/item/Item";
import { removeEntryWithId } from "../utilities/array";
import { CategoriesCollectionName, ItemsCollectionName, pb, ShoppingListsCollectionName } from "./pocketbase";


export function  subscribeToChanges<T extends BaseItem>(typeName: string, collectionName: string, repo : ItemRepository, recordId: string = "*"){ 
    console.log(`Subscribe to ${collectionName} to record(s): ${recordId}`)
    const unsubscribe = pb.collection(collectionName).subscribe(recordId, ({ action, record }) => {
        if (action == "delete") {
            console.log(`Deletion in ${collectionName}: Record with id ${record.id}`)
            repo.delete(typeName, record.id)

        }
        else if (action == "create") {
            console.log(`New dataset added in ${collectionName}: Record with id ${record.id}`)
            const newElement : T = {...record as T, type: typeName};
            repo.add(newElement)
        }
        else if (action == "update"){
            console.log(`Updated data in ${collectionName}: Record with id ${record.id}`)
            const updatedElement : T = {...record as T, type: typeName};
            repo.update(updatedElement)
        }
    });
    return unsubscribe;
}