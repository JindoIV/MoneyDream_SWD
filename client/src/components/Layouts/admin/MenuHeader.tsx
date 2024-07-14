"use client";
import UseAxiosAuth from "@/utils/axiosClient";
import { Menu, MenuProps } from "antd";
import { useSession } from "next-auth/react";
import { useEffect, useState } from "react";
import Image from "next/image";
import sample from "@/assets/image/avatar.jpg";
import ProfileDrawer from "@/components/ProfileDrawer/ProfileDrawer";

type Props = {
  path: string;
};

const MenuHeader = (props: Props) => {
  const { path } = props;
  const [current, setCurrent] = useState(`${path}`);
  const session = useSession();
  const user = session.data?.user;
  const [visible, setVisible] = useState<boolean>(false);

  const onClick: MenuProps["onClick"] = (e) => {
    setCurrent(e.key);
  };

  useEffect(() => {
    setCurrent(path);
  }, [path]);

  const items: MenuProps["items"] = [
    {
      label: (
        <>
          <Image
            onClick={() => {
              setVisible(true);
            }}
            src={user?.image ? user?.image : sample}
            priority={true}
            placeholder="empty"
            width={28}
            height={28}
            alt="Picture of the author"
            style={{ borderRadius: "50%", backgroundColor: "transparent" }}
          />
        </>
      ),
      key: "user",
    },
  ];

  return (
    <>
      <Menu onClick={onClick} theme="light" mode="horizontal" items={items} />
      {visible && (
        <ProfileDrawer open={visible} onClose={() => setVisible(false)} />
      )}
    </>
  );
};

export default MenuHeader;
