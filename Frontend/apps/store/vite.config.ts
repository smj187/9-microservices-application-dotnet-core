import { defineConfig } from "vite"
import react from "@vitejs/plugin-react"
import fs from "fs"

export default defineConfig(({ mode }) => {
  return {
    plugins: [react()],
    server: {
      host: "0.0.0.0",
      port: 4000,
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

