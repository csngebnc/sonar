<template>
  <div>
    <Navbar class="" />
    <button
      v-if="showScrollToTop"
      @click="scrollToTop"
      class="fixed w-10 h-10 rounded-full bg-primary-light-100 right-10 bottom-7 animate-bounce flex justify-center items-center focus:bg-accent-dark-100 scrollToTopBoxShadow"
    >
      <IconArrowUp />
    </button>
    <n-config-provider :theme-overrides="themeOverrides">
      <RouterView class="text-primary-dark-100 overflow-hidden" />
    </n-config-provider>
  </div>
</template>

<script setup lang="ts">
import IconArrowUp from "@/components/icons/IconArrowUp.vue";
import Navbar from "@/components/shared/Navbar.vue";
import { ref } from "vue";
import { NConfigProvider } from "naive-ui";

const themeOverrides = {
  common: {
    primaryColor: "#4842b8",
    primaryColorHover: "#4842b8",
    primaryColorPressed: "#4842b8",
    primaryColorSuppl: "#4842b8",
  },
  Input: {
    border: "1px solid #4842b850",
    borderFocus: "#4842b8",
    borderHover: "#4842b8",
  },
  Button: {
    colorFocusPrimary: "#4842b8",
    colorPressedPrimary: "#4842b8",
    colorHoverPrimary: "#4842b8",
  },
};

window.onscroll = function () {
  scrollFunction();
};

const showScrollToTop = ref(false);
const scrollToTop = () => {
  window.scroll({
    top: 0,
    left: 0,
    behavior: "smooth",
  });
};

const scrollFunction = function () {
  if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
    showScrollToTop.value = true;
  } else {
    showScrollToTop.value = false;
  }
};
</script>

<style scoped>
.scrollToTopBoxShadow:focus {
  box-shadow: 0px 0px 20px 20px #f1b739;
}

.scrollToTopBoxShadow {
  box-shadow: 0px 0px 20px 5px #4842b8;
}
</style>
