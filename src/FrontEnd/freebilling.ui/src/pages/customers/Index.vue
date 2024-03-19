<script setup lang="ts">
import { useCustomerStore } from "@/stores/customerStore";
import { onMounted, ref } from "vue";
import ConfirmationDialog from "@/components/ConfirmationDialog.vue";

const customerStore = useCustomerStore();
const confirmationDialog = ref<typeof ConfirmationDialog>();
let tempId = 0;

function checkDelete(id: number) {
  tempId = id;
  confirmationDialog?.value?.showModal();
}

async function finishDelete(confirmed: boolean) {
  if (confirmed) {
    await customerStore.deleteCustomer(tempId);
  }
  tempId = 0;
}

onMounted(async () => await customerStore.loadCustomers());

</script>

<template>
  <div>
    <confirmation-dialog @confirm="finishDelete" ref="confirmationDialog" />
    <div class="flex justify-between">
      <h2>Customers</h2>
      <router-link to="/customers/editor/new" class="btn btn-sm btn-success mb-1">
        <IconPlus /> New Customer
      </router-link>
    </div>
    <div class="p-1">
      <table class="table table-auto text-lg table-zebra border border-base-content/25">
        <thead class="text-lg bg-base-300 text-gray-100">
          <tr>
            <th>Company Name</th>
            <th>Contact</th>
            <th>Phone</th>
            <th class="w-96"></th>
          </tr>
        </thead>
        <tbody>
          <tr class="hover:!bg-slate-800/75" v-for="c in customerStore.customers" :key="c.id">
            <td>{{ c.companyName }}</td>
            <td>{{ c.contact }}</td>
            <td class="text-blue-400"><a class=" hover:underline" :href="`tel:${c.phoneNumber}`">{{
            c.phoneNumber }}</a></td>
            <td>
              <div class="join">
                <router-link :to="'/customers/' + c.id" title="Details"
                  class="btn btn-xs btn-info join-item">
                  <icon-details /> Details
                </router-link>
                <router-link :to="'/customers/editor/' + c.id" title="Edit"
                  class="btn btn-xs btn-primary join-item">
                  <icon-edit /> Edit
                </router-link>
                <button @click="checkDelete(c.id)" title="Delete"
                  class="btn btn-xs btn-warning join-item">
                  <icon-delete /> Delete
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>@/stores/customerStore
