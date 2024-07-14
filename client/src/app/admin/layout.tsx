"use client";
import MenuHeader from "@/components/Layouts/admin/MenuHeader";
import MenuSider from "@/components/MenuSider/MenuSider";
import {
  AlignLeftOutlined,
  BellOutlined,
  SettingOutlined,
} from "@ant-design/icons";
import { Badge, Button, ConfigProvider, Layout, MenuProps, Space } from "antd";
import Sider from "antd/es/layout/Sider";
import { Content, Header } from "antd/es/layout/layout";
import { usePathname } from "next/navigation";
import { useState } from "react";
import {
  MdDashboard,
  MdMedicalServices,
  MdOutlineChat,
  MdOutlinePayment,
  MdSwitchAccount,
} from "react-icons/md";
import { GrUserAdmin } from "react-icons/gr";
import { FaChalkboardTeacher, FaProductHunt, FaUser } from "react-icons/fa";
import { PiExamBold, PiStudent } from "react-icons/pi";
import { SiCoursera, SiGoogleclassroom } from "react-icons/si";
import "@/styles/admin.scss";
import Link from "next/link";
import { GiBirdCage } from "react-icons/gi";
import { FaUserDoctor } from "react-icons/fa6";
import { AiOutlineProduct } from "react-icons/ai";
import { CgProductHunt } from "react-icons/cg";
import { IoGift } from "react-icons/io5";

const domain = "/admin";

type MenuItem = Required<MenuProps>["items"][number];

const items: MenuItem[] = [
  {
    key: `${domain}/dashboard`,
    label: <Link href={`${domain}/dashboard`}>Dashboard</Link>,
    icon: <MdDashboard />
  },
  {
    key: `${domain}/account`,
    label: <Link href={`${domain}/account`}>Account</Link>,
    icon: <FaUser />
  },
  {
    key: `${domain}/product`,
    label: <Link href={`${domain}/product`}>Product</Link>,
    icon: <FaProductHunt />
  },
  {
    key: `${domain}/voucher`,
    label: <Link href={`${domain}/voucher`}>Voucher</Link>,
    icon: <IoGift />
  },
  {
    key: `${domain}/order`,
    label: <Link href={`${domain}/order`}>Order</Link>,
    icon: <MdOutlinePayment />
  },
  {
    key: `${domain}/chat`,
    label: <Link href={`${domain}/chat`}>Chat</Link>,
    icon: <MdOutlineChat />
  }
  //   getItem(
  //     "Account",
  //     `${domain}/account`,
  //     <MdSwitchAccount />,
  //     [
  //       getItem("Account", `${domain}/account/admins`, <GrUserAdmin />),
  //       getItem("Account", `${domain}/account/teachers`, <FaChalkboardTeacher />),
  //       getItem("Account", `${domain}/account/students`, <PiStudent />),
  //     ],
  //     "group"
  //   ),
];

export default function AdminLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const pathname = usePathname();
  const [collapsed, setCollapsed] = useState(false);

  return (
    <Layout className="layout">
      <Header>
        <Space size={"middle"}>
          <Button
            type="text"
            size="middle"
            className="sidebar-toggle"
            onClick={() => setCollapsed(!collapsed)}
          >
            <AlignLeftOutlined />
          </Button>
          <Link href={"/"} className="logo">
            <span className={`textLogo`}>MoneyDream</span>
          </Link>
        </Space>
        <Space size={"middle"} align="center" className={"right_menu"}>
          <Badge count={1} dot offset={[-10, 10]} className="iconButton">
            <BellOutlined />
          </Badge>
          <Link href="/settings" style={{ color: "#000000" }}>
            <SettingOutlined className="iconButton" />
          </Link>
          <MenuHeader path={"pathName"} />
        </Space>
      </Header>
      <Layout className="site_layout m_h" hasSider>
        <Sider
          width={230}
          theme="light"
          className="sidebar"
          collapsible
          trigger={null}
          collapsed={collapsed}
          onCollapse={(value) => setCollapsed(value)}
        >
          <MenuSider path={pathname} items={items} />
        </Sider>
        <ConfigProvider
          theme={{
            components: {
              Button: {
                colorPrimary: "#ed6436",
                // defaultHoverBg: "#ed6436",
                primaryShadow: "#ed6436",
                defaultHoverBg: "#ed6436",
                defaultActiveColor: "#ed6436",
                // defaultHoverBorderColor: "#ed6436",
              },
            },
          }}
        >
          <Content className="site_layout_background">{children}</Content>
        </ConfigProvider>
      </Layout>
    </Layout>
  );
}
