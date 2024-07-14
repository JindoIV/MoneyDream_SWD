import { Button, Image, Row } from 'antd';
import logo from '../../../../../assets/image/3.png';
import { useEffect, useState } from 'react';
import { getSession } from 'next-auth/react';
import UseAxiosAuth from '@/utils/axiosClient';

const ChatPending = () => {
  const instance = UseAxiosAuth();
  const [userAuthenticated, setUserAuthenticated] = useState<number>();
  const fetchData = async () => {
    const sessionUse = await getSession();
    setUserAuthenticated(Number(sessionUse?.user.Id));
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleCreateChat = async () => {
    try {
      const res = await instance.post('/createPendingConversation', {
        customerID: userAuthenticated
      });

      let tempRes = res.data.dataResponse;
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <>
      <Row className='d-flex justify-content-center'>
        <Row className='d-flex flex-column'>
          <h4>You not have conversation yet</h4>
          <Image
            preview={false}
            src={logo.src}
            width={280}
            height={280}
          />
          <button
            onClick={handleCreateChat}
            className='btn btn-primary rounded'
          >
            Make a chat
          </button>
        </Row>
      </Row>
    </>
  );
};

export default ChatPending;
