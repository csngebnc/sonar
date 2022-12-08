<template>
  <div class="grid grid-cols-2">
    <div>
      <div class="text-4xl font-extrabold font-sausage mb-0 tracking-[12px]">
        Csomagkövetés
      </div>
      <div class="mt-4 font-sausage">
        <div>
          Az online csomagkeresés funkció a nap bármely szakában elérhető: csak
          adjon meg egy csomagszámot.
        </div>

        <div class="mt-2">A csomagtörténet azonnal megjelenik.</div>
      </div>
      <div class="mt-8 font-sausage text-2xl">Csomagszám:</div>
      <div class="flex mt-4">
        <div class="w-3/4 mr-5">
          <n-input
            type="text"
            placeholder="Pl. CBLS000000123122"
            v-model:value="trackingNumber"
          />
        </div>
        <n-button
          type="primary"
          class="bg-primary-dark-100"
          :loading="showLoading"
          @click="search()"
        >
          Keresés
        </n-button>
      </div>
      <div class="mt-1 grid grid-cols-[80%_10%]">
        <div class="space-y-2 space-x-2 mt-1.5">
          <n-tag
            v-for="trackingNum in previousSearch"
            closable
            size="small"
            round
            @close="handleClose(trackingNum)"
            @click="selectNumber(trackingNum)"
            :key="trackingNum"
          >
            <div class="cursor-pointer">
              {{ trackingNum }}
            </div>
          </n-tag>
        </div>
      </div>
    </div>
    <div class="flex justify-end">
      <IconBox class="w-[65%] h-[65%]" />
    </div>
  </div>
</template>

<script setup lang="ts">
import IconBox from "../icons/IconBox.vue";
import { ref } from "vue";
import { router } from "@/plugins/router";

const showLoading = ref(false);

const previousSearch = ref<string[]>(
  JSON.parse(localStorage.getItem("previous")) ?? []
);

const trackingNumber = ref<string>("");

function selectNumber(num: string) {
  trackingNumber.value = num;
}

function handleClose(identifier: any) {
  const index = previousSearch.value.indexOf(identifier);
  if (index > -1) {
    previousSearch.value.splice(index, 1);
    localStorage.setItem("previous", JSON.stringify(previousSearch.value));
  }
}

function search() {
  showLoading.value = true;
  if (trackingNumber.value) {
    console.log("here");

    if (previousSearch.value.indexOf(trackingNumber.value) < 0) {
      previousSearch.value.push(trackingNumber.value);
      localStorage.setItem("previous", JSON.stringify(previousSearch.value));
    }

    router.push(`/trackings/${trackingNumber.value}`);
  }
  setTimeout(() => (showLoading.value = false), 300);
}
</script>
