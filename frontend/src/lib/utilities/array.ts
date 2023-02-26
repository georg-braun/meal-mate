import type { ItemWithId } from "../ItemWithId";

/**
 * Removes the entry in a copy of the provided array and returns the result.
 */
export function removeEntryWithId<T extends ItemWithId>(array : T[], id: string): T[]{
    const index = array.findIndex(item => item.id == id)
    const arrayTemp = [...array];
    arrayTemp.splice(index, 1);
    return arrayTemp;
}

export function findbyId(array : ItemWithId[], id): ItemWithId{
    const index = array.findIndex(item => item.id == id)
    return index < 0 ? undefined : array[index];
}

export function sortByCategory(elements : {category: string}[]): any[]{
    const copy = [...elements];
    copy.sort((a,b) => (a.category > b.category) ? 1 : ((b.category > a.category) ? -1 : 0));
    return copy;
}