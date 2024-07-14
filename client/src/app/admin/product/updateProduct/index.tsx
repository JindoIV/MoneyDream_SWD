"use client";
import UseAxiosAuth from "@/utils/axiosClient";
import { LoadingOutlined, UploadOutlined } from "@ant-design/icons";
import {
  Button,
  Col,
  Form,
  Input,
  Modal,
  Row,
  Select,
  Space,
  Spin,
  TimePicker,
  Upload,
  notification,
} from "antd";
import form from "antd/es/form";
import { useForm } from "antd/es/form/Form";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import Image from "next/image";
import { ProductModel } from "@/app/admin/product/_components/type";

interface IProductModal {
  isOpen: boolean;
  onClose: () => void;
  onReload: () => void;
  id: string;
  image: string;
}

const ProductUpdate = (props: IProductModal) => {
  const { isOpen, onClose, onReload, id, image } = props;
  const [form] = useForm();
  const instance = UseAxiosAuth();
  const router = useRouter();

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const [fileList, setFileList] = useState<any[]>([]);
  const [error, setError] = useState<string>("");

  const [api, contextHolder] = notification.useNotification();
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  const [productUpdate, setProductUpdate] = useState<ProductModel>();
  const [imageUpdate, setImageUpdate] = useState<string>("");

  const fetchProduct = async () => {
    setIsLoading(true);
    try {
      const res = await instance.get(`/api/products/view/${id}`);
      setProductUpdate(res.data);
    } catch (error: any) {}
    setIsLoading(false);
  };

  useEffect(() => {
    fetchProduct();
  }, [isOpen]);

  return (
    <>
      {contextHolder}
      <Modal
        title={<h3 style={{ textAlign: "center" }}>Update Product</h3>}
        open={isOpen}
        width={900}
        onCancel={onClose}
        footer={() => <></>}
        cancelText="No"
        closable={false}
      >
        <Spin spinning={isLoading}>
          <Row>
            <Col style={{ width: "30%" }}>
              <div>
                <Form className="d-flex justify-content-center flex-column">
                  <Image src={image} alt="Product Image" />
                  <Form.Item>
                    <Upload
                      listType="picture"
                      fileList={fileList}
                      //   onChange={handleChange}
                      beforeUpload={() => false}
                      maxCount={1}
                    >
                      <Button className="mt-3" icon={<UploadOutlined />}>
                        Select image
                      </Button>
                    </Upload>
                  </Form.Item>
                </Form>
              </div>
            </Col>

            <Col style={{ width: "70%" }}>
              <Form
                // {...layout}
                className={"container"}
                form={form}
                name="control-hooks"
                layout={"vertical"}
                // onFinish={onFinish}
                style={{ margin: "16px" }}
              >
                <Form.Item
                  label="Product name"
                  name="name"
                  rules={[
                    {
                      required: true,
                      message: "Enter product name",
                    },
                  ]}
                >
                  <Input placeholder="Enter product name" />
                </Form.Item>

                <Form.Item
                  label="Product price"
                  name="unitPrice"
                  rules={[
                    {
                      required: true,
                      message: "Enter product price",
                    },
                  ]}
                >
                  <Input placeholder="Enter product price" />
                </Form.Item>

                <Form.Item
                  label="Product name"
                  name="name"
                  rules={[
                    {
                      required: true,
                      message: "Enter product name",
                    },
                  ]}
                >
                  <Input placeholder="Enter product name" />
                </Form.Item>

                <Form.Item
                  label="Product name"
                  name="name"
                  rules={[
                    {
                      required: true,
                      message: "Enter product name",
                    },
                  ]}
                >
                  <Input placeholder="Enter product name" />
                </Form.Item>

                <Form.Item
                  label="Product name"
                  name="name"
                  rules={[
                    {
                      required: true,
                      message: "Enter product name",
                    },
                  ]}
                >
                  <Input placeholder="Enter product name" />
                </Form.Item>

                {error && <p style={{ color: "red" }}>{error}</p>}

                <Row justify="center">
                  <Space size={"large"}>
                    <Button onClick={onClose}>Cancel</Button>
                    <Spin
                      indicator={
                        <LoadingOutlined style={{ fontSize: 24 }} spin />
                      }
                      spinning={false}
                    >
                      <Button type="primary" htmlType="submit">
                        Save
                      </Button>
                    </Spin>
                  </Space>
                </Row>
              </Form>
            </Col>
          </Row>
        </Spin>
      </Modal>
    </>
  );
};

export default ProductUpdate;
