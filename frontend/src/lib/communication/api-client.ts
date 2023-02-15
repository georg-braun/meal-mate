import type { GetCategoriesDetailsQueryDto } from './dtos/GetCategoriesDetailsQuery';
import type { ShoppingListQueryResponse } from './dtos/ShoppingListQueryResponse';
import axios from 'axios';
import { categoriesWithItemsStore } from '../store';


export const serverUrl = import.meta.env.VITE_API_SERVER;


class ApiClient {

	private baseUrlWithSlash = `${serverUrl}/`;

	async createCategoryAsync(name: string) {
		await this.sendPostAsync("CreateCategoryCommand", { name: name });
	}

	async createItemAsync(categoryId: string, name: string) {
		await this.sendPostAsync("CreateItemCommand", { name: name, categoryId: categoryId });
	}

	async createEntryAsync(itemId: string, shoppingListId: string, qualifier: string) {
		await this.sendPostAsync("CreateEntryCommand", { itemId: itemId, ShoppingListId: shoppingListId, qualifier: qualifier });
	}

	async createShoppingListAsync(name: string): Promise<string> {
		let response = await this.sendPostAsync("CreateShoppingListCommand", { name: name });
		let shoppingListId = response.data.id;
		return shoppingListId
	}

	async deleteItemAsync(itemId: string) {
		await this.sendPostAsync("DeleteItemCommand", { itemId: itemId });
	}

	async deleteEntryAsync(shoppingListId: string, entryId: string) {
		await this.sendPostAsync("DeleteEntryCommand", { shoppingListId: shoppingListId, entryId: entryId});
	}

	async refreshCategoriesDetailsStoreAsync(): Promise<void> {
		const config = {
			url: `${serverUrl}/GetCategoriesDetailsQuery`,
			method: "GET",
			headers: {
				"content-type": "application/json",
			},
		};
		const response = await this.makeRequest(config);
		categoriesWithItemsStore.set(response.data);
	}

	async getShoppingListAsync(id: string): Promise<ShoppingListQueryResponse> {
		const config = {
			url: `${serverUrl}/ShoppingListQuery?id=${id}`,
			method: "GET",
			headers: {
				"content-type": "application/json",
			},
		};
		const response = await this.makeRequest(config);
		return response.data;
	}



	private makeRequest(config) {
		try {
			config.headers = {
				...config.headers,
			};

			const response = axios.request(config);
			return response;
		} catch (error) {
			console.log(error);
		}
	}

	async sendPostAsync(endpoint, data) {
		try {
			const config = {
				url: `${serverUrl}/${endpoint}`,
				method: "POST",
				headers: {
					"content-type": "application/json",
				},
			};

			try {
				const response = await axios.post(
					`${this.baseUrlWithSlash}${endpoint}`,
					data,
					config
				);
				return response;
			} catch (error) {
				console.log(`${error.response.status}: ${error.response.data}`);
				return error.response;
			}
		} catch (error) {
			console.log(error);
		}
	}

}

export default new ApiClient();



