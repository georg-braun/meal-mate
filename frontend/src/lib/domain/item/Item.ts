import type { BaseItem } from "../BaseItem";

export class Item implements BaseItem {
    public static typeName = "item"
    public type: string = Item.typeName;
    id: string;
    title: string;
    categoryId: string;

    public static create(id: string, title: string, categoryId: string) : Item{
        const item = new Item();
        item.id = id;
        item.title = title;
        item.categoryId = categoryId
        return item;
    }
} 