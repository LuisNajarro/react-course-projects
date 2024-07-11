import { configureStore } from '@reduxjs/toolkit';

import counterReducer from './counter.js';
import authReducer from './auth.js';

const store = configureStore({
  reducer: { counter: counterReducer, auth: authReducer },
});

export default store;
