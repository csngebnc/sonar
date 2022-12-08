import type { LoadingBarInst } from "naive-ui/es/loading-bar/src/LoadingBarProvider";
import { setupLayouts } from "virtual:generated-layouts";
import generatedRoutes from "virtual:generated-pages";
import { createRouter, createWebHistory } from "vue-router";

export let loadingBarRef: LoadingBarInst;

const routes = setupLayouts(generatedRoutes);
export const router = createRouter({
  history: createWebHistory(),
  routes,
});

export function setupLoadingBarBetweenRoutes(loadingBar: LoadingBarInst) {
  loadingBarRef = loadingBar;

  router.beforeEach((to, from) => {
    if (to.path !== from.path) {
      loadingBar.start();
    }
  });

  router.afterEach((to, from) => {
    if (to.path !== from.path) {
      loadingBar.finish();
    }
  });
}
