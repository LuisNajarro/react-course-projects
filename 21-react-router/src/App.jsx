import {
  createBrowserRouter,
  // createRoutesFromElements,
  RouterProvider,
  // Route,
} from 'react-router-dom';

import HomePage from './pages/Home.jsx';
import ProductsPage from './pages/Products.jsx';
import RootLayout from './pages/Root.jsx';
import ErrorPage from './pages/Error.jsx';
import ProductDetailPage from './pages/ProductDetail.jsx';

// const routeDefinitions = createRoutesFromElements(
//   <Route>
//     <Route path="/" element={<HomePage />} />
//     <Route path="/products" element={<ProductsPage />} />
//   </Route>
// );

const router = createBrowserRouter([
  {
    path: '/',
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    children: [
      { path: '/', element: <HomePage /> },
      { path: '/products', element: <ProductsPage /> },
      { path: '/products/:productId', element: <ProductDetailPage /> },
    ],
  },
]);

// const router = createBrowserRouter(routeDefinitions);

function App() {
  return <RouterProvider router={router} />;
}

export default App;
