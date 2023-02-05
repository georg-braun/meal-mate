import { debug } from "svelte/internal";
import type { BaseItem } from "../domain/BaseItem";
import { ShoppingListEntry } from "../domain/shopping-list/ShoppingListEntry";
import { CategoriesCollectionName, currentUser, ItemsCollectionName, pb, ShoppingListsCollectionName, UserShoppingListsConnectionCollectionName } from "./pocketbase";
import { v4 as uuidv4 } from 'uuid';
import type { ShoppingList } from "../domain/shopping-list/ShoppingList";



export async function createCategory(title): Promise<void> {
    // Send the new category to the server. If the category is valid and created, the server will inform the client about the added category.
    await pbCreate(CategoriesCollectionName, { title: title })
}

export async function createItem(title: string, categoryId: string): Promise<void> {
    // Send the new item to the server. If the item is valid and created, the server will inform the client.
    await pbCreate(ItemsCollectionName, { title: title, categoryId: categoryId })
}

export async function createShoppingList(title: string, userId: string): Promise<void> {
    if (userId == undefined || userId == "") {
        console.log(`Can't create the shopping list ${title} because the user id is undefined or empty.`)
        return;
    }

    // Send the new item to the server. If the item is valid and created, the server will inform the client.
    const createdList = await pbCreate(ShoppingListsCollectionName, { title: title, createdBy: userId })

    if (createdList.id != undefined) {
        // The shopping list is created. Create a new entry to associate the user with this list
        await pbCreate(UserShoppingListsConnectionCollectionName, { shopping_list: createdList.id, user: userId })
        console.log(`Linked ${userId} to list ${createdList.id}.`)
    }
}

export async function createEntryOnList(shoppingList: ShoppingList, itemId: string, qualifier: string) {

    if (shoppingList == undefined) {
        console.log(`Could not find shopping list ${shoppingList.id}.`)
        return;
    }

    const entry = ShoppingListEntry.create(uuidv4(), itemId, qualifier)

    // just modify a local copy of the shopping list. The changes to real data are reflected by subscription 
    const tempList = { ...shoppingList };
    if (tempList.items == undefined)
        tempList.items = []
    tempList.items.push(entry);

    // Send the new item to the server. If the item is valid and created, the server will inform the client.
    await pbUpdate(ShoppingListsCollectionName, tempList)
}




export async function updateShoppingList(list: ShoppingList) {
    await pbUpdate(ShoppingListsCollectionName, list);
}

/**
 * Add the dataset in the specified collection
 * @param collectionName 
 * @param data 
 */
async function pbCreate(collectionName: string, data) {
    CheckFieldNames(data);

    console.log(`Create new dataset in ${collectionName}: ${JSON.stringify(data)}`)
    return await pb.collection(collectionName).create(data);
}

async function pbUpdate(collectionName: string, data: BaseItem) {
    CheckFieldNames(data);

    console.log(`Update dataset with id ${data.id} in ${collectionName}: ${JSON.stringify(data)}`)
    await pb.collection(collectionName).update(data.id, data)
}

function CheckFieldNames(data) {
    Object.keys(data).forEach(_ => {
        const firstChar = _.charAt(0)
        if (firstChar == firstChar.toUpperCase())
            console.log(`CAUTION! The member name "${_}" of the object have to start with a lower-case. Otherwise it won't match the pocketbase field name.`)
    })
}