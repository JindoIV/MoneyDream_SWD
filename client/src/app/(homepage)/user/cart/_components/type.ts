export type ProductCart = {
  quantity: number;
  productId: string;
  name: string;
  categoryId: string;
  supplierId: string;
  importId: string;
  oldPrice: number;
  sizeId: string;
  discount: number;
  publicId: string;
  imageUrlcloud: string;
  size: {
    productId: string;
    sizeId: string;
    name: string;
    productWidth: string;
  };
  updateCart: (productId: string, inputNumber: number) => void;
};
