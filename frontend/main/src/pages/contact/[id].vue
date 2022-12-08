<template>
  <div class="mx-72">
    <div class="flex justify-center text-4xl font-sausage">
      Gyakori címzettek
    </div>
    <div class="mx-72 mt-4">
      <n-data-table :columns="columns" :data="contacts" :bordered="false" />
    </div>
  </div>
</template>

<script setup lang="ts">
import type { ContactDto } from "@/generated/partnerApiClient";
import { partnerService } from "@/services/partner";
import type { DataTableColumns } from "naive-ui";
import { useRoute } from "vue-router";
const route = useRoute();

const contacts = ref<ContactDto[]>([]);

partnerService.apiPartnerContacts(route.params.id as string).then((res) => {
  contacts.value = res.contacts;
});

const columns: DataTableColumns<ContactDto> = [
  {
    title: "Név",
    key: "name",
    render(item: ContactDto) {
      return h("div", {}, { default: () => item.name });
    },
  },
  {
    title: "Rögzítés dátuma",
    key: "creationDate",
    render(item: ContactDto) {
      return h("div", {}, { default: () => item.creationDate });
    },
  },
  {
    title: "Cím",
    key: "address",
    render(item: ContactDto) {
      return h("div", {}, { default: () => item.address.country });
    },
  },
  {
    title: "",
    key: "actions",
    render(item: ContactDto) {
      return h("div", { id: item.id });
    },
  },
];
</script>
