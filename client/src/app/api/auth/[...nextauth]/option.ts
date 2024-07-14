import { http } from '@/utils/config';
import { AuthOptions } from 'next-auth';

import CredentialsProvider from 'next-auth/providers/credentials';
import { useDispatch } from 'react-redux';

export const authOptions: AuthOptions = {
  providers: [
    CredentialsProvider({
      id: 'credentials',
      name: 'sign in',
      credentials: {
        username: {
          label: 'username',
          type: 'text'
        },
        password: {
          label: 'password',
          type: 'password'
        }
      },
      async authorize(credentials, request) {
        try {
          const response = await http.post('/login', {
            username: credentials?.username as string,
            password: credentials?.password as string
          });

          const user = response.data.dataResponse;
          if (
            (response.data.statusCode === 200 || response.data.statusCode === 201) &&
            user
          ) {
            const resRole = await http.post(`/?token=${user.accessToken}`, {
              token: user.accessToken
            });
            if (resRole.data.statusCode === 200 || resRole.data.statusCode === 201) {
              user.roleId = resRole.data.dataResponse.roleId;
              user.Id = resRole.data.dataResponse.accountId;
            }
            return user;
          }
        } catch (error) {
          // console.log(error);
        }
        return null;
      }
    })
  ],
  session: {
    strategy: 'jwt',
    maxAge: 12 * 60 * 60
  },
  callbacks: {
    signIn: async ({ user, account, profile }) => {
      if (account?.provider === 'credentials') {
        // console.log('login credentials', account?.provider);
        return true;
      }
      return false;
    },

    jwt: async ({ token, user }) => {
      if (user) {
        token.access_token = user?.accessToken;
        token.refresh_token = user?.refreshToken;
        token.roleId = user?.roleId;
        token.accountId = user?.Id;
      }
      return token;
    },

    session: async ({ session, token, user }) => {
      session.user.access = token.access_token as string;
      session.user.refresh = token.refresh_token as string;
      session.user.roleId = token.roleId as string;
      session.user.Id = token?.accountId as string;
      return session;
    }
  },
  pages: {
    signIn: '/auth/login'
    // newUser: "/auth/register",
  }
  // secret: process.env.NEXTAUTH_SECRET,
};
