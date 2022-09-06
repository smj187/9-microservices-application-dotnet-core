<script lang="ts" setup>
  import { RouterLink } from "vue-router"
</script>

<template>
  <header
    class="fixed top-0 left-0 border border-b w-full h-16 flex items-center justify-center space-x-3 bg-white shadow-sm z-50"
  >
    <RouterLink :to="{ name: 'HomeView' }">Home</RouterLink>
    <RouterLink :to="{ name: 'StoreView' }">Store</RouterLink>
  </header>
</template>
