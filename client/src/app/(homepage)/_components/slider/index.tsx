'use client';

import Image from 'next/image';
import React from 'react';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import imageInHome from '@/assets/image/sliderInHome.jpg';
import styles from '@/app/(homepage)/_components/scss/slider.module.scss';

export default function SliderInHome() {
  const settings = {
    dots: true,
    infinite: false,
    arrow: false,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1
  };

  return (
    <>
      <div className='container-fluid mb-3 overflow_hidden'>
        <Slider {...settings}>
          <div className={`carousel-item position-relative height_slider`}>
            <Image
              className='position-absolute w-100 h-100'
              src={imageInHome.src}
              alt={'image in home'}
              fill={true}
              style={{ objectFit: 'cover' }}
            />
            <div className='carousel-caption d-flex flex-column align-items-center justify-content-center'>
              <div className='p-3'>
                <h1 className='display-4 text-white mb-3 animate__animated animate__fadeInDown'>
                  Men Fashion
                </h1>
                <p className='mx-md-5 px-5 animate__animated animate__bounceIn'>
                  Discover the latest trends in men&apos;s fashion. Shop the new
                  collection now.
                </p>
                <a
                  className='btn btn-outline-light py-2 px-4 mt-3 animate__animated animate__fadeInUp'
                  href='#'
                >
                  Shop Now
                </a>
              </div>
            </div>
          </div>

          <div className={`carousel-item position-relative height_slider`}>
            <Image
              className='position-absolute w-100 h-100'
              src={imageInHome.src}
              alt={'image in home'}
              fill={true}
              style={{ objectFit: 'cover' }}
            />
            <div className='carousel-caption d-flex flex-column align-items-center justify-content-center'>
              <div
                className='p-3'
                style={{ maxWidth: '700px' }}
              >
                <h1 className='display-4 text-white mb-3 animate__animated animate__fadeInDown'>
                  Women Fashion
                </h1>
                <p className='mx-md-5 px-5 animate__animated animate__bounceIn'>
                  Elevate your style with our exclusive women&apos;s fashion collection.
                </p>
                <a
                  className='btn btn-outline-light py-2 px-4 mt-3 animate__animated animate__fadeInUp'
                  href='#'
                >
                  Shop Now
                </a>
              </div>
            </div>
          </div>
        </Slider>
      </div>
    </>
  );
}
