'use client';

import { http } from '@/utils/config';
import { useEffect, useState } from 'react';
import {
  product,
  productRes
} from '@/app/(homepage)/shop/_components/@filterProduct/_components/type';
import { Rate } from 'antd';
import ImageNotFound from '@/assets/image/notFound.jpg';
import { BiSolidDetail } from 'react-icons/bi';
import { FaHeart } from 'react-icons/fa';
import { IoMdCart } from 'react-icons/io';
import ClientImageWithFallback from '@/components/ImageWithFallback';
import { useRouter } from 'next/navigation';

const FilterProduct = () => {
  const router = useRouter();
  const [productRes, setProductRes] = useState<productRes>();
  const [products, setProducts] = useState<product[]>([]);

  const fetchData = async () => {
    try {
      const res = await http.get('/products');
      if (res?.data.statusCode === 200) {
        setProductRes(res.data.dataResponse.paginationData);
        setProducts(res.data.dataResponse.paginationData.pageData ?? []);
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  function formatMoney(number: any) {
    return Number(number).toLocaleString('vi-VN');
  }

  return (
    <>
      <div className='row px-xl-5'>
        <div className='col-lg-3 col-md-4'>
          <h5 className='section-title position-relative text-uppercase mb-3'>
            <span className='bg-secondary pr-3'>Filter By Category</span>
          </h5>
          <div className='bg-light p-4 mb-30'>
            <form>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  checked
                  id='price-all'
                />
                <label
                  className='custom-control-label'
                  htmlFor='price-all'
                >
                  All Category
                </label>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='price-1'
                />
                <label
                  className='custom-control-label'
                  htmlFor='price-1'
                >
                  $0 - $100
                </label>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='price-2'
                />
                <label
                  className='custom-control-label'
                  htmlFor='price-2'
                >
                  $100 - $200
                </label>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='price-3'
                />
                <label
                  className='custom-control-label'
                  htmlFor='price-3'
                >
                  $200 - $300
                </label>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='price-4'
                />
                <label
                  className='custom-control-label'
                  htmlFor='price-4'
                >
                  $300 - $400
                </label>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='price-5'
                />
                <label
                  className='custom-control-label'
                  htmlFor='price-5'
                >
                  $400 - $500
                </label>
              </div>
            </form>
          </div>

          <h5 className='section-title position-relative text-uppercase mb-3'>
            <span className='bg-secondary pr-3'>Filter by Price</span>
          </h5>
          <div className='bg-light p-4 mb-30'>
            <form>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  checked
                  id='size-all'
                />
                <label
                  className='custom-control-label'
                  htmlFor='size-all'
                >
                  All Size
                </label>
                <span className='badge border font-weight-normal'>1000</span>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='size-1'
                />
                <label
                  className='custom-control-label'
                  htmlFor='size-1'
                >
                  XS
                </label>
                <span className='badge border font-weight-normal'>150</span>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='size-2'
                />
                <label
                  className='custom-control-label'
                  htmlFor='size-2'
                >
                  S
                </label>
                <span className='badge border font-weight-normal'>295</span>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='size-3'
                />
                <label
                  className='custom-control-label'
                  htmlFor='size-3'
                >
                  M
                </label>
                <span className='badge border font-weight-normal'>246</span>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='size-4'
                />
                <label
                  className='custom-control-label'
                  htmlFor='size-4'
                >
                  L
                </label>
                <span className='badge border font-weight-normal'>145</span>
              </div>
              <div className='custom-control custom-checkbox d-flex align-items-center justify-content-between'>
                <input
                  type='checkbox'
                  className='custom-control-input'
                  id='size-5'
                />
                <label
                  className='custom-control-label'
                  htmlFor='size-5'
                >
                  XL
                </label>
                <span className='badge border font-weight-normal'>168</span>
              </div>
            </form>
          </div>
        </div>

        <div className='col-lg-9 col-md-8'>
          <div className='row pb-3'>
            <div className='col-12 pb-1'>
              <div className='d-flex align-items-center justify-content-between mb-4'>
                <div>
                  <button className='btn btn-sm btn-light'>
                    <i className='fa fa-th-large'></i>
                  </button>
                  <button className='btn btn-sm btn-light ml-2'>
                    <i className='fa fa-bars'></i>
                  </button>
                </div>
                <div className='ml-2'>
                  <div className='btn-group'>
                    <button
                      type='button'
                      className='btn btn-sm btn-light dropdown-toggle'
                      data-toggle='dropdown'
                    >
                      Sorting
                    </button>
                    <div className='dropdown-menu dropdown-menu-right'>
                      <a
                        className='dropdown-item'
                        href='#'
                      >
                        Latest
                      </a>
                      <a
                        className='dropdown-item'
                        href='#'
                      >
                        Popularity
                      </a>
                      <a
                        className='dropdown-item'
                        href='#'
                      >
                        Best Rating
                      </a>
                    </div>
                  </div>
                  <div className='btn-group ml-2'>
                    <button
                      type='button'
                      className='btn btn-sm btn-light dropdown-toggle'
                      data-toggle='dropdown'
                    >
                      Showing
                    </button>
                    <div className='dropdown-menu dropdown-menu-right'>
                      <a
                        className='dropdown-item'
                        href='#'
                      >
                        10
                      </a>
                      <a
                        className='dropdown-item'
                        href='#'
                      >
                        20
                      </a>
                      <a
                        className='dropdown-item'
                        href='#'
                      >
                        30
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            {products &&
              products.map((product, index) => {
                return (
                  <>
                    <div
                      key={index}
                      className='col-lg-4 col-md-6 col-sm-6 pb-1'
                    >
                      <div className='product-item bg-light mb-4'>
                        <div className='product-Image position-relative overflow-hidden'>
                          <ClientImageWithFallback
                            src={ImageNotFound.src ?? ImageNotFound.src}
                            alt={'Error Image'}
                            fallbackSrc={ImageNotFound.src}
                            className='Image-fluid w-100'
                          />
                          <div className='product-action'>
                            <button className='btn btn-outline-dark btn-square'>
                              <IoMdCart />
                            </button>
                            <button className='btn btn-outline-dark btn-square'>
                              <FaHeart />
                            </button>
                            <button
                              className='btn btn-outline-dark btn-square'
                              onClick={() => router.push(`shop/${product.productId}`)}
                            >
                              <BiSolidDetail />
                            </button>
                          </div>
                        </div>
                        <div className='text-center py-4'>
                          <a
                            className='h6 text-decoration-none text-truncate'
                            href=''
                          >
                            {product.name}
                          </a>
                          <div className='d-flex align-items-center justify-content-center mt-2'>
                            <h5>{formatMoney(product.oldPrice)}</h5>
                            <h6 className='text-muted ml-2'>
                              <del>{formatMoney(product.exportPrice)}</del>
                            </h6>
                          </div>
                          <div className='d-flex align-items-center justify-content-center mb-1'>
                            <Rate
                              className='text-primary mr-1'
                              defaultValue={4.3}
                              disabled
                            ></Rate>
                            {/* <FaStar className='text-primary mr-1' />
                          <FaStar className='text-primary mr-1' />
                          <FaStar className='text-primary mr-1' />
                          <FaStar className='text-primary mr-1' />
                          <FaStar className='text-primary mr-1' /> */}
                            {/* <small>(99)</small> */}
                          </div>
                        </div>
                      </div>
                    </div>
                  </>
                );
              })}

            <div className='col-12'>
              <nav>
                <ul className='pagination justify-content-center'>
                  <li className='page-item disabled'>
                    <a
                      className='page-link'
                      href='#'
                    >
                      <span>Previous</span>
                    </a>
                  </li>
                  <li className='page-item active'>
                    <a
                      className='page-link'
                      href='#'
                    >
                      1
                    </a>
                  </li>
                  <li className='page-item'>
                    <a
                      className='page-link'
                      href='#'
                    >
                      2
                    </a>
                  </li>
                  <li className='page-item'>
                    <a
                      className='page-link'
                      href='#'
                    >
                      3
                    </a>
                  </li>
                  <li className='page-item'>
                    <a
                      className='page-link'
                      href='#'
                    >
                      Next
                    </a>
                  </li>
                </ul>
              </nav>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};
export default FilterProduct;
