import { Search, ShoppingCart, User, Menu } from 'lucide-react';
import type { HeaderProps } from '../interfaces/product';

export const Header: React.FC<HeaderProps> = ({ cartCount }) => (
    <header className="sticky top-0 z-50 bg-gray-900 border-b border-gray-800 shadow-sm">
        <div className="max-w-7xl mx-auto px-4 py-3">
            <div className="flex items-center justify-between">
                <button className="p-2 rounded-lg text-white hover:bg-gray-800">
                    <Menu className="w-6 h-6" />
                </button>

                <h1 className="text-2xl font-bold tracking-wider text-white">JCommerce</h1>

                <div className="flex items-center gap-2">
                    <button className="p-2 rounded-lg text-white hover:bg-gray-800">
                        <Search className="w-6 h-6" />
                    </button>
                    <button className="p-2 rounded-lg text-white hover:bg-gray-800">
                        <User className="w-6 h-6" />
                    </button>
                    <button className="relative p-2 rounded-lg text-white hover:bg-gray-800">
                        <ShoppingCart className="w-6 h-6" />
                        {cartCount > 0 && (
                            <span className="absolute top-0 right-0 bg-red-500 text-white text-xs rounded-full w-5 h-5 flex items-center justify-center">
                                {cartCount}
                            </span>
                        )}
                    </button>
                </div>
            </div>
        </div>
    </header>
);