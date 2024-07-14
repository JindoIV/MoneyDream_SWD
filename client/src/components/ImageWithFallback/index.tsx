'use client';
import Image, { ImageProps } from 'next/image';
import { useEffect, useState } from 'react';
import '@/styles/homepage.scss';

type ClientImageWithFallbackProps = ImageProps & {
  src: string;
  fallbackSrc: string;
};

const ClientImageWithFallback = ({
  src = '',
  alt = '',
  fallbackSrc,
  ...props
}: ClientImageWithFallbackProps) => {
  const [error, setError] = useState<boolean>(false);

  useEffect(() => {
    setError(false);
  }, [src]);

  return (
    <>
      <img
        alt={alt}
        onError={() => setError(true)}
        src={error ? fallbackSrc : src}
        {...props}
      />
    </>
  );
};

export default ClientImageWithFallback;
