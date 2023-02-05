import type { BaseItem } from "../BaseItem";

export class ShoppingListEntry implements BaseItem {
    id: string;
    itemId: string;
    qualifier: string;
    public static typeName = "shoppingListEntry"
    public type: string = ShoppingListEntry.typeName;

    public static create(id: string, itemId: string, qualifier: string) : ShoppingListEntry{
        const item = new ShoppingListEntry();
        item.id = id;
        item.itemId = itemId;
        item.qualifier = qualifier
        return item;
    }
}