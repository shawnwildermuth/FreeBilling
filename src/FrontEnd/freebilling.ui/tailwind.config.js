/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "src/**/*.{ts,vue,html}"
  ],
  theme: {
    extend: {},
  },
  plugins: [require("@tailwindcss/typography"), require("daisyui")],
  daisyui: {
    themes: ["dim"]
  } 
}

