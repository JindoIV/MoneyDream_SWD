"use client";
import { http } from "@/utils/config";
import { getSession, signIn, useSession } from "next-auth/react";

export const useRefreshToken = () => {
  const { data: session } = useSession();
  let isRefreshing = false;

  const refreshToken = async () => {
    try {
      const sessionUse = await getSession();
      const res = await http.post("/refreshToken", {
        token: sessionUse?.user.refresh,
      });

      if (res.data.accessToken && session) {
        session.user.access = res.data.accessToken;
        return res.data.accessToken;
      } else {
        signIn();
        return null;
      }
    } catch (error: any) {
      console.error("Failed to refresh token:", error);
    }
  };

  return refreshToken;
};
