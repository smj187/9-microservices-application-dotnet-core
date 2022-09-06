<script setup lang="ts">
  import Header from "@/components/Header.vue"
  import { RouterView } from "vue-router"
</script>

<template>
  <div class="relative">
    <Header />
    <main class="mt-16 p-9">
      <RouterView />
    </main>
  </div>
</template>
