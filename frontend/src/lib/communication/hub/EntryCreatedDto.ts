import type { ItemWithId } from "../../ItemWithId";

export class EntryCreatedDto implements ItemWithId  {
    id: string;
    itemId: string;
    itemName: string;
    qualifier: string;
}