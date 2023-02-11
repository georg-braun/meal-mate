export type GetCategoriesDetailsQueryDto = {
  id: string;
  name: string;
  items: { id: string; name: string }[];
};
