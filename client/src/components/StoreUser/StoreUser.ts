'use client';

import { setUser } from '@/libs/features/userInfo/userInfo';
import { userInfo } from '@/types/userInfo';
import { useDispatch } from 'react-redux';

export const StoreUser = (user: userInfo) => {
  const dispatch = useDispatch();
  dispatch(setUser(user));
};
