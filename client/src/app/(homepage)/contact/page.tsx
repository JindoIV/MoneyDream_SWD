"use client";

import Link from "next/link";
import { FaPhoneAlt } from "react-icons/fa";
import { FaLocationDot } from "react-icons/fa6";
import { MdEmail } from "react-icons/md";

const ContactPage = () => {
  return (
    <>
      <div className="container-fluid">
        <div className="row px-xl-5">
          <div className="col-12">
            <nav className="breadcrumb bg-light mb-30">
              <Link className="breadcrumb-item text-dark" href="/">
                Home
              </Link>
              <span className="breadcrumb-item active">Contact</span>
            </nav>
          </div>
        </div>
      </div>

      <div className="container-fluid">
        <h2 className="section-title position-relative text-uppercase mx-xl-5 mb-4">
          <span className="bg-secondary pr-3">Contact Us</span>
        </h2>
        <div className="row px-xl-5">
          <div className="col-lg-7 mb-5">
            <div className="contact-form bg-light p-30">
              <div id="success"></div>
              <form name="sentMessage" id="contactForm">
                <div className="control-group">
                  <input
                    type="text"
                    className="form-control"
                    id="name"
                    placeholder="Your Name"
                    data-validation-required-message="Please enter your name"
                  />
                  <p className="help-block text-danger"></p>
                </div>
                <div className="control-group">
                  <input
                    type="email"
                    className="form-control"
                    id="email"
                    placeholder="Your Email"
                    data-validation-required-message="Please enter your email"
                  />
                  <p className="help-block text-danger"></p>
                </div>
                <div className="control-group">
                  <input
                    type="text"
                    className="form-control"
                    id="subject"
                    placeholder="Subject"
                    data-validation-required-message="Please enter a subject"
                  />
                  <p className="help-block text-danger"></p>
                </div>
                <div className="control-group">
                  <textarea
                    className="form-control"
                    // rows="8"
                    id="message"
                    placeholder="Message"
                    data-validation-required-message="Please enter your message"
                  ></textarea>
                  <p className="help-block text-danger"></p>
                </div>
                <div>
                  <button
                    className="btn btn-primary py-2 px-4"
                    type="submit"
                    id="sendMessageButton"
                  >
                    Send Message
                  </button>
                </div>
              </form>
            </div>
          </div>
          <div className="col-lg-5 mb-5">
            <div className="bg-light p-30 mb-30">
              <iframe
                style={{ width: "100%", height: "250px", border: "0" }}
                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3929.0532902991113!2d105.72985667416333!3d10.012457072822015!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31a0882139720a77%3A0x3916a227d0b95a64!2sFPT%20University!5e0!3m2!1svi!2s!4v1719931940705!5m2!1svi!2s"
                // frameborder="0"
                // allowfullscreen=""
                aria-hidden="false"
                // tabindex="0"
              ></iframe>
            </div>
            <div className="bg-light p-30 mb-3">
              <p className="mb-2">
                <FaLocationDot className="text-primary mr-3" />
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
          </div>
        </div>
      </div>
    </>
  );
};

export default ContactPage;
