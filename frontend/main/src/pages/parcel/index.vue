<template>
  <div class="mx-72 font-sausage">
    <div class="flex flex-col">
      <div class="text-center text-5xl mb-10">Csomagfeladás</div>

      <div class="flex justify-center w-full my-3" v-if="!complete">
        <div>
          <n-steps :current="(current as number)">
            <n-step title="Csomagok összeállítása" />
            <n-step title="Összegzés" />
          </n-steps>
        </div>
      </div>
    </div>

    <div v-if="!complete">
      <n-form
        ref="parcelsFormInstance"
        :model="dynamicForm"
        v-if="current === 1"
      >
        <div class="mt-20 mx-20">
          <NCard class="mb-5 p-10">
            <div class="text-primary-dark-100 text-3xl">Feladó adatai:</div>
            <div class="w-full">
              <div class="flex w-full space-x-5">
                <div>
                  <n-form-item
                    :path="`sender.name`"
                    :show-feedback="false"
                    :rule="{
                      required: true,
                      trigger: ['input', 'blur'],
                    }"
                  >
                    <n-input
                      placeholder="Feladó neve"
                      type="text"
                      v-model:value="dynamicForm.sender.name"
                    />
                  </n-form-item>
                </div>
                <div>
                  <n-form-item
                    :path="`sender.email`"
                    :show-feedback="false"
                    :rule="{
                      required: true,
                      trigger: ['input', 'blur'],
                    }"
                  >
                    <n-input
                      placeholder="Feladó e-mail címe"
                      type="text"
                      v-model:value="dynamicForm.sender.email"
                    />
                  </n-form-item>
                </div>

                <div>
                  <n-form-item
                    :path="`sender.phoneNumber`"
                    :show-feedback="false"
                    :rule="{
                      required: true,
                      trigger: ['input', 'blur'],
                    }"
                  >
                    <n-input
                      placeholder="Feladó telefonszáma"
                      type="text"
                      v-model:value="dynamicForm.sender.phoneNumber"
                    />
                  </n-form-item>
                </div>
              </div>

              <div class="flex space-x-5">
                <n-form-item
                  class="w-3/5"
                  :path="`pickupAddress.country`"
                  :show-feedback="false"
                  :rule="{
                    required: true,
                    trigger: ['input', 'blur'],
                  }"
                >
                  <n-input
                    placeholder="Ország"
                    type="text"
                    v-model:value="dynamicForm.pickupAddress.country"
                  />
                </n-form-item>

                <n-form-item
                  class="w-1/3"
                  :path="`pickupAddress.county`"
                  :rule="{
                    required: true,
                    trigger: ['input', 'blur'],
                  }"
                  :show-feedback="false"
                >
                  <n-input
                    placeholder="Megye"
                    type="text"
                    v-model:value="dynamicForm.pickupAddress.county"
                  />
                </n-form-item>
              </div>
              <div class="flex space-x-5">
                <n-form-item
                  class="w-2/5"
                  :path="`pickupAddress.city`"
                  :rule="{
                    required: true,
                    trigger: ['input', 'blur'],
                  }"
                  :show-feedback="false"
                >
                  <n-input
                    placeholder="Település"
                    type="text"
                    v-model:value="dynamicForm.pickupAddress.city"
                  />
                </n-form-item>

                <n-form-item
                  class="w-1/3"
                  :path="`pickupAddress.postalCode`"
                  :rule="{
                    required: true,
                    trigger: ['input', 'blur'],
                  }"
                  :show-feedback="false"
                >
                  <n-input
                    placeholder="Irányítószám"
                    type="text"
                    v-model:value="dynamicForm.pickupAddress.postalCode"
                  />
                </n-form-item>
              </div>
              <div class="flex">
                <n-form-item
                  class="w-3/5"
                  :path="`pickupAddress.streetAndNumber`"
                  :rule="{
                    required: true,
                    trigger: ['input', 'blur'],
                  }"
                  :show-feedback="false"
                >
                  <n-input
                    placeholder="Utca, házszám"
                    type="text"
                    v-model:value="dynamicForm.pickupAddress.streetAndNumber"
                  />
                </n-form-item>
              </div>

              <n-form-item class="" :path="`pickupAddress.other`">
                <n-input
                  placeholder="Egyéb"
                  type="textarea"
                  v-model:value="dynamicForm.pickupAddress.other"
                />
              </n-form-item>
            </div>
          </NCard>
          <NCard>
            <div class="flex flex-col p-10">
              <div class="flex justify-between">
                <div class="text-primary-dark-100 text-3xl">
                  Csomagok adatai:
                </div>
                <div>
                  <n-button type="primary" @click="newParcel"
                    >+ Új csomag</n-button
                  >
                </div>
              </div>
              <div
                class="grid grid-cols-[50%_45%_5%] mt-5"
                v-for="(parcel, index) in dynamicForm.parcels"
                :key="index"
              >
                <div>
                  <div class="text-primary-dark-100 text-lg">
                    {{ index + 1 }}. csomag adatai
                  </div>

                  <div
                    class="flex text-right mt-3 text-primary-dark-100 space-x-5 mb-5"
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
                        <template #suffix> kg </template>
                      </n-input-number>
                    </n-form-item>
                  </div>

                  <div class="text-primary-dark-100">
                    <div class="text-lg">Címzett adatai:</div>
                    <div class="">
                      <n-form-item
                        class="w-2/5"
                        :path="`parcels[${index}].receiver.name`"
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
                            v-model:value="
                              parcel.deliveryAddress.streetAndNumber
                            "
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

                      <div class="flex space-x-5">
                        <div class="w-2/5">
                          <n-form-item
                            :path="`parcels[${index}].receiver.email`"
                            :show-feedback="false"
                            :rule="{
                              required: true,
                              trigger: ['input', 'blur'],
                            }"
                          >
                            <n-input
                              placeholder="Címzett e-mail címe"
                              type="text"
                              v-model:value="parcel.receiver.email"
                            />
                          </n-form-item>
                        </div>

                        <div class="w-2/5">
                          <n-form-item
                            :path="`parcels[${index}].receiver.phoneNumber`"
                            :show-feedback="false"
                            :rule="{
                              required: true,
                              trigger: ['input', 'blur'],
                            }"
                          >
                            <n-input
                              placeholder="Címzett telefonszáma"
                              type="text"
                              v-model:value="parcel.receiver.phoneNumber"
                            />
                          </n-form-item>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="text-primary-dark-100 text-2xl mx-5">
                  Extra szolgáltatások

                  <div class="flex">
                    <n-checkbox-group
                      v-model:value="parcel.extraOptions"
                      class="flex flex-col space-y-3 pl-3 mt-3"
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
                <div v-if="dynamicForm.parcels.length > 1">
                  <IconTrash
                    class="cursor-pointer"
                    @click="deleteItem(index)"
                  />
                </div>
              </div>
            </div>

            <div class="flex justify-end">
              <NButton type="primary" @click="handleValidateClick"
                >Következő (összegzés)</NButton
              >
            </div>
          </NCard>
        </div>
      </n-form>

      <div class="flex justify-center p-5 mt-3" v-else>
        <div class="border rounded-27 w-[40rem] py-5 px-10">
          <div class="grid grid-cols-[45%_55%]">
            <div class="space-y-1">
              <div class="text-2xl">Feladó adatai:</div>
              <div>Név: {{ dynamicForm.sender.name }}</div>
              <div>E-mail: {{ dynamicForm.sender.email }}</div>
              <div>Telefonszám: {{ dynamicForm.sender.phoneNumber }}</div>
            </div>

            <div class="space-y-1">
              <div class="text-2xl">Csomagfelvétel helyszíne:</div>
              <div>
                {{ dynamicForm.pickupAddress.country }}
                {{ dynamicForm.pickupAddress.county }}
              </div>
              <div>
                {{ dynamicForm.pickupAddress.postalCode }},
                {{ dynamicForm.pickupAddress.city }}
              </div>
              <div>
                {{ dynamicForm.pickupAddress.streetAndNumber }}
              </div>
              <div>
                {{ dynamicForm.pickupAddress.other }}
              </div>
            </div>
          </div>
          <div class="mt-5">
            <div class="text-xl">
              Csomagok száma: {{ dynamicForm.parcels.length }} db
            </div>
            <div class="text-lg">Felvételkor fizetendő: {{ price }} Forint</div>
          </div>

          <div
            v-for="(parcel, index) in dynamicForm.parcels"
            :key="index"
            class="mt-5"
          >
            <div class="text-xl">{{ index + 1 }}. csomag</div>
            <div class="mt-2">
              Szélesség: {{ parcel.size.width }} cm, Magasság:
              {{ parcel.size.height }} cm, Mélység: {{ parcel.size.depth }} cm,
              Súly: {{ parcel.size.weight }} kg
            </div>
            <div class="mb-2">
              Csomag kézbesítésének díja: {{ pricePerParcel(parcel) }} Forint
            </div>
            <div>
              Kézbesítés helyszíne:
              {{ parcel.deliveryAddress.postalCode }}
              {{ parcel.deliveryAddress.country }},
              {{ parcel.deliveryAddress.county }},
              {{ parcel.deliveryAddress.city }},
              {{ parcel.deliveryAddress.streetAndNumber }} <br />
              {{ parcel.deliveryAddress.other }}
            </div>
          </div>
          <div class="flex justify-end space-x-1 mt-10">
            <NButton type="primary" @click="currentRef--">
              Vissza a csomagokhoz
            </NButton>
            <NButton type="primary" @click="submit">
              Szállítás megrendelése
            </NButton>
          </div>
        </div>
      </div>
    </div>
    <div v-else class="mt-10 text-center">
      <div class="text-2xl text-center">
        Sikeres megrendelés! Futárunk hamarosan felveszi Önnel a kapcsolatot!
      </div>
      <div class="text-lg mt-3 mb-2">
        A feladott csomagok csomagszámai a rögzítés sorrendje szerint:
      </div>
      <div class="text-lg" v-if="trackingNumbers && trackingNumbers.length > 0">
        {{ trackingNumbers.join(", ") }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { FormInst } from "naive-ui";
import {
  ParcelType,
  type ParcelDto,
  type ExtraOptionDto,
  type SendParcelsCommand,
} from "@/generated/orderApiClient";
import { ref } from "vue";
import IconTrash from "@/components/icons/IconTrash.vue";
import { orderService } from "@/services/order";

const extras = ref<ExtraOptionDto[]>([]);
orderService
  .apiOrderOptions()
  .then((response) => (extras.value = response.options));

const currentRef = ref<number | null>(1);
const current = computed(() => currentRef.value);
const complete = ref(false);
const trackingNumbers = ref<string[]>([]);

function pricePerParcel(parcel: ParcelDto) {
  let width = 60 * parcel.size.width;
  let height = 40 * parcel.size.height;
  let depth = 15 * parcel.size.depth;
  let weight = 900 * parcel.size.weight;

  let extra = 0;
  parcel.extraOptions.forEach((extra) => {
    extra += extras.value.find((i) => i.id === extra).price;
  });
  return width + height + depth + weight + extra;
}

const price = computed(() => {
  let p = 0;
  dynamicForm.parcels.forEach((element) => {
    p += pricePerParcel(element);
  });

  return p;
});

const parcelsFormInstance = ref<FormInst | null>(null);
const dynamicForm = reactive<SendParcelsCommand>({
  sender: {
    email: "",
    name: "",
    phoneNumber: "",
  },
  pickupAddress: {
    city: "",
    country: "",
    county: "",
    other: "",
    postalCode: "",
    streetAndNumber: "",
  },
  parcels: [
    {
      deliveryAddress: {
        city: "",
        country: "",
        county: "",
        other: undefined,
        postalCode: "",
        streetAndNumber: "",
      },
      extraOptions: [],
      lockerId: undefined,
      receiver: {
        email: "",
        name: "",
        phoneNumber: "",
      },
      size: {
        depth: 5,
        height: 10,
        weight: 0.1,
        width: 10,
      },
      type: 1,
    },
  ],
});

const handleValidateClick = () => {
  parcelsFormInstance.value?.validate((errors) => {
    if (!errors) {
      currentRef.value++;
    }
  });
};

function submit() {
  orderService
    .apiOrderParcel(dynamicForm)
    .then((res) => {
      complete.value = true;
      trackingNumbers.value = res;
    })
    .catch(() => {
      alert(
        "A megrendelés rögzítése sikertelen volt! Kérjük próbálja meg később."
      );
    });
}

function newParcel() {
  dynamicForm.parcels.push({
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

function deleteItem(index: number) {
  dynamicForm.parcels.splice(index, 1);
}
</script>

<style scoped>
:deep(.n-checkbox__label) {
  @apply text-base;
  @apply text-primary-dark-100;
}

:deep(.n-input__suffix) {
  @apply text-primary-dark-100;
}

:deep(.n-collapse-item__header-main) {
  @apply text-2xl;
  @apply font-sausage;
  @apply mt-4;
  color: #4842b8 !important;
}

:deep(.n-form-item.n-form-item--top-labelled) {
  grid-template-rows: 0.5fr 1fr;
}
</style>
