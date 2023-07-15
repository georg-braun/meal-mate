import type { ShoppingListQueryResponseEntry } from "./ShoppingListQueryResponseEntry";


export class ShoppingListQueryResponse  {
    id: string;
    name: string;
    entries: ShoppingListQueryResponseEntry[];
};

