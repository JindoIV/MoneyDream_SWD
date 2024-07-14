// "use client";

import { configureStore, combineReducers } from '@reduxjs/toolkit';
import scoreReducer from '@/libs/features/score/scoreSlide';
import productInCartReducer from '@/libs/features/productInCart/productInCartSlide';
import userInfoReducer from '@/libs/features/userInfo/userInfo';
import storage from 'redux-persist/lib/storage';
import { persistReducer } from 'redux-persist';
import persistStore from 'redux-persist/es/persistStore';

const persistConfig = {
  key: 'root',
  storage
};

const rootReducer = combineReducers({
  score: scoreReducer,
  productInCart: productInCartReducer,
  userinfo: userInfoReducer
});

const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
  reducer: persistedReducer,
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware({
      serializableCheck: false
    })
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export const persistor = persistStore(store);
