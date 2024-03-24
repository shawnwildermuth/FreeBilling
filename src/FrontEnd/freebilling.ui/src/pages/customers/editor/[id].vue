<script setup lang="ts">
import { type Customer, CustomerSchema, type CustomerErrors, generateEmptyCustomer } from '@/models/Customer';
import { useCustomerStore } from '@/stores/customerStore';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { stateList } from "@/lookups/stateList";

const store = useCustomerStore();
const route = useRoute();
const router = useRouter();

let id = 0;
let lastUrl: string | symbol = "";
const customer = ref<Customer | null>(null);
const errors = ref<CustomerErrors | null>(null);
const dirty = ref(false);


onMounted(async () => {
  if (route.params.id === "new") {
    // reset the object
    id = -1;
    customer.value = generateEmptyCustomer();
  } else {
    id = Number(route.params.id);
    const result = await store.getCustomer(id);
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

function validate() {
  dirty.value = true;
  errors.value = null;
  const valid = CustomerSchema.safeParse(customer.value);
  if (!valid.success) errors.value = valid.error.format();
  return valid.success;
}

async function save() {
  if (validate()) {
    if (id === -1) {
      // Save it
      if (await store.saveCustomer(customer.value!)) {
        router.back();
      }
    } else {
      if (await store.updateCustomer(customer.value!)) {
        router.back();
      }
    }
  }
}

function handleFocusOut() {
  if (dirty.value) validate();
}

</script>

<template>
  <div class="w-[48rem]">
    <h3>Customer</h3>
    <div v-if="customer" class="p-1 flex flex-col gap-2"
      @focusout="handleFocusOut">
      <label class="theLabel">
        <div class="label">Company Name</div>
        <input type="text" placeholder="e.g. Acme Inc." class="theInput"
          :class="{ error: errors?.companyName }"
          v-model="customer.companyName" />
        <form-error :errors="errors?.companyName" />
      </label>
      <label class="theLabel">
        <div class="label">Contact Name</div>
        <input type="text" placeholder="e.g. Bob Smith" class="theInput"
          :class="{ error: errors?.contact }" v-model="customer.contact" />
        <form-error :errors="errors?.contact" />

      </label>
      <label class="theLabel">
        <div class="label">Phone</div>
        <input type="text" placeholder="e.g. 404-555-1212" class="theInput"
          :class="{ error: errors?.phoneNumber }"
          v-model="customer.phoneNumber" />
        <form-error :errors="errors?.phoneNumber" />

      </label>
      <label class="theLabel" v-if="customer.address">
        <div class="label">Address</div>
        <input type="text" placeholder="e.g. 123 Main St." class="theInput"
          :class="{ error: errors?.address?.addressLine1 }"
          v-model="customer.address.addressLine1" />
        <form-error :errors="errors?.address?.addressLine1" />

        <input type="text" class="theInput"
          v-model="customer.address.addressLine2" />
        <input type="text" class="theInput"
          v-model="customer.address.addressLine3" />
      </label>
      <div class="flex gap-1" v-if="customer.address">
        <label class="theLabel">
          <div class="label">City</div>
          <input type="text" placeholder="e.g. Anytown" class="theInput"
            :class="{ error: errors?.address?.city }"
            v-model="customer.address.city" />
          <form-error :errors="errors?.address?.city" />

        </label>
        <label class="theLabel">
          <div class="label">State/Province</div>
          <select class="theSelect"
            :class="{ error: errors?.address?.stateProvince }"
            v-model="customer.address.stateProvince">
            <option disabled selected value="">Select one...</option>
            <option v-for="s in stateList" :value="s.abbreviation">{{ s.name }}
            </option>
          </select>
          <form-error :errors="errors?.address?.stateProvince" />

        </label>
        <label class="theLabel">
          <div class="label">Postal Code</div>
          <input type="text" placeholder="e.g. 30303" class="theInput"
            :class="{ error: errors?.address?.postalCode }"
            v-model="customer.address.postalCode" />
          <form-error :errors="errors?.address?.postalCode" />

        </label>
        <label class="theLabel">
          <div class="label">Country</div>
          <input type="text" placeholder="e.g. USA" class="theInput"
            :class="{ error: errors?.address?.country }"
            v-model="customer.address.country" />
          <form-error :errors="errors?.address?.country" />

        </label>
      </div>

      <div class="flex justify-end gap-1">
        <button @click="cancel" class="btn btn-neutral">Cancel</button>
        <button @click="save" class="btn btn-success">Save</button>
      </div>
    </div>
  </div>
</template>
