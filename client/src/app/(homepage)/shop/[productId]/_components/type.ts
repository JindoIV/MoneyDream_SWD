export type productDetail = {
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
  proImages: proImage[];
  size: productSize[];
};

export type proImage = {
  image: string;
};

export type productSize = {
  productId: string;
  sizeId: string;
  name: string;
  productWidth: string;
};
