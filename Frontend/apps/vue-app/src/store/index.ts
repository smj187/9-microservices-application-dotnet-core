import { defineStore } from "pinia"

type Todo = {
  userId: number
  id: number
  title: string
  completed: boolean
}

type State = {
  counter: number
  todos: Array<Todo>
}

export const useCounterStore = defineStore("counter-store", {
  state: (): State => ({ counter: 0, todos: [] }),

  getters: {
    getDoubleCounter: (state: State) => state.counter * 2
  },

  actions: {
    incrementCounter() {
      this.counter++
    },
    resetCounter() {
      this.counter = 0
    },

    async fetchTodos() {
      const res = await fetch("https://jsonplaceholder.typicode.com/todos")
      const data: Array<Todo> = await res.json()

      this.todos = data
    }
  },

  persist: {
    paths: ["counter"]
  }
})
