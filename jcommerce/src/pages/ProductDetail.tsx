
import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { 
  ChevronLeft, 
  ChevronRight, 
  Heart, 
  Share2, 
  Maximize2
} from 'lucide-react';
import { Layout } from '../Components/Layout';
import { useCart } from '../hooks/useCart';
import type { Product } from '../interfaces/product';
import apiService from '../services/product';

export const ProductDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [product, setProduct] = useState<Product | null>(null);
  const [loading, setLoading] = useState(true);
  const [selectedColor, setSelectedColor] = useState<string>('');
  const [selectedSize, setSelectedSize] = useState<string>('');
  const [currentImageIndex, setCurrentImageIndex] = useState(0);

  const { cartCount, addToCart } = useCart();

  // Mock multiple images (in real app, this would come from API)
  const productImages = product ? [
    product.imageUrl,
    product.imageUrl,
    product.imageUrl
  ] : [];

  useEffect(() => {
    const fetchProduct = async () => {
      try {
        setLoading(true);
        const products = await apiService.getProducts();
        const foundProduct = products.find(p => p.id === Number(id));
        
        if (foundProduct) {
          setProduct(foundProduct);
          setSelectedColor(foundProduct.colors[0]);
          setSelectedSize(foundProduct.sizes[0]);
        }
      } catch (error) {
        console.error('Error fetching product:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchProduct();
  }, [id]);

  const handlePrevImage = () => {
    setCurrentImageIndex((prev) => 
      prev === 0 ? productImages.length - 1 : prev - 1
    );
  };

  const handleNextImage = () => {
    setCurrentImageIndex((prev) => 
      prev === productImages.length - 1 ? 0 : prev + 1
    );
  };

  const handleAddToCart = () => {
    if (product) {
      addToCart(product);
      console.log('Added to cart:', product);
    }
  };

  if (loading) {
    return (
      <Layout cartCount={cartCount}>
        <div className="flex items-center justify-center min-h-screen">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-gray-900"></div>
        </div>
      </Layout>
    );
  }

  if (!product) {
    return (
      <Layout cartCount={cartCount}>
        <div className="flex items-center justify-center min-h-screen">
          <div className="text-center">
            <h2 className="text-2xl font-bold mb-4">Product not found</h2>
            <button 
              onClick={() => navigate('/')}
              className="px-6 py-2 bg-black text-white rounded-lg"
            >
              Go Back
            </button>
          </div>
        </div>
      </Layout>
    );
  }

  return (
    <Layout cartCount={cartCount}>
      <div className="bg-white">
        {/* Breadcrumb */}
        <div className="bg-gray-50 border-b border-gray-200">
          <div className="max-w-7xl mx-auto px-4 py-3">
            <div className="flex items-center gap-2 text-sm">
              <button 
                onClick={() => navigate('/')}
                className="text-gray-900 hover:underline"
              >
                Home
              </button>
              <ChevronRight className="w-4 h-4 text-gray-400" />
              <span className="text-gray-500">Products</span>
              <ChevronRight className="w-4 h-4 text-gray-400" />
              <span className="text-gray-500 truncate">{product.name}</span>
            </div>
          </div>
        </div>

        {/* Main Content */}
        <div className="max-w-7xl mx-auto px-4 py-6 pb-24 md:pb-6">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
            {/* Image Gallery */}
            <div className="space-y-4">
              {/* Main Image */}
              <div className="relative bg-gray-50 rounded-lg overflow-hidden aspect-square">
                <img
                  src={productImages[currentImageIndex]}
                  alt={product.name}
                  className="w-full h-full object-cover"
                />
                
                {/* Image Navigation */}
                <button
                  onClick={handlePrevImage}
                  className="absolute left-4 top-1/2 -translate-y-1/2 p-2 bg-white rounded-full shadow-lg hover:bg-gray-100"
                >
                  <ChevronLeft className="w-6 h-6" />
                </button>
                <button
                  onClick={handleNextImage}
                  className="absolute right-4 top-1/2 -translate-y-1/2 p-2 bg-white rounded-full shadow-lg hover:bg-gray-100"
                >
                  <ChevronRight className="w-6 h-6" />
                </button>

                {/* Image Counter */}
                <div className="absolute bottom-4 right-4 bg-white px-3 py-1 rounded-full text-sm font-medium">
                  {currentImageIndex + 1} / {productImages.length}
                </div>

                {/* Fullscreen Button */}
                <button className="absolute bottom-4 left-4 p-2 bg-white rounded-full hover:bg-gray-100">
                  <Maximize2 className="w-5 h-5" />
                </button>

                {/* Action Buttons */}
                <div className="absolute top-4 right-4 flex flex-col gap-2">
                  <button className="p-2 bg-white rounded-full hover:bg-gray-100">
                    <Heart className="w-5 h-5" />
                  </button>
                  <button className="p-2 bg-white rounded-full hover:bg-gray-100">
                    <Share2 className="w-5 h-5" />
                  </button>
                </div>
              </div>

              {/* Thumbnail Gallery */}
              <div className="flex gap-4 overflow-x-auto">
                {productImages.map((img, idx) => (
                  <button
                    key={idx}
                    onClick={() => setCurrentImageIndex(idx)}
                    className={`flex-shrink-0 w-24 h-24 rounded-lg overflow-hidden border-2 transition-all ${
                      currentImageIndex === idx 
                        ? 'border-black' 
                        : 'border-gray-200 hover:border-gray-400'
                    }`}
                  >
                    <img
                      src={img}
                      alt={`${product.name} ${idx + 1}`}
                      className="w-full h-full object-cover"
                    />
                  </button>
                ))}
              </div>
            </div>

            {/* Product Info */}
            <div className="space-y-6">
              <div>
                <h1 className="text-3xl font-bold mb-4">{product.name}</h1>
                
                {/* Sold Recently Badge */}
                {product.soldRecently > 0 && (
                  <div className="inline-flex items-center gap-2 bg-red-50 text-red-600 px-4 py-2 rounded-full text-sm font-semibold mb-4">
                    🔥 {product.soldRecently} sold in last 15 hours
                  </div>
                )}

                <div className="space-y-2 text-gray-700">
                  <p><span className="font-semibold">Vendor:</span> {product.vendor}</p>
                  <p><span className="font-semibold">Availability:</span> <span className="text-green-600">In Stock</span></p>
                  <p><span className="font-semibold">Product Type:</span> Jacket</p>
                </div>
              </div>

              {/* Price */}
              <div className="text-4xl font-bold">${product.price.toFixed(2)}</div>

              {/* Color Selection */}
              <div>
                <label className="block text-lg font-semibold mb-3">
                  Color: <span className="font-normal text-gray-600 capitalize">{selectedColor}</span>
                </label>
                <div className="flex gap-3">
                  {product.colors.map((color, idx) => (
                    <button
                      key={idx}
                      onClick={() => setSelectedColor(color)}
                      className={`w-12 h-12 rounded-full border-2 transition-all ${
                        selectedColor === color 
                          ? 'border-black ring-2 ring-offset-2 ring-black' 
                          : 'border-gray-300 hover:border-gray-400'
                      }`}
                      style={{ backgroundColor: color }}
                      title={color}
                    />
                  ))}
                </div>
              </div>

              {/* Size Selection */}
              <div>
                <label className="block text-lg font-semibold mb-3">
                  Size: <span className="font-normal text-gray-600">{selectedSize}</span>
                </label>
                <div className="grid grid-cols-4 gap-3">
                  {product.sizes.map((size) => (
                    <button
                      key={size}
                      onClick={() => setSelectedSize(size)}
                      className={`py-3 border-2 rounded-lg font-semibold transition-all ${
                        selectedSize === size
                          ? 'border-black bg-black text-white'
                          : 'border-gray-300 hover:border-gray-400'
                      }`}
                    >
                      {size}
                    </button>
                  ))}
                </div>
              </div>

              {/* Compare Color Button */}
              <button className="flex items-center gap-2 text-blue-600 hover:underline">
                <div className="w-6 h-6 rounded-full bg-gradient-to-r from-red-500 via-yellow-500 to-blue-500"></div>
                Compare Color
              </button>

              {/* Add to Cart Button */}
              <button
                onClick={handleAddToCart}
                className="w-full bg-black text-white py-4 rounded-lg text-lg font-semibold hover:bg-gray-800 transition-colors"
              >
                Add to Cart
              </button>

              {/* Product Description */}
              {product.description && (
                <div className="border-t pt-6">
                  <h3 className="font-semibold text-lg mb-2">Description</h3>
                  <p className="text-gray-700">{product.description}</p>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </Layout>
  );
};