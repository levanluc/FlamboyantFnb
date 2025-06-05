export interface Product {
  id?: string;
  productCode: string;
  productName: string;
  priceBeforeTax?: number;
  costPrice?: number;
  inventory?: number;
  createdAt?: string;
  imageUrl?: string;
  description?: string;
  categoryName?: string;
  categoryId?: string;
  orderNote?: string;
  typeName?: string;
  // Add more fields as needed from the template
}
