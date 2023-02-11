export type ShoppingListQueryResponse = {
    id: string;
    name: string;
    entries: ShoppingListQueryResponseEntry[];
};

export type ShoppingListQueryResponseEntry = {
    entryId: string,
    itemId: string;
    itemName: string,
    qualifier: string
}