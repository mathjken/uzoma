
import React, { useEffect, useState } from 'react';
import './index.css';
import { CategoryFilter } from './Components/CategoryFilter';
import { PinterestGrid } from './Components/PinterestGrid';
import { Layout } from './Components/Layout';
import { useCart } from './hooks/useCart';
import type { Product } from './interfaces/product';
import apiService from './services/product';

const App: React.FC = () => {
  const [activeCategory, setActiveCategory] = useState<string>('All Products');
  const [products, setProducts] = useState<Product[]>([]);
  const [allProducts, setAllProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<string[]>(['All Products']);
  const [categoryMap, setCategoryMap] = useState<Map<string, number>>(new Map());
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const { cartCount, addToCart } = useCart();

  // Fetch categories and products from API
  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);

        // Fetch both categories and products in parallel
        const [categoriesData, productsData] = await Promise.all([
          apiService.getCategories(),
          apiService.getProducts()
        ]);

        // Transform categories for display
        const categoryNames = ['All Products', ...categoriesData.map(cat => cat.name)];
        setCategories(categoryNames);

        // Create a map of category names to IDs for filtering
        const catMap = new Map<string, number>();
        categoriesData.forEach(cat => {
          catMap.set(cat.name, cat.id);
        });
        setCategoryMap(catMap);

        // Set products
        setAllProducts(productsData);
        setProducts(productsData);

      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load data');
        console.error('Error fetching data:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  // Filter products when category changes
  useEffect(() => {
    if (activeCategory === 'All Products') {
      setProducts(allProducts);
    } else {
      const categoryId = categoryMap.get(activeCategory);
      if (categoryId) {
        const filtered = allProducts.filter(p => p.categoryId === categoryId);
        setProducts(filtered);
      }
    }
  }, [activeCategory, allProducts, categoryMap]);

  const handleAddToCart = (product: Product): void => {
    addToCart(product);
    console.log('Added to cart:', product);
  };

  const handleCategoryChange = (category: string): void => {
    setActiveCategory(category);
  };

  return (
    <Layout cartCount={cartCount}>
      {!loading && !error && (
        <CategoryFilter
          categories={categories}
          activeCategory={activeCategory}
          onCategoryChange={handleCategoryChange}
        />
      )}

      {/* Loading State */}
      {loading && (
        <div className="flex items-center justify-center min-h-[400px]">
          <div className="text-center">
            <div className="inline-block animate-spin rounded-full h-12 w-12 border-b-2 border-gray-900"></div>
            <p className="mt-4 text-gray-600 font-medium">Loading products...</p>
          </div>
        </div>
      )}

      {/* Error State */}
      {error && (
        <div className="max-w-7xl mx-auto px-4 py-8">
          <div className="bg-red-50 border border-red-200 rounded-lg p-6 text-center">
            <p className="text-red-600 font-semibold text-lg">Error loading data</p>
            <p className="text-red-500 mt-2">{error}</p>
            <button 
              onClick={() => window.location.reload()} 
              className="mt-4 px-6 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700 transition-colors"
            >
              Retry
            </button>
          </div>
        </div>
      )}

      {/* Products Grid */}
      {!loading && !error && products.length > 0 && (
        <PinterestGrid products={products} onAddToCart={handleAddToCart} />
      )}

      {/* Empty State */}
      {!loading && !error && products.length === 0 && (
        <div className="max-w-7xl mx-auto px-4 py-8">
          <div className="bg-gray-50 border border-gray-200 rounded-lg p-12 text-center">
            <p className="text-gray-600 text-lg">
              {activeCategory === 'All Products' 
                ? 'No products available' 
                : `No products found in "${activeCategory}" category`
              }
            </p>
            {activeCategory !== 'All Products' && (
              <button
                onClick={() => setActiveCategory('All Products')}
                className="mt-4 px-6 py-2 bg-black text-white rounded-lg hover:bg-gray-800 transition-colors"
              >
                View All Products
              </button>
            )}
          </div>
        </div>
      )}
    </Layout>
  );
};

export default App;