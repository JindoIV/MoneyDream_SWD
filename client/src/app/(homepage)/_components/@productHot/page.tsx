'use client';
import {
  product,
  productRes
} from '@/app/(homepage)/_components/@productList/_components/types';
import { http } from '@/utils/config';
import { Image, Rate } from 'antd';
import { useEffect, useState } from 'react';
import ImageNotFound from '@/assets/image/notFound.jpg';
import { FaHeart, FaStar } from 'react-icons/fa';
import { BiSolidDetail } from 'react-icons/bi';
import { IoMdCart } from 'react-icons/io';
import { formatMoney } from '@/utils/configFormat';
import ClientImageWithFallback from '@/components/ImageWithFallback';
import { useRouter } from 'next/navigation';

export default function ProductHot() {
  const router = useRouter();
  const [productRes, setProductRes] = useState<productRes>();
  const [products, setProducts] = useState<product[]>([]);

  const fetchData = async () => {
    try {
      const res = await http.get('/products');
      if (res?.data.statusCode === 200) {
        setProductRes(res.data.dataResponse);
        setProducts(res.data.dataResponse.pageData.slice(0, 7) ?? []);
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <>
      <div className='container-fluid pt-5 pb-3'>
        <h2 className='section-title position-relative text-uppercase mx-xl-5 mb-4'>
          <span className='bg-secondary pr-3'>Sale Products</span>
        </h2>
        <div className='row px-xl-5'>
          {products &&
            products.map(product => {
              return (
                <>
                  <div className='col-lg-3 col-md-4 col-sm-6 pb-1'>
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
        </div>
      </div>
    </>
  );
}
