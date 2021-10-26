import {FoodType} from "./interfaces"
import {WASTED_SERVER_URL} from "./urls"

export const getFoodTypeByTypeId = async (id: string): Promise<FoodType> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/TypeOfFood/${id}`)
    const data = await response.json()
    return data
  } catch (error) {
    console.error(error)
    return Promise.resolve({id: "-1", name: "not found"})
  }
}
