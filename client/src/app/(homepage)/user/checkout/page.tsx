'use client';

import { ProductCart } from '@/app/(homepage)/user/cart/_components/type';
import ModalCreateAddress from '@/app/(homepage)/user/checkout/_components/modalCreateAddress';
import { AddressModel } from '@/app/(homepage)/user/checkout/_components/type';
import UseAxiosAuth from '@/utils/axiosClient';
import { LoadingOutlined } from '@ant-design/icons';
import { Form, Input, Select, Row, Space, Button, Spin } from 'antd';
import form from 'antd/es/form';
import { useForm } from 'antd/es/form/Form';
import { error } from 'console';
import axios from 'axios';
import { getSession } from 'next-auth/react';
import { ReactElement, useEffect, useState } from 'react';
import { formatMoney } from '@/utils/configFormat';
import { useRouter } from 'next/navigation';
import { useDispatch, useSelector } from 'react-redux';
import { product, productRes } from '@/types/product';
import { productCart } from '@/types/productInCart';

interface Options {
  label: any;
  value: string;
}

interface IWards {
  Id: string;
  Name: string;
}

interface IDistricts {
  Id: string;
  Name: string;
  Wards: IWards[];
}

interface IProvince {
  Id: string;
  Name: string;
  Districts: IDistricts[];
}

const CheckoutPage = () => {
  const router = useRouter();
  const dispatch = useDispatch();
  const instance = UseAxiosAuth();
  const productInCart = useSelector((state: any) => state.productInCart.products);

  const [openCreateAddress, setOpenCreateAddress] = useState<boolean>(false);
  const [form] = useForm();

  const [provinces, setProvinces] = useState<IProvince[]>([]);
  const [districts, setDistricts] = useState<IDistricts[]>([]);
  const [wards, setWards] = useState<IWards[]>([]);

  const [addresses, setAddresses] = useState<AddressModel[]>([]);
  const [address, setAddress] = useState<AddressModel>();

  const [provinceSelected, setProvinceSelected] = useState<IProvince | undefined>();
  const [districtSelected, setDistrictSelected] = useState<IDistricts>();
  const [wardSelected, setWardSelected] = useState<IWards>();

  const [provinceOptions, setProvinceOptions] = useState<Options[]>([]);
  const [districtOptions, setDistrictOptions] = useState<Options[]>([]);
  const [wardOptions, setWardOptions] = useState<Options[]>([]);

  const [districtDisable, setDistrictDisable] = useState<boolean>(false);
  const [wardDisable, setWardDisable] = useState<boolean>(false);

  const [addressOptions, setAddressOptions] = useState<Options[]>([]);

  const [productSelected, setProductSeleted] = useState<productCart[]>([]);

  const fetchData = async () => {
    try {
      const useSession = await getSession();
      const res = await instance.get(`/getAllAddress?AccountID=${useSession?.user.Id}`);
      if (res?.data.statusCode === 200) {
        setAddresses(res.data.dataResponse ?? []);
        setAddress(res.data.dataResponse[0] ?? {});
      } else {
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  const fetchProvinces = async () => {
    try {
      const res = await axios.get(
        `https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json`
      );
      setProvinces(res.data);
    } catch (error: any) {}
  };

  useEffect(() => {
    handleInitProvince();
  }, [provinces]);

  const handleInitProvince = () => {
    setProvinceOptions(
      provinces.map((province: IProvince) => ({
        label: province.Name,
        value: province.Id
      }))
    );
  };

  useEffect(() => {
    fetchData();
    fetchProvinces();
    // setProductSeleted(productInCart);
  }, []);

  useEffect(() => {
    setProductSeleted(productInCart);
  }, [productInCart]);

  const handleResetProvince = () => {
    form.resetFields(['district', 'ward']);
  };

  const handleResetDistrict = () => {
    form.resetFields(['ward']);
  };

  const handleSetOptionsDistrict = () => {
    setDistrictOptions(
      districts.map((district: IDistricts) => ({
        label: district.Name,
        value: district.Id
      }))
    );
  };

  useEffect(() => {
    handleSetOptionsDistrict();
  }, [districts]);

  const handleSetOptionsWard = () => {
    setWardOptions(
      wards.map((ward: IWards) => ({
        label: ward.Name,
        value: ward.Id
      }))
    );
  };

  useEffect(() => {
    handleSetOptionsWard();
  }, [wards]);

  const handleChangeProvince = () => {
    const selectProvinceId = form.getFieldValue('province');
    const selectProvince = provinces.find(
      province => province.Id.toString() === selectProvinceId.toString()
    );

    const _distrisct = provinces.find(
      province => province.Id.toString() === selectProvinceId.toString()
    )?.Districts;

    handleResetProvince();
    setDistricts(_distrisct ?? []);
    setProvinceSelected(selectProvince);
  };

  const handleChangeDistrisct = () => {
    const selectDistrictId = form.getFieldValue('district');
    const selectDistrict = districts.find(
      disctrict => disctrict.Id.toString() === selectDistrictId.toString()
    );

    const _ward = districts.find(
      province => province.Id.toString() === selectDistrictId.toString()
    )?.Wards;

    handleResetDistrict();
    setWards(_ward ?? []);
    setDistrictSelected(selectDistrict);
  };

  const handleChangeWard = () => {
    const selectWardId = form.getFieldValue('ward');
    const selectWard = wards.find(ward => ward.Id.toString() === selectWardId.toString());

    setWardSelected(selectWard);
  };

  useEffect(() => {
    if (Array.isArray(addresses)) {
      const op = addresses.map((_address: AddressModel, index) => ({
        value: index.toString(),
        label: (
          <>
            <span>{_address.deliveryName}</span> | <span>{_address.deliveryPhone}</span>
            <br />
            <span>{_address.address}</span>
          </>
        )
      }));

      setAddressOptions(op);
    }
  }, [addresses]);

  useEffect(() => {
    const addressSplit = address?.address.split(',');
    if (Array.isArray(addressSplit)) {
      const selectedProvince = provinces.find(
        province => province.Name.toString() === addressSplit[0].toString()
      );
      setProvinceSelected(selectedProvince);
      setDistricts(selectedProvince?.Districts ?? []);
      handleSetOptionsWard();

      const selectedDistrict = selectedProvince?.Districts.find(
        district => district.Name.toString() === addressSplit[1].toString()
      );
      setDistrictSelected(selectedDistrict);
      setWards(selectedDistrict?.Wards ?? []);
      handleSetOptionsDistrict();

      const selectedWard = selectedDistrict?.Wards.find(
        ward => ward.Name.toString() === addressSplit[2].toString()
      );
      setWardSelected(selectedWard);
      handleSetOptionsWard();

      form.setFieldsValue({
        name: address?.deliveryName,
        phone: address?.deliveryPhone,
        province: addressSplit[0] ?? '',
        district: addressSplit[1] ?? '',
        ward: addressSplit[2] ?? '',
        addressDetails: addressSplit[3] ?? ''
      });
    }
  }, [address]);

  const handleChangeAddress = (value: any) => {
    setAddress(
      addresses.find((_address, index) => index.toString() === value.toString())
    );
  };

  return (
    <>
      <ModalCreateAddress
        isOpen={openCreateAddress}
        onClose={() => setOpenCreateAddress(false)}
        onReload={function (): void {
          throw new Error('Function not implemented.');
        }}
        provinces={provinces}
      ></ModalCreateAddress>
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
              <span className='breadcrumb-item active'>Checkout</span>
            </nav>
          </div>
        </div>
      </div>

      <div className='container-fluid'>
        <div className='row px-xl-5'>
          <div className='col-lg-7'>
            <h5 className='section-title position-relative text-uppercase mb-3'>
              <span className='bg-secondary pr-3'>Billing Address</span>
            </h5>
            <div className='bg-light p-30 mb-5'>
              <div className='w-100 d-flex justify-content-between mb-3'>
                <Select
                  options={addressOptions}
                  style={{ width: '80%', height: '10vh' }}
                  onChange={handleChangeAddress}
                ></Select>
                <button
                  className='btn btn-primary '
                  onClick={() => {
                    setOpenCreateAddress(true);
                  }}
                >
                  Add new
                </button>
              </div>

              <Form
                // {...layout}
                form={form}
                name='control-hooks'
                layout={'vertical'}
                style={{ margin: '16px' }}
              >
                <div className='row'>
                  <div className='col-md-6 form-group'>
                    <Form.Item
                      label='Name'
                      name='name'
                    >
                      <Input
                        type='text'
                        placeholder='Enter name'
                        disabled
                      />
                    </Form.Item>
                  </div>

                  <div className='col-md-6 form-group'>
                    <Form.Item
                      label='Phone'
                      name='phone'
                    >
                      <Input
                        type='text'
                        placeholder='Enter Phone'
                        disabled
                      />
                    </Form.Item>
                  </div>

                  <div className='col-md-12 form-group'>
                    <Form.Item
                      label='Province/City'
                      name='province'
                    >
                      <Select
                        placeholder='Select Province/City'
                        onChange={handleChangeProvince}
                        options={provinceOptions}
                        disabled
                      />
                    </Form.Item>
                  </div>

                  <div className='col-md-12 form-group'>
                    <Form.Item
                      label='Districts'
                      name='district'
                    >
                      <Select
                        placeholder='Select districts'
                        onChange={handleChangeDistrisct}
                        options={districtOptions}
                        disabled
                      />
                    </Form.Item>
                  </div>

                  <div className='col-md-12 form-group'>
                    <Form.Item
                      label='Wards'
                      name='ward'
                    >
                      <Select
                        placeholder='Select ward'
                        onChange={handleChangeWard}
                        options={wardOptions}
                        disabled
                      />
                    </Form.Item>
                  </div>

                  <div className='col-md-12 form-group'>
                    <Form.Item
                      label='Address Details'
                      name='addressDetails'
                    >
                      <Input
                        type='text'
                        placeholder='Enter address detail'
                        disabled
                      />
                    </Form.Item>
                  </div>
                </div>
              </Form>
            </div>
          </div>
          <div className='col-lg-5'>
            <h5 className='section-title position-relative text-uppercase mb-3'>
              <span className='bg-secondary pr-3'>Order Total</span>
            </h5>
            <div className='bg-light p-30 mb-5'>
              <div className='border-bottom'>
                <h6 className='mb-3'>Products</h6>
                {productSelected.map((_product: productCart) => {
                  return (
                    <>
                      <div className='d-flex justify-content-between'>
                        <p>{_product.name}</p>
                        <p>{formatMoney(_product.oldPrice * _product.quantity)}</p>
                      </div>
                    </>
                  );
                })}
              </div>
              <div className='border-bottom pt-3 pb-2'>
                <div className='d-flex justify-content-between mb-3'>
                  <h6>Subtotal</h6>
                  <h6>$150</h6>
                </div>
                <div className='d-flex justify-content-between'>
                  <h6 className='font-weight-medium'>Shipping</h6>
                  <h6 className='font-weight-medium'>$10</h6>
                </div>
              </div>
              <div className='pt-2'>
                <div className='d-flex justify-content-between mt-2'>
                  <h5>Total</h5>
                  <h5>$160</h5>
                </div>
              </div>
            </div>
            <div className='mb-5'>
              <h5 className='section-title position-relative text-uppercase mb-3'>
                <span className='bg-secondary pr-3'>Payment</span>
              </h5>
              <div className='bg-light p-30'>
                <div className='form-group'>
                  <div className='custom-control custom-radio'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      name='payment'
                      id='paypal'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='paypal'
                    >
                      Paypal
                    </label>
                  </div>
                </div>
                <div className='form-group'>
                  <div className='custom-control custom-radio'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      name='payment'
                      id='directcheck'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='directcheck'
                    >
                      Direct Check
                    </label>
                  </div>
                </div>
                <div className='form-group mb-4'>
                  <div className='custom-control custom-radio'>
                    <input
                      type='radio'
                      className='custom-control-input'
                      name='payment'
                      id='banktransfer'
                    />
                    <label
                      className='custom-control-label'
                      htmlFor='banktransfer'
                    >
                      Bank Transfer
                    </label>
                  </div>
                </div>
                <button className='btn btn-block btn-primary font-weight-bold py-3'>
                  Place Order
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default CheckoutPage;
