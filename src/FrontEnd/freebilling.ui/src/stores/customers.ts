import { defineStore } from 'pinia'
import { type Customer } from "@/models/customer";

const http = useHttp();

export const useCustomersStore = defineStore('customers', () => {
  const customers = reactive(new Array<Customer>());
  async function load() {
    const result = await http.get<Customer[]>("/api/customers");
    if (result.data) {
      customers.splice(0, customers.length, ...result.data);
    }
  }

  return { customers, load }
})
