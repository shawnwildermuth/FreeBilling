import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

import Pages from 'vite-plugin-pages';
import Layouts from 'vite-plugin-vue-layouts'
import Components from "unplugin-vue-components/vite";
import AutoImport from 'unplugin-auto-import/vite';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    AutoImport({
      imports: [
        'vue',
        'vue-router'
      ],
      dirs: [
        "./src/composables/**",
        "./src/stores/**"
      ],
    }),
    vue(),
    Pages(),
    Layouts(),
    Components(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    host: true,
    port: parseInt(process.env.PORT ?? "5173")
  }
})
