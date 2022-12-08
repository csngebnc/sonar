<template>
  <div class="flex flex-col w-1/4">
    <div
      class="text-3xl h-32 font-sausage flex justify-center items-center text-center mt-6 tracking-[12px]"
    >
      {{ title }}
    </div>
    <div class="mb-10 mt-5 text-center">
      Lorem ipsum dolor, sit amet consectetur adipisicing elit. Porro, eum
      itaque dignissimos, laborum est veniam ducimus sunt ab in nobis omnis
      doloribus dolor, architecto minus vel doloremque deleniti natus autem.
    </div>
    <div class="flex justify-center">
      <RouterLink :to="to">
        <NButton type="primary">Tovább</NButton>
      </RouterLink>
    </div>
  </div>
</template>
<script setup lang="ts">
defineProps<{
  title: string;
  description: string;
  to: string;
}>();
</script>
