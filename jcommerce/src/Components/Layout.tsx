import React from 'react';
import type { ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import { 
  Search, ShoppingCart, User, Menu, Heart, LogOut, User as UserIcon,
  Facebook, Twitter, Instagram, Youtube, Mail, Phone, MapPin,
  CreditCard, Shield, Truck, Headphones
} from 'lucide-react';
import { useAuth } from '../hooks/useAuth';

interface LayoutProps {
  children: ReactNode;
  cartCount?: number;
  showBottomNav?: boolean;
}

export const Layout: React.FC<LayoutProps> = ({ 
  children, 
  cartCount = 0,
  showBottomNav = true 
}) => {
  const navigate = useNavigate();
  const { user, isAuthenticated, logout } = useAuth();
  const [showUserMenu, setShowUserMenu] = React.useState(false);

  const handleUserClick = () => {
    if (isAuthenticated) {
      setShowUserMenu(!showUserMenu);
    } else {
      navigate('/login');
    }
  };

  const handleLogout = () => {
    logout();
    setShowUserMenu(false);
    navigate('/');
  };

  const footerLinks = {
    shop: ['All Products', 'New Arrivals', 'Best Sellers', 'Sale', 'Trending'],
    categories: ['Electronics', 'Fashion', 'Home & Garden', 'Beauty', 'Sports', 'Books'],
    company: ['About Us', 'Careers', 'Press', 'Blog', 'Affiliates'],
    support: ['Help Center', 'Contact Us', 'Shipping Info', 'Returns', 'FAQ', 'Size Guide'],
    legal: ['Privacy Policy', 'Terms of Service', 'Cookie Policy', 'Accessibility']
  };

  const features = [
    { icon: <Truck className="w-5 h-5" />, title: 'Free Shipping', desc: 'On orders over $50' },
    { icon: <CreditCard className="w-5 h-5" />, title: 'Secure Payment', desc: '100% secure transactions' },
    { icon: <Shield className="w-5 h-5" />, title: 'Quality Guarantee', desc: '30-day return policy' },
    { icon: <Headphones className="w-5 h-5" />, title: '24/7 Support', desc: 'Dedicated customer service' }
  ];

  return (
    <div className="min-h-screen bg-gray-50 flex flex-col">
      {/* Header */}
      <header className="sticky top-0 z-50 bg-white border-b border-gray-200 shadow-sm">
        <div className="max-w-7xl mx-auto px-4 py-3">
          <div className="flex items-center justify-between">
            <button 
              onClick={() => navigate('/')}
              className="p-2 hover:bg-gray-100 rounded-lg" 
              aria-label="Menu"
            >
              <Menu className="w-6 h-6" />
            </button>
            
            <h1 
              onClick={() => navigate('/')}
              className="text-2xl font-bold tracking-wider cursor-pointer"
            >
              Uzoma
            </h1>
            
            <div className="flex items-center gap-2">
              <button className="p-2 hover:bg-gray-100 rounded-lg" aria-label="Search">
                <Search className="w-6 h-6" />
              </button>
              
              <div className="relative">
                <button 
                  onClick={handleUserClick}
                  className="p-2 hover:bg-gray-100 rounded-lg flex items-center gap-2"
                  aria-label="User profile"
                >
                  {isAuthenticated && user ? (
                    <div className="w-8 h-8 bg-gray-900 text-white rounded-full flex items-center justify-center text-sm font-medium">
                      {user.name.charAt(0).toUpperCase()}
                    </div>
                  ) : (
                    <UserIcon className="w-6 h-6" />
                  )}
                </button>

                {showUserMenu && isAuthenticated && (
                  <div className="absolute right-0 mt-2 w-48 bg-white rounded-lg shadow-lg border border-gray-200 py-1 z-50">
                    <div className="px-4 py-2 border-b border-gray-100">
                      <p className="text-sm font-medium text-gray-900">{user?.name}</p>
                      <p className="text-xs text-gray-500 truncate">{user?.email}</p>
                    </div>
                    <button
                      onClick={() => navigate('/profile')}
                      className="w-full px-4 py-2 text-left text-sm text-gray-700 hover:bg-gray-50 flex items-center gap-2"
                    >
                      <UserIcon className="w-4 h-4" />
                      My Profile
                    </button>
                    <button
                      onClick={handleLogout}
                      className="w-full px-4 py-2 text-left text-sm text-red-600 hover:bg-gray-50 flex items-center gap-2"
                    >
                      <LogOut className="w-4 h-4" />
                      Sign out
                    </button>
                  </div>
                )}
              </div>
              
              <button className="relative p-2 hover:bg-gray-100 rounded-lg" aria-label="Shopping cart">
                <ShoppingCart className="w-6 h-6" />
                {cartCount > 0 && (
                  <span className="absolute top-0 right-0 bg-black text-white text-xs rounded-full w-5 h-5 flex items-center justify-center">
                    {cartCount}
                  </span>
                )}
              </button>
            </div>
          </div>
        </div>
      </header>

      {/* Main Content */}
      <main className="flex-grow">
        {children}
      </main>

      {/* Features Section */}
      <div className="bg-white border-y border-gray-200 py-6">
        <div className="max-w-7xl mx-auto px-4">
          <div className="grid grid-cols-2 md:grid-cols-4 gap-6">
            {features.map((feature, index) => (
              <div key={index} className="flex items-center gap-3">
                <div className="bg-gray-100 p-2 rounded-lg">
                  <div className="text-gray-900">
                    {feature.icon}
                  </div>
                </div>
                <div>
                  <p className="font-medium text-gray-900">{feature.title}</p>
                  <p className="text-sm text-gray-500">{feature.desc}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>

      {/* Footer */}
      <footer className="bg-gray-900 text-white">
        <div className="max-w-7xl mx-auto px-4 py-12">
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-8">
            {/* Brand Column */}
            <div className="lg:col-span-2">
              <h2 className="text-2xl font-bold mb-4">Uzoma</h2>
              <p className="text-gray-400 mb-6 max-w-md">
                Your premium destination for quality products. We bring you the best from around the world with exceptional customer service.
              </p>
              <div className="flex gap-4 mb-6">
                <a href="#" className="bg-gray-800 hover:bg-gray-700 p-2 rounded-lg transition-colors">
                  <Facebook className="w-5 h-5" />
                </a>
                <a href="#" className="bg-gray-800 hover:bg-gray-700 p-2 rounded-lg transition-colors">
                  <Twitter className="w-5 h-5" />
                </a>
                <a href="#" className="bg-gray-800 hover:bg-gray-700 p-2 rounded-lg transition-colors">
                  <Instagram className="w-5 h-5" />
                </a>
                <a href="#" className="bg-gray-800 hover:bg-gray-700 p-2 rounded-lg transition-colors">
                  <Youtube className="w-5 h-5" />
                </a>
              </div>
            </div>

            {/* Shop Links */}
            <div>
              <h3 className="font-semibold text-lg mb-4">Shop</h3>
              <ul className="space-y-2">
                {footerLinks.shop.map((link) => (
                  <li key={link}>
                    <a href="#" className="text-gray-400 hover:text-white transition-colors">
                      {link}
                    </a>
                  </li>
                ))}
              </ul>
            </div>

            {/* Support Links */}
            <div>
              <h3 className="font-semibold text-lg mb-4">Support</h3>
              <ul className="space-y-2">
                {footerLinks.support.map((link) => (
                  <li key={link}>
                    <a href="#" className="text-gray-400 hover:text-white transition-colors">
                      {link}
                    </a>
                  </li>
                ))}
              </ul>
            </div>

            {/* Contact Info */}
            <div>
              <h3 className="font-semibold text-lg mb-4">Contact Us</h3>
              <ul className="space-y-3">
                <li className="flex items-center gap-3 text-gray-400">
                  <Phone className="w-4 h-4 flex-shrink-0" />
                  <span>+1 (555) 123-4567</span>
                </li>
                <li className="flex items-center gap-3 text-gray-400">
                  <Mail className="w-4 h-4 flex-shrink-0" />
                  <span>support@uzoma.com</span>
                </li>
                <li className="flex items-start gap-3 text-gray-400">
                  <MapPin className="w-4 h-4 flex-shrink-0 mt-1" />
                  <span>123 Commerce Street<br />New York, NY 10001</span>
                </li>
              </ul>
            </div>
          </div>

          {/* Newsletter Section */}
          <div className="mt-12 pt-8 border-t border-gray-800">
            <div className="flex flex-col md:flex-row justify-between items-center gap-6">
              <div>
                <h3 className="text-lg font-semibold mb-2">Stay Updated</h3>
                <p className="text-gray-400">Subscribe to our newsletter for the latest offers</p>
              </div>
              <form className="flex gap-2 w-full md:w-auto">
                <input
                  type="email"
                  placeholder="Enter your email"
                  className="px-4 py-2 rounded-lg bg-gray-800 text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 flex-grow md:w-64"
                />
                <button
                  type="submit"
                  className="px-6 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded-lg font-medium transition-colors"
                >
                  Subscribe
                </button>
              </form>
            </div>
          </div>

          {/* Bottom Bar */}
          <div className="mt-12 pt-8 border-t border-gray-800">
            <div className="flex flex-col md:flex-row justify-between items-center gap-4">
              <p className="text-gray-400 text-sm">
                © {new Date().getFullYear()} Uzoma. All rights reserved.
              </p>
              <div className="flex flex-wrap gap-6">
                {footerLinks.legal.map((link) => (
                  <a 
                    key={link} 
                    href="#" 
                    className="text-gray-400 hover:text-white text-sm transition-colors"
                  >
                    {link}
                  </a>
                ))}
              </div>
             
            </div>
          </div>
        </div>
      </footer>

      {/* Bottom Navigation (Mobile) */}
      {showBottomNav && (
        <nav className="fixed bottom-0 left-0 right-0 bg-white border-t border-gray-200 md:hidden z-50">
          <div className="flex justify-around items-center py-3">
            <button 
              onClick={() => navigate('/')}
              className="flex flex-col items-center gap-1" 
              aria-label="Home"
            >
              <Menu className="w-6 h-6" />
              <span className="text-xs">Home</span>
            </button>
            <button className="flex flex-col items-center gap-1" aria-label="Search">
              <Search className="w-6 h-6" />
              <span className="text-xs">Search</span>
            </button>
            <button className="flex flex-col items-center gap-1" aria-label="Collection">
              <Heart className="w-6 h-6" />
              <span className="text-xs">Collection</span>
            </button>
            <button 
              onClick={() => navigate(isAuthenticated ? '/profile' : '/login')}
              className="flex flex-col items-center gap-1" 
              aria-label="Account"
            >
              <User className="w-6 h-6" />
              <span className="text-xs">Account</span>
            </button>
            <button className="flex flex-col items-center gap-1 relative" aria-label="Cart">
              <ShoppingCart className="w-6 h-6" />
              {cartCount > 0 && (
                <span className="absolute -top-1 -right-2 bg-black text-white text-xs rounded-full w-5 h-5 flex items-center justify-center">
                  {cartCount}
                </span>
              )}
              <span className="text-xs">Cart</span>
            </button>
          </div>
        </nav>
      )}
    </div>
  );
};