import type { PinterestGridProps } from "../interfaces/product";
import { ProductCard } from "./ProductCard";


export const PinterestGrid: React.FC<PinterestGridProps> = ({ products, onAddToCart }) => {
    return (
      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
          {products.map((product) => (
            <ProductCard
              key={product.id}
              product={product}
              onAddToCart={onAddToCart}
            />
          ))}
        </div>
      </div>
    );
  };