import React from "react";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { useAuth } from "../context/useAuth";
import { ILoginDto } from "../models/auth";
import { useNavigate } from "react-router-dom";
import { PATH_PUBLIC } from "../routes/paths";

type Props = {};

type LoginFormsInputs = {
  email: string;
  password: string;
};

const validation = Yup.object().shape({
  email: Yup.string().required("Email is required"),
  password: Yup.string().required("Password is required"),
});

const LoginPage = (props: Props) => {
  const { loginUser } = useAuth();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormsInputs>({ resolver: yupResolver(validation) });

  const handleLogin = (form: LoginFormsInputs) => {
    const loginDTO: ILoginDto = {
      email: form.email,
      password: form.password,
    };
    loginUser(loginDTO);
  };

  return (
    <div className="flex justify-center items-center min-h-screen ">
      <div className="bg-[#2F0740] p-8 rounded-3xl shadow-lg w-full max-w-md">
        <h2 className="text-2xl font-bold text-center text-[#F244D5] mb-6">
          Welcome Back
        </h2>
        <form onSubmit={handleSubmit(handleLogin)} className="space-y-6">
          <div>
            <label className="block text-sm font-medium text-[#F244D5] mb-2">
              Email
            </label>
            <input
              type="email"
              {...register("email")}
              className={`w-full px-5 py-3 rounded-full bg-[#1E0A29] text-[#F244D5] placeholder-[#710973] focus:outline-none focus:ring-4 focus:ring-[#F244D5] transition-all ${
                errors.email ? "ring-2 ring-[#F244D5]" : ""
              }`}
              placeholder="Enter your email"
            />
            {errors.email && (
              <p className="text-[#F244D5] text-sm mt-1">
                {errors.email.message}
              </p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-[#F244D5] mb-2">
              Password
            </label>
            <input
              type="password"
              {...register("password")}
              className={`w-full px-5 py-3 rounded-full bg-[#1E0A29] text-[#F244D5] placeholder-[#710973] focus:outline-none focus:ring-4 focus:ring-[#F244D5] transition-all ${
                errors.password ? "ring-2 ring-[#F244D5]" : ""
              }`}
              placeholder="Enter your password"
            />
            {errors.password && (
              <p className="text-[#F244D5] text-sm mt-1">
                {errors.password.message}
              </p>
            )}
          </div>

          <button
            type="submit"
            className="w-full bg-gradient-to-r from-[#F244D5] to-[#710973] text-white py-3 rounded-full font-semibold hover:shadow-lg hover:from-[#710973] hover:to-[#F244D5] transition-all"
          >
            Login
          </button>

          <p className="text-center text-sm text-[#F244D5]">
            Don't have an account?{" "}
            <button
              type="button"
              onClick={() => navigate(PATH_PUBLIC.register)}
              className="text-[#75BFB8] underline hover:text-[#F244D5] transition-all"
            >
              Register
            </button>
          </p>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;
