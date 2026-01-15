
import React from 'react';
import type { CategoryFilterProps } from "../interfaces/product";

export const CategoryFilter: React.FC<CategoryFilterProps> = ({ 
  categories, 
  activeCategory, 
  onCategoryChange 
}) => (
  <div className="sticky top-16 z-40 bg-white border-b border-gray-200 shadow-sm">
    <div className="max-w-7xl mx-auto px-4 py-4">
      <div className="flex gap-3 overflow-x-auto scrollbar-hide">
        {categories.map((category) => (
          <button
            key={category}
            onClick={() => onCategoryChange(category)}
            className={`px-6 py-2 rounded-full whitespace-nowrap transition-all ${
              activeCategory === category
                ? 'bg-black text-white'
                : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
            }`}
          >
            {category}
          </button>
        ))}
      </div>
    </div>
  </div>
);