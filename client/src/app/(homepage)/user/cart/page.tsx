'use client';

import { productCartColumn } from '@/app/(homepage)/user/cart/_components/comlumnTypes';
import { ProductCart } from '@/app/(homepage)/user/cart/_components/type';
import {
  initProductInCart,
  setProductInCart
} from '@/libs/features/productInCart/productInCartSlide';
import UseAxiosAuth from '@/utils/axiosClient';
import { http } from '@/utils/config';
import { formatMoney } from '@/utils/configFormat';
import { Table } from 'antd';
import { getSession } from 'next-auth/react';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import { FaMinus, FaTimes } from 'react-icons/fa';
import { IoMdAdd } from 'react-icons/io';
import { MdAdd } from 'react-icons/md';
import { TiDelete } from 'react-icons/ti';
import { useDispatch, useSelector } from 'react-redux';

const CartPage = () => {
  const dispatch = useDispatch();
  const user = useSelector((state: any) => state.useinfo.userinfo);
  const productsInCart = useSelector((state: any) => state.productInCart.products);

  const router = useRouter();
  const instance = UseAxiosAuth();
  const [products, setProducts] = useState<ProductCart[]>([]);

  const [selectedProducts, setSelectedProducts] = useState<ProductCart[]>([]);

  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  const [totalPrice, setTotalPrice] = useState<number>(0);
  const [endPrice, setEndPrice] = useState<number>(0);

  const onSelectChange = (newSelectedRowKeys: React.Key[]) => {
    console.log('selectedRowKeys changed: ', newSelectedRowKeys);

    if (newSelectedRowKeys.length > 0) {
      const _selectedProducts = [];
      let total = 0;
      for (const key of newSelectedRowKeys) {
        const id = key.toString();
        const selectProduct = products.find(
          product => product.productId.toString() === id.toString()
        );
        if (selectProduct) {
          _selectedProducts.push(selectProduct);
          total += selectProduct.oldPrice * selectProduct.quantity;
        }
      }
      setSelectedProducts(_selectedProducts);
      setTotalPrice(total);
      setEndPrice(total);
    } else {
      setSelectedProducts([]);
      setTotalPrice(0);
      setEndPrice(0);
    }

    setSelectedRowKeys(newSelectedRowKeys);
  };

  const rowSelection = {
    selectedRowKeys,
    onChange: onSelectChange
  };

  const fetchData = async () => {
    try {
      const useSession = await getSession();
      const res = await instance.get(
        `/getProductsInCart?accountID=${useSession?.user.Id}`
      );
      if (res?.data.statusCode === 200) {
        setProducts(res.data.dataResponse ?? []);
      } else {
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  const handleChangeQuantity = async (_productId: string, _quantity: number) => {
    try {
      console.log(user);
      const useSession = await getSession();
      const promise = instance.post(`/EditQuantityInCart`, {
        acccountID: useSession?.user.Id,
        productID: _productId,
        quantity: _quantity
      });

      toast.promise(promise, {
        loading: 'Change item in cart...',
        success: 'Update item to cart successfully',
        error: 'Failed pdate item to cart'
      });

      const res = await promise;

      if (res?.data.statusCode === 200) {
        fetchData();
      } else {
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchData();
    dispatch(initProductInCart());
  }, []);

  return (
    <>
      <div className='container-fluid'>
        <div className='row px-xl-5'>
          <div className='col-12'>
            <nav className='breadcrumb bg-light mb-30'>
              <a
                className='breadcrumb-item text-dark'
                href='#'
              >
                Home
              </a>
              <a
                className='breadcrumb-item text-dark'
                href='#'
              >
                Shop
              </a>
              <span className='breadcrumb-item active'>Shopping Cart</span>
            </nav>
          </div>
        </div>
      </div>

      <div className='container-fluid'>
        <div className='row px-xl-5'>
          <div className='col-lg-8  mb-5'>
            <Table
              // loading={isLoading}
              rowKey='productId'
              rowSelection={rowSelection}
              onRow={record => ({
                onClick: event => {
                  const target = event.target as HTMLElement;
                  const isWithinLink = target.tagName === 'A' || target.closest('a');
                  const isWithinAction =
                    target.closest('td')?.classList.contains('ant-table-cell') &&
                    !target
                      .closest('td')
                      ?.classList.contains('ant-table-selection-column') &&
                    !target.closest('td')?.classList.contains('ant-table-cell-fix-right');

                  if (isWithinAction && !isWithinLink) {
                    // handleUpdate(record.id);
                  }
                }
              })}
              columns={productCartColumn}
              dataSource={products.map((product: ProductCart) => ({
                ...product,
                updateCart: handleChangeQuantity
              }))}
              // pagination={{
              //   ...pagination,
              //   onChange: (page, pageSize) => handlePagination(page),
              //   onShowSizeChange: (_, size) => handlePagination(),
              //   showSizeChanger: true,
              //   pageSizeOptions: ['5']
              //   // showTotal: (total, range) =>
              //   //   {
              //   //     from: range[0],
              //   //     to: range[1],
              //   //     total: total,
              //   //   }),
              // }}
            />

            {/* <table className='table table-light table-borderless table-hover text-center mb-0'>
              <thead className='thead-dark'>
                <tr>
                  <th>Products</th>
                  <th>Price</th>
                  <th>Quantity</th>
                  <th>Total</th>
                  <th>Remove</th>
                </tr>
              </thead>
              <tbody className='align-middle'>
                {products.map((product, index) => {
                  return (
                    <>
                      <tr key={index}>
                        <td className='align-middle'>
                          <img
                            src='img/product-1.jpg'
                            alt=''
                            style={{ width: '50px' }}
                          />{' '}
                          {product.name}
                        </td>
                        <td className='align-middle'>$150</td>
                        <td className='align-middle'>
                          <div
                            className='input-group quantity mx-auto'
                            style={{ width: '100px' }}
                          >
                            <div className='input-group-btn'>
                              <button className='btn btn-sm btn-primary btn-minus'>
                                <FaMinus />
                              </button>
                            </div>
                            <input
                              type='text'
                              className='form-control form-control-sm bg-secondary border-0 text-center'
                              value={1}
                            />
                            <div className='input-group-btn'>
                              <button className='btn btn-sm btn-primary btn-plus'>
                                <MdAdd />
                              </button>
                            </div>
                          </div>
                        </td>
                        <td className='align-middle'>$150</td>
                        <td className='align-middle'>
                          <button className='btn btn-sm btn-danger'>
                            <FaTimes />
                          </button>
                        </td>
                      </tr>
                    </>
                  );
                })}
              </tbody>
            </table> */}
          </div>
          <div className='col-lg-4'>
            <form
              className='mb-30'
              action=''
            >
              {/* <div className='input-group'>
                <input
                  type='text'
                  className='form-control border-0 p-4'
                  placeholder='Coupon Code'
                />
                <div className='input-group-append'>
                  <button className='btn btn-primary'>Apply Coupon</button>
                </div>
              </div> */}
            </form>
            <h5 className='section-title position-relative text-uppercase mb-3'>
              <span className='bg-secondary pr-3'>Cart Summary</span>
            </h5>
            <div className='bg-light p-30 mb-5'>
              <div className='border-bottom pb-2'>
                <div className='d-flex justify-content-between mb-3'>
                  <h6>Subtotal</h6>
                  <h6>{formatMoney(totalPrice)}</h6>
                </div>
                <div className='d-flex justify-content-between'>
                  <h6 className='font-weight-medium'>Shipping</h6>
                  <h6 className='font-weight-medium'>$10</h6>
                </div>
              </div>
              <div className='pt-2'>
                <div className='d-flex justify-content-between mt-2'>
                  <h5>Total</h5>
                  <h5>{formatMoney(endPrice)}</h5>
                </div>
                <button
                  className='btn btn-block btn-primary font-weight-bold my-3 py-3'
                  onClick={() => {
                    dispatch(setProductInCart(selectedProducts));
                    router.push(`/user/checkout`);
                  }}
                >
                  Proceed To Checkout
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default CartPage;
