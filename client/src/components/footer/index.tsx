"use client";

import { Image } from "antd";
import { FaFacebookF, FaLinkedinIn, FaPhoneAlt } from "react-icons/fa";
import { FaLocationDot, FaXTwitter } from "react-icons/fa6";
import { MdEmail } from "react-icons/md";
import { PiInstagramLogoFill } from "react-icons/pi";

const Footer = () => {
  return (
    <>
      <div className="container-fluid bg-dark text-secondary mt-5 pt-5">
        <div className="row px-xl-5 pt-5">
          <div className="col-lg-4 col-md-12 mb-5 pr-3 pr-xl-5">
            <h5 className="text-secondary text-uppercase mb-4">
              Welcome to Money Dream
            </h5>
            <p className="mb-4 text-justify">
              Discover the latest fashion trends at Money Dream. From stylish
              dresses to comfy jeans, we offer high-quality products at great
              prices. Enjoy an exceptional shopping experience with us!
            </p>
            <p className="mb-2">
              <FaLocationDot className="text-primary mr-3"></FaLocationDot>
              Ninh Kiều, Cần Thơ
            </p>
            <p className="mb-2">
              <MdEmail className="text-primary mr-3" />
              MoneyDream@gmail.com
            </p>
            <p className="mb-0">
              <FaPhoneAlt className="text-primary mr-3" />
              +84 813 113 149
            </p>
          </div>
          <div className="col-lg-8 col-md-12">
            <div className="row">
              <div className="col-md-4 mb-5">
                <h5 className="text-secondary text-uppercase mb-4">
                  Quick Shop
                </h5>
                <div className="d-flex flex-column justify-content-start">
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Home
                  </a>
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Our Shop
                  </a>
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Shop Detail
                  </a>
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Shopping Cart
                  </a>
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Checkout
                  </a>
                  <a className="text-secondary ml-2" href="#">
                    Contact Us
                  </a>
                </div>
              </div>
              <div className="col-md-4 mb-5">
                <h5 className="text-secondary text-uppercase mb-4">
                  My Account
                </h5>
                <div className="d-flex flex-column justify-content-start">
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Home
                  </a>
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Our Shop
                  </a>
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Shop Detail
                  </a>
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Shopping Cart
                  </a>
                  <a className="text-secondary mb-2 ml-2" href="#">
                    Checkout
                  </a>
                  <a className="text-secondary ml-2" href="#">
                    Contact Us
                  </a>
                </div>
              </div>
              <div className="col-md-4 mb-5">
                <h5 className="text-secondary text-uppercase mb-4">
                  Cooperate with Us
                </h5>
                <p>
                  Stay updated with the latest news and offers. Sign up now!
                </p>
                <form action="">
                  <div className="input-group">
                    <input
                      type="text"
                      className="form-control"
                      placeholder="Your Email Address"
                    />
                    <div className="input-group-append">
                      <button className="btn btn-primary">Sign Up</button>
                    </div>
                  </div>
                </form>
                <h6 className="text-secondary text-uppercase mt-4 mb-3">
                  Follow Us
                </h6>
                <div className="d-flex">
                  <a className="btn btn-primary btn-square mr-2" href="#">
                    <FaXTwitter />
                  </a>
                  <a className="btn btn-primary btn-square mr-2" href="#">
                    <FaFacebookF />
                  </a>
                  <a className="btn btn-primary btn-square mr-2" href="#">
                    <FaLinkedinIn />
                  </a>
                  <a className="btn btn-primary btn-square" href="#">
                    <PiInstagramLogoFill />
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div
          className="row border-top mx-xl-5 py-4"
          style={{ borderColor: "rgba(256, 256, 256, .1) !important" }}
        ></div>
      </div>
    </>
  );
};

export default Footer;
