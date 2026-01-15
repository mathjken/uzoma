import { createContext } from 'react';
import type { Product } from '../interfaces/product';

export interface CartContextType {
  cartCount: number;
  cartItems: Product[];
  addToCart: (product: Product) => void;
  removeFromCart: (productId: number) => void;
  clearCart: () => void;
}

export const CartContext = createContext<CartContextType | undefined>(undefined);