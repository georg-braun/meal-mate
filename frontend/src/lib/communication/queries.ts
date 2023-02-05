import { shoppingListStore } from "../data/stores";
import type { BaseItem } from "../domain/BaseItem";
import { ShoppingList } from "../domain/shopping-list/ShoppingList";
import { pb, ShoppingListsCollectionName, UserShoppingListsConnectionCollectionName } from "./pocketbase";



export async function loadInitialData<T extends BaseItem>(collectionName: string, typeName: string) : Promise<T[]>{
    console.log(`Load initial data from collection ${collectionName}`);
    let items = await pb.collection(collectionName).getFullList();
    let typedItems = items.map(item => {
        const typedItem : T = item as T;
        typedItem.type = typeName;
        return typedItem;
    });
    return typedItems;
}

export async function loadUserShoppingLists() : Promise<ShoppingList[]>{
    console.log(`Load shopping lists`);

    // Get a list with the connection info betweeen the user and the shopping list. 
    // Expand this list with the shopping lists.
    const userListConnections = await pb.collection(UserShoppingListsConnectionCollectionName).getFullList(100, {
        expand: "shopping_list"
    });

    // Extract the shopping lists and add type informations.
    const shoppingLists = userListConnections.map(_ => {
        const list = _.expand.shopping_list as ShoppingList;
        list.type = ShoppingList.typeName;
        return list;
    })

    return shoppingLists as ShoppingList[];
}


export async function initializeShoppingList() {
    let items = await pb.collection(ShoppingListsCollectionName).getFullList();
    shoppingListStore.set(items);
}

export async function getShoppingLists(userId){
    console.log(`"Get shopping lists for user ${userId}`)

    // Query the User -> ShoppingList Collection and include (expand) the ShoppingLists.
    let shoppingListConnectionsOfUserRecord = await pb.collection(UserShoppingListsConnectionCollectionName).getList(1, 100,{expand: 'shopping_list'});
    console.log(shoppingListConnectionsOfUserRecord)
    let shoppingLists = shoppingListConnectionsOfUserRecord.items.map(_ => _.expand.shopping_list);

    console.log(shoppingLists)

    shoppingListStore.set(shoppingLists);
}