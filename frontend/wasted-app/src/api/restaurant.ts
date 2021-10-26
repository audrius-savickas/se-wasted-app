import {Food, Restaurant} from "./interfaces"
import {WASTED_SERVER_URL} from "./urls"

export const getAllRestaurants = async (): Promise<Restaurant[]> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Restaurant`)
    const data = await response.json()
    return data
  } catch (error) {
    console.error(error)
    return []
  }
}

export const getAllFoodByRestaurantId = async (id: string): Promise<Food[]> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Restaurant/${id}/food`)
    const data = await response.json()
    return data
  } catch (error) {
    return []
  }
}
