import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

// https://vitejs.dev/config/
export default defineConfig(({ command }) => {
  if (command === "serve") { // development
    return {
      base: "/dist/",
      plugins: [vue()],
      server: {
        port: 5000,
      }
    }
  } else { // build
    return {
      base: "/dist/",
      plugins: [vue()],
      build: {
        outDir: "../wwwroot/client/",
        emptyOutDir: true,
        rollupOptions: {
          output: {
            entryFileNames: "client.js",
            chunkFileNames: "[name].js"
          }
        }
      },
      css: {
        postcss: {
          "postcss-plugin": false,
        },
      },
    }
  }
});