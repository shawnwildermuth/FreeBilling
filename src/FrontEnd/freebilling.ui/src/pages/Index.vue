<script setup lang="ts">
import { useCustomersStore } from "@/stores/customers";

const store = useCustomersStore();

onMounted(async () => await store.load());

</script>

<template>
  <main>
    <div v-for="c in store.customers">
      {{ c.companyName }}
    </div>
  </main>
</template>
