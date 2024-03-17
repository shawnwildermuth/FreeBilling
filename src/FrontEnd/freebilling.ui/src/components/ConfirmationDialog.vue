<script setup lang="ts">
import { ref } from 'vue';

const modal = ref<HTMLDialogElement>();

const emit = defineEmits(["confirm"]);

defineExpose({
  showModal,
  close
});

function close(success: boolean) {
  modal.value?.close();
  emit("confirm", success);
}

function showModal() {
  modal.value?.showModal();
}
</script>

<template>
  <dialog ref="modal"
    class="modal modal-backdrop backdrop:bg-black/75 backdrop:blur-sm text-white"
    @close="close(false)">
    <div class="modal-box">
      <h3>Are you sure?</h3>
      <div class="modal-action">
        <button class="btn btn-info" @click="close(false)">No</button>
        <button class="btn btn-success" focus @click="close(true)">Yes</button>
      </div>
    </div>

  </dialog>
</template>