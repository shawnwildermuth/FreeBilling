import { defineStore } from 'pinia'
import { type Customer } from "@/models/Customer";
import { useHttp } from '@/composables/http';
import { reactive, ref } from 'vue';
import type { CustomerDetails } from '@/models/CustomerDetails';
import type { AxiosError } from 'axios';
import { useStore } from "@/stores";
import StoreImpl from './storeImpl';

const http = useHttp();
const store = useStore();

const customers = reactive(new Array<Customer>());
const implementation = new StoreImpl<Customer>("customer", customers);

const loadCustomers = async () => await implementation.loadItems();
const getCustomer = async (id: number) => await implementation.getItem(id);

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

const saveCustomer = async (customer: Customer) => await implementation.saveItem(customer);
const updateCustomer = async (customer: Customer) => await implementation.updateItem(customer);
const deleteCustomer  = async (id: Number) => await implementation.deleteItem(id);

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
