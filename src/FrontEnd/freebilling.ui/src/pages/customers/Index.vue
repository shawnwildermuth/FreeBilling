<script setup lang="ts">
import { useState } from "@/stores";

const state = useState();

onMounted(async () => await state.loadCustomers());

</script>

<template>
  <div>
    <h2>Customers</h2>
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
            <td class="text-blue-400"><a href="tel:{{c.phoneNumber}}">{{ c.phoneNumber }}</a></td>
            <td>
              <div class="join">
                <router-link :to="'/customers/' + c.id" class="btn btn-sm btn-info join-item">Edit</router-link>
                <router-link :to="'/customers/tickets/' + c.id" class="btn btn-sm btn-success join-item">Tickets</router-link>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
