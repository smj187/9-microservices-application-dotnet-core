import { defineConfig } from "vite"
import vue from "@vitejs/plugin-vue"
import fs from "fs"

export default defineConfig(({ mode }) => {
  return {
    plugins: [vue()],
    server: {
      host: "0.0.0.0",
      port: 4001,
      proxy: {
        "/proxy": {
          target: "https://localhost:10000",
          changeOrigin: true,
          secure: false,
          rewrite: path => path.replace(/^\/proxy/, "")
        }
      },
      https: {
        key: fs.readFileSync("./https/server.key"),
        cert: fs.readFileSync("./https/server.crt")
      }
    }
  }
})

