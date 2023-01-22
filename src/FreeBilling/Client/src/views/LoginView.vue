<script setup>
  import { reactive, ref } from "vue";
  import store from "../store";

  const loginInfo = reactive({
    username: "",
    password: ""
  })

  const message = ref("");
  const error = ref("");

  async function submitLogin() {

    // Clear these first
    message.value = "";
    error.value = "";

    const response = await fetch("/api/auth/signin", {
      method: "POST",
      //headers: {
      //  'Content-Type': 'application/json'
      //},
      body: JSON.stringify(loginInfo)
    });

    const body = await response.json();

    if (response.status === 201) { // Created
      store.auth.token = body.token;
      store.auth.expiration = body.expiration;
    }
    else {
      error.value = body;
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

