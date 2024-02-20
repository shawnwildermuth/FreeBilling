<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import { useState } from './stores';

const state = useState();

</script>

<template>
  <div data-theme="night">
    <div class="flex flex-row">
      <div class="flex-grow-0 p-2 h-screen  bg-base-200 min-w-60">
        <h1 class="hover:text-gray-200">
          <RouterLink to="/">FreeBilling</RouterLink>
        </h1>
        <ul class="menu text-lg">
          <li><router-link to="/">Home</router-link></li>
          <li><router-link to="/customers">Customers</router-link></li>
          <li><router-link to="/reports">Reporting</router-link></li>
          <li><router-link to="/accounting">Accounting</router-link></li>
        </ul>
      </div>
      <div class="flex-grow p-2">
        <div role="alert" class="alert shadow-lg" v-if="state.error">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
            class="stroke-info shrink-0 w-6 h-6">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
              d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z">
            </path>
          </svg>
          <div>
            <h3 class="font-bold">Error!</h3>
            <div class="text-xs">{{ state.error }}</div>
          </div>
          <button class="btn btn-sm" @click="state.error = ''">&xcap;</button>
        </div>
        <RouterView />
      </div>
  </div>
</div></template>

