import Axios from "axios";
import { useVars } from "./vars";

const vars = useVars();

const http = Axios.create({
  baseURL: vars.API_URL
})

function get<T>(url: string) {
  return http.get<T>(url); 
}

function post<T>(url: string, data: T) {
  return http.post(url, data);
}

function put<T>(url: string, data: T) {
  return http.put(url, data);
}

function del<T>(url: string) {
  return http.delete<T>(url);
}


export function useHttp() {
  return {
    get,
    post,
    put,
    del
  };
};