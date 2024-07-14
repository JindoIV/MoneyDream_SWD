'use client';
import { userColumn, userColumnChat } from '@/app/admin/account/_components/columnTypes';
import {
  ConversationPendingModel,
  UserModel,
  UserResponse
} from '@/app/admin/account/_components/type';
import ChatPopUp from '@/components/ChatPopUp/Index';
import UseAxiosAuth from '@/utils/axiosClient';
import { LoadingOutlined } from '@ant-design/icons';
import {
  Breadcrumb,
  Button,
  Col,
  Divider,
  Dropdown,
  Row,
  Space,
  Table,
  notification
} from 'antd';
import { getSession, useSession } from 'next-auth/react';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';

type ConversationType = {
  conversationId: number;
  type: string;
  createAt: string;
  accountId: number;
  fullName: string;
  picture: string;
};

export default function AdminChat() {
  const instance = UseAxiosAuth();
  const router = useRouter();

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const [api, contextHolder] = notification.useNotification();
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  const [createState, setCreateState] = useState<boolean>(false);
  const [updateState, setUpdateState] = useState<boolean>(false);
  const [openChatPopUp, setOpenChatPopUp] = useState<boolean>(false);
  const [chatRoom, setChatRoom] = useState<ConversationType>();

  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 5,
    total: 0
  });

  const [userAuthenticated, setUserAuthenticated] = useState<number>();
  let userId = 0;
  const [userToChat, setUserToChat] = useState<number>(4);
  const session = useSession();

  const fetchData = async () => {
    const sessionUse = await getSession();
    userId = Number(sessionUse?.user.Id);
    setUserAuthenticated(Number(sessionUse?.user.Id));
  };

  useEffect(() => {
    fetchData();
  }, []);

  const [users, setUsers] = useState<ConversationPendingModel[]>([]);
  const [usersInChat, setUsersInChat] = useState<ConversationPendingModel[]>([]);

  const [userRes, setUserRes] = useState<UserResponse>();

  const openNotification = (type: 'success' | 'error', status: string) => {
    api[type]({
      message: `Account ${status}`,
      placement: 'bottomRight',
      duration: 1.5
    });
  };

  const hasSelected = selectedRowKeys.length > 0;

  const onSelectChange = (newSelectedRowKeys: React.Key[]) => {
    // console.log('selectedRowKeys changed: ', newSelectedRowKeys);

    if (newSelectedRowKeys.length > 0) {
      const selectedEmails = [];
      for (const key of newSelectedRowKeys) {
        const accountId = key.toString();
      }
    } else {
    }

    setSelectedRowKeys(newSelectedRowKeys);
  };

  const HandleLoadData = async () => {
    setIsLoading(true);
    try {
      const res = await instance.get('/getPendingConversation');

      let tempRes = res.data.dataResponse;
      setUsers(tempRes);

      setIsLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  const HandleLoadDataInChat = async () => {
    setIsLoading(true);
    const sessionUse = await getSession();
    try {
      const res = await instance.get('/getConversationByUserID', {
        params: {
          userID: sessionUse?.user.Id
        }
      });

      let tempRes = res.data.dataResponse;
      setUsersInChat(tempRes);

      setIsLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    HandleLoadData();
    HandleLoadDataInChat();
  }, [openChatPopUp]);

  useEffect(() => {
    HandleLoadDataInChat();
    HandleLoadData();
  }, []);

  const handleEvent = () => {
    HandleLoadData();
  };

  const handleJoinChat = (record: ConversationType) => {
    setChatRoom(record);
    setOpenChatPopUp(true);
  };

  const HandleLoadAddAdminToChat = async () => {
    const sessionUse = await getSession();
    setIsLoading(true);
    try {
      const res = await instance.post('/addAdminToConversation', {
        adminID: sessionUse?.user.Id,
        conversationID: chatRoom?.conversationId
      });

      let tempRes = res.data.dataResponse;
      if (res.data.statusCode === 200 || res.data.statusCode === 201) {
      }
      setIsLoading(false);
    } catch (error: any) {}
  };

  useEffect(() => {
    HandleLoadAddAdminToChat();
  }, [chatRoom]);

  const handelClose = () => {
    setOpenChatPopUp(false);
    setChatRoom(undefined);
  };

  return (
    <>
      <div>
        {contextHolder}
        <Row
          justify={'space-between'}
          style={{ marginBottom: 16 }}
        >
          <Col>
            <strong
            // className={cx('title')}
            >
              ACCOUNTS
            </strong>{' '}
            <br />
            <Breadcrumb
              items={[
                {
                  href: '/admin/dashboard',
                  title: 'Home'
                },
                {
                  title: 'Chat'
                }
              ]}
            />
          </Col>
        </Row>
        <div>Pending</div>

        <Table
          loading={isLoading}
          rowKey='id'
          onRow={record => ({
            onClick: event => {
              const target = event.target as HTMLElement;
              const isWithinLink = target.tagName === 'A' || target.closest('a');
              const isWithinAction =
                target.closest('td')?.classList.contains('ant-table-cell') &&
                !target.closest('td')?.classList.contains('ant-table-selection-column') &&
                !target.closest('td')?.classList.contains('ant-table-cell-fix-right');

              if (isWithinAction && !isWithinLink) {
              }
            }
          })}
          columns={userColumnChat(handleJoinChat)}
          dataSource={users.map((user: any) => ({
            ...user
          }))}
          showHeader={false}
        />
        <div>In Chat</div>
        <Table
          loading={isLoading}
          rowKey='id'
          onRow={record => ({
            onClick: event => {
              const target = event.target as HTMLElement;
              const isWithinLink = target.tagName === 'A' || target.closest('a');
              const isWithinAction =
                target.closest('td')?.classList.contains('ant-table-cell') &&
                !target.closest('td')?.classList.contains('ant-table-selection-column') &&
                !target.closest('td')?.classList.contains('ant-table-cell-fix-right');

              if (isWithinAction && !isWithinLink) {
                // handleUpdate(record.id);
              }
            }
          })}
          columns={userColumnChat(handleJoinChat)}
          dataSource={usersInChat.map((user: any) => ({
            ...user
          }))}
          showHeader={false}
        />
        {chatRoom != undefined && (
          <ChatPopUp
            ChatRoom={chatRoom}
            show={openChatPopUp}
            handleClose={handelClose}
          />
        )}
      </div>
    </>
  );
}
