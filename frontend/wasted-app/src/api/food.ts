import {Food, FoodType} from "./interfaces"
import {WASTED_SERVER_URL} from "./urls"

export const getFoodTypeByFoodId = async (id: string): Promise<FoodType> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Food/${id}/type`)
    const data = await response.json()
    return data
  } catch (error) {
    console.error(error)
    return Promise.resolve({id: "-1", name: "not found"})
  }
}

export const getAllFood = async (): Promise<Food[]> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Food`)
    const data = await response.json()
    console.log(data)
    return data
  } catch (error) {
    console.error(error)
    return []
  }
}
