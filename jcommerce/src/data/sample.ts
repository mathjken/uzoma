import type { Product } from "../interfaces/product";


// Mock product data
export const products: Product[] = [
  {
    id: 1,
    name: 'Vintage Irregular Pocket Jacket',
    price: 41.67,
    vendor: 'Roopka',
    imageUrl: 'https://images.unsplash.com/photo-1551028719-00167b16eac5?w=400&h=500&fit=crop',
    colors: ['green', 'gray', 'black'],
    sizes: ['S', 'M', 'L', 'XL'],
    inStock: true,
    soldRecently: 6,
    height: 400
  },
  {
    id: 2,
    name: 'Classic Denim Jacket',
    price: 52.99,
    vendor: 'UrbanWear',
    imageUrl: 'https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=400&h=600&fit=crop',
    colors: ['blue', 'black'],
    sizes: ['S', 'M', 'L', 'XL', 'XXL'],
    inStock: true,
    soldRecently: 12,
    height: 500
  },
  {
    id: 3,
    name: 'Leather Biker Jacket',
    price: 89.99,
    vendor: 'RockStyle',
    imageUrl: 'https://images.unsplash.com/photo-1520975954732-35dd22299614?w=400&h=450&fit=crop',
    colors: ['black', 'brown'],
    sizes: ['M', 'L', 'XL'],
    inStock: true,
    soldRecently: 8,
    height: 450
  },
  {
    id: 4,
    name: 'Oversized Hoodie',
    price: 35.50,
    vendor: 'ComfortZone',
    imageUrl: 'https://images.unsplash.com/photo-1556821840-3a63f95609a7?w=400&h=550&fit=crop',
    colors: ['gray', 'black', 'white', 'navy'],
    sizes: ['S', 'M', 'L', 'XL'],
    inStock: true,
    soldRecently: 15,
    height: 520
  },
  {
    id: 5,
    name: 'Bomber Jacket',
    price: 67.80,
    vendor: 'AeroStyle',
    imageUrl: 'https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=400&h=480&fit=crop',
    colors: ['olive', 'black', 'navy'],
    sizes: ['S', 'M', 'L', 'XL'],
    inStock: true,
    soldRecently: 5,
    height: 480
  },
  {
    id: 6,
    name: 'Wool Blend Coat',
    price: 125.00,
    vendor: 'LuxeFashion',
    imageUrl: 'https://images.unsplash.com/photo-1539533018447-63fcce2678e3?w=400&h=600&fit=crop',
    colors: ['camel', 'black', 'gray'],
    sizes: ['S', 'M', 'L'],
    inStock: true,
    soldRecently: 3,
    height: 580
  },
  {
    id: 7,
    name: 'Sports Track Jacket',
    price: 44.99,
    vendor: 'ActiveWear',
    imageUrl: 'https://images.unsplash.com/photo-1556821840-3a63f95609a7?w=400&h=420&fit=crop',
    colors: ['navy', 'black', 'red'],
    sizes: ['S', 'M', 'L', 'XL', 'XXL'],
    inStock: true,
    soldRecently: 20,
    height: 420
  },
  {
    id: 8,
    name: 'Puffer Jacket',
    price: 78.50,
    vendor: 'WinterEssentials',
    imageUrl: 'https://images.unsplash.com/photo-1548126032-079d1ad4f339?w=400&h=530&fit=crop',
    colors: ['black', 'navy', 'red'],
    sizes: ['M', 'L', 'XL'],
    inStock: true,
    soldRecently: 10,
    height: 530
  }
];



export const categories: string[] = [
    'All Products',
    'Jackets',
    'Hoodies',
    'Coats',
    'Sportswear',
    'Casual',
    'Formal',
    'Outerwear'
  ];