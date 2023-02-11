import  axios  from 'axios';


const serverUrl = import.meta.env.VITE_API_SERVER;


class ApiClient{

	private baseUrlWithSlash = `${serverUrl}/`;
	async createCategoryAsync(name: string){
		await this.sendPostAsync("CreateCategoryCommand", {name: name});
	}

	async makeRequest(config) {
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



export async function getAllData() {
  const config = {
    url: `${serverUrl}/api/GetAll`,
    method: "GET",
    headers: {
      "content-type": "application/json",
    },
  };
  const response = await makeRequest(config);

 
}
