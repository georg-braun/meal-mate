import type { BaseItem } from "../BaseItem";
import type { ShoppingListEntry } from "./ShoppingListEntry";

export class ShoppingList implements BaseItem {
    public static typeName = "shoppingList"
    public type: string = ShoppingList.typeName;
    id: string;
    title: string;
    items: ShoppingListEntry[] = [];


    // Todo: This should be a method of the object. But during object copy this method get lost. But the copy stuff should be avoidable with the removal of the typename stuff.
    public static removeEntry(list: ShoppingList, entryId: string) {
        const index = list.items.findIndex(_ => _.id == entryId);
        if (index < 0) {
            console.log(`Couldn't find the entry with id ${entryId}.`)
            return;
        }

        list.items.splice(index, 1)
    }

    public static create(id: string, title: string): ShoppingList {
        const shoppingList = new ShoppingList();
        shoppingList.id = id;
        shoppingList.title = title;
        return shoppingList;
    }
}