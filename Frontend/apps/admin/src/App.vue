<script setup lang="ts">
  import axios from "axios"
  import { onMounted } from "vue"

  onMounted(async () => {
    const p = await axios.get(
      "https://localhost:10000/ca/api/v1/products/list",
      {
        headers: {
          "tenant-id": "tenant-a"
        }
        //
      }
    )

    console.log(p.data)
  })
</script>

<template>
  <div class="text-5xl">yes</div>
</template>

