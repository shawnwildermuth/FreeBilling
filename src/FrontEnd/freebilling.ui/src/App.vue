<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <div data-theme="dim">
    <header>
      <div class="flex justify-between p-1">
        <h1 class="hover:text-gray-200">
          <RouterLink to="/">FreeBilling</RouterLink>
        </h1>
        <nav>
          <RouterLink class="btn" to="/">Home</RouterLink>
          <RouterLink class="btn" to="/about">About</RouterLink>
        </nav>
      </div>
    </header>

    <RouterView />
  </div>
</template>

