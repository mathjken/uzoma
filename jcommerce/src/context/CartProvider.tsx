
import React, { useState } from 'react';
import type { ReactNode } from 'react';
import type { Product } from '../interfaces/product';
import { CartContext } from './CartContext';

export const CartProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [cartItems, setCartItems] = useState<Product[]>([]);
  const [cartCount, setCartCount] = useState<number>(0);

  const addToCart = (product: Product) => {
    setCartItems(prev => [...prev, product]);
    setCartCount(prev => prev + 1);
  };

  const removeFromCart = (productId: number) => {
    setCartItems(prev => prev.filter(item => item.id !== productId));
    setCartCount(prev => Math.max(0, prev - 1));
  };

  const clearCart = () => {
    setCartItems([]);
    setCartCount(0);
  };

  return (
    <CartContext.Provider value={{ cartCount, cartItems, addToCart, removeFromCart, clearCart }}>
      {children}
    </CartContext.Provider>
  );
};