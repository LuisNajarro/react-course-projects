import React from 'react';
import { Route } from 'react-router-dom';

import Navigation from './components/Nav/Navigation.jsx';
import ProductsPage from './containers/Products.jsx';
import FavoritesPage from './containers/Favorites.jsx';

const App = (props) => {
  return (
    <React.Fragment>
      <Navigation />
      <main>
        <Route path="/" component={ProductsPage} exact />
        <Route path="/favorites" component={FavoritesPage} />
      </main>
    </React.Fragment>
  );
};

export default App;
