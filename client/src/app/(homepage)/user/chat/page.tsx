'use client';
import React, { use, useEffect, useRef, useState } from 'react';
import classNames from 'classnames/bind';
import Chat from './Chatpage/Chat';
import ChatPending from './ChatWaiting';
import UseAxiosAuth from '@/utils/axiosClient';
import { getSession } from 'next-auth/react';

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

const ChatCustomer = () => {
  const instance = UseAxiosAuth();

  const [userAuthenticated, setUserAuthenticated] = useState<number>();
  const [chatRoom, setChatRoom] = useState<ConversationType>();
  const [isPending, setIsPending] = useState<boolean>(true);

  const fetchData = async () => {
    const sessionUse = await getSession();
    setUserAuthenticated(Number(sessionUse?.user.Id));
  };

  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    getConversation();
  }, [userAuthenticated]);

  const getConversation = async () => {
    try {
      const res = await instance.get('/getConversationByUserID', {
        params: {
          userID: userAuthenticated
        }
      });

      let tempRes = res.data.dataResponse as ConversationType[];

      if (tempRes.length != 0) {
        setIsPending(false);
        setChatRoom(tempRes[0]);
      } else {
        setIsPending(true);
      }
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <>
      {!isPending && <Chat chatRoom={chatRoom!} />}
      {isPending && <ChatPending />}
    </>
  );
};

export default ChatCustomer;
