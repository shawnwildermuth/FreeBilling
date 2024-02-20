<script setup lang="ts">
import type { Customer } from '@/models/customer';
import type { Project } from '@/models/project';
import { useState } from '@/stores';
import { onMounted, reactive, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const router = useRouter();
const route = useRoute();
const state = useState();
const customer = ref<Customer>();
const projects = reactive(new Array<Project>());

onMounted(async () => {
  customer.value = await state.getCustomer(Number(route.params.id));
  const data = await state.getProjects(Number(route.params.id));
  if (data) projects.splice(0, projects.length, ...data);
});

</script>
<template>
  <div>
    <div class="flex justify-between">
      <button class="btn btn-sm btn-info"
        @click.prevent="router.back()"><icon-back /> Back</button>
      <div>
        <router-link class="btn btn-sm btn-secondary"
        to="/customer/edit/{{ customer?.id }}"><icon-edit /> Edit</router-link>
      </div>
    </div>
    <div v-if="customer">
      <h2>{{ customer?.companyName }}</h2>
      <div v-if="customer?.address" class="text-lg p-1 ml-2 border-gray-500/50">
        <customer-address :address="customer?.address" />
      </div>
    </div>
    <div>
      <h3>Projects</h3>
      <div v-for="p in projects">
        {{ p.projectName }}
      </div>
    </div>
  </div>
</template>

