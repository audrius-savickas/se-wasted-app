import {Coordinates, Credentials, Food, Restaurant, RestaurantRegisterRequest, RestaurantSortObject} from "./interfaces"
import {Pagination} from "./pagination"
import {WASTED_SERVER_URL} from "./urls"

export const getAllRestaurants = async ({
  sortObject,
  pagination
}: {
  sortObject?: RestaurantSortObject
  pagination?: Pagination
}): Promise<Restaurant[]> => {
  try {
    let queryString = `${WASTED_SERVER_URL}/Restaurant`

    if (sortObject || pagination) {
      queryString += "?"
    }
    if (sortObject?.sortType) {
      queryString += `sortOrder=${sortObject.sortType}`
      if (sortObject.coordinates) {
        queryString += `&Longitude=${sortObject.coordinates.longitude.toString()}&Latitude=${sortObject.coordinates.latitude.toString()}`
      }
    } else if (sortObject?.coordinates) {
      queryString += `&Longitude=${sortObject.coordinates.longitude.toString()}&Latitude=${sortObject.coordinates.latitude.toString()}`
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

export const getAllFoodByRestaurantId = async (id: string): Promise<Food[]> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Restaurant/${id}/food`)
    const data = await response.json()
    return data
  } catch (error) {
    return []
  }
}

export const getRestaurantById = async ({
  idRestaurant,
  coordinates
}: {
  idRestaurant: string
  coordinates?: Coordinates
}): Promise<Restaurant> => {
  try {
    let queryString = `${WASTED_SERVER_URL}/Restaurant/${idRestaurant}`

    if (coordinates) {
      queryString += `?&Longitude=${coordinates.longitude.toString()}&Latitude=${coordinates.latitude.toString()}`
    }
    const response = await fetch(queryString)
    const data = await response.json()
    return data
  } catch (error) {
    throw new Error("Restaurant not found")
  }
}

export const updateRestaurantApi = async (updatedRestaurant: Restaurant) => {
  const resp = await fetch(`${WASTED_SERVER_URL}/Restaurant/${updatedRestaurant.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(updatedRestaurant)
  })
  if (resp.status !== 200) {
    return null
  }
  return true
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

export const registerRestaurant = async ({
  name,
  coords,
  credentials: {email, password},
  address,
  description = "",
  imageUrl
}: RestaurantRegisterRequest) => {
  try {
    const response = await fetch(
      `${WASTED_SERVER_URL}/Restaurant?Mail.Value=${encodeURIComponent(email)}&Password.Value=${encodeURIComponent(
        password
      )}`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({name, coords, address, imageUrl, description})
      }
    )
    if (response.status !== 201) throw new Error("There is already a restaurant registered on this email.")
    const data = await response.json()
    return data
  } catch (error) {
    return null
  }
}

export const updateRestaurantPassword = async ({credentials}: {credentials: Credentials}): Promise<boolean> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Restaurant/`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({mail: {value: credentials.email}, password: {value: credentials.password}})
    })
    if (response.status === 401) throw new Error("Invalid email.")
    return true
  } catch (error) {
    return false
  }
}

export const getRestaurantReservedFoods = async ({restaurantId}: {restaurantId: string}): Promise<Food[] | null> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Restaurant/${restaurantId}/food/reserved`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json"
      }
    })
    const data = await response.json()
    return data
  } catch (error) {
    return null
  }
}
