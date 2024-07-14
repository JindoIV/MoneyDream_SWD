'use client';

import { DownOutlined, MenuOutlined } from '@ant-design/icons';
import { Button, ConfigProvider, Dropdown, MenuProps, Space, Typography } from 'antd';
import { useSession } from 'next-auth/react';
import { usePathname, useRouter } from 'next/navigation';
import { useState } from 'react';
import Link from 'next/link';

const items: MenuProps['items'] = [
  {
    key: '1',
    label: 'Item 1'
  },
  {
    key: '2',
    label: 'Item 2'
  },
  {
    key: '3',
    label: 'Item 3'
  }
];

const Header = () => {
  const router = useRouter();
  const path = usePathname();
  const { data: session } = useSession();

  const [role, setRole] = useState<any>();

  return (
    <>
      <div className='container-fluid'>
        <div className='row align-items-center bg-light py-3 px-xl-5 d-none d-lg-flex'>
          <div className='col-lg-4'>
            <a
              href=''
              className='text-decoration-none'
            >
              <span className='h1 text-uppercase text-primary bg-dark px-2'>Money</span>
              <span className='h1 text-uppercase text-dark bg-primary px-2 ml-n1'>
                Dream
              </span>
            </a>
          </div>
          <div className='col-lg-4 col-6 text-left'>
            <form action=''>
              <div className='input-group'></div>
            </form>
          </div>
          <div className='col-lg-4 col-6 text-right'></div>
        </div>
      </div>

      <div className='container-fluid bg-dark mb-30'>
        <div className='row px-xl-5'>
          <div className='col-lg-3 d-none d-lg-block'>
            <Dropdown
              menu={{
                items
                // selectable: true,
                // defaultSelectedKeys: ["3"],
              }}
            >
              <a
                className='btn d-flex align-items-center justify-content-between bg-primary w-100'
                style={{ height: '65px', padding: '0 30px' }}
              >
                <h6 className='text-dark m-0'>
                  <Space>
                    <MenuOutlined /> Categories
                  </Space>
                </h6>
                <DownOutlined />
              </a>
            </Dropdown>
          </div>
          <div className='col-lg-9'>
            <nav className='navbar navbar-expand-lg bg-dark navbar-dark py-3 py-lg-0 px-0'>
              <a
                href=''
                className='text-decoration-none d-block d-lg-none'
              >
                <span className='h1 text-uppercase text-dark bg-light px-2'>Money</span>
                <span className='h1 text-uppercase text-light bg-primary px-2 ml-n1'>
                  Dream
                </span>
              </a>
              <button
                type='button'
                className='navbar-toggler'
                data-toggle='collapse'
                data-target='#navbarCollapse'
              >
                <span className='navbar-toggler-icon'></span>
              </button>
              <div
                className='collapse navbar-collapse justify-content-between'
                id='navbarCollapse'
              >
                <div className='navbar-nav mr-auto py-0'>
                  <Link
                    href='/'
                    className={`nav-item nav-link ${path === '/' ? 'active' : ''}`}
                  >
                    Home
                  </Link>
                  <Link
                    href='/shop'
                    className={`nav-item nav-link ${path === '/shop' ? 'active' : ''}`}
                  >
                    Shop
                  </Link>
                  <Link
                    href='/shop'
                    className={`nav-item nav-link ${path === '/shop' ? 'active' : ''}`}
                  >
                    AboutUs
                  </Link>
                  {/* <div className='nav-item dropdown'>
                    <a
                      href='#'
                      className='nav-link dropdown-toggle'
                      data-toggle='dropdown'
                    >
                      Pages <i className='fa fa-angle-down mt-1'></i>
                    </a>
                    <div className='dropdown-menu bg-primary rounded-0 border-0 m-0'>
                      <a
                        href='cart.html'
                        className='dropdown-item'
                      >
                        Shopping Cart
                      </a>
                      <a
                        href='checkout.html'
                        className='dropdown-item'
                      >
                        Checkout
                      </a>
                    </div>
                  </div> */}
                  <Link
                    href='/contact'
                    className={`nav-item nav-link ${path === '/contact' ? 'active' : ''}`}
                  >
                    Contact
                  </Link>
                </div>
                <div className='navbar-nav ml-auto py-0 d-none d-lg-block'>
                  {/* <a href="" className="btn px-0">
                    <i className="fas fa-heart text-primary"></i>
                    <span
                      className="badge text-secondary border border-secondary rounded-circle"
                      style={{ paddingBottom: "2px" }}
                    >
                      0
                    </span>
                  </a> */}
                  {/* <a href="" className="btn px-0 ml-3">
                    <i className="fas fa-shopping-cart text-primary"></i>
                    <span
                      className="badge text-secondary border border-secondary rounded-circle"
                      style={{ paddingBottom: "2px" }}
                    >
                      0
                    </span>
                  </a> */}
                  <button
                    className='btn btn-secondary rounded mx-2'
                    onClick={() => router.push('/auth/login')}
                  >
                    Sign in
                  </button>
                  <button
                    className='btn btn-primary rounded mx-2'
                    onClick={() => router.push('/auth/register')}
                  >
                    Sign up
                  </button>
                </div>
              </div>
            </nav>
          </div>
        </div>
      </div>
    </>
  );
};

export default Header;
