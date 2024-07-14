"use client";
import {
  Badge,
  Button,
  Dropdown,
  MenuProps,
  Modal,
  Space,
  Tag,
  Image,
} from "antd";
import { ColumnsType, TableProps } from "antd/es/table";
// import { AccountModel } from './models';

import {
  EllipsisOutlined,
  EditOutlined,
  DeleteOutlined,
} from "@ant-design/icons";
import { MdDisabledByDefault } from "react-icons/md";
import { ProductModel } from "@/app/admin/product/_components/type";

export const productColumn: ColumnsType<ProductModel> = [
  {
    title: "Product Image",
    dataIndex: "product image",
    render: (_, record) => {
      return (
        <>
          <Image
            src={record.imageUrl}
            alt={"product image"}
            width={70}
            height={70}
          />
        </>
      );
    },
  },
  {
    title: "Product Name",
    dataIndex: "name",
  },
  {
    title: "Price",
    dataIndex: "unitPrice",
  },
  {
    title: "Stock",
    dataIndex: "unitInStock",
  },
  // {
  //   title: "Deleted",
  //   dataIndex: "delete",
  //   render: (text, record) => {
  //     return (
  //       <>
  //         {record.status === "ACTIVE" ? (
  //           <>
  //             <Tag color="success">ACTIVE</Tag>
  //           </>
  //         ) : (
  //           <>
  //             <Tag color="error">BLOCKED</Tag>
  //           </>
  //         )}
  //       </>
  //     );
  //   },
  // },
  // {
  //   title: "Action",
  //   dataIndex: "action",
  //   render(value, record, index) {
  //     return (
  //       <>
  //         <Button onClick={record.onBlock}>Block</Button>
  //       </>
  //     );
  //   },
  // },
];
