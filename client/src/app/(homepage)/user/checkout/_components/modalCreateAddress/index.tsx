'use client';

import UseAxiosAuth from '@/utils/axiosClient';
import { LoadingOutlined } from '@ant-design/icons';
import { Button, Form, Input, Modal, Row, Select, Space, Spin } from 'antd';
import { useForm } from 'antd/es/form/Form';
import { getSession } from 'next-auth/react';
import { useEffect, useState } from 'react';
import toast from 'react-hot-toast';

interface addressModal {
  isOpen: boolean;
  onClose: () => void;
  onReload: () => void;
  provinces: IProvince[];
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

interface Options {
  label: string;
  value: string;
}

const ModalCreateAddress = ({ isOpen, onClose, onReload, provinces }: addressModal) => {
  const instance = UseAxiosAuth();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>('');

  const [form] = useForm();

  const [districts, setDistricts] = useState<IDistricts[]>([]);
  const [wards, setWards] = useState<IWards[]>([]);

  const [provinceSelected, setProvinceSelected] = useState<IProvince>();
  const [districtSelected, setDistrictSelected] = useState<IDistricts>();
  const [wardSelected, setWardSelected] = useState<IWards>();

  const [provinceOptions, setProvinceOptions] = useState<Options[]>([]);
  const [districtOptions, setDistrictOptions] = useState<Options[]>([]);
  const [wardOptions, setWardOptions] = useState<Options[]>([]);

  const [districtDisable, setDistrictDisable] = useState<boolean>(true);
  const [wardDisable, setWardDisable] = useState<boolean>(true);

  useEffect(() => {}, []);

  useEffect(() => {
    form.resetFields();
    console.log(provinces);
  }, [isOpen]);

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
    setDistrictDisable(false);
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
    setWardDisable(false);
  };

  const handleChangeWard = () => {
    const selectWardId = form.getFieldValue('ward');
    const selectWard = wards.find(ward => ward.Id.toString() === selectWardId.toString());

    setWardSelected(selectWard);
  };

  useEffect(() => {}, []);

  const onFinish = async (values: any) => {
    setError('');

    try {
      const useSession = await getSession();
      const promise = instance.post(`/createAddress`, {
        accountID: useSession?.user.Id,
        address:
          provinceSelected?.Name +
          ',' +
          districtSelected?.Name +
          ',' +
          wardSelected?.Name +
          ',' +
          values.addressDetails,
        phone: values.phone,
        name: values.name
      });

      toast.promise(promise, {
        loading: 'Add new address...',
        success: 'Add new address successfully',
        error: 'Add new address Failed'
      });

      const res = await promise;

      if (res.data.statusCode === 200 || res.data.statusCode === 201) {
        onClose();
      } else {
      }
    } catch (error: any) {
      setError(error);
      console.log(error);
    }
  };

  return (
    <>
      <Modal
        title={<h3 style={{ textAlign: 'center' }}>Create address</h3>}
        open={isOpen}
        width={800}
        onCancel={onClose}
        footer={() => <></>}
        cancelText='No'
        closable={false}
        style={{ display: 'block' }}
      >
        <Spin spinning={isLoading}>
          <div className='px-xl-5'>
            <div className='bg-light mb-5'>
              <Form
                // {...layout}
                form={form}
                name='control-hooks'
                layout={'vertical'}
                onFinish={onFinish}
                style={{ margin: '16px' }}
              >
                <div className='row'>
                  <div className='col-md-6 form-group'>
                    <Form.Item
                      label='Name'
                      name='name'
                      rules={[
                        {
                          required: true,
                          message: 'Enter name'
                        }
                      ]}
                    >
                      <Input
                        type='text'
                        placeholder='Enter name'
                      />
                    </Form.Item>
                  </div>

                  <div className='col-md-6 form-group'>
                    <Form.Item
                      label='Phone'
                      name='phone'
                      rules={[
                        {
                          required: true,
                          message: 'Enter phone'
                        }
                      ]}
                    >
                      <Input
                        type='text'
                        placeholder='Enter Phone'
                      />
                    </Form.Item>
                  </div>

                  <div className='col-md-12 form-group'>
                    <Form.Item
                      label='Province/City'
                      name='province'
                      rules={[
                        {
                          required: true,
                          message: 'Select Province/City'
                        }
                      ]}
                    >
                      <Select
                        placeholder='Select Province/City'
                        onChange={handleChangeProvince}
                        options={provinceOptions}
                      />
                    </Form.Item>
                  </div>

                  <div className='col-md-12 form-group'>
                    <Form.Item
                      label='Districts'
                      name='district'
                      rules={[
                        {
                          required: true,
                          message: 'Select districts'
                        }
                      ]}
                    >
                      <Select
                        placeholder='Select districts'
                        onChange={handleChangeDistrisct}
                        options={districtOptions}
                        disabled={districtDisable}
                      />
                    </Form.Item>
                  </div>

                  <div className='col-md-12 form-group'>
                    <Form.Item
                      label='Wards'
                      name='ward'
                      rules={[
                        {
                          required: true,
                          message: 'Select ward'
                        }
                      ]}
                    >
                      <Select
                        placeholder='Select ward'
                        onChange={handleChangeWard}
                        options={wardOptions}
                        disabled={wardDisable}
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
                      />
                    </Form.Item>
                  </div>
                </div>

                {error && <p style={{ color: 'red' }}>{error}</p>}

                <Row justify='center'>
                  <Space size={'large'}>
                    <Button onClick={onClose}>Cancel</Button>
                    <Spin
                      indicator={
                        <LoadingOutlined
                          style={{ fontSize: 24 }}
                          spin
                        />
                      }
                      spinning={false}
                    >
                      <Button
                        type='primary'
                        htmlType='submit'
                      >
                        Save
                      </Button>
                    </Spin>
                  </Space>
                </Row>
              </Form>
            </div>
          </div>
        </Spin>
      </Modal>
    </>
  );
};

export default ModalCreateAddress;
