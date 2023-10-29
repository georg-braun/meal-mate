import {axios} from '@src/lib/axios';

export type CreateShoppingListCommand = {
    Name: string;
}

export type CreateListResponse = {
    name: string;
    id: string;
}

export const createList = (command : CreateShoppingListCommand) : Promise<CreateListResponse> => {
    return axios.post("CreateShoppingListCommand", command)
}