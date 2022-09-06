<script lang="ts" setup>
  import { useCounterStore } from "@/store"
  import { storeToRefs } from "pinia"

  const { todos, counter } = storeToRefs(useCounterStore())
  const { fetchTodos, incrementCounter, resetCounter } = useCounterStore()
</script>

<template>
  <div class="grid grid-cols-2">
    <div class="flex flex-col space-y-3">
      <div class="flex items-center space-x-3">
        <span>
          {{ counter }}
        </span>
        <button
          class="cursor-pointer px-3 py-1 rounded-md bg-rose-500 text-white"
          @click="incrementCounter"
        >
          +1
        </button>
        <button
          class="border border-gray-600/70 rounded-md px-3 py-1"
          @click="resetCounter"
        >
          Reset
        </button>
      </div>
    </div>
    <div class="flex flex-col space-y-3">
      <button
        class="cursor-pointer w-fit px-3 py-1 rounded-md bg-rose-500 text-white"
        @click="fetchTodos"
      >
        load todos
      </button>
      <div class="mt-3 flex flex-col space-y-3">
        <template v-for="todo in todos" :key="todo.id">
          <div>
            <div>title: {{ todo.title }}</div>
            <div>completed: {{ todo.completed }}</div>
          </div>
        </template>
      </div>
    </div>
  </div>
</template>
