'use client';
import { userColumn } from '@/app/admin/account/_components/columnTypes';
import { UserModel, UserResponse } from '@/app/admin/account/_components/type';
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
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';

export default function AdminAccount() {
  const instance = UseAxiosAuth();
  const router = useRouter();

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const [api, contextHolder] = notification.useNotification();
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  const [userUpdate, setUserUpdate] = useState<string>('');

  const [createState, setCreateState] = useState<boolean>(false);
  const [updateState, setUpdateState] = useState<boolean>(false);
  // const [deleteState, setDeleteState] = useState<boolean>(false);

  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 5,
    total: 0
  });

  const [users, setUsers] = useState<UserModel[]>([]);
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
    console.log('selectedRowKeys changed: ', newSelectedRowKeys);

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
    onChange: onSelectChange
  };

  const handleBlock = async (
    _accountId: string,
    _accountStatus: 'ACTIVE' | 'BLOCKED'
  ) => {
    setIsLoading(true);
    console.log(_accountId);
    try {
      if (_accountStatus === 'ACTIVE') {
        const res = await instance.get(`/block?id=${_accountId}`);

        if (res.data.statusCode === 200) {
          openNotification('success', 'block success');
          handleEvent();
        } else {
          openNotification('error', 'block fail');
        }
      } else {
        const res = await instance.get(`/active?id=${_accountId}`);

        if (res.data.statusCode === 200) {
          openNotification('success', 'active success');
          handleEvent();
        } else {
          openNotification('error', 'active fail');
        }
      }
    } catch (error) {
      openNotification('error', 'action failure');
      console.log(error);
    }
    setIsLoading(false);
  };

  const handlePagination = async (
    page: number = pagination.current,
    pageSize: number = pagination.pageSize
  ) => {
    setIsLoading(true);
    try {
      const res = await instance.get('/', {
        params: {
          PageNumber: page,
          PageSize: pageSize
        }
      });

      let tempRes = res.data.dataResponse.paginationData;

      setUserRes(tempRes);
      setUsers(tempRes.pageData);
      setPagination({
        current: page,
        pageSize: tempRes.pageSize,
        total: tempRes.totalRecord
      });
      setIsLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    handlePagination();
  }, []);

  const handleEvent = () => {
    handlePagination();
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
                  title: 'Accounts'
                }
              ]}
            />
          </Col>
          <Col>
            {hasSelected ? (
              <Space>
                <Button
                  type='text'
                  style={{ color: 'grey' }}
                  // onClick={onClearSelect}
                >
                  <span style={{ textDecoration: 'underline' }}>Clear</span>
                </Button>
                <Divider type='vertical' />
                <span style={{ color: 'grey', lineHeight: '12px' }}>
                  {' '}
                  <b>{selectedRowKeys.length}</b> Record Selected
                </span>
                <Divider type='vertical' />
              </Space>
            ) : (
              <Space>
                <span style={{ color: 'grey', lineHeight: '12px' }}>
                  Totals {!isLoading ? userRes?.totalRecord : <LoadingOutlined />} records
                </span>
                {/* <Divider type="vertical" /> */}
              </Space>
            )}
          </Col>
        </Row>
        <Table
          loading={isLoading}
          rowKey='id'
          rowSelection={rowSelection}
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
          columns={userColumn}
          dataSource={users.map((user: UserModel) => ({
            ...user,
            onBlock: () => handleBlock(user.accountId, user.status)
          }))}
          pagination={{
            ...pagination,
            onChange: (page, pageSize) => handlePagination(page, pageSize),
            onShowSizeChange: (_, size) => handlePagination(),
            showSizeChanger: true,
            pageSizeOptions: [5, 10, 20, 30]
            // showTotal: (total, range) =>
            //   {
            //     from: range[0],
            //     to: range[1],
            //     total: total,
            //   }),
          }}
        />
        {/* <UpdateUser
          isOpen={updateState}
          onClose={() => setUpdateState(false)}
          onReload={handleEvent}
          id={userUpdate}
        /> */}
      </div>
    </>
  );
}
