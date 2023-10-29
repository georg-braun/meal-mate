import {axios} from '@src/lib/axios';
import type { Entry } from '@src/types/Entry';



export type CreateEntryCommand = {
    ShoppingListId: string,
    Name: string
    
}

export const createEntry = (command: CreateEntryCommand) : Promise<any> => {
    return axios.post(`CreateEntryCommand`, command)
}