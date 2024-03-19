<script setup lang="ts">
import type { CustomerDetails } from '@/models/CustomerDetails';
import { useCustomerStore } from '@/stores/customerStore';
import { onMounted, reactive, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { money, shortDate } from "@/filters";
import type { ProjectModel } from '@/models/ProjectModel';

const router = useRouter();
const route = useRoute();
const store = useCustomerStore();
const customer = ref<CustomerDetails>();

onMounted(async () => {
  customer.value = await store.getCustomerDetails(Number(route.params.id));
});

function endProject(project: ProjectModel) {
  // todo
}

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
      <p>Customer Balance: {{ money(customer?.totalBalance) }}</p>
      <div v-if="customer?.address" class="text-lg p-1 ml-2 border-gray-500/50">
        <customer-address :address="customer?.address" />
      </div>
    </div>
    <div>
      <h3>Projects</h3>
      <table class="table table-zebra m-1 ">
        <thead class="bg-slate-800 text-lg text-slate-200">
          <tr>
            <td>Name</td>
            <td>Start Date</td>
            <td>End Date</td>
            <td>Project Total</td>
            <td class="w-72"></td>
          </tr>
        </thead>
        <tbody>
          <tr v-for="p in customer?.projects">
            <td>{{ p.projectName }}</td>
            <td>{{ shortDate(p?.startDate) }}</td>
            <td>{{ shortDate(p?.endDate) }}</td>
            <td>{{ money(p.projectTotal) }}</td>
            <td>
              <div class="join">
                <router-link :to="`/projects/edit/${p.id}`" class="btn btn-xs btn-primary join-item"><icon-edit class="w-4 h-4" /> Edit</router-link>
                <router-link :to="`/tickets/new?projectid=${p.id}`" class="btn btn-xs btn-secondary join-item"><icon-plus class="w-4 h-4"/> Ticket</router-link>
                <button @click="endProject(p)" class="btn btn-xs btn-warning join-item"><icon-post class="w-4 h-4"/> End</button>
              </div>

            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
@/stores/customerStore
