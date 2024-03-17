import { defineStore } from 'pinia'
import { type Customer } from "@/models/Customer";
import type { Ticket } from '@/models/Ticket';
import type { Employee } from '@/models/Employee';
import type { Project } from '@/models/Project';
import { useHttp } from '@/composables/http';
import { reactive, ref } from 'vue';
import type { CustomerDetails } from '@/models/CustomerDetails';
import type { AxiosError } from 'axios';

const http = useHttp();

const customers = reactive(new Array<Customer>());
const tickets = reactive(new Array<Ticket>());
const employee = ref<Employee>();
const projects = ref<Project>();
const error = ref("");
const isBusy = ref(false);

function startRequest() {
  isBusy.value = true;
  error.value = "";
}

function endRequest() {
  isBusy.value = false;
}

async function loadCustomers() {
  try {
    startRequest()
    const result = await http.get<Customer[]>("/api/customers");
    if (result.data) {
      customers.splice(0, customers.length, ...result.data);
    }
  } catch (e) {
    const httpError = e as AxiosError;
    error.value = httpError.message;
  } finally {
    endRequest();
  }
}

async function getCustomer(id: number) {
  // TODO, get from cached customers
  try {
    startRequest()
    const result = await http.get<Customer>(`/api/customers/${id}`);
    if (result.data) {
      return result.data;
    } else {
      error.value = "Could not find customer";
      return undefined;
    }
  } finally {
    endRequest();
  }
}

async function getCustomerDetails(id: number) {
  try {
    startRequest()
    const result = await http.get<CustomerDetails>(`/api/customers/${id}/details`);
    if (result.data) {
      return result.data;
    } else {
      error.value = "Could not find projects";
      return undefined;
    }
  } catch (e) {
    const httpError = e as AxiosError;
    error.value = httpError.message;
  } finally {
    endRequest();
  }
}

async function saveCustomer(customer: Customer) {
  try {
    error.value = "";
    const result = await http.post("/api/customers/", customer);
    if (result.status === 201) {
      const found = customers.findIndex(c => c.id === result.data.id);
      if (found > -1) {
        customers.splice(found, 1, result.data);
      }
      return true;
    }
    error.value = result.statusText;
  } catch (e) {
    error.value = `Failed to save customer: ${e}`;
  }
  return false;
}

async function updateCustomer(customer: Customer) {
  try {
    error.value = "";
    const result = await http.put(`/api/customers/${customer.id}`, customer);
    if (result.status === 200) {
      const found = customers.findIndex(c => c.id === result.data.id);
      if (found > -1) {
        customers.splice(found, 1, result.data);
      }
      return true;
    }
    error.value = result.statusText;
  } catch (e) {
    error.value = `Failed to update customer: ${e}`;
  }
  return false;
}

async function deleteCustomer(id: Number) {
  try {
    error.value = "";
    const result = await http.del(`/api/customers/${id}`);
    if (result.status === 200) {
      const found = customers.findIndex(c => c.id === id);
      if (found > -1) {
        customers.splice(found, 1);
      }
      return true;
    }
    error.value = result.statusText;
  } catch (e) {
    error.value = `Failed to delete customer: ${e}`;
  }
  return false;
}

export const useState = defineStore('state', () => {

  return {
    customers,
    error,
    isBusy,
    loadCustomers,
    getCustomer,
    getCustomerDetails,
    saveCustomer,
    updateCustomer,
    deleteCustomer
  };
})
