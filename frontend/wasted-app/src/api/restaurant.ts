import {Credentials, Food, Restaurant, RestaurantSortObject} from "./interfaces"
import {WASTED_SERVER_URL} from "./urls"

export const getAllRestaurants = async (sortObject?: RestaurantSortObject): Promise<Restaurant[]> => {
  try {
    let queryString = `${WASTED_SERVER_URL}/Restaurant`
    if (sortObject?.sortType) {
      queryString += `?sortOrder=${sortObject.sortType}`
      if (sortObject.coordinates) {
        queryString += `&Longitude=${sortObject.coordinates.longitude.toString()}&Latitude=${sortObject.coordinates.latitude.toString()}`
      }
    } else if (sortObject?.coordinates) {
      queryString += `?&Longitude=${sortObject.coordinates.longitude.toString()}&Latitude=${sortObject.coordinates.latitude.toString()}`
    }
    const response = await fetch(queryString)
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

export const getRestaurantById = async (id: string): Promise<Restaurant> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Restaurant/${id}`)
    const data = await response.json()
    return data
  } catch (error) {
    throw new Error("Restaurant not found")
  }
}

export const updateRestaurant = async (updatedRestaurant: Restaurant) => {
  await fetch(`${WASTED_SERVER_URL}/Restaurant/${updatedRestaurant.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(updatedRestaurant)
  })
}

export const loginRestaurant = async (credentials: Credentials) => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Restaurant/Login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({mail: {value: credentials.email}, password: {value: credentials.password}})
    })
    if (response.status === 401) throw new Error("Invalid credentials.")
    const data = await response.json()
    return data
  } catch (error) {
    return null
  }
}
