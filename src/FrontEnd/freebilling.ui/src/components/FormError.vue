<script setup lang="ts">
import { ref } from 'vue';

type ErrorList = { _errors: string[] } | undefined;

defineProps<{
  errors: ErrorList
}>()

</script>

<template>
  <div v-if="errors" class="text-warning">
    <ul>
      <li v-for="e in errors._errors">{{ e }}</li>
    </ul>
  </div>
</template>
