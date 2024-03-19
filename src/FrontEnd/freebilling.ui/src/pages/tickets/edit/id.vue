<script setup lang="ts">
import { computed } from 'vue';
import { useRoute } from 'vue-router';


const route = useRoute();
const title = computed(() => route.params.id === "new" ? "New Ticket" : "Edit Ticket");


</script>

<template>
  <h2>{{ title }}</h2>
</template>