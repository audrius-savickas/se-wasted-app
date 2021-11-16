import {Food, FoodSortObject, FoodType} from "./interfaces"
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

export const getAllFood = async (sortObject?: FoodSortObject): Promise<Food[]> => {
  try {
    let response
    if (sortObject) {
      response = await fetch(`${WASTED_SERVER_URL}/Food?sortOrder=${sortObject.sortType}`)
    } else {
      response = await fetch(`${WASTED_SERVER_URL}/Food`)
    }
    const data = await response.json()
    return data
  } catch (error) {
    console.error(error)
    return []
  }
}

export const addNewFood = async (food: Food) => {
  try {
    await fetch(`${WASTED_SERVER_URL}/Food`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(food)
    })
    return {
      ok: true
    }
  } catch (error) {
    return {
      ok: false,
      error
    }
  }
}
