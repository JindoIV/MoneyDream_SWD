'use client';
import React, { use, useEffect, useRef, useState } from 'react';
import {
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBBtn,
  MDBCardHeader,
  MDBCardFooter,
  MDBInputGroup
} from 'mdb-react-ui-kit';
import { Image, Row } from 'antd';
import classNames from 'classnames/bind';
import style from './ChatUser.module.scss';
import 'mdb-react-ui-kit/dist/css/mdb.min.css';
import { RiSendPlane2Fill, RiSendPlaneFill } from 'react-icons/ri';
import { FaComments, FaMinus, FaTimes } from 'react-icons/fa';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { getSession, signOut, useSession } from 'next-auth/react';
import { DefaultSession } from 'next-auth';
import UseAxiosAuth from '@/utils/axiosClient';
import ClientImageWithFallback from '@/components/ImageWithFallback';
import userDefault from '../../../../../assets/image/avatar.jpg';

type MessageType = {
  senderId: number;
  conversationId: number;
  messageContent: string;
  attachedFileUrl: string | null;
  createAt: string;
};

type ConversationType = {
  conversationId: number;
  type: string;
  createAt: string;
  accountId: number;
  fullName: string;
  picture: string;
};

type ChatProps = {
  chatRoom: ConversationType;
};

const cx = classNames.bind(style);

const Chat = ({ chatRoom }: ChatProps) => {
  const [newMessage, setNewMessage] = useState<MessageType>();
  const [message, setMessage] = useState('');
  const [mesageList, setMessageList] = useState<MessageType[]>([]);
  const [currentUserToken, setCurrentUserToken] = useState<string>();
  const [userAuthenticated, setUserAuthenticated] = useState<number>(5);
  const connectionRef = useRef<HubConnection | null>(null);
  const { data: session } = useSession();

  const fetchData = async () => {
    const sessionUse = await getSession();
    setCurrentUserToken(sessionUse?.user.access);
    setUserAuthenticated(Number(sessionUse?.user.Id));
  };

  useEffect(() => {
    fetchData();
  }, []);

  const instance = UseAxiosAuth();

  // OK
  const LoadMessageFormDB = async (roomID: number) => {
    try {
      const res = await instance.get('/getConversation', {
        params: {
          conversationID: roomID
        }
      });

      let tempRes = res.data.dataResponse;
      if (res.data.statusCode === 200 || res.data.statusCode === 201) {
        console.log(tempRes);
        setMessageList(tempRes);
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  //OK
  useEffect(() => {
    LoadMessageFormDB(chatRoom.conversationId);
  }, [chatRoom]);

  useEffect(() => {
    if (currentUserToken) {
      connectionRef.current = new HubConnectionBuilder()
        .withUrl('http://localhost:8080/chatHub', {
          accessTokenFactory: () => currentUserToken
        })
        .build();

      connectionRef.current.on('ReceiveMessage', function (data: any) {
        const item = {
          senderId: data.senderId,
          conversationId: data.conversationId,
          messageContent: data.messageContent,
          attachedFileUrl: data.attachedFileUrl,
          createAt: data.createAt
        };
        console.log(data);
        setNewMessage(item);
      });

      connectionRef.current
        .start()
        .catch(err => console.error('Error while starting connection: ' + err));
    }

    // Clean up the connection on component unmount
    return () => {
      if (connectionRef.current) {
        connectionRef.current.stop().then(() => console.log('Connection stopped'));
      }
    };
  }, [currentUserToken]);

  const SendMessage = async () => {
    const sessionUse = await getSession();
    if (connectionRef.current) {
      connectionRef.current
        .invoke('SendMessageToConversation', {
          SenderId: sessionUse?.user.Id,
          ConversationId: chatRoom.conversationId,
          Message: message,
          AttachmentFileUrl: '',
          CreateAt: ''
        })
        .catch(function (err: any) {
          return console.error(err.toString());
        });
    }
    setMessage('');
  };

  useEffect(() => {
    if (newMessage) {
      const value = mesageList?.filter(
        x => x?.createAt?.toString() === newMessage?.createAt?.toString()
      );
      if (value.length !== 1) setMessageList([...mesageList, newMessage!]);
    }
  }, [newMessage]);

  return (
    <>
      <div className={cx('chatBoxContainer')}>
        <div className='d-flex justify-content-end me-2'>
          <div className='w-100'>
            <MDBCard>
              <MDBCardHeader
                className='d-flex justify-content-between align-items-center p-3'
                style={{ borderTop: '4px solid #ffa900' }}
              >
                <h5 className='mb-0'>Chat with Admin</h5>
                <div className='d-flex flex-row align-items-center'></div>
              </MDBCardHeader>
              <Row style={{ overflowY: 'scroll', position: 'relative', height: '400px' }}>
                <MDBCardBody>
                  {mesageList.length != 0 &&
                    mesageList.map((m, i) => {
                      if (m.senderId != userAuthenticated) {
                        return (
                          <div
                            key={i}
                            className={cx('clientMessage')}
                          >
                            <div className='d-flex justify-content-between'>
                              <p className='small mb-1'></p>
                              {/* <p className='small mb-1 text-muted'>23 Jan 2:00 pm</p> */}
                            </div>
                            <div className='d-flex flex-row justify-content-start'>
                              <ClientImageWithFallback
                                fallbackSrc={userDefault.src}
                                src={chatRoom.picture}
                                alt='avatar 1'
                                style={{
                                  width: '45px',
                                  height: '45px',
                                  borderRadius: '50%'
                                }}
                              />
                              <div>
                                <p
                                  className='small p-2 ms-3 mb-3 rounded-3'
                                  style={{ backgroundColor: '#f5f6f7' }}
                                >
                                  {m.messageContent}
                                </p>
                              </div>
                            </div>
                          </div>
                        );
                      } else {
                        return (
                          <div
                            key={i}
                            className={cx('ownerMessage')}
                          >
                            <div className='d-flex justify-content-between'>
                              {/* <p className='small mb-1 text-muted'>23 Jan 2:05 pm</p> */}
                              <p className='small mb-1'></p>
                            </div>
                            <div className='d-flex flex-row justify-content-end mb-4 pt-1'>
                              <div>
                                <p className='small p-2 me-3 mb-3 text-white rounded-3 bg-warning'>
                                  {m.messageContent}
                                </p>
                              </div>
                              <ClientImageWithFallback
                                fallbackSrc={userDefault.src}
                                src={'userimg'}
                                alt='avatar 1'
                                style={{
                                  width: '45px',
                                  height: '45px',
                                  borderRadius: '50%'
                                }}
                              />
                            </div>
                          </div>
                        );
                      }
                    })}
                </MDBCardBody>
              </Row>
              <MDBCardFooter className='text-muted d-flex justify-content-start align-items-center p-3'>
                <MDBInputGroup className='mb-0'>
                  <input
                    onChange={e => setMessage(e.target.value)}
                    value={message}
                    className='form-control'
                    placeholder=''
                    type='text'
                  />
                  <MDBBtn
                    onClick={SendMessage}
                    color='warning'
                    style={{ paddingTop: '.55rem' }}
                  >
                    <RiSendPlane2Fill />
                  </MDBBtn>
                </MDBInputGroup>
              </MDBCardFooter>
            </MDBCard>
          </div>
        </div>
      </div>
    </>
  );
};

export default Chat;
