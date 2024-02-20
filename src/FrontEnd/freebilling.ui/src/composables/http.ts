import Axios from "axios";
import { useVars } from "./vars";

const vars = useVars();

const http = Axios.create({
  baseURL: vars.API_URL
})

function get<T>(url: string) {
  return http.get<T>(url); 
}

export function useHttp() {
  return {
    get
  };
};