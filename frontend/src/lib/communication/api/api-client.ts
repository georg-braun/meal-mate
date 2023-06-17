import type { ShoppingListQueryResponse } from './queries/ShoppingListQueryResponse';
import axios from 'axios';
import { itemsStore } from '../../store';
import type { Template } from '../../components/template/Template';


export const serverUrl = import.meta.env.VITE_API_SERVER;



interface CreateShoppingListCommandResponse{
	id: string;
	name: string;
}

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

	async createEntryWithFreeTextAsync(shoppingListId: string, text: string) {
		console.log(`Create entry ${text} for list ${shoppingListId}.`)
		await this.sendPostAsync("CreateEntryWithFreeTextCommand", { ShoppingListId: shoppingListId, freeText: text });
	}

	async createShoppingListAsync(name: string): Promise<string> {
		let response = await this.sendPostAsync("CreateShoppingListCommand", { name: name });
		let responseData = response.data as CreateShoppingListCommandResponse;
		let shoppingListId = responseData.id;
		return shoppingListId
	}

	async deleteItemAsync(itemId: string) {
		await this.sendPostAsync("DeleteItemCommand", { itemId: itemId });
	}

	async deleteEntryAsync(shoppingListId: string, entryId: string) {
		await this.sendPostAsync("DeleteEntryCommand", { shoppingListId: shoppingListId, entryId: entryId});
	}

	async refreshItemsStoreAsync(): Promise<void> {
		console.log(`Refresh items.`)
		const config = {
			url: `${serverUrl}/ItemsQuery`,
			method: "GET",
			headers: {
				"content-type": "application/json",
			},
		};
		const response = await this.makeRequest(config);
		itemsStore.set(response.data);
	}

	async getShoppingListAsync(id: string): Promise<ShoppingListQueryResponse> {
		console.log(`Get shopping list ${id}.`)
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

	async createTemplateAsync(template: Template){
		await this.sendPostAsync("template", template);
	}

	async updateTemplateAsync(template: Template){
		await this.sendPutAsync(`template/${template.id}`, template);
	}

	async getTemplatesAsync(): Promise<Template[]> {
		console.log(`Get templates.`)
		const data = await this.getRequestAsync("template");
		return data;
	}

	async getTemplateAsync(id: string): Promise<Template> {
		console.log(`Get template ${id}.`)
		const data = await this.getRequestAsync(`template/${id}`);
		console.log(data);
		return data;
	}

	async getAvailableTemplatesAsync(): Promise<Template[]> {
		console.log(`Get available templates.`)
		const data = await this.getRequestAsync("AvailableTemplatesQuery");
		return data;
	}

	async applyTemplate(templateId, listId): Promise<void> {
		console.log(`Apply template.`)
		await this.sendPostAsync("ApplyTemplateCommand", {listId: listId, templateId: templateId});
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

	async getRequestAsync(path: string) {
		try {
			const config = {
				url: `${serverUrl}/${path}`,
				method: "GET",
				headers: {
					"content-type": "application/json",
				},
			};
			const response = await this.makeRequest(config);
			return response.data;
		}
		catch (error) {
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

	async sendPutAsync(endpoint, data) {
		try {
			const config = {
				url: `${serverUrl}/${endpoint}`,
				method: "PUT",
				headers: {
					"content-type": "application/json",
				},
			};

			try {
				const response = await axios.put(
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



