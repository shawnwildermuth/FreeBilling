<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { type Project, generateEmptyProject } from "@/models/Project";
import { useProjectStore } from "@/stores/projectStore";
import { useCustomerStore } from "@/stores/customerStore";

const route = useRoute();
const store = useProjectStore();
const customerStore = useCustomerStore();

let id = 0;
const project = ref<Project | null>();

onMounted(async () => {
  await customerStore.loadCustomers();
  
  if (route.params.id === "new") {
    // reset the object
    id = -1;
    project.value = generateEmptyProject();
    if (route.query.customerId) {
      project.value.customerId = Number(route.query.customerId);
    }
  } else {
    id = Number(route.params.id);
    const result = await store.getProject(id);
    if (result) {
      project.value = result;
    }
  }
});

</script>

<template>

  
</template>