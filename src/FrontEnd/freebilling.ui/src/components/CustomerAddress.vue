<script setup lang="ts">
import type { Address } from '@/models/address';

defineProps<{
  address: Address
}>();
</script>

<template>
  <div v-if="address">
    <div v-if="address.addressLine1">{{ address.addressLine1 }}</div>
    <div v-if="address.addressLine2">{{ address.addressLine2 }}</div>
    <div v-if="address.addressLine2">{{ address.addressLine3 }}</div>
    <div v-if="address.city || address.stateProvince || address.postalCode">{{ address.city }}, {{ address.stateProvince }}  {{ address.postalCode }}</div>
    <div v-if="address.country">{{ address.country }}</div>
  </div>
</template>