<template>
  <div class="mx-[24rem]">
    <div class="font-sausage text-6xl mt-4 mb-10 text-center">
      Díjkalkulátor
    </div>

    <div class="text-center font-sausage text-xl mb-20">
      <div class="mb-4">Végösszeg: {{ price }} Forint</div>
      <NButton type="primary" @click="addParcel()">Új csomag</NButton>
    </div>

    <div>
      <n-form :model="sizeForm">
        <div v-for="(parcel, index) in sizeForm.parcels" :key="index">
          <div class="font-sausage text-2xl flex justify-between mt-10">
            <div>{{ index + 1 }}. csomag</div>
            <div
              v-if="sizeForm.parcels.length > 1"
              :class="index > 1 ? 'mt-10' : ''"
            >
              <IconTrash class="cursor-pointer" @click="deleteItem(index)" />
            </div>
          </div>
          <div
            class="flex text-right text-primary-dark-100 space-x-5 mt-0 mb-5"
          >
            <div class="w-1/4">
              <n-form-item :path="`parcels[${index}].size.width`">
                <div class="flex flex-col">
                  <div>Szélesség</div>
                  <n-input-number
                    placeholder="Szélesség"
                    v-model:value="parcel.width"
                    :show-button="false"
                    :min="10"
                  >
                    <template #suffix> cm </template>
                  </n-input-number>
                </div>
              </n-form-item>
            </div>

            <div class="w-1/4">
              <n-form-item :path="`parcels[${index}].size.height`">
                <div class="flex flex-col">
                  <div>Magasság</div>
                  <n-input-number
                    placeholder="Magasság"
                    v-model:value="parcel.height"
                    :show-button="false"
                    :min="10"
                  >
                    <template #suffix> cm </template>
                  </n-input-number>
                </div>
              </n-form-item>
            </div>

            <div class="w-1/4">
              <n-form-item :path="`parcels[${index}].size.depth`">
                <div class="flex flex-col">
                  <div>Mélység</div>
                  <n-input-number
                    placeholder="Mélység"
                    v-model:value="parcel.depth"
                    :show-button="false"
                    :min="5"
                  >
                    <template #suffix> cm </template>
                  </n-input-number>
                </div>
              </n-form-item>
            </div>

            <div class="w-1/4">
              <n-form-item :path="`parcels[${index}].size.weight`">
                <div class="flex flex-col">
                  <div>Súly</div>
                  <n-input-number
                    placeholder="Súly"
                    v-model:value="parcel.weight"
                    :show-button="false"
                    :min="0.1"
                  >
                    <template #suffix> kg </template>
                  </n-input-number>
                </div>
              </n-form-item>
            </div>
          </div>
          <div class="w-full">
            <n-checkbox-group
              v-model:value="parcel.extraOptions"
              class="flex justify-between mt-3"
            >
              <n-checkbox
                v-for="option in extras"
                :value="option.id"
                :key="option.id"
                :label="`${option.name} (${option.price} Ft)`"
              />
            </n-checkbox-group>
          </div>
        </div>
      </n-form>
    </div>
  </div>
</template>

<script setup lang="ts">
import IconTrash from "@/components/icons/IconTrash.vue";
import type { ExtraOptionDto } from "@/generated/orderApiClient";
import { orderService } from "@/services/order";

const extras = ref<ExtraOptionDto[]>([]);
orderService
  .apiOrderOptions()
  .then((response) => (extras.value = response.options));

interface PriceOption {
  depth: number;
  height: number;
  weight: number;
  width: number;
  extraOptions: string[];
}

const sizeForm = reactive<{
  parcels: PriceOption[];
}>({
  parcels: [
    {
      depth: 5,
      height: 10,
      weight: 0.1,
      width: 10,
      extraOptions: [],
    },
  ],
});

function addParcel() {
  sizeForm.parcels.push({
    depth: 5,
    height: 10,
    weight: 0.1,
    width: 10,
    extraOptions: [],
  });
}

function deleteItem(index: number) {
  sizeForm.parcels.splice(index, 1);
}

function pricePerParcel(parcel: PriceOption) {
  let width = 60 * parcel.width;
  let height = 40 * parcel.height;
  let depth = 15 * parcel.depth;
  let weight = 900 * parcel.weight;

  let extraPrice = 0;
  parcel.extraOptions.forEach((extra) => {
    extraPrice += extras.value.find((i) => i.id === extra).price;
  });

  return width + height + depth + weight + extraPrice;
}

const price = computed(() => {
  let p = 0;
  sizeForm.parcels.forEach((element) => {
    p += pricePerParcel(element);
  });

  return p;
});
</script>
