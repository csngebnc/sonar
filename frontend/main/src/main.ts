import { createApp } from "vue";

import App from "./App.vue";
import { router } from "./plugins/router";
import { pinia } from "./plugins/pinia";
import { i18n } from "./plugins/i18n";
import { getToken, useKeycloak, vueKeycloak } from "@baloise/vue-keycloak";

import "./assets/main.css";
import axios from "axios";
import VueAxios from "vue-axios";

const meta = document.createElement("meta");
meta.name = "naive-ui-style";
document.head.appendChild(meta);

const app = createApp(App)
  .use(router)
  .use(pinia)
  .use(i18n)
  .use(VueAxios, axios);

console.log(window.location.origin + "/silent-check-sso.html");

app.use(vueKeycloak, {
  initOptions: {
    flow: "standard", // default
    checkLoginIframe: false, // default
    onLoad: "check-sso", // default
    silentCheckSsoRedirectUri:
      window.location.origin + "/silent-check-sso.html",
  },
  config: {
    url: "https://localhost:8443",
    realm: "delivery",
    clientId: "delivery",
  },
});

app.mount("#app");
