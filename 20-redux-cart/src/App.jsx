import { useEffect } from 'react';
import { useSelector } from 'react-redux';

import Cart from './components/Cart/Cart.jsx';
import Layout from './components/Layout/Layout.jsx';
import Products from './components/Shop/Products.jsx';

const firebaseUrl = import.meta.env.VITE_FIREBASE_URL;

function App() {
  const showCart = useSelector((state) => state.ui.cartIsVisible);
  const cart = useSelector((state) => state.cart);

  useEffect(() => {
    fetch(`${firebaseUrl}/cart.json`, {
      method: 'PUT',
      body: JSON.stringify(cart),
    });
  }, [cart]);

  return (
    <Layout>
      {showCart && <Cart />}
      <Products />
    </Layout>
  );
}

export default App;
