import * as Yup from "yup";
import { useAuth } from "../context/useAuth";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { IRegisterDto } from "../models/auth";
import { PATH_PUBLIC } from "../routes/paths";

type Props = {};

type RegisterFormsInputs = {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
};

const validation = Yup.object().shape({
  firstName: Yup.string().required("First name is required"),
  lastName: Yup.string().required("Last name is required"),
  userName: Yup.string()
    .required("Username is required")
    .min(3, "Username must be at least 3 characters"),
  email: Yup.string()
    .required("Email is required")
    .email("Enter a valid email address"),
  password: Yup.string()
    .required("Password is required")
    .min(6, "Password must be at least 6 characters"),
});

const RegisterPage = (props: Props) => {
  const { registerUser } = useAuth();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormsInputs>({ resolver: yupResolver(validation) });

  const handleRegister = (form: RegisterFormsInputs) => {
    const registerDTO: IRegisterDto = {
      firstName: form.firstName,
      lastName: form.lastName,
      userName: form.userName,
      email: form.email,
      password: form.password,
    };
    registerUser(registerDTO);
  };
  return (
    <div className="flex justify-center items-center min-h-screen  relative overflow-hidden">
      <div className="relative bg-[#2F0740] p-8 rounded-3xl shadow-lg w-full max-w-md z-10">
        <h2 className="text-2xl font-bold text-center text-[#F244D5] mb-6">
          Create Account
        </h2>
        <form onSubmit={handleSubmit(handleRegister)} className="space-y-6">
          <div>
            <label className="block text-sm font-medium text-[#F244D5] mb-2">
              First Name
            </label>
            <input
              type="text"
              {...register("firstName")}
              className={`w-full px-5 py-3 rounded-full bg-[#1E0A29] text-[#F244D5] placeholder-[#710973] focus:outline-none focus:ring-4 focus:ring-[#F244D5] transition-all ${
                errors.firstName ? "ring-2 ring-[#F244D5]" : ""
              }`}
              placeholder="Enter your first name"
            />
            {errors.firstName && (
              <p className="text-[#F244D5] text-sm mt-1">
                {errors.firstName.message}
              </p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-[#F244D5] mb-2">
              Last Name
            </label>
            <input
              type="text"
              {...register("lastName")}
              className={`w-full px-5 py-3 rounded-full bg-[#1E0A29] text-[#F244D5] placeholder-[#710973] focus:outline-none focus:ring-4 focus:ring-[#F244D5] transition-all ${
                errors.lastName ? "ring-2 ring-[#F244D5]" : ""
              }`}
              placeholder="Enter your last name"
            />
            {errors.lastName && (
              <p className="text-[#F244D5] text-sm mt-1">
                {errors.lastName.message}
              </p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-[#F244D5] mb-2">
              Username
            </label>
            <input
              type="text"
              {...register("userName")}
              className={`w-full px-5 py-3 rounded-full bg-[#1E0A29] text-[#F244D5] placeholder-[#710973] focus:outline-none focus:ring-4 focus:ring-[#F244D5] transition-all ${
                errors.userName ? "ring-2 ring-[#F244D5]" : ""
              }`}
              placeholder="Enter your username"
            />
            {errors.userName && (
              <p className="text-[#F244D5] text-sm mt-1">
                {errors.userName.message}
              </p>
            )}
          </div>

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
            Register
          </button>

          <p className="text-center text-sm text-[#F244D5]">
            Already have an account?{" "}
            <button
              type="button"
              onClick={() => navigate(PATH_PUBLIC.login)}
              className="text-[#75BFB8] underline hover:text-[#F244D5] transition-all"
            >
              Login
            </button>
          </p>
        </form>
      </div>
    </div>
  );
};

export default RegisterPage;
