<script setup lang="ts">
import { type Customer, CustomerSchema } from '@/models/Customer';
import { useState } from '@/stores';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { stateList } from "@/lookups/stateList";


const state = useState();
const route = useRoute();
const router = useRouter();

let id = 0;
let lastUrl: string | symbol = "";
const customer = ref<Customer>();

onMounted(async () => {
  if (route.params.id === "new") {
    // reset the object
    id = -1;
    customer.value = {} as Customer;
  } else {
    id = Number(route.params.id);
    const result = await state.getCustomer(id);
    if (result) {
      customer.value = result;
    }
  }

  if (route.redirectedFrom) {
    lastUrl = route.redirectedFrom?.name ?? "";
  }
});

function cancel() {
  router.back();
}

async function save() {
  
}
</script>

<template>
  <div class="w-[48rem]">
    <h3>Customer</h3>
    <div class="p-1 flex flex-col gap-2">
      <label class="theLabel">
        <div class="label">Company Name</div>
        <input type="text" placeholder="e.g. Acme Inc." class="theInput" />
      </label>
      <label class="theLabel">
        <div class="label">Contact Name</div>
        <input type="text" placeholder="e.g. Bob Smith" class="theInput" />
      </label>
      <label class="theLabel">
        <div class="label">Phone</div>
        <input type="text" placeholder="e.g. 404-555-1212" class="theInput" />
      </label>
      <label class="theLabel">
        <div class="label">Address</div>
        <input type="text" placeholder="e.g. 123 Main St." class="theInput" />
        <input type="text" class="theInput" />
        <input type="text" class="theInput" />
      </label>
      <div class="flex gap-1">
        <label class="theLabel">
          <div class="label">City</div>
          <input type="text" placeholder="e.g. Anytown" class="theInput" />
        </label>
        <label class="theLabel">
          <div class="label">State/Province</div>
          <select class="select select-bordered w-full" >
            <option disabled>Pick One...</option>
            <option v-for="s in stateList" :value="s.abbreviation">{{ s.name }}</option>
                    </select>
        </label>
        <label class="theLabel">
          <div class="label">Postal Code</div>
          <input type="text" placeholder="e.g. 30303" class="theInput" />
        </label>
      </div>
      <label class="theLabel">
        <div class="label">Country</div>
        <input type="text" placeholder="e.g. Acme Inc." class="theInput" />
      </label>

      <div class="flex justify-end gap-1">
        <button @click="cancel" class="btn btn-neutral">Cancel</button>
        <button @click="save" class="btn btn-success">Save</button>
      </div>
    </div>
  </div>
</template>
