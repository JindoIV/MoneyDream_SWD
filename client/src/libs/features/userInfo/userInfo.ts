'use client';

import { userInfo } from '@/types/userInfo';
import { createSlice } from '@reduxjs/toolkit';
import { stat } from 'fs';

export interface UserState {
  userinfo: userInfo;
}

const initialState: UserState = {
  userinfo: {
    accountId: '',
    picture: '',
    email: '',
    roleId: 0
  }
};

export const userInfoSlice = createSlice({
  name: 'userinfo',
  initialState,
  reducers: {
    setUser: (state, action) => {
      state.userinfo = action.payload;
    },
    initUser: state => {
      state.userinfo = initialState.userinfo;
    }
  }
});

export const { setUser, initUser } = userInfoSlice.actions;

export default userInfoSlice.reducer;
