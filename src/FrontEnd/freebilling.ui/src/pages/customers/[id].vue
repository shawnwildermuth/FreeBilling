<script setup lang="ts">
import type { Customer } from '@/models/customer';
import { useState } from '@/stores';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const state = useState();
const customer = ref<Customer>();

onMounted(async ()=> customer.value = await state.loadCustomer(Number(route.params.id)));

</script>
<template>
  <div>
    <h2>{{ customer?.companyName }}</h2>
    
  </div>
</template>

