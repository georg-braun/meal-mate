import {axios} from '@src/lib/axios';
import type { Entry } from '@src/types/Entry';



export type GetListQueryResponse = {
    name: string;
    id: string;
    entries: Entry[]
    
}

export const getList = (id : string) : Promise<GetListQueryResponse> => {
    return axios.get(`ShoppingListQuery/?id=${id}`)
}