import {Credentials} from "./interfaces"
import {WASTED_SERVER_URL} from "./urls"

export const loginUser = async ({credentials}: {credentials: Credentials}) => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Customer/Login`, {
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

export const registerUser = async ({
  credentials,
  firstName,
  lastName
}: {
  credentials: Credentials
  firstName: string
  lastName: string
}) => {
  try {
    const response = await fetch(
      `${WASTED_SERVER_URL}/Customer?Mail.Value=${encodeURIComponent(
        credentials.email
      )}&Password.Value=${encodeURIComponent(credentials.password)}`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({firstName, lastName})
      }
    )
    if (response.status === 401) throw new Error("Invalid credentials.")
    const data = await response.json()
    return data
  } catch (error) {
    return null
  }
}
