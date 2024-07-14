'use client';

import SliderInHome from '@/app/(homepage)/_components/slider';
import Advertisement from '@/app/(homepage)/_components/advertisement';
import ProductRecent from '@/app/(homepage)/_components/productRecent';
import PolicyList from '@/app/(homepage)/_components/policyList';
import { UpOutlined } from '@ant-design/icons';
import { MutableRefObject, useEffect, useRef } from 'react';
import ProductFeature from '@/app/(homepage)/_components/@productList/page';
import ProductHot from '@/app/(homepage)/_components/@productHot/page';

const HomePage = () => {
  const buttonBackToTop = useRef<HTMLButtonElement | null>(null);

  useEffect(() => {
    const handleScroll = () => {
      if (buttonBackToTop.current) {
        if (document.documentElement.scrollTop > 20) {
          buttonBackToTop.current.classList.remove('fade-out');
          buttonBackToTop.current.classList.add('fade-in');
          setTimeout(() => {
            if (buttonBackToTop.current) {
              buttonBackToTop.current.style.display = 'block';
            }
          }, 300);
        } else {
          buttonBackToTop.current.classList.remove('fade-in');
          buttonBackToTop.current.classList.add('fade-out');
          setTimeout(() => {
            if (buttonBackToTop.current) {
              buttonBackToTop.current.style.display = 'none';
            }
          }, 300);
        }
      }
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  const backToTop = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  return (
    <>
      <SliderInHome></SliderInHome>

      <PolicyList></PolicyList>

      <ProductFeature></ProductFeature>

      <Advertisement></Advertisement>

      <ProductHot></ProductHot>

      <button
        type='button'
        className='btn back-to-top'
        id='btn_back_to_top'
        ref={buttonBackToTop}
        onClick={backToTop}
      >
        <UpOutlined />
      </button>
    </>
  );
};

export default HomePage;
