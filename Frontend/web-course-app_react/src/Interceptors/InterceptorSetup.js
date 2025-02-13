import api from "./api";
import { authInterceptor } from "./AuthInterceptor";
import { redirectInterceptor } from "./RedirectInterceptor";

api.interceptors.request.use(authInterceptor);
api.interceptors.response.use((response) => response, redirectInterceptor);

export default api;