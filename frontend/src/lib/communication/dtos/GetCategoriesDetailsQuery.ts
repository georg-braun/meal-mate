export type GetCategoriesDetailsQueryDto = {
  id: string;
  name: string;
  items: GetCategoriesDetailsQueryItem[];
};


export type GetCategoriesDetailsQueryItem = {
  id: string;
  name: string
}
