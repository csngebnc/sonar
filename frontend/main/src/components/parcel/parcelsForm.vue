<template>
  <div class="flex flex-col p-10">
    <div class="flex justify-between">
      <div class="text-primary-dark-100 text-3xl font-sausage">
        Csomagok adatai:
      </div>
      <div>
        <n-button type="primary" @click="newParcel">+ Új csomag</n-button>
      </div>
    </div>
    <n-form ref="parcelsFormInstance" :model="props.data.parcels">
      <n-collapse>
        <n-collapse-item
          v-for="(parcel, index) in props.data.parcels"
          :key="index"
          class="mt-5"
          :title="`${index + 1}. csomag`"
        >
          <div class="grid grid-cols-[50%_45%_5%]">
            <div>
              <div class="font-sausage text-primary-dark-100 text-lg">
                {{ index + 1 }}. csomag adatai
              </div>

              <div
                class="flex text-right mt-3 font-sausage text-primary-dark-100 space-x-5 mb-5"
              >
                <n-form-item
                  class="w-1/5"
                  :path="`parcels[${index}].size.width`"
                >
                  <n-input-number
                    placeholder="Szélesség"
                    v-model:value="parcel.size.width"
                    :show-button="false"
                    :min="10"
                  >
                    <template #suffix> cm </template>
                  </n-input-number>
                </n-form-item>

                <n-form-item
                  class="w-1/5"
                  :path="`parcels[${index}].size.height`"
                >
                  <n-input-number
                    placeholder="Magasság"
                    v-model:value="parcel.size.height"
                    :show-button="false"
                    :min="10"
                  >
                    <template #suffix> cm </template>
                  </n-input-number>
                </n-form-item>

                <n-form-item
                  class="w-1/5"
                  :path="`parcels[${index}].size.depth`"
                >
                  <n-input-number
                    placeholder="Mélység"
                    v-model:value="parcel.size.depth"
                    :show-button="false"
                    :min="5"
                  >
                    <template #suffix> cm </template>
                  </n-input-number>
                </n-form-item>

                <n-form-item
                  class="w-1/5"
                  :path="`parcels[${index}].size.weight`"
                >
                  <n-input-number
                    placeholder="Súly"
                    v-model:value="parcel.size.weight"
                    :show-button="false"
                    :min="0.1"
                  >
                    <template #suffix> cm </template>
                  </n-input-number>
                </n-form-item>
              </div>

              <div class="font-sausage text-primary-dark-100">
                <div class="text-lg">Címzett adatai:</div>
                <div class="">
                  <n-form-item
                    class="w-2/5"
                    :path="`data.parcels[${index}].receiver.name`"
                    :show-feedback="false"
                    :rule="{
                      required: true,
                      trigger: ['input', 'blur'],
                    }"
                  >
                    <n-input
                      placeholder="Címzett neve"
                      type="text"
                      v-model:value="parcel.receiver.name"
                    />
                  </n-form-item>

                  <div class="flex space-x-5">
                    <n-form-item
                      class="w-3/5"
                      :path="`parcels[${index}].deliveryAddress.country`"
                      :show-feedback="false"
                      :rule="{
                        required: true,
                        trigger: ['input', 'blur'],
                      }"
                    >
                      <n-input
                        placeholder="Ország"
                        type="text"
                        v-model:value="parcel.deliveryAddress.country"
                      />
                    </n-form-item>

                    <n-form-item
                      class="w-1/3"
                      :path="`parcels[${index}].deliveryAddress.county`"
                      :rule="{
                        required: true,
                        trigger: ['input', 'blur'],
                      }"
                      :show-feedback="false"
                    >
                      <n-input
                        placeholder="Megye"
                        type="text"
                        v-model:value="parcel.deliveryAddress.county"
                      />
                    </n-form-item>
                  </div>
                  <div class="flex space-x-5">
                    <n-form-item
                      class="w-2/5"
                      :path="`parcels[${index}].deliveryAddress.city`"
                      :rule="{
                        required: true,
                        trigger: ['input', 'blur'],
                      }"
                      :show-feedback="false"
                    >
                      <n-input
                        placeholder="Település"
                        type="text"
                        v-model:value="parcel.deliveryAddress.city"
                      />
                    </n-form-item>

                    <n-form-item
                      class="w-1/3"
                      :path="`parcels[${index}].deliveryAddress.postalCode`"
                      :rule="{
                        required: true,
                        trigger: ['input', 'blur'],
                      }"
                      :show-feedback="false"
                    >
                      <n-input
                        placeholder="Irányítószám"
                        type="text"
                        v-model:value="parcel.deliveryAddress.postalCode"
                      />
                    </n-form-item>
                  </div>
                  <div class="flex">
                    <n-form-item
                      class="w-3/5"
                      :path="`parcels[${index}].deliveryAddress.streetAndNumber`"
                      :rule="{
                        required: true,
                        trigger: ['input', 'blur'],
                      }"
                      :show-feedback="false"
                    >
                      <n-input
                        placeholder="Utca, házszám"
                        type="text"
                        v-model:value="parcel.deliveryAddress.streetAndNumber"
                      />
                    </n-form-item>
                  </div>

                  <n-form-item
                    class=""
                    :path="`parcels[${index}].deliveryAddress.other`"
                  >
                    <n-input
                      placeholder="Egyéb"
                      type="textarea"
                      v-model:value="parcel.deliveryAddress.other"
                    />
                  </n-form-item>
                </div>
              </div>
            </div>
            <div class="font-sausage text-primary-dark-100 text-2xl mx-5">
              Extra szolgáltatások

              <div class="flex">
                <n-checkbox-group
                  v-model:value="parcel.extraOptions"
                  class="flex flex-col space-y-3 pl-3 mt-3"
                >
                  <n-checkbox value="Beijing" label="Utánvét (1500 ft)" />
                  <n-checkbox value="Shanghai" label="Háznál felvétel" />
                  <n-checkbox value="Guangzhou" label="SMS értesítés" />
                  <n-checkbox value="2" label="Rugalmas kézbesítés" />
                  <n-checkbox value="Shenzen" label="Törékeny" />
                </n-checkbox-group>
              </div>
            </div>
            <div v-if="props.data.parcels.length > 1">
              <IconTrash class="cursor-pointer" @click="deleteItem(index)" />
            </div>
          </div>
        </n-collapse-item>
      </n-collapse>
    </n-form>
  </div>

  <NButton type="primary" @click="handleValidateClick">Next</NButton>
</template>

<script setup lang="ts">
import type { FormInst } from "naive-ui";
import {
  ParcelType,
  type ParcelDto,
  type SendMultipleParcelCommand,
} from "@/generated/orderApiClient";
import IconTrash from "@/components/icons/IconTrash.vue";

const props = defineProps<{
  data: SendMultipleParcelCommand;
}>();

const parcelsFormInstance = ref<FormInst | null>(null);

function newParcel() {
  props.data.parcels.push({
    size: {
      depth: 5,
      height: 10,
      weight: 0.1,
      width: 10,
    },
    deliveryAddress: {
      city: "",
      country: "",
      county: "",
      other: undefined,
      postalCode: "",
      streetAndNumber: "",
    },
    receiver: {
      email: "",
      name: "",
      phoneNumber: "",
    },
    type: ParcelType.NORMAL,
    extraOptions: [],
  });
}

const handleValidateClick = () => {
  parcelsFormInstance.value?.validate((errors) => {
    if (!errors) {
      //currentRef.value++;
      console.log("1");
    } else {
      console.log(errors);
    }
  });
};

function deleteItem(index: number) {
  props.data.parcels.splice(index, 1);
}
</script>
