import type { Product } from "../interfaces/product";
import  {API_BASE_URL } from "./constants"
export interface ApiCategory {
  id: number;
  name: string;
  slug: string;
  products: object[];
}


export interface Category {
  id: number;
  name: string;
  slug: string;
}

class ApiService {
  private async fetchData<T>(endpoint: string): Promise<T> {
    try {
      const response = await fetch(`${API_BASE_URL}${endpoint}`);
      
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      const data = await response.json();
      return data;
    } catch (error) {
      console.error(`Error fetching ${endpoint}:`, error);
      throw error;
    }
  }

  // Fetch all products
  async getProducts(): Promise<Product[]> {
    const apiProducts = await this.fetchData<Product[]>('/products');
    
    return apiProducts.map((apiProduct) => ({
      id: apiProduct.id,
      name: apiProduct.name,
      description: apiProduct.description,
      price: apiProduct.price,
      vendor: 'Roopka',
      imageUrl: apiProduct.imageUrl,
      colors: this.getRandomColors(),
      sizes: ['S', 'M', 'L', 'XL'],
      inStock: true,
      soldRecently: Math.floor(Math.random() * 20) + 1,
      height: this.getRandomHeight(),
      categoryId: apiProduct.categoryId
    }));
  }

  // Fetch all categories
  async getCategories(): Promise<Category[]> {
    const apiCategories = await this.fetchData<ApiCategory[]>('/categories');
    
    return apiCategories.map(category => ({
      id: category.id,
      name: category.name,
      slug: category.slug
    }));
  }

  // Fetch products by category
  async getProductsByCategory(categoryId: number): Promise<Product[]> {
    const allProducts = await this.getProducts();
    return allProducts.filter(product => product.categoryId === categoryId);
  }

  // Helper method to generate random colors
  private getRandomColors(): string[] {
    const colorPalettes = [
      ['black', 'gray', 'white'],
      ['navy', 'blue', 'lightblue'],
      ['brown', 'tan', 'beige'],
      ['green', 'olive', 'darkgreen'],
      ['red', 'maroon', 'pink']
    ];
    
    return colorPalettes[Math.floor(Math.random() * colorPalettes.length)];
  }

  // Helper method to generate random height for Pinterest layout
  private getRandomHeight(): number {
    const heights = [380, 420, 460, 500, 540, 580];
    return heights[Math.floor(Math.random() * heights.length)];
  }
}

// Export singleton instance
export const apiService = new ApiService();
export default apiService;