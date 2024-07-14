import Footer from '@/components/footer';
import Header from '@/components/header';
import '@/styles/homepage.scss';
export default function HomePageLayout({ children }: { children: React.ReactNode }) {
  return (
    <>
      <header>
        <Header />
      </header>
      <section>{children}</section>
      <footer>
        <Footer />
      </footer>
    </>
  );
}
