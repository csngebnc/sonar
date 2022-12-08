<template>
  <div>
    <div class="flex w-full justify-center mb-16">
      <div
        class="w-1/2 text-primary-dark-100 font-rainbow flex flex-col -space-y-[3.5rem] mr-24 justify-center -ml-28 items-end"
      >
        <div class="text-[15rem]">Szállítás</div>
        <div class="text-[6rem] mr-8 text-accent-dark-100">legyen gyors</div>
      </div>
      <div class="w-2/6 mt-24">
        <div class="relative car">
          <IconCar />
        </div>
      </div>
    </div>

    <div><Tracking class="mx-72" /></div>

    <div class="mx-72 mb-10">{{ token }}</div>
    <div class="mx-72">{{ decodedToken }}</div>

    <div class="mx-72 h-80 mb-20"><IndexCards /></div>
  </div>
</template>

<script setup lang="ts">
import IconCar from "../components/icons/IconCar.vue";
import Tracking from "../components/index/Tracking.vue";
import IndexCards from "../components/index/IndexCards.vue";
import { useKeycloak } from "@baloise/vue-keycloak";

const { token, decodedToken } = useKeycloak();
</script>

<style scoped>
@keyframes car-animation {
  0% {
    right: 1.25rem;
  }
  20% {
    right: 1.25rem;
  }
  60% {
    right: -55rem;
    transform: scale(1, 1);
  }
  70% {
    transform: scale(-1, 1);
  }
  100% {
    right: 1.25rem;
    transform: scale(-1, 1);
  }
}

.car {
  transition: all;
  animation-fill-mode: forwards;
  animation-name: car-animation;
  animation-duration: 10s;
}
</style>
