"use client";
import React, { useState, useEffect } from "react";
import { MenuProps, Menu, ConfigProvider, GetProp } from "antd";
import Link from "next/link";
import { MdDashboard, MdSwitchAccount } from "react-icons/md";
import { GrUserAdmin } from "react-icons/gr";
import { FaChalkboardTeacher } from "react-icons/fa";
import { PiExamBold, PiStudent } from "react-icons/pi";
import { SiCoursera, SiGoogleclassroom } from "react-icons/si";

type Props = {
  path: string;
  items: MenuItem[];
};

type MenuItem = Required<MenuProps>["items"][number];

const MenuSider = (props: Props) => {
  const { path, items } = props;
  const [current, setCurrent] = useState(`${path}`);

  const onClick: MenuProps["onClick"] = (e) => {
    setCurrent(e.key);
  };

  useEffect(() => {
    setCurrent(path);
  }, [path]);

  return (
    <ConfigProvider
      theme={{
        components: {
          Menu: {
            itemSelectedBg: "#ed6436",
            itemSelectedColor: "#fff",
            itemBg: "#fff",
            iconSize: 15,
            itemHeight: 46,
            itemActiveBg: "#ed6436",
            subMenuItemBg: "#fff",
          },
          Button: {
            colorPrimary: "#8000FF",
          },
        },
      }}
    >
      <Menu
        onClick={onClick}
        selectedKeys={[current]}
        mode="inline"
        className={`site_layout_element`}
        items={items}
        forceSubMenuRender={true}
      />
    </ConfigProvider>
  );
};

export default MenuSider;
