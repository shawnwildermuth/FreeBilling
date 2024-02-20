import { defineStore } from 'pinia'
import { type Customer } from "@/models/customer";
import type { Ticket } from '@/models/Ticket';
import type { Employee } from '@/models/employee';
import type { Project } from '@/models/project';
import { useHttp } from '@/composables/http';
import { reactive, ref } from 'vue';

const http = useHttp();

export const useState = defineStore('state', () => {
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

async function getProjects(id: number) {
  try {
    startRequest()
    const result = await http.get<Project[]>(`/api/customers/${id}/projects`);
    if (result.data) {
      return result.data;
    } else {
      error.value = "Could not find projects";
      return undefined;
    }
  } finally {
    endRequest();
  }

}

  return {
    customers,
    error,
    isBusy,
    loadCustomers,
    getCustomer,
    getProjects
  };
})
