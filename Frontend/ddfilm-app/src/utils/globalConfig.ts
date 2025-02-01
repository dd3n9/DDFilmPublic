export const MOVIES_URL = "/movies";

export const REGISTER_URL = "/account/register";
export const LOGIN_URL = "/account/login";
export const REFRESH_TOKEN_URL = "/account/refreshToken";

export const SESSION_URL = "/sessions";

export const SESSION_HUB_URL = "/sessions/session-hub";
export const RATING_HUB_URL = "/sessions/rating-hub";
export const WATCHING_HUB_URL = "/sessions/watching-hub";

export const LOCAL_VIDEO = "localVideo";

export type provideMediaRefType = (
  id: string,
  node: HTMLVideoElement | null
) => void;
