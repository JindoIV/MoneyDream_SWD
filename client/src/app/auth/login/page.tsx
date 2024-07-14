'use client';
import '@/styles/homepage.scss';
import styles from '../_components/login.module.scss';
import { Button, Col, Form, Input, Row, Spin, notification } from 'antd';

import Card from 'antd/es/card/Card';
import {
  EyeInvisibleOutlined,
  EyeTwoTone,
  LeftOutlined,
  LoadingOutlined
} from '@ant-design/icons';
import Link from 'next/link';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { useForm } from 'antd/es/form/Form';
import { SignInResponse, signIn, useSession } from 'next-auth/react';
import { ADMIN_PATH, USER_PATH } from '@/constants/routes';
import toast from 'react-hot-toast';

const LoginPage = () => {
  const [form] = useForm();
  const router = useRouter();
  const session = useSession();
  const [api, contextHolder] = notification.useNotification();
  const [error, setError] = useState<string>('');

  const [saveReq, setSaveReq] = useState<SignInResponse | undefined>();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const openNotification = (title: string, message: string) => {
    api.info({
      message: title,
      description: message,
      placement: `top`,
      duration: 2
    });
  };

  const onFinish = async (values: any) => {
    setError('');
    setIsLoading(true);

    try {
      const promise = signIn('credentials', {
        username: values?.username as string,
        password: values?.password as string,
        redirect: false
        // callbackUrl
      });

      toast.promise(promise, {
        loading: 'Sign in...',
        success: 'Sign in Successfully',
        error: 'Sign in Failed '
      });

      const res = await promise;

      if (res?.ok) {
        console.log(res);
        await handleChangeRouter();
      } else {
        setError('Username or password is incorrect');
      }
    } catch (error: any) {
      setError(error?.message);
    }
  };

  const handleChangeRouter = async () => {
    const user = await session.data?.user;
    console.log(user);
    if (user?.roleId.toString() == '1') {
      router.push(ADMIN_PATH);
      return;
    }
    if (user?.roleId.toString() == '2') {
      router.push(USER_PATH);
      return;
    }
    setIsLoading(false);
  };

  return (
    <>
      <section className={`${styles.LoginBG}`}>
        <div className={`${styles.backContainer}`}>
          <Button onClick={() => router.push('/')}>
            <LeftOutlined />
          </Button>
        </div>
        <div className='container min-vh-100 d-flex justify-content-center align-items-center'>
          <Card
            style={{ width: '90%' }}
            className='border-radius-30 shadow-medium'
          >
            <Row style={{ height: '80vh' }}>
              <Col span={14}>
                <div className={`${styles.LoginImage} border-radius-24`}></div>
              </Col>
              <Col span={10}>
                <div className={`h-100 d-flex justify-content-end align-items-center `}>
                  <div
                    className={`${styles.LoginFormContainer} d-flex justify-content-center align-items-center`}
                  >
                    <div className={`${styles.LoginForm} `}>
                      <h3 className='text-center pb-4'>Sign in</h3>
                      <Form
                        form={form}
                        onFinish={onFinish}
                        layout={'vertical'}
                        // variant='filled'
                        // style={{ maxWidth: 1000 }}
                      >
                        <Form.Item
                          name='username'
                          rules={[
                            {
                              required: true,
                              message: 'Please enter username'
                            }
                          ]}
                          className='py-2'
                        >
                          <Input placeholder='Enter username' />
                        </Form.Item>

                        <Form.Item
                          name='password'
                          rules={[
                            {
                              required: true,
                              message: 'Please enter password'
                            }
                          ]}
                          className='py-2'
                        >
                          <Input.Password
                            placeholder='Enter password'
                            iconRender={visible =>
                              visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />
                            }
                          />
                        </Form.Item>

                        <div>
                          {/* {error && <p style={{ color: "red" }}>{error}</p>} */}
                          <Spin
                            indicator={
                              <LoadingOutlined
                                style={{ fontSize: 24 }}
                                spin
                              />
                            }
                            spinning={isLoading}
                          >
                            <Button
                              style={{ width: '100%' }}
                              type='primary'
                              htmlType='submit'
                              className={styles.buttonCustom}
                            >
                              Sign in
                            </Button>
                          </Spin>
                        </div>
                      </Form>
                      <div className='text-end mb-1'>
                        <Link
                          href='recovery'
                          className={styles.colorText}
                        >
                          Forgot password?
                        </Link>
                      </div>

                      <div className='mt-2 text-center'>
                        <span className={styles.colorText}>Create new account </span>
                        <Link
                          href='register'
                          className={styles.colorText}
                        >
                          Sign up
                        </Link>
                      </div>
                    </div>
                  </div>
                </div>
              </Col>
            </Row>
          </Card>
        </div>
      </section>
    </>
  );
};

export default LoginPage;
