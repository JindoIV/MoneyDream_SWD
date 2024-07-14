'use client';
import { Badge, Button, Dropdown, Input, MenuProps, Modal, Space, Tag } from 'antd';
import { ColumnsType, TableProps } from 'antd/es/table';
// import { AccountModel } from './models';

import { EllipsisOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { MdAdd, MdDisabledByDefault } from 'react-icons/md';
import { ProductCart } from '@/app/(homepage)/user/cart/_components/type';
import { FaMinus, FaTimes } from 'react-icons/fa';
import { formatMoney, formatQuantity } from '@/utils/configFormat';
import { useState } from 'react';
import InputQuantity from '@/components/InputQuantity';

export const productCartColumn: ColumnsType<ProductCart> = [
  {
    dataIndex: 'productId',
    key: 'productId',
    render: () => {
      return <></>;
    }
  },
  {
    align: 'left',
    title: 'Product Name',
    dataIndex: 'name'
  },
  {
    align: 'left',
    title: 'Size',
    dataIndex: 'sizee',
    render: (_, record) => {
      return <>{record.size.productWidth}</>;
    }
  },
  {
    align: 'left',
    title: 'Price',
    dataIndex: 'oldPrice',
    render: (_, record) => {
      return <>{formatMoney(record.oldPrice)}</>;
    }
  },
  {
    align: 'center',
    title: 'Quantity',
    dataIndex: 'quantityy',
    render: (_, record) => {
      return (
        <>
          <InputQuantity
            className='form-control form-control-sm bg-secondary border-0 text-center'
            min={1}
            max={99}
            defaultValues={record.quantity}
            productId={record.productId}
            updateCart={record.updateCart}
          ></InputQuantity>
        </>
      );
    }
  },
  {
    align: 'left',
    title: 'Total',
    dataIndex: 'total',
    render: (_, record) => {
      return <>{formatMoney(record.oldPrice * record.quantity)}</>;
    }
  },
  {
    align: 'center',
    title: 'Remove',
    dataIndex: 'remove',
    render: (_, record) => {
      return (
        <>
          <button className='btn btn-sm btn-danger'>
            <FaTimes />
          </button>
        </>
      );
    }
  }
];
