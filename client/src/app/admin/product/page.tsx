"use client";
import { userColumn } from "@/app/admin/account/_components/columnTypes";
import { UserModel, UserResponse } from "@/app/admin/account/_components/type";
import { productColumn } from "@/app/admin/product/_components/columnTypes";
import { ProductModel } from "@/app/admin/product/_components/type";
import ProductUpdate from "@/app/admin/product/updateProduct";
import UseAxiosAuth from "@/utils/axiosClient";
import { LoadingOutlined } from "@ant-design/icons";
import {
  Breadcrumb,
  Button,
  Col,
  Divider,
  Dropdown,
  Row,
  Space,
  Table,
  notification,
} from "antd";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

export default function AdminProduct() {
  const instance = UseAxiosAuth();
  const router = useRouter();

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const [api, contextHolder] = notification.useNotification();
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  const [productUpdate, setProductUpdate] = useState<string>("");
  const [productUpdateModal, setProductUpdateModal] = useState<boolean>(false);

  const [createState, setCreateState] = useState<boolean>(false);
  const [updateState, setUpdateState] = useState<boolean>(false);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 5,
    total: 0,
  });

  const [products, setProducts] = useState<ProductModel[]>([]);
  // const [userRes, setUserRes] = useState<UserResponse>();

  const openNotification = (type: "success" | "error", status: string) => {
    api[type]({
      message: `Product ${status}`,
      placement: "bottomRight",
      duration: 1.5,
    });
  };

  const hasSelected = selectedRowKeys.length > 0;

  const onSelectChange = (newSelectedRowKeys: React.Key[]) => {
    console.log("selectedRowKeys changed: ", newSelectedRowKeys);

    if (newSelectedRowKeys.length > 0) {
      const selectedEmails = [];
      for (const key of newSelectedRowKeys) {
        const accountId = key.toString();
      }
    } else {
    }

    setSelectedRowKeys(newSelectedRowKeys);
  };

  const rowSelection = {
    selectedRowKeys,
    onChange: onSelectChange,
  };

  //  const handlePagination = async (
  //   page: number = pagination.current,
  //   pageSize: number = pagination.pageSize
  // ) => {
  //   setIsLoading(true);
  //   try {
  //     const res = await instance.get("/", {
  //       params: {
  //         PageNumber: page,
  //         PageSize: pageSize,
  //       },
  //     });

  //     let tempRes = res.data.dataResponse.paginationData;

  //     setUserRes(tempRes);
  //     setUsers(tempRes.pageData);
  //     setPagination({
  //       current: page,
  //       pageSize: tempRes.pageSize,
  //       total: tempRes.totalRecord,
  //     });
  //     setIsLoading(false);
  //   } catch (error: any) {
  //     console.log(error);
  //   }
  // };

  const handleFetchProduct = async () => {
    setIsLoading(true);
    try {
      const res = await instance.get("/api/products/view");
      if (res.data.statusCode === 200) {
        setProducts(res.data.dataResponse);
      }
    } catch (error: any) {
      console.log(error);
    }
    setIsLoading(false);
  };

  useEffect(() => {
    handleFetchProduct();
  }, []);

  const handleEvent = () => {
    handleFetchProduct();
  };

  const handleUpdate = (productId: string) => {
    setProductUpdate(productId);
    setProductUpdateModal(true);
  };

  return (
    <>
      <div>
        {contextHolder}
        <Row justify={"space-between"} style={{ marginBottom: 16 }}>
          <Col>
            <strong
            // className={cx('title')}
            >
              PRODUCTS
            </strong>{" "}
            <br />
            <Breadcrumb
              items={[
                {
                  href: "/admin/dashboard",
                  title: "Home",
                },
                {
                  title: "Products",
                },
              ]}
            />
          </Col>
          <Col>
            {hasSelected ? (
              <Space>
                <Button
                  type="text"
                  style={{ color: "grey" }}
                  // onClick={onClearSelect}
                >
                  <span style={{ textDecoration: "underline" }}>Clear</span>
                </Button>
                <Divider type="vertical" />
                <span style={{ color: "grey", lineHeight: "12px" }}>
                  {" "}
                  <b>{selectedRowKeys.length}</b> Record Selected
                </span>
                <Divider type="vertical" />
              </Space>
            ) : (
              <Space>
                <span style={{ color: "grey", lineHeight: "12px" }}>
                  Totals {!isLoading ? products.length : <LoadingOutlined />}{" "}
                  records
                </span>
                <Divider type="vertical" />
              </Space>
            )}
          </Col>
        </Row>
        <Table
          loading={isLoading}
          rowKey="id"
          rowSelection={rowSelection}
          onRow={(record) => ({
            onClick: (event) => {
              const target = event.target as HTMLElement;
              const isWithinLink =
                target.tagName === "A" || target.closest("a");
              const isWithinAction =
                target.closest("td")?.classList.contains("ant-table-cell") &&
                !target
                  .closest("td")
                  ?.classList.contains("ant-table-selection-column") &&
                !target
                  .closest("td")
                  ?.classList.contains("ant-table-cell-fix-right");

              if (isWithinAction && !isWithinLink) {
                handleUpdate(record.productId);
              }
            },
          })}
          columns={productColumn}
          dataSource={products.map((product: ProductModel) => ({
            ...product,
            // onBlock: () => handleBlock(user.accountId, user.status),
          }))}
          // pagination={{
          //   ...pagination,
          //   onChange: (page, pageSize) => handlePagination(page, pageSize),
          //   onShowSizeChange: (_, size) => handlePagination(),
          //   showSizeChanger: true,
          //   pageSizeOptions: [5, 10, 20, 30],
          //   showTotal: (total, range) =>
          //     {
          //       from: range[0],
          //       to: range[1],
          //       total: total,
          //     }),
          // }}
        />
      </div>
      <ProductUpdate
        isOpen={productUpdateModal}
        onClose={() => setProductUpdateModal(false)}
        onReload={handleEvent}
        id={productUpdate}
        image={""}
      ></ProductUpdate>
    </>
  );
}
