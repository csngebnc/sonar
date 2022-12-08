import { fileURLToPath, URL } from "node:url";

import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import AutoImport from "unplugin-auto-import/vite";
import { NaiveUiResolver } from "unplugin-vue-components/resolvers";
import Components from "unplugin-vue-components/vite";
import Pages from "vite-plugin-pages";
import Layouts from "vite-plugin-vue-layouts";
import VueI18n from "@intlify/unplugin-vue-i18n/vite";
import svgLoader from "vite-svg-loader";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    svgLoader({
      defaultImport: "component", // or 'raw'
    }),

    // https://github.com/hannoeru/vite-plugin-pages
    Pages({
      dirs: [{ dir: "src/pages", baseRoute: "" }],
      exclude: ["**/*.html"],
    }),

    // https://github.com/JohnCampionJr/vite-plugin-vue-layouts
    Layouts({
      layoutsDirs: ["src/layouts"],
    }),

    // https://github.com/antfu/unplugin-vue-components
    Components({
      dts: "src/components.d.ts",
      directoryAsNamespace: true,
      resolvers: [NaiveUiResolver()],
    }),

    // https://github.com/antfu/unplugin-auto-import
    AutoImport({
      imports: ["vue", "vue-router", "pinia", "vue-i18n"],
      dts: "src/autoImports.d.ts",
    }),

    // https://github.com/intlify/bundle-tools/tree/main/packages/vite-plugin-vue-i18n
    VueI18n({
      compositionOnly: true,
    }),

    // https://github.com/jpkleemans/vite-svg-loader
    svgLoader({
      svgo: false,
    }),
  ],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
});
