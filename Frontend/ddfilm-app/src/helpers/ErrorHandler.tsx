import axios from "axios";
import { error } from "console";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { PATH_PUBLIC } from "../routes/paths";

export const handleError = (error: any) => {
  if (axios.isAxiosError(error)) {
    const err = error.response;

    if (err && err.data?.errors && Array.isArray(err.data?.errors)) {
      for (const errorMessage of err.data.errors) {
        toast.warning(errorMessage);
      }
      for (const validationError of err.data.errors) {
        toast.warning(validationError.errorMessage);
      }
    } else if (err && typeof err.data?.errors === "object") {
      for (const key in err.data.errors) {
        if (Array.isArray(err.data.errors[key])) {
          for (const msg of err.data.errors[key]) {
            toast.warning(msg);
          }
        } else {
          toast.warning(err.data.errors[key]);
        }
      }
    } else if (err && err.data?.errors) {
      toast.warning(err.data.errors);
    } else if (err?.status === 401) {
      toast.warning("Please login");
      window.location.href = PATH_PUBLIC.login;
    } else if (err?.data) {
      toast.warning(err.data);
    }
  } else {
    console.error("Unexpected error:", error);
    toast.error("Something went wrong!");
  }
};
