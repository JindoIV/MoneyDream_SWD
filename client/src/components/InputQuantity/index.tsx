'use client';

import { formatQuantity } from '@/utils/configFormat';
import { Input, InputProps } from 'antd';
import { useEffect, useState } from 'react';
import { FaMinus } from 'react-icons/fa';
import { MdAdd } from 'react-icons/md';

type InputQuantity = InputProps & {
  min: number;
  max: number;
  defaultValues: number;
  productId: string;
  updateCart: (productId: string, inputNumber: number) => void;
};

const InputQuantity = ({
  min,
  max,
  defaultValues,
  productId,
  updateCart,
  ...props
}: InputQuantity) => {
  const [inputNumber, setInputNumber] = useState<number>(defaultValues);

  const handleChangeNumber = (event: any) => {
    setInputNumber(formatQuantity(event.target.value));
  };

  return (
    <>
      <div
        className='input-group quantity mx-auto'
        style={{ width: '100px' }}
      >
        <div className='input-group-btn'>
          <button
            className='btn btn-sm btn-primary btn-minus'
            onClick={() => {
              updateCart(productId, formatQuantity(inputNumber - 1));
              setInputNumber(formatQuantity(inputNumber - 1));
            }}
          >
            <FaMinus />
          </button>
        </div>
        <Input
          type='number'
          onChange={handleChangeNumber}
          value={inputNumber}
          onBlur={() => {
            updateCart(productId, inputNumber);
          }}
          {...props}
        ></Input>
        <div className='input-group-btn'>
          <button
            className='btn btn-sm btn-primary btn-plus'
            onClick={() => {
              updateCart(productId, formatQuantity(inputNumber + 1));
              setInputNumber(formatQuantity(inputNumber + 1));
            }}
          >
            <MdAdd />
          </button>
        </div>
      </div>
    </>
  );
};

export default InputQuantity;
