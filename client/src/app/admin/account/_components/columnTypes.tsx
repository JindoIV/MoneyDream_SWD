'use client';
import { Badge, Button, Dropdown, Image, MenuProps, Modal, Space, Tag } from 'antd';
import { ColumnsType, TableProps } from 'antd/es/table';
// import { AccountModel } from './models';

import { EllipsisOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { MdDisabledByDefault } from 'react-icons/md';
import {
  ConversationPendingModel,
  UserModel
} from '@/app/admin/account/_components/type';

export const userColumn: ColumnsType<UserModel> = [
  {
    dataIndex: 'picture',
    width: 60,
    render: picture => (
      <Image
        alt='Avatar'
        src={picture}
        width={50}
        height={50}
        style={{ borderRadius: '50%', border: '1px solid #ed6436' }}
      />
    )
  },
  {
    dataIndex: 'fullName',
    width: 860
  },
  {
    title: 'Age',
    dataIndex: 'age'
  },
  {
    title: 'Gender',
    dataIndex: 'gender'
  },
  {
    title: 'Deleted',
    dataIndex: 'delete',
    render: (text, record) => {
      return (
        <>
          {record.status === 'ACTIVE' ? (
            <>
              <Tag color='success'>ACTIVE</Tag>
            </>
          ) : (
            <>
              <Tag color='error'>BLOCKED</Tag>
            </>
          )}
        </>
      );
    }
  },
  {
    title: 'Action',
    dataIndex: 'action',
    render(value, record, index) {
      return (
        <>
          <Button onClick={record.onBlock}>Block</Button>
        </>
      );
    }
  }
];

export const userColumnChat = (
  handleJoinChat: any
): ColumnsType<ConversationPendingModel> => [
  {
    dataIndex: 'picture',
    width: 60,
    render: picture => (
      <Image
        alt='Avatar'
        src={picture}
        width={50}
        height={50}
        style={{ borderRadius: '50%', border: '1px solid #ed6436' }}
      />
    )
  },
  {
    dataIndex: 'fullName',
    width: 860
  },
  {
    dataIndex: 'conversationId',
    render: (conversationId: number,record : any) => (
      <Button onClick={() => handleJoinChat(record)}>Chat</Button>
    )
  }
];
