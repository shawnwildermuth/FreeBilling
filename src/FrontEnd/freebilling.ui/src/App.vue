<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import { useState } from './stores';
import { watch } from 'vue';

const state = useState();
let timeoutId = 0;

// Clear after five seconds
watch(() => state.error,
  () => {
    if (state.error) {
      timeoutId = setTimeout(() => {
        state.error = "";
      }, 5000)
    }
  })

function closeError() {
  clearTimeout(timeoutId);
  state.error = "";
}

</script>

<template>
  <div data-theme="night">
    <div v-if="state.isBusy"
      class="absolute w-full h-screen z-50 flex justify-center items-center bg-gray-800/75">
      <span class="loading loading-ring loading-lg"></span>
    </div>
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
      <div class="flex-grow p-1">
        <transition enter-active-class="duration-500 ease-out"
          enter-from-class="transform opacity-0" enter-to-class="opacity-100"
          leave-active-class="duration-300 ease-in"
          leave-from-class="opacity-100" leave-to-class="transform opacity-0">
          <div role="alert" class="alert p-1 alert-warning shadow-sm"
            v-if="state.error">
            <icon-info />
            <div>
              <h3 class="font-bold">Error!</h3>
              <div class="text-xs">{{ state.error }}</div>
            </div>
            <div @click="closeError">
              <icon-delete />
            </div>
          </div>
        </transition>
        <RouterView />
      </div>
    </div>
  </div>
</template>
