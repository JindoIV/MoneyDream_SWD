"use client";
import { LogoutOutlined, UserOutlined } from "@ant-design/icons";
import { Button, Col, Divider, Drawer, Row } from "antd";
import { getSession, signOut } from "next-auth/react";
import Image from "next/image";
import sample from "@/assets/image/avatar.jpg";
import { useEffect, useState } from "react";

interface profileDrawer {
  open: boolean;
  onClose: () => void;
}

const ProfileDrawer = ({ open, onClose }: profileDrawer) => {
  const getProfile = async () => {
    try {
      const sessionUse = await getSession();
      // if (sessionUse?.user.profile) {
      //   setProfile(sessionUse?.user.profile);
      // }
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    getProfile();
  }, []);

  return (
    <>
      <Drawer
        width={"440px"}
        placement="right"
        closable={false}
        onClose={onClose}
        open={open}
        bodyStyle={{ overflowY: "hidden" }}
        footer={
          <>
            <Col
              style={{
                display: "flex",
                justifyContent: "space-between",
                marginBottom: "16px",
              }}
            >
              <Button type="primary" style={{ width: "45%" }} href="#">
                <UserOutlined />
              </Button>
              <Button
                type="primary"
                danger
                style={{ width: "45%" }}
                onClick={() => {
                  signOut();
                }}
              >
                <LogoutOutlined />
              </Button>
            </Col>
          </>
        }
      >
        <div style={{ justifyItems: "center", display: "grid" }}>
          <Image
            src={sample}
            priority={true}
            placeholder="empty"
            width={80}
            height={80}
            alt="Picture of the author"
            style={{ borderRadius: "50%", backgroundColor: "transparent" }}
          />
        </div>
        <Divider style={{ margin: "0" }} />
        <h4 style={{ margin: "16px 0 40px 0" }}>Information</h4>
        <Row>
          <Col span={24}>{/* <p>Email : {profile?.email}</p> */}</Col>
        </Row>
        <Divider style={{ margin: "0 0 16px 0" }} />
      </Drawer>
    </>
  );
};

export default ProfileDrawer;
