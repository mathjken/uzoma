export interface Product {
    id: number;
    name: string;
    price: number;
    vendor: string;
    imageUrl: string;
    colors: string[];
    sizes: string[];
    inStock: boolean;
    soldRecently: number;
    height: number;
    description?: string;
    categoryId: number;
    createdAt?: string;
}

export interface HeaderProps {
    cartCount: number;
}

export interface CategoryFilterProps {
    categories: string[];
    activeCategory: string;
    onCategoryChange: (category: string) => void;
}

export interface ProductCardProps {
    product: Product;
    onAddToCart: (product: Product) => void;
}

export interface PinterestGridProps {
    products: Product[];
    onAddToCart: (product: Product) => void;
}
