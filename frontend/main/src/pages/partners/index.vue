<template>
  <div class="mx-72">
    <div class="flex justify-center text-4xl font-sausage">Partnerek</div>
    <div class="mx-72 mt-4">
      <n-data-table
        :columns="columns"
        :data="partnerList.partners"
        :bordered="false"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import type {
  ListPartnerQueryResponse,
  PartnerDto,
} from "@/generated/partnerApiClient";
import { partnerService } from "@/services/partner";
import type { DataTableColumns } from "naive-ui";
import PartnerTableActionButtons from "@/components/partner/PartnerTableActionButtons.vue";

const partnerList = ref<ListPartnerQueryResponse>({});
partnerService.apiPartnerList().then((res) => (partnerList.value = res));

const columns: DataTableColumns<PartnerDto> = [
  {
    title: "Név",
    key: "name",
    render(item: PartnerDto) {
      return h("div", {}, { default: () => item.name });
    },
  },
  {
    title: "Kapcsolattartó",
    key: "contactPerson",
    render(item: PartnerDto) {
      return h("div", {}, { default: () => item.contactPerson });
    },
  },
  {
    title: "Adószám",
    key: "taxNumber",
    render(item: PartnerDto) {
      return h("div", {}, { default: () => item.taxNumber });
    },
  },
  {
    title: "",
    key: "actions",
    render(item: PartnerDto) {
      return h(PartnerTableActionButtons, { id: item.id });
    },
  },
];
</script>
