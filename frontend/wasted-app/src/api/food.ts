import {Food, FoodSortObject, FoodType} from "./interfaces"
import {Pagination} from "./pagination"
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

export const getAllFood = async ({
  sortObject,
  pagination
}: {
  sortObject?: FoodSortObject
  pagination?: Pagination
}): Promise<Food[]> => {
  try {
    let queryString = `${WASTED_SERVER_URL}/Food`

    if (sortObject || pagination) {
      queryString += "?"
    }
    if (sortObject) {
      queryString += `${WASTED_SERVER_URL}/Food?sortOrder=${sortObject.sortType}`
    }
    if (pagination) {
      queryString += `&PageNumber=${pagination.pageNumber}&PageSize=${pagination.pageSize}`
    }

    const response = await fetch(queryString)
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
