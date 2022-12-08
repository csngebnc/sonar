<template>
  <div
    class="text-primary-dark-100 w-full h-28 flex items-center justify-between"
  >
    <RouterLink to="/">
      <div class="ml-16 flex -mt-2">
        <IconBox class="w-[7%] h-[7%] -mt-3" />
        <div class="ml-5 font-rainbow text-xl -mt-1">Szállítás</div>
      </div>
    </RouterLink>
    <div class="space-x-16 flex mr-28 font-bold font-sausage">
      <RouterLink to="/" class="group h-10">
        <button
          class="group-hover:text-primary-light-600 group-hover:text-xl transition-all"
        >
          Főoldal
        </button>
      </RouterLink>
      <RouterLink to="/parcel" class="group">
        <button
          class="group-hover:text-primary-light-600 group-hover:text-xl transition-all"
        >
          Csomagfeladás
        </button>
      </RouterLink>
      <RouterLink to="/about" class="group">
        <button
          class="group-hover:text-primary-light-600 group-hover:text-xl transition-all"
        >
          Rólunk
        </button>
      </RouterLink>
      <RouterLink to="/tracking" class="group">
        <button
          class="group-hover:text-primary-light-600 group-hover:text-xl transition-all"
        >
          Csomagkövetés
        </button>
      </RouterLink>
      <RouterLink to="/price" class="group">
        <button
          class="group-hover:text-primary-light-600 group-hover:text-xl transition-all"
        >
          Díjkalkulátor
        </button>
      </RouterLink>
      <button @click="callAuth()">Callauth</button>
      <RouterLink to="" class="group" v-if="!isAuthenticated">
        <button
          class="group-hover:text-primary-light-600 group-hover:text-xl transition-all"
          @click="keycloak.login()"
        >
          Bejelentkezés
        </button>
      </RouterLink>
      <RouterLink to="" class="group" v-else>
        <button
          class="group-hover:text-primary-light-600 group-hover:text-xl transition-all"
          @click="keycloak.logout()"
        >
          Kijelentkezés
        </button>
      </RouterLink>
    </div>
  </div>
</template>

<script setup lang="ts">
import { orderService } from "@/services/order";
import { partnerService } from "@/services/partner";
import { useKeycloak } from "@baloise/vue-keycloak";
import IconBox from "../icons/IconBox.vue";

const { keycloak, isAuthenticated } = useKeycloak();

function callAuth() {
  orderService.apiOrderTest().then((res) => alert(res));
}
</script>

<style scoped>
button {
  letter-spacing: 0.15rem;
}
</style>
