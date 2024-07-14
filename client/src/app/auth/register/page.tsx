"use client";
import "@/styles/homepage.scss";
import styles from "../_components/login.module.scss";
import { Button, Col, DatePicker, Form, Input, Row, Spin } from "antd";

import Card from "antd/es/card/Card";
import {
  EyeInvisibleOutlined,
  EyeTwoTone,
  LeftOutlined,
  LoadingOutlined,
} from "@ant-design/icons";
import Link from "next/link";
import { useState } from "react";
import { useRouter } from "next/navigation";

const RegisterPage = () => {
  const router = useRouter();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  return (
    <>
      <section className={`${styles.LoginBG}`}>
        <div className={`${styles.backContainer}`}>
          <Button onClick={() => router.push("/")}>
            <LeftOutlined />
          </Button>
        </div>
        <div className="container min-vh-100 d-flex justify-content-center align-items-center">
          <Card
            style={{ width: "90%" }}
            className="border-radius-30 my-3 shadow-medium"
          >
            <Row style={{ height: "80%" }}>
              <Col span={14}>
                <div
                  className={`h-100 d-flex justify-content-start align-items-center `}
                >
                  <div
                    className={`${styles.LoginFormContainer} d-flex justify-content-center align-items-center border-radius-24`}
                  >
                    <div className={`${styles.LoginForm} `}>
                      <h3 className="text-center py-3">Sign up</h3>
                      <Form
                        // form={form}
                        // onFinish={onFinish}
                        layout={"vertical"}
                        // variant='filled'
                        // style={{ maxWidth: 1000 }}
                      >
                        <Form.Item
                          name="Full Name"
                          rules={[
                            {
                              required: true,
                              message: "Please enter full name",
                            },
                          ]}
                          className="py-2"
                        >
                          <Input placeholder="Enter full name" />
                        </Form.Item>

                        <Form.Item
                          name="email"
                          rules={[
                            {
                              required: true,
                              message: "Please enter email",
                            },
                            {
                              type: "email",
                              message: "Please enter valid email",
                            },
                          ]}
                          className="py-2"
                        >
                          <Input placeholder="Enter Email" />
                        </Form.Item>

                        <Form.Item
                          name="phoneNumber"
                          rules={[
                            {
                              required: true,
                              message: "Please enter phone",
                            },
                          ]}
                          className="py-2"
                        >
                          <Input placeholder="Enter phone" />
                        </Form.Item>

                        <Form.Item
                          name="dateofBirth"
                          rules={[
                            {
                              required: true,
                              message: "Please enter date of Birth",
                            },
                          ]}
                          className="py-2"
                        >
                          <DatePicker
                            placeholder="Enter date of Birth"
                            style={{ width: "100%" }}
                          />
                        </Form.Item>

                        <Form.Item
                          name="username"
                          rules={[
                            {
                              required: true,
                              message: "Please enter username",
                            },
                          ]}
                          className="py-2"
                        >
                          <Input placeholder="Enter username" />
                        </Form.Item>

                        <Form.Item
                          name="password"
                          rules={[
                            {
                              required: true,
                              message: "Please enter password",
                            },
                          ]}
                          className="py-2"
                        >
                          <Input.Password
                            placeholder="Enter password"
                            iconRender={(visible) =>
                              visible ? (
                                <EyeTwoTone />
                              ) : (
                                <EyeInvisibleOutlined />
                              )
                            }
                          />
                        </Form.Item>

                        <div>
                          {/* {error && <p style={{ color: "red" }}>{error}</p>} */}
                          <Spin
                            indicator={
                              <LoadingOutlined style={{ fontSize: 24 }} spin />
                            }
                            spinning={isLoading}
                          >
                            <Button
                              style={{ width: "100%" }}
                              type="primary"
                              htmlType="submit"
                              className={styles.buttonCustom}
                            >
                              Sign up
                            </Button>
                          </Spin>
                        </div>
                      </Form>

                      <div className="my-2 text-center">
                        <span className={styles.colorText}>
                          Already have an account{" "}
                        </span>
                        <Link href="login" className={styles.colorText}>
                          Sign in
                        </Link>
                      </div>
                    </div>
                  </div>
                </div>
              </Col>
              <Col span={10}>
                <div className={`${styles.LoginImage} border-radius-24`}></div>
              </Col>
            </Row>
          </Card>
        </div>
      </section>
    </>
  );
};

export default RegisterPage;
