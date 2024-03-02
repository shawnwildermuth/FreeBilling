<script setup lang="ts">
import { type Customer, CustomerSchema, type CustomerErrors } from '@/models/Customer';
import { useState } from '@/stores';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { stateList } from "@/lookups/stateList";

const state = useState();
const route = useRoute();
const router = useRouter();

let id = 0;
let lastUrl: string | symbol = "";
const customer = ref<Customer | null>(null);
const errors = ref<CustomerErrors | null>(null);

onMounted(async () => {
  if (route.params.id === "new") {
    // reset the object
    id = -1;
    customer.value = {
      address: {}
    } as Customer;
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
  if (lastUrl) {
    router.push(lastUrl.toString())
  } else {
    router.back();
  }
}

async function save() {
  errors.value = null;
  const valid = CustomerSchema.safeParse(customer.value);
  if (!valid.success) {
    // Store so we can show error
    errors.value = valid.error.format();
  } else {
    // Save it
  }
}
</script>

<template>
  <div class="w-[48rem]">
    <h3>Customer</h3>
    <div v-if="customer" class="p-1 flex flex-col gap-2">
      <label class="theLabel">
        <div class="label">Company Name</div>
        <input type="text" placeholder="e.g. Acme Inc." class="theInput"
          v-model="customer.companyName" />
        <form-error :errors="errors?.companyName" />
      </label>
      <label class="theLabel">
        <div class="label">Contact Name</div>
        <input type="text" placeholder="e.g. Bob Smith" class="theInput"
          v-model="customer.contact" />
        <form-error :errors="errors?.contact" />

      </label>
      <label class="theLabel">
        <div class="label">Phone</div>
        <input type="text" placeholder="e.g. 404-555-1212" class="theInput"
          v-model="customer.phoneNumber" />
        <form-error :errors="errors?.phoneNumbe" />

      </label>
      <label class="theLabel" v-if="customer.address">
        <div class="label">Address</div>
        <input type="text" placeholder="e.g. 123 Main St." class="theInput"
          v-model="customer.address.addressLine1" />
          <form-error :errors="errors?.address?.addressLine1" />

        <input type="text" class="theInput"
          v-model="customer.address.addressLine2" />
        <input type="text" class="theInput"
          v-model="customer.address.addressLine3" />
      </label>
      <div class="flex gap-1">
        <label class="theLabel">
          <div class="label">City</div>
          <input type="text" placeholder="e.g. Anytown" class="theInput"
            v-model="customer.address.city" />
            <form-error :errors="errors?.address?.city" />

        </label>
        <label class="theLabel">
          <div class="label">State/Province</div>
          <select class="select select-bordered w-full"
            v-model="customer.address.stateProvince">
            <option v-for="s in stateList" :value="s.abbreviation">{{ s.name }}
            </option>
          </select>
          <form-error :errors="errors?.address?.stateProvince" />

        </label>
        <label class="theLabel">
          <div class="label">Postal Code</div>
          <input type="text" placeholder="e.g. 30303" class="theInput"
            v-model="customer.address.postalCode" />
            <form-error :errors="errors?.address?.postalCode" />

        </label>
      </div>
      <label class="theLabel">
        <div class="label">Country</div>
        <input type="text" placeholder="e.g. Acme Inc." class="theInput"
          v-model="customer.address.country" />
          <form-error :errors="errors?.address?.country" />

      </label>

      <div class="flex justify-end gap-1">
        <button @click="cancel" class="btn btn-neutral">Cancel</button>
        <button @click="save" class="btn btn-success">Save</button>
      </div>
    </div>
    <pre>{{ customer }}</pre>
    <pre>{{ errors }}</pre>
  </div>
</template>
