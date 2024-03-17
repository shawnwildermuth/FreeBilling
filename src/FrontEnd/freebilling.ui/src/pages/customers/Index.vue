<script setup lang="ts">
import { useState } from "@/stores";
import { onMounted, ref } from "vue";
import ConfirmationDialog
 from "@/components/ConfirmationDialog.vue";
const state = useState();

const confirmationDialog = ref<typeof ConfirmationDialog>();

let tempId = 0;

function checkDelete(id: number) {
  tempId = id;
  confirmationDialog?.value?.showModal();
}

async function finishDelete(confirmed: boolean) {
  if (confirmed) {
    await state.deleteCustomer(tempId);
  }
  tempId = 0;
}

onMounted(async () => await state.loadCustomers());

</script>

<template>
  <div>
    <confirmation-dialog @confirm="finishDelete" ref="confirmationDialog" />
    <div class="flex justify-between">
      <h2>Customers</h2>
      <router-link to="/customers/editor/new" class="btn btn-success">
        <IconPlus /> New Customer
      </router-link>
    </div>
    <div class="p-1">
      <table class="table text-lg table-zebra border border-base-content/25">
        <thead class="text-lg">
          <tr>
            <th>Company Name</th>
            <th>Contact</th>
            <th>Phone</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="c in state.customers" :key="c.id">
            <td>{{ c.companyName }}</td>
            <td>{{ c.contact }}</td>
            <td class="text-blue-400"><a href="tel:{{c.phoneNumber}}">{{
            c.phoneNumber }}</a></td>
            <td>
              <div class="join">
                <router-link :to="'/customers/' + c.id" title="Details"
                  class="btn btn-sm btn-info join-item">
                  <icon-details />
                </router-link>
                <router-link :to="'/customers/tickets/' + c.id"
                  title="View Tickets"
                  class="btn btn-sm btn-success join-item">
                  <icon-ticket />
                </router-link>
                <router-link :to="'/customers/editor/' + c.id" title="Edit"
                  class="btn btn-sm btn-primary join-item">
                  <icon-edit />
                </router-link>
                <button @click="checkDelete(c.id)" title="Delete"
                  class="btn btn-sm btn-warning join-item">
                  <icon-delete />
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
