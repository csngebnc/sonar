<template>
  <div class="flex justify-around h-full">
    <IndexCard to="/parcel" title="Csomagfeladás" description="Lorem" />
    <IndexCard to="/price" title="Díjkalkulátor" description="Lorem" />
    <IndexCard to="/" title="Legyünk partnerek" description="Lorem" />
  </div>
</template>
<script setup lang="ts">
import IndexCard from "./IndexCard.vue";
</script>
