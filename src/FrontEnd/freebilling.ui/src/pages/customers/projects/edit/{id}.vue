<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import { type Project, generateEmptyProject, type ProjectErrors, ProjectSchema } from "@/models/Project";
import { useProjectStore } from "@/stores/projectStore";
import { useCustomerStore } from "@/stores/customerStore";
import { VDatePicker } from "v-calendar";
const route = useRoute();
const router = useRouter();
const store = useProjectStore();
const customerStore = useCustomerStore();

let id = 0;
let lastUrl: string | symbol = "";
const project = ref<Project | null>();
const errors = ref<ProjectErrors | null>(null);
const dirty = ref(false);

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
  const valid = ProjectSchema.safeParse(project.value);
  if (!valid.success) errors.value = valid.error.format();
  return valid.success;
}

async function save() {
  if (validate()) {
    if (id === -1) {
      // Save it
      if (await store.saveProject(project.value!)) {
        router.back();
      }
    } else {
      if (await store.updateProject(project.value!)) {
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
    <div v-if="project" class="p-1 flex flex-col gap-2"
      @focusout="handleFocusOut">
      <label class="theLabel">
        <div class="label">Project Name</div>
        <input type="text" placeholder="e.g. Audit" class="theInput"
          :class="{ error: errors?.projectName }"
          v-model="project.projectName" />
        <form-error :errors="errors?.projectName" />
      </label>
      <label class="theLabel">
        <div class="label">Company</div>
        <select type="text" class="theInput"
          :class="{ error: errors?.customerId }" v-model="project.customerId">
          <option disabled selected>Pick One...</option>
          <option v-for="c in customerStore.customers" :value="c.id">
            {{ c.companyName }}
          </option>
        </select>
        <form-error :errors="errors?.customerId" />
      </label>

      <label class="theLabel">
        <div class="label">Starting Date</div>
        <div class="flex">
          <input type="text" placeholder="e.g 04-04-2024" class="grow theInput"
            :class="{ error: errors?.startDate }" v-model="project.startDate" />
          <vcalendar  v-model="project.startDate">
            <template #default="{ togglePopover }">
              <button @click="togglePopover">...</button>
            </template>
          </vcalendar>
        </div>
        <form-error :errors="errors?.startDate" />
      </label>

      <label class="theLabel">
        <div class="label">Ending Date</div>
        <div class="flex">
          <input type="text" placeholder="e.g 04-04-2024" class="grow theInput"
            :class="{ error: errors?.endDate }" v-model="project.endDate" />
          <vcalendar v-model="project.endDate">
            <template #default="{ togglePopover }">
              <button @click="togglePopover">...</button>
            </template>
          </vcalendar>
        </div>
        <form-error :errors="errors?.startDate" />
      </label>
    </div>

    <div class="flex justify-end gap-1">
      <button @click="cancel" class="btn btn-neutral">Cancel</button>
      <button @click="save" class="btn btn-success">Save</button>
    </div>
  </div>
  </div>
</template>
