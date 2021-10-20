import {WASTED_SERVER_URL} from "./urls"

export const getAllRestaurants = async () => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Restaurant`)
    console.log(response)
    const data = await response.json()
    console.log(data)
  } catch (error) {
    console.error(error)
  }
}
