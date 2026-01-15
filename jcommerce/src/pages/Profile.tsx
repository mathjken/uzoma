import { Layout } from '../Components/Layout';
import { useAuth } from '../hooks/useAuth';
import { User } from 'lucide-react';

const Profile = () => {
  const { user } = useAuth();

  if (!user) {
    return null;
  }

  return (
    <Layout>
      <div className="max-w-4xl mx-auto px-4 py-8">
        <div className="bg-white rounded-2xl shadow-lg border border-gray-100 overflow-hidden">
          {/* Profile Header */}
          <div className="bg-gray-900 p-8 text-white">
            <div className="flex items-center gap-6">
              <div className="w-24 h-24 bg-white text-gray-900 rounded-full flex items-center justify-center text-3xl font-bold">
                {user.name.charAt(0).toUpperCase()}
              </div>
              <div>
                <h1 className="text-3xl font-bold">{user.name}</h1>
                <p className="text-gray-300 mt-1">{user.email}</p>
                <div className="flex items-center gap-4 mt-4">
                  <span className={`px-3 py-1 rounded-full text-sm font-medium ${user.role === 'Admin' ? 'bg-red-500' : 'bg-blue-500'}`}>
                    {user.role}
                  </span>
                  <span className="text-gray-300 text-sm">
                    Member since {new Date(user.createdAt).toLocaleDateString()}
                  </span>
                </div>
              </div>
            </div>
          </div>

          {/* Profile Details */}
          <div className="p-8">
            <h2 className="text-2xl font-bold text-gray-900 mb-6">Account Information</h2>
            
            <div className="grid md:grid-cols-2 gap-6">
              <div className="bg-gray-50 rounded-xl p-6">
                <div className="flex items-center gap-3 mb-4">
                  <User className="w-5 h-5 text-gray-600" />
                  <h3 className="font-semibold text-gray-900">Personal Info</h3>
                </div>
                <dl className="space-y-3">
                  <div>
                    <dt className="text-sm text-gray-500">Full Name</dt>
                    <dd className="font-medium text-gray-900">{user.name}</dd>
                  </div>
                  <div>
                    <dt className="text-sm text-gray-500">Email Address</dt>
                    <dd className="font-medium text-gray-900">{user.email}</dd>
                  </div>
                </dl>
              </div>
            </div>
          </div>
        </div>
      </div>
    </Layout>
  );
};

export default Profile;