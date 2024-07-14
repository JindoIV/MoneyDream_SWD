"use client";
import { useRefreshToken } from "@/utils/axiosRefresh";
import axios from "axios";
import { getSession, signIn, signOut, useSession } from "next-auth/react";
import { usePathname, useRouter } from "next/navigation";
import { useEffect } from "react";

export const DOMAIN = `${process.env.NEXT_PUBLIC_API}`;

export const axiosAuth = axios.create({
  baseURL: DOMAIN,
  headers: { "Content-Type": "application/json" },
  timeout: 10000,
});

const UseAxiosAuth = () => {
  const { data: session } = useSession();
  const path = usePathname();
  const refreshToken = useRefreshToken();

  useEffect(() => {
    const requestIntercept = axiosAuth.interceptors.request.use(
      async (config) => {
        ``;
        const sessionUse = await getSession();
        if (!config.headers.Authorization) {
          config.headers.Authorization = `Bearer ${
            sessionUse?.user?.access as string
          }`;
        }
        return config;
      },
      (error) => Promise.reject(error)
    );

    const responseIntercept = axiosAuth.interceptors.response.use(
      (response) => response,
      async (error) => {
        const prevRequest = error?.config;
        console.log(prevRequest);
        if (
          (error.response?.status === 401 || error.response?.status === 403) &&
          !prevRequest?.sent
        ) {
          const sessionUse = await getSession();
          prevRequest.sent = true;
          const newAccessToken = await refreshToken();
          if (newAccessToken) {
            prevRequest.headers.Authorization = `Bearer ${sessionUse?.user.access}`;
            return axiosAuth(prevRequest);
          } else {
            await signIn();
          }
        }

        if (error.response?.status === 400 || error.response?.status === 404) {
          console.log(error.response.data.error);

          // console.log('Status 400 during HTTP request.');
          return Promise.resolve(error.response);
        }

        return Promise.reject(error);
      }
    );

    return () => {
      axiosAuth.interceptors.request.eject(requestIntercept);
      axiosAuth.interceptors.response.eject(responseIntercept);
    };
  }, [session, path]);

  return axiosAuth;
};

export default UseAxiosAuth;
