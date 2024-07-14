export type ProductResponse = {
  totalPage: number;
  totalRecord: number;
  pageNumber: number;
  pageSize: number;
  pageData: ProductModel[];
};

export type ProductModel = {
  productId: string;
  name: string;
  supplierId: string;
  categoryId: string;
  subCategoryId: string;
  quantityPerUnit: string;
  unitPrice: number;
  oldPrice: number;
  unitWeight: number;
  size: number;
  discount: null;
  unitInStock: number;
  unitOnOrder: number;
  productAvailable: boolean;
  imageUrl: string;
  altText: string;
  addBadge: null;
  offerTitle: null;
  offerBadgeClass: null;
  shortDescription: string;
  longDescription: string;
  note: string;
  carts: [];
  category: string;
  orderDetails: [];
  productImages: [];
  reviews: [];
  wishlists: [];
};
