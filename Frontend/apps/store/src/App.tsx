import { useEffect, useState } from "react"
import axios from "axios"
function App() {
  const load = async () => {
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
  }

  useEffect(() => {
    load()
  }, [])

  return <div className="text-rose-500">test</div>
}

export default App

