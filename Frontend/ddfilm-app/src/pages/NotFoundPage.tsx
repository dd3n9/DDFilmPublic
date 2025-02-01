import { Link } from "react-router-dom";

const NotFoundPage = () => {
  return (
    <section className=" min-h-screen flex items-center justify-center">
      <div className="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
        <div className="mx-auto max-w-screen-sm text-center">
          <h1 className="mb-4 text-7xl tracking-tight font-extrabold lg:text-9xl text-[#710973]">
            404
          </h1>
          <p className="mb-4 text-3xl tracking-tight font-bold text-[#F244D5] md:text-4xl">
            Something's missing.
          </p>
          <p className="mb-6 text-lg font-light text-[#bd65ae]">
            Sorry, we can't find that page. You'll find lots to explore on the
            home page.
          </p>
          <Link
            to="/"
            className=" px-4 py-2 bg-[#710973] text-white rounded-full hover:bg-[#F244D5] transition-all"
          >
            Back to Homepage
          </Link>
        </div>
      </div>
    </section>
  );
};

export default NotFoundPage;
