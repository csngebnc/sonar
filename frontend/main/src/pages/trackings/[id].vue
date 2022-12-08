<i18n lang="yaml">
en:
  0: A csomag adatai megérkeztek hozzánk, futárunk hamarosan felveszi a csomagot.
  1: A csomag futárunknál van.
  2: Beérkezett egy logisztikai központba.
  3: Megérkezett a csomagautomatába, most már átvehető.
  4: Kiszállítva.
  5: A kiszállítás nem sikerült.
  6: Visszaküldés megkezdődött a felvételi címre.
  7: A visszaküldés megtörtént.
</i18n>

<template>
  <div class="mx-72">
    <div class="flex justify-center text-4xl font-sausage" v-if="!notFound">
      Csomaghoz tartozó események
    </div>
    <div class="mx-72 mt-4" v-if="!notFound">
      <n-data-table
        :columns="columns"
        :data="history.events"
        :bordered="false"
      />
    </div>
    <div class="flex justify-center mt-10" v-else>
      <div class="text-3xl font-sausage">
        Nincs találat a megadott csomagszámra.
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import type {
  TrackingHistory,
  TrackingHistoryItem,
} from "@/generated/statusApiClient";
import { statusService } from "@/services/status";
import { useRoute } from "vue-router";
import Tracking from "@/components/index/Tracking.vue";
import type { DataTableColumns } from "naive-ui";

const history = ref<TrackingHistory>({});
const { t } = useI18n();
const notFound = ref(true);

function padTo2Digits(num: number) {
  return num.toString().padStart(2, "0");
}

const route = useRoute();
statusService
  .apiStatusHistory(route.params.id as string)
  .then((res) => {
    notFound.value = false;
    history.value = res;
  })
  .catch(() => {
    notFound.value = true;
  });

const columns: DataTableColumns<TrackingHistoryItem> = [
  {
    title: "Esemény",
    key: "state",
    render(item: TrackingHistoryItem) {
      return h("div", {}, { default: () => t(item.state) });
    },
  },
  {
    title: "Időpont",
    key: "time",
    render(item: TrackingHistoryItem) {
      return h(
        "div",
        {},
        {
          default: () => {
            const date = new Date(item.time);
            return `${date.getFullYear()}. ${padTo2Digits(
              date.getMonth()
            )}. ${padTo2Digits(date.getDate())}. - ${padTo2Digits(
              date.getHours()
            )}:${padTo2Digits(date.getMinutes())}`;
          },
        }
      );
    },
  },
];
</script>
