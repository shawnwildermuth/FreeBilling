import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  base: "/user",
  server: {
    port: 5000,
  },
  build: {
    outDir: "../wwwroot/client/",
    emptyOutDir: true
  },
  css: {
    postcss: {
      "postcss-plugin": false,
    },
  },
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  }
})
