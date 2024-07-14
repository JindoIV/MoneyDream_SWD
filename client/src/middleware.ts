import { withAuth } from 'next-auth/middleware';
import { NextResponse } from 'next/server';
import path from 'path';

const role = ['ADMIN', 'USER'];

export default withAuth(
  function middleware(req) {
    if (!req.nextauth.token) {
      return NextResponse.redirect(new URL('/auth/login', req.url));
    }

    // console.log(req.nextauth.token.accountId);

    if (req.nextUrl.pathname.startsWith('/admin') && req.nextauth.token.roleId === 1) {
      return NextResponse.next();
    }

    if (req.nextUrl.pathname.startsWith('/user') && req.nextauth.token.roleId === 2) {
      return NextResponse.next();
    }

    return NextResponse.redirect(new URL('/', req.url));
  },
  {
    callbacks: {
      authorized: params => {
        let { token } = params;
        return !!token;
      }
    }
  }
);

export const config = {
  matcher: ['/admin/:path*']
};
