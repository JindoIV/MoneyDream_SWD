"use client";
import Image from "next/image";
import advL from "@/assets/image/advL.jpg";
import advR from "@/assets/image/advR.jpg";

export default function Advertisement() {
  return (
    <>
      <div className="container-fluid pt-5 pb-3">
        <div className="row px-xl-5">
          <div className="col-md-6">
            <div className="product-offer mb-30" style={{ height: "300px" }}>
              <Image className="Image-fluid" src={advL.src} alt="" fill />
              <div className="offer-text">
                <h6 className="text-white text-uppercase">Save 20%</h6>
                <h3 className="text-white mb-3">Special Offer</h3>
                <a href="" className="btn btn-primary">
                  Shop Now
                </a>
              </div>
            </div>
          </div>
          <div className="col-md-6">
            <div className="product-offer mb-30" style={{ height: "300px" }}>
              <Image className="Image-fluid" src={advR.src} alt="" fill />
              <div className="offer-text">
                <h6 className="text-white text-uppercase">Save 20%</h6>
                <h3 className="text-white mb-3">Special Offer</h3>
                <a href="" className="btn btn-primary">
                  Shop Now
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
