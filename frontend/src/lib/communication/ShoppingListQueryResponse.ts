import type { ItemWithId } from "../ItemWithId";

export class ShoppingListQueryResponse  {
    id: string;
    name: string;
    entries: ShoppingListQueryResponseEntry[];
};

export class ShoppingListQueryResponseEntry implements ItemWithId  {
    id: string;
    itemId: string;
    itemName: string;
    qualifier: string;
}