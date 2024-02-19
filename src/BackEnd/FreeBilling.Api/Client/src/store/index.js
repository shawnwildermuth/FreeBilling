import { reactive } from "vue";

export default reactive({
  auth: {
    token: "",
    expiration: Date(),
    username: "",
    isAuthenticated() {
      return this.token && this.expiration < Date();
    }
  }
});