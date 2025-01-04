export interface Product {
    id: string;
    name: string;
    description: string;
    price: number;
    categoryId: string;
    categoryName?: string; // Optional for display purposes
    status: string;
    stockQuantity: number;
    imageUrl?: string;
  }
  