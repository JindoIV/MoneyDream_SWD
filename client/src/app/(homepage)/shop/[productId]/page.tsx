'use client';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import ImageNotFound from '@/assets/image/notFound.jpg';
// import { Image } from 'antd';
import Image from 'next/image';
import './_components/sliderCustom/sliderCss.scss';
import { IoMdAdd } from 'react-icons/io';
import { FaFacebook, FaLinkedin, FaMinus, FaPinterest } from 'react-icons/fa';
import { http } from '@/utils/config';
import {
  productDetail,
  productSize
} from '@/app/(homepage)/shop/[productId]/_components/type';
import { CSSProperties, useEffect, useState } from 'react';
import ClientImageWithFallback from '@/components/ImageWithFallback';
import { Rate } from 'antd';
import { formatMoney } from '@/utils/configFormat';
import { FaSquareXTwitter } from 'react-icons/fa6';
import UseAxiosAuth from '@/utils/axiosClient';
import { getSession } from 'next-auth/react';
import toast from 'react-hot-toast';

const ShopDetailId = ({ params }: { params: { productId: string } }) => {
  const instace = UseAxiosAuth();
  const [productDetail, setProductDetail] = useState<productDetail>();
  const [productSize, setProductSize] = useState<productSize[]>([]);

  const [quantity, setQuantity] = useState<number>(1);
  const fetchData = async () => {
    try {
      const res = await http.get(`/product?id=${params.productId}`);
      if (res?.data.statusCode === 200) {
        setProductDetail(res.data.dataResponse);
        setProductSize(res.data.dataResponse.size);
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const settings = {
    customPaging: function (i: any) {
      return (
        <a style={{ width: '50px', height: '50px' }}>
          <ClientImageWithFallback
            src={productDetail?.proImages[i].image ?? ImageNotFound.src}
            alt={'Error Image'}
            width={50}
            height={50}
            fallbackSrc={ImageNotFound.src}
          />
        </a>
      );
    },
    dots: true,
    dotsClass: 'slick-dots slick-thumb my-3',
    arrows: false,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1
  };

  const changeQuantity = (_plus: boolean, _quantity: number = 1) => {
    if (quantity === 1 && !_plus) return;

    if (_plus) {
      setQuantity(quantity + 1);
    } else {
      setQuantity(quantity - 1);
    }
  };

  const handleAddToCart = async () => {
    try {
      const useSession = await getSession();
      const promise = instace.post(`/addToCart`, {
        acccountID: useSession?.user.Id,
        productID: params.productId,
        quantity: quantity
      });

      toast.promise(promise, {
        loading: 'Adding item to cart...',
        success: 'Item added to cart successfully',
        error: 'Failed to add item to cart'
      });

      const res = await promise;

      if (res?.data.statusCode === 200) {
      } else {
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  return (
    <>
      <div className='container-fluid'>
        <div className='row px-xl-5'>
          <div className='col-12'>
            <nav className='breadcrumb bg-light mb-30'>
              <a
                className='breadcrumb-item text-dark'
                href='/'
              >
                Home
              </a>
              <a
                className='breadcrumb-item text-dark'
                href='Shop'
              >
                Shop
              </a>
              <span className='breadcrumb-item active'>Shop Detail</span>
            </nav>
          </div>
        </div>
      </div>

      <div className='container-fluid pb-5'>
        <div className='row px-xl-5'>
          <div className='col-lg-5 mb-30'>
            <div className='slider-container'>
              <Slider {...settings}>
                {productDetail?.proImages &&
                  productDetail.proImages.map((_value, index) => {
                    return (
                      <>
                        <div key={index}>
                          <ClientImageWithFallback
                            src={
                              productDetail?.proImages[index].image ?? ImageNotFound.src
                            }
                            alt={'Error Image'}
                            fallbackSrc={ImageNotFound.src}
                            className='w-100 h-100'
                          />
                          {/* <img
                            className='w-100 h-100'
                            src={productDetail.imageUrl[index] ?? ImageNotFound.src}
                          /> */}
                        </div>
                      </>
                    );
                  })}
              </Slider>
              {/* </div> */}
              {/* </div> */}
              {/* <a
                className='carousel-control-prev'
                href='#product-carousel'
                data-slide='prev'
              >
                <i className='fa fa-2x fa-angle-left text-dark'></i>
              </a>
              <a
                className='carousel-control-next'
                href='#product-carousel'
                data-slide='next'
              >
                <i className='fa fa-2x fa-angle-right text-dark'></i>
              </a> */}
            </div>
          </div>

          <div className='col-lg-7 h-auto mb-30'>
            <div className='h-120 bg-light p-30'>
              <h3>{productDetail?.name}</h3>
              <div className='d-flex mb-3'>
                <div className='text-primary mr-2'>
                  <Rate
                    defaultValue={4}
                    disabled
                  ></Rate>
                </div>
                <small className='pt-1'>(99 Reviews)</small>
              </div>
              <h3 className='font-weight-semi-bold mb-4'>
                {formatMoney(productDetail?.exportPrice)} VNĐ
              </h3>
              {/* <p className='mb-4'>
                Volup erat ipsum diam elitr rebum et dolor. Est nonumy elitr erat diam
                stet sit clita ea. Sanc ipsum et, labore clita lorem magna duo dolor no
                sea Nonumy
              </p> */}

              {/* <div className='d-flex mb-3'>
                <strong className='text-dark mr-3'>Sizes:</strong>
                <form>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='size-1'
                      name='size'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='size-1'
                    >
                      XS
                    </label>
                  </div>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='size-2'
                      name='size'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='size-2'
                    >
                      S
                    </label>
                  </div>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='size-3'
                      name='size'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='size-3'
                    >
                      M
                    </label>
                  </div>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='size-4'
                      name='size'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='size-4'
                    >
                      L
                    </label>
                  </div>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='size-5'
                      name='size'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='size-5'
                    >
                      XL
                    </label>
                  </div>
                </form>
              </div>
              <div className='d-flex mb-4'>
                <strong className='text-dark mr-3'>Colors:</strong>
                <form>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='color-1'
                      name='color'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='color-1'
                    >
                      Black
                    </label>
                  </div>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='color-2'
                      name='color'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='color-2'
                    >
                      White
                    </label>
                  </div>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='color-3'
                      name='color'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='color-3'
                    >
                      Red
                    </label>
                  </div>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='color-4'
                      name='color'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='color-4'
                    >
                      Blue
                    </label>
                  </div>
                  <div className='custom-control custom-radio custom-control-inline'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      id='color-5'
                      name='color'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='color-5'
                    >
                      Green
                    </label>
                  </div>

                </form>
              </div> */}
              <div className='d-flex align-items-center mb-4 pt-2'>
                <div
                  className='input-group quantity mr-3'
                  style={{ width: '130px' }}
                >
                  <div className='input-group-btn'>
                    <button
                      className='btn btn-primary btn-minus'
                      onClick={() => changeQuantity(false)}
                    >
                      <FaMinus />
                    </button>
                  </div>
                  <input
                    type='text'
                    className='form-control bg-secondary border-0 text-center'
                    value={quantity}
                  />
                  <div className='input-group-btn'>
                    <button
                      className='btn btn-primary btn-plus'
                      onClick={() => changeQuantity(true)}
                    >
                      <IoMdAdd />
                    </button>
                  </div>
                </div>
                <button
                  className='btn btn-primary px-3'
                  onClick={handleAddToCart}
                >
                  <IoMdAdd className='mr-1' /> Add To Cart
                </button>
              </div>
              <div className='d-flex pt-2'>
                <strong className='text-dark mr-2'>Share on:</strong>
                <div className='d-inline-flex'>
                  <a
                    className='text-dark px-2'
                    href=''
                  >
                    <FaFacebook />
                  </a>
                  <a
                    className='text-dark px-2'
                    href=''
                  >
                    <FaSquareXTwitter />
                  </a>
                  <a
                    className='text-dark px-2'
                    href=''
                  >
                    <FaLinkedin />
                  </a>
                  <a
                    className='text-dark px-2'
                    href=''
                  >
                    <FaPinterest />
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className='container-fluid py-5'>
        <h2 className='section-title position-relative text-uppercase mx-xl-5 mb-4'>
          <span className='bg-secondary pr-3'>You May Also Like</span>
        </h2>
        <div className='row px-xl-5'>
          <div className='col'>
            <div className='owl-carousel related-carousel'>
              <div className='product-item bg-light'>
                <div className='product-img position-relative overflow-hidden'>
                  <img
                    className='img-fluid w-100'
                    src='img/product-2.jpg'
                    alt=''
                  />
                  <div className='product-action'>
                    <a
                      className='btn btn-outline-dark btn-square'
                      href=''
                    >
                      <i className='fa fa-shopping-cart'></i>
                    </a>
                    <a
                      className='btn btn-outline-dark btn-square'
                      href=''
                    >
                      <i className='far fa-heart'></i>
                    </a>
                    <a
                      className='btn btn-outline-dark btn-square'
                      href=''
                    >
                      <i className='fa fa-sync-alt'></i>
                    </a>
                    <a
                      className='btn btn-outline-dark btn-square'
                      href=''
                    >
                      <i className='fa fa-search'></i>
                    </a>
                  </div>
                </div>
                <div className='text-center py-4'>
                  <a
                    className='h6 text-decoration-none text-truncate'
                    href=''
                  >
                    Product Name Goes Here
                  </a>
                  <div className='d-flex align-items-center justify-content-center mt-2'>
                    <h5>$123.00</h5>
                    <h6 className='text-muted ml-2'>
                      <del>$123.00</del>
                    </h6>
                  </div>
                  <div className='d-flex align-items-center justify-content-center mb-1'>
                    <small className='fa fa-star text-primary mr-1'></small>
                    <small className='fa fa-star text-primary mr-1'></small>
                    <small className='fa fa-star text-primary mr-1'></small>
                    <small className='fa fa-star text-primary mr-1'></small>
                    <small className='fa fa-star text-primary mr-1'></small>
                    <small>(99)</small>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ShopDetailId;
