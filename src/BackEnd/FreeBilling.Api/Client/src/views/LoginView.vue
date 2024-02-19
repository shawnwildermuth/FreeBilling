<script setup>
  import { reactive, ref } from "vue";
  import store from "../store";
  import axios from "axios";
  import router from "../router";

  const loginInfo = reactive({
    username: "shawn@wildermuth.com",
    password: "P@ssw0rd!"
  })

  const message = ref("");
  const error = ref("");

  async function submitLogin() {

    // Clear these first
    message.value = "";
    error.value = "";

    const response = await axios.post("/api/auth/signin", loginInfo);

    if (response.status === 201) { // Created
      store.auth.token = response.data.token;
      store.auth.expiration = response.data.expiration;
      store.auth.username = response.data.username;
      router.push("/");
    }
    else {
      error.value = response.data;
    }
  }
</script>

<template>
  <div class="w-1/2 mx-auto">
    <h1>Login</h1>
    <div class="w-full" v-if="message">{{ message }}</div>
    <div class="bg-red-800 text-white w-full rounded p-2" v-if="error">{{ error }}</div>
    <form novalidate @submit.prevent="submitLogin" class="flex flex-col">
      <label for="username">User Name</label>
      <input type="text" name="username" v-model="loginInfo.username" />
      <label for="Password">Password</label>
      <input type="password" name="password" v-model="loginInfo.password" />
      <button type="submit">Login</button>
    </form>
  </div>
</template>

