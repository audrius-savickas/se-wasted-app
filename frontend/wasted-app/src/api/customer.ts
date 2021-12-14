import {Credentials, Customer} from "./interfaces"
import {WASTED_SERVER_URL} from "./urls"

export const loginCustomer = async ({credentials}: {credentials: Credentials}): Promise<string | null> => {
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
    console.log(data)
    return data
  } catch (error) {
    return null
  }
}

export const registerCustomer = async ({
  credentials,
  firstName,
  lastName
}: {
  credentials: Credentials
  firstName: string
  lastName: string
}): Promise<{customerId: string} | null> => {
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

export const getCustomerById = async ({customerId}: {customerId: string}): Promise<Customer> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Customer/${customerId}`)
    const data = await response.json()
    return data
  } catch (error) {
    throw new Error("Customer not found")
  }
}

export const updateCustomerPassword = async ({credentials}: {credentials: Credentials}): Promise<boolean> => {
  try {
    const response = await fetch(`${WASTED_SERVER_URL}/Customer/`, {
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
