import { createRouter, createWebHistory } from "vue-router"

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "HomeView",
      alias: "/home",
      component: () => import("@/views/HomeView.vue")
    },
    {
      path: "/store",
      name: "StoreView",
      component: () => import("@/views/StoreView.vue")
    }
  ]
})

export default router
