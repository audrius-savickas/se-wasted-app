import {WASTED_SERVER_URL} from "./urls"

export const reserveFood = async ({
  foodId,
  customerId
}: {
  foodId: string
  customerId: string
}): Promise<string | null> => {
  const response = await fetch(`${WASTED_SERVER_URL}/Reservation?foodId=${foodId}&customerId=${customerId}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    }
  })
  if (response.status === 201) {
    const data = await response.json()
    return data
  } else {
    return null
  }
}

export const cancelFoodReservation = async ({
  foodId,
  customerId
}: {
  foodId: string
  customerId: string
}): Promise<boolean> => {
  const response = await fetch(`${WASTED_SERVER_URL}/Reservation?foodId=${foodId}&customerId=${customerId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json"
    }
  })
  if (response.status === 200) {
    return true
  } else {
    return false
  }
}

export const finishFoodReservation = async ({foodId, customerId}: {foodId: string; customerId: string}) => {
  const response = await fetch(`${WASTED_SERVER_URL}/Reservation?foodId=${foodId}&customerId=${customerId}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json"
    }
  })
  if (response.status === 200) {
    return true
  } else {
    return false
  }
}
