import { UserProfile } from "./User";

export interface IRegisterDto {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
}

export interface ILoginDto {
  email: string;
  password: string;
}

export type UserProfileToken = {
  authenticationDto: UserProfile;
  token: string;
};
