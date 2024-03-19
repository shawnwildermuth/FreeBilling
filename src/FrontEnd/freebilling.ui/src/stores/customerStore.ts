import { defineStore } from 'pinia'
import { type Customer } from "@/models/Customer";
import { useHttp } from '@/composables/http';
import { reactive, ref } from 'vue';
import type { CustomerDetails } from '@/models/CustomerDetails';
import type { AxiosError } from 'axios';
import { useStore } from "@/stores";

const http = useHttp();
const store = useStore();

const customers = reactive(new Array<Customer>());

async function loadCustomers() {
  try {
    store.startRequest()
    const result = await http.get<Customer[]>("/api/customers");
    if (result.data) {
      customers.splice(0, customers.length, ...result.data);
    }
  } catch (e) {
    const httpError = e as AxiosError;
    store.error = httpError.message;
  } finally {
    store.endRequest();
  }
}

async function getCustomer(id: number) {
  // TODO, get from cached customers
  try {
    store.startRequest()
    const result = await http.get<Customer>(`/api/customers/${id}`);
    if (result.data) {
      return result.data;
    } else {
      store.error = "Could not find customer";
      return undefined;
    }
  } finally {
    store.endRequest();
  }
}

async function getCustomerDetails(id: number) {
  try {
    store.startRequest()
    const result = await http.get<CustomerDetails>(`/api/customers/${id}/details`);
    if (result.data) {
      return result.data;
    } else {
      store.error = "Could not find projects";
      return undefined;
    }
  } catch (e) {
    const httpError = e as AxiosError;
    store.error = httpError.message;
  } finally {
    store.endRequest();
  }
}

async function saveCustomer(customer: Customer) {
  try {
    store.startRequest()
    const result = await http.post("/api/customers/", customer);
    if (result.status === 201) {
      const found = customers.findIndex(c => c.id === result.data.id);
      if (found > -1) {
        customers.splice(found, 1, result.data);
      }
      return true;
    }
    store.error = result.statusText;
  } catch (e) {
    store.error = `Failed to save customer: ${e}`;
  } finally {
    store.endRequest();
  }
  
  return false;
}

async function updateCustomer(customer: Customer) {
  try {
    store.startRequest()
    const result = await http.put(`/api/customers/${customer.id}`, customer);
    if (result.status === 200) {
      const found = customers.findIndex(c => c.id === result.data.id);
      if (found > -1) {
        customers.splice(found, 1, result.data);
      }
      return true;
    }
    store.error = result.statusText;
  } catch (e) {
    store.error = `Failed to update customer: ${e}`;
  } finally {
    store.endRequest();
  }
  return false;
}

async function deleteCustomer(id: Number) {
  try {
    store.startRequest()
    const result = await http.del(`/api/customers/${id}`);
    if (result.status === 200) {
      const found = customers.findIndex(c => c.id === id);
      if (found > -1) {
        customers.splice(found, 1);
      }
      return true;
    }
    store.error = result.statusText;
  } catch (e) {
    store.error = `Failed to delete customer: ${e}`;
  } finally {
    store.endRequest();
  }
  return false;
}

export const useCustomerStore = defineStore('customers', () => {

  return {
    customers,
    loadCustomers,
    getCustomer,
    getCustomerDetails,
    saveCustomer,
    updateCustomer,
    deleteCustomer
  };
})
