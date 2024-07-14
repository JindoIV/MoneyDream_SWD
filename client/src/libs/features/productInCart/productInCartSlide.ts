'use client';

import { productCart } from '@/types/productInCart';
import { createSlice } from '@reduxjs/toolkit';
import { stat } from 'fs';

export interface ProductInCartState {
  products: productCart[];
}

const initialState: ProductInCartState = {
  products: []
};

export const productInCartSlice = createSlice({
  name: 'productInCart',
  initialState,
  reducers: {
    setProductInCart: (state, action) => {
      state.products = action.payload;
    },
    addProductInCart: (state, action) => {
      state.products.push(action.payload);
    },
    initProductInCart: state => {
      state.products = [];
    }
  }
});

export const { setProductInCart, addProductInCart, initProductInCart } =
  productInCartSlice.actions;

export default productInCartSlice.reducer;
