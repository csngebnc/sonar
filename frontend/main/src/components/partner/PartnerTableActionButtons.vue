<template>
  <div class="space-x-2">
    <n-button tertiary circle type="info" @click="goToContact()"> C </n-button>
    <n-button tertiary circle type="info"> E </n-button>
  </div>
</template>

<script setup lang="ts">
import { router } from "@/plugins/router";
const props = defineProps<{
  id: string;
}>();

function goToContact() {
  router.push(`/contact/${props.id}`);
}
</script>
