import { useHttp } from '@/composables/http';
import type { AxiosError } from 'axios';
import { useStore } from "@/stores";

const http = useHttp();
const store = useStore();

export default class StoreImpl<T extends { id: Number }> {

  collection: Array<T>;
  name: string;

  constructor(name: string, collection: Array<T>) {
    this.name = name;
    this.collection = collection;
  }

  async loadItems() {
    try {
      store.startRequest()
      const result = await http.get<T[]>(`/api/${name}`);
      if (result.data) {
        this.collection.splice(0, this.collection.length, ...result.data);
      }
    } catch (e) {
      const httpError = e as AxiosError;
      store.error = `Could not load ${name}(s). Error: ${httpError.message}`;
    } finally {
      store.endRequest();
    }
  }

  async getItem(id: number) {
    try {
      store.startRequest()
      const result = await http.get<T>(`/api/${this.name}/${id}`);
      if (result.data) {
        return result.data;
      } else {
        store.error = `Could not find ${this.name}`;
        return undefined;
      }
    } finally {
      store.endRequest();
    }
  }

  async saveItem(item: T) {
    try {
      store.startRequest()
      const result = await http.post(`/api/${this.name}/`, item);
      if (result.status === 201) {
        const found = this.collection.findIndex(c => c.id === result.data.id);
        if (found > -1) {
          this.collection.splice(found, 1, result.data);
        }
        return true;
      }
      store.error = result.statusText;
    } catch (e) {
      store.error = `Failed to save ${this.name}: ${e}`;
    } finally {
      store.endRequest();
    }

    return false;
  }

  async updateItem(item: T) {
    try {
      store.startRequest()
      const result = await http.put(`/api/${this.name}/${item.id}`, item);
      if (result.status === 200) {
        const found = this.collection.findIndex(c => c.id === result.data.id);
        if (found > -1) {
          this.collection.splice(found, 1, result.data);
        }
        return true;
      }
      store.error = result.statusText;
    } catch (e) {
      store.error = `Failed to update ${this.name}: ${e}`;
    } finally {
      store.endRequest();
    }
    return false;
  }

  async deleteItem(id: Number) {
    try {
      store.startRequest()
      const result = await http.del(`/api/${this.name}/${id}`);
      if (result.status === 200) {
        const found = this.collection.findIndex(c => c.id === id);
        if (found > -1) {
          this.collection.splice(found, 1);
        }
        return true;
      }
      store.error = result.statusText;
    } catch (e) {
      store.error = `Failed to delete ${this.name}: ${e}`;
    } finally {
      store.endRequest();
    }
    return false;
  }
} 
