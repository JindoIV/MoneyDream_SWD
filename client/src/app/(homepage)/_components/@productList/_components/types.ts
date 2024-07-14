export type productRes = {
  totalPage: number;
  totalRecord: number;
  pageNumber: number;
  pageSize: number;
};

export type product = {
  productId: string;
  name: string;
  categoryId: string;
  supplierId: string;
  supplier: string;
  importId: string;
  exportPrice: number;
  oldPrice: number;
  sizeId: string;
  discount: number;
  publicId: string;
  imageUrlcloud: string;
  proImages: string[];
};
