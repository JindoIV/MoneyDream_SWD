import NextAuth, { DefaultSession } from "next-auth";

declare module "next-auth" {
  interface Session {
    user: {
      userId: string;
      access: string;
      refresh: string;
      roleId: string;
      Id: string;
    } & DefaultSession["user"];
  }

  interface User {
    accessToken: string;
    refreshToken: string;
    roleId: string;
    Id: string;
  }
}
