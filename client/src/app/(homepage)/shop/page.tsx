'use client';

import FilterProduct from '@/app/(homepage)/shop/_components/@filterProduct/page';
import Link from 'next/link';

const ShopPage = () => {
  return (
    <>
      <div className='container-fluid'>
        <div className='row px-xl-5'>
          <div className='col-12'>
            <nav className='breadcrumb bg-light mb-30'>
              <Link
                className='breadcrumb-item text-dark'
                href='/'
              >
                Home
              </Link>
              {/* <Link
                className='breadcrumb-item text-dark'
                href='Shop'
              >
                Shop
              </Link> */}
              <span className='breadcrumb-item active'>Shop List</span>
            </nav>
          </div>
        </div>
      </div>

      <div className='container-fluid'>
        <FilterProduct></FilterProduct>
      </div>
    </>
  );
};

export default ShopPage;
