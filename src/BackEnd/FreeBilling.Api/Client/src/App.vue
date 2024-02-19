<script setup>
  import { RouterLink, RouterView } from "vue-router";
  import store from "./store";
</script>

<template>
  <div class="min-h-screen">
    <header>
      <nav class="flex justify-end">
        <RouterLink
          class="mr-1 rounded p-2 bg-green-700 text-white hover:bg-green-500"
          to="/"
          >Home</RouterLink
        >
        <RouterLink
          class="mr-1 rounded p-2 bg-green-700 text-white hover:bg-green-500"
          to="/login"
          >Login</RouterLink
        >
      </nav>
    </header>

    <RouterView />
    <div>
      {{ store.auth }}
    </div>
  </div>
</template>
