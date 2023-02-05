import type { BaseItem } from "../BaseItem";

export class Category implements BaseItem{
    public static typeName = "category"
    public type: string = Category.typeName;
    public id: string;
    public title: string;

    public static create(id: string, title: string) : Category{
        const category = new Category();
        category.id = id;
        category.title = title;
        return category;
    }
}

