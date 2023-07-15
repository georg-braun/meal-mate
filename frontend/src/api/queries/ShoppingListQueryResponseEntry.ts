import type { ItemWithId } from "../../lib/ItemWithId";



export class ShoppingListQueryResponseEntry implements ItemWithId {
    id: string;
    itemId: string;
    itemName: string;
    qualifier: string;
}
