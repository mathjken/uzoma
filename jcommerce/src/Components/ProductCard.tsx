
import { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { Heart, Share2 } from 'lucide-react';
import type { ProductCardProps } from "../interfaces/product";

export const ProductCard: React.FC<ProductCardProps> = ({ product, onAddToCart }) => {
    const [isHovered, setIsHovered] = useState<boolean>(false);
    const navigate = useNavigate();

    const handleCardClick = () => {
        navigate(`/product/${product.id}`);
    };

    const handleQuickAdd = (e: React.MouseEvent) => {
        e.stopPropagation(); // Prevent navigation when clicking quick add
        onAddToCart(product);
    };

    const handleWishlist = (e: React.MouseEvent) => {
        e.stopPropagation();
        // Add wishlist logic here
        console.log('Added to wishlist:', product);
    };

    const handleShare = (e: React.MouseEvent) => {
        e.stopPropagation();
        // Add share logic here
        console.log('Share product:', product);
    };

    return (
        <div
            className="group bg-white text-gray-900 rounded-xl overflow-hidden shadow-md hover:shadow-2xl transition-all duration-300 cursor-pointer"
            onMouseEnter={() => setIsHovered(true)}
            onMouseLeave={() => setIsHovered(false)}
            onClick={handleCardClick}
        >
            <div className="relative overflow-hidden" style={{ height: `${product.height}px` }}>
                <img
                    src={product.imageUrl}
                    alt={product.name}
                    className="w-full h-full object-cover transition-transform duration-500 group-hover:scale-110"
                />
                
                {/* Overlay on hover */}
                <div className={`absolute inset-0 bg-black bg-opacity-40 transition-opacity duration-300 ${
                    isHovered ? 'opacity-100' : 'opacity-0'
                }`}>
                    <div className="absolute top-4 right-4 flex flex-col gap-2">
                        <button 
                            onClick={handleWishlist}
                            className="p-2 bg-white rounded-full hover:bg-gray-100 transition-colors"
                            aria-label="Add to wishlist"
                        >
                            <Heart className="w-5 h-5" />
                        </button>
                        <button 
                            onClick={handleShare}
                            className="p-2 bg-white rounded-full hover:bg-gray-100 transition-colors"
                            aria-label="Share product"
                        >
                            <Share2 className="w-5 h-5" />
                        </button>
                    </div>
                    
                    <div className="absolute bottom-4 left-4 right-4">
                        <button
                            onClick={handleQuickAdd}
                            className="w-full bg-white text-black py-3 rounded-lg font-semibold hover:bg-gray-100 transition-colors"
                        >
                            Quick Add
                        </button>
                    </div>
                </div>
                
                {/* Stock indicator */}
                {product.soldRecently > 0 && (
                    <div className="absolute top-4 left-4 bg-red-500 text-white px-3 py-1 rounded-full text-xs font-semibold flex items-center gap-1">
                        🔥 {product.soldRecently} sold in last 15 hours
                    </div>
                )}
            </div>
            
            <div className="p-4">
                <h3 className="font-semibold text-lg mb-2 line-clamp-2">{product.name}</h3>
                
                <div className="flex items-center justify-between mb-3">
                    <span className="text-2xl font-bold">${product.price.toFixed(2)}</span>
                    <span className="text-sm text-gray-500">{product.vendor}</span>
                </div>
                
                {/* Colors */}
                <div className="flex gap-2 mb-3">
                    {product.colors.map((color, idx) => (
                        <div
                            key={idx}
                            className="w-6 h-6 rounded-full border-2 border-gray-300"
                            style={{ backgroundColor: color }}
                            title={color}
                        />
                    ))}
                </div>
                
                {/* Sizes */}
                <div className="flex gap-2 flex-wrap">
                    {product.sizes.map((size) => (
                        <span
                            key={size}
                            className="px-3 py-1 border border-gray-300 rounded text-sm hover:border-black transition-colors"
                        >
                            {size}
                        </span>
                    ))}
                </div>
            </div>
        </div>
    );
};