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

export const updateRestaurant = async (updatedRestaurant: Restaurant) => {
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
  // try {
  //   const response = await fetch(`${WASTED_SERVER_URL}/Customer/${customerId}/food`, {
  //     method: "GET",
  //     headers: {
  //       "Content-Type": "application/json"
  //     }
  //   })
  //   const data = await response.json()
  //   return data
  // } catch (error) {
  //   return null
  // }
  return Promise.all(mockReservedFoods)
}

const mockReservedFoods = [
  {
    startingPrice: 2,
    minimumPrice: 2,
    description: "Circular disks of some meat on Pizza, who doesn’t enjoy that?",
    currentPrice: 2,
    createdAt: "2021-12-07T10:33:55.469574",
    idRestaurant: "94f3aec1-88cb-4b4a-ad2e-e883544dbe27",
    typesOfFood: [
      {
        id: "df7f6ca6-726c-4029-814e-cab89c6f7298",
        name: "Drinks"
      }
    ],
    startDecreasingAt: "2021-12-06T19:33:41.877",
    decreaseType: 1,
    intervalTimeInMinutes: 0,
    amountPerInterval: 0,
    percentPerInterval: 0,
    imageURL: "https://www.vynomeka.lt/images/uploader/ge/gerimas-coca-cola-stikle-025-l-1.jpg",
    reservation: {
      reservedAt: "2021-12-15T18:15:50.445322",
      price: 2,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "c0b1b8e0-cd59-4a48-8e6c-090ad6bcab42"
    },
    id: "c0b1b8e0-cd59-4a48-8e6c-090ad6bcab42",
    name: "Coca Cola 0.5l"
  },
  {
    startingPrice: 6.49,
    minimumPrice: 0.81,
    description: "Guacamole, hard taco shell, beans, what else would you want?",
    currentPrice: 0.81,
    createdAt: "2021-12-07T11:22:34.150052",
    idRestaurant: "8a46fbd8-8608-4bf1-8b82-d51684986cf4",
    typesOfFood: [
      {
        id: "cf7f6ca6-726c-4029-814e-c4d89c6f7298",
        name: "Fast Food"
      },
      {
        id: "cf7f6ca6-726c-4029-814e-cac89c6f7298",
        name: "Traditional Food"
      }
    ],
    startDecreasingAt: "2021-12-06T20:21:48.513",
    decreaseType: 1,
    intervalTimeInMinutes: 30,
    amountPerInterval: 0.7139,
    percentPerInterval: 11,
    imageURL: "https://media-cdn.tripadvisor.com/media/photo-s/1b/fa/f6/f8/tacos-plate.jpg",
    reservation: {
      reservedAt: "2021-12-15T19:41:27.02111",
      price: 0.81,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "a89427ba-d2cd-4adf-9a48-17dc04ac1f4b"
    },
    id: "a89427ba-d2cd-4adf-9a48-17dc04ac1f4b",
    name: "Taco"
  },
  {
    startingPrice: 1.5,
    minimumPrice: 0.29,
    description: "",
    currentPrice: 0.29,
    createdAt: "2021-12-07T11:06:56.53865",
    idRestaurant: "aec9eef1-a87e-4dea-894f-b82ee91cc700",
    typesOfFood: [
      {
        id: "cf7f6ca6-726c-4029-814e-cac89c6f7298",
        name: "Traditional Food"
      }
    ],
    startDecreasingAt: "2021-12-07T12:06:38.168",
    decreaseType: 0,
    intervalTimeInMinutes: 60,
    amountPerInterval: 0.36,
    percentPerInterval: 24,
    imageURL: "https://img1.moteris.lt/article/c4f3d1f2-bc94-42ac-82ab-67b5c3a1f7c5/cover/7289526.jpeg",
    reservation: {
      reservedAt: "2021-12-15T19:41:43.079104",
      price: 0.29,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "86585f5d-8d42-4672-8b08-1d70064edfa0"
    },
    id: "86585f5d-8d42-4672-8b08-1d70064edfa0",
    name: "Borsch with sour cream"
  },
  {
    startingPrice: 6.99,
    minimumPrice: 0.19,
    description: "",
    currentPrice: 0.19,
    createdAt: "2021-12-07T13:00:11.508462",
    idRestaurant: "aec9eef1-a87e-4dea-894f-b82ee91cc700",
    typesOfFood: [
      {
        id: "cf7f6ca6-726c-4029-814e-cac89c6f7298",
        name: "Traditional Food"
      }
    ],
    startDecreasingAt: "2021-12-07T08:59:00.967",
    decreaseType: 0,
    intervalTimeInMinutes: 30,
    amountPerInterval: 1.05,
    percentPerInterval: 15.021459227467812,
    imageURL:
      "https://s1.15min.lt/static/cache/MTIwMHg5MDAsMzMxeDI4OSw2MTYzMzcsb3JpZ2luYWwsLGlkPTQ1OTIyOSZkYXRlPTIwMTIlMkYwNCUyRjI0LDM1OTU4NzcwNzE=/aromatingas-kiaulienos-kepsnys-4f9675b9d7c80.jpg",
    reservation: {
      reservedAt: "2021-12-15T19:36:38.674542",
      price: 0.19,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "6637d1fa-9711-4028-8cb3-304d5e17fcaa"
    },
    id: "6637d1fa-9711-4028-8cb3-304d5e17fcaa",
    name: "Pork Steak"
  },
  {
    startingPrice: 5,
    minimumPrice: 1.22,
    description:
      "Big big big big big big big big big big big big big big big big big big big big big big big big big big big big big big big big big big mac.",
    currentPrice: 1.22,
    createdAt: "2021-12-06T20:28:44.800588",
    idRestaurant: "ea26117f-3502-4b82-b4f4-a2a81daa5a9d",
    typesOfFood: [
      {
        id: "cf7f6ca6-726c-4029-814e-c4d89c6f7298",
        name: "Fast Food"
      }
    ],
    startDecreasingAt: "2021-12-06T18:28:27",
    decreaseType: 1,
    intervalTimeInMinutes: 40,
    amountPerInterval: 1.35,
    percentPerInterval: 27,
    imageURL: "https://g3.dcdn.lt/images/pix/big-mac-65385318.jpg",
    reservation: {
      reservedAt: "2021-12-15T18:59:32.532861",
      price: 1.22,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "1e9e3e50-e0d2-4253-9561-33f4f9f1ec18"
    },
    id: "1e9e3e50-e0d2-4253-9561-33f4f9f1ec18",
    name: "Big Mac"
  },
  {
    startingPrice: 8.2,
    minimumPrice: 0.86,
    description: "Fat and sugar, hooray!",
    currentPrice: 0.86,
    createdAt: "2021-12-07T10:32:41.737166",
    idRestaurant: "ba26b456-6bb5-4154-926d-40c6cc3348f7",
    typesOfFood: [
      {
        id: "cf7f6ca6-726c-4029-814e-c4d89c6f7298",
        name: "Fast Food"
      }
    ],
    startDecreasingAt: "2021-12-06T11:32:20",
    decreaseType: 1,
    intervalTimeInMinutes: 15,
    amountPerInterval: 0.574,
    percentPerInterval: 7,
    imageURL:
      "https://prod-wolt-venue-images-cdn.wolt.com/5e709c1ac155e2597c73d760/8c584886-eeb7-11ea-b75e-8acdb8767903_french_toast_1_.jpg",
    reservation: {
      reservedAt: "2021-12-15T20:37:58.209885",
      price: 0.86,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "3ff28a99-64fd-42b5-bad0-49dc253889e5"
    },
    id: "3ff28a99-64fd-42b5-bad0-49dc253889e5",
    name: "Egg Sandwich + Bacon"
  },
  {
    startingPrice: 11,
    minimumPrice: 0.78,
    description: "Circular disks of some meat on Pizza, who doesn’t enjoy that?",
    currentPrice: 0.78,
    createdAt: "2021-12-07T11:32:32.415402",
    idRestaurant: "94f3aec1-88cb-4b4a-ad2e-e883544dbe27",
    typesOfFood: [
      {
        id: "cf7f6ca6-726c-4029-814e-c4d89c6f7298",
        name: "Fast Food"
      }
    ],
    startDecreasingAt: "2021-12-06T23:32:18",
    decreaseType: 1,
    intervalTimeInMinutes: 120,
    amountPerInterval: 0.11,
    percentPerInterval: 1,
    imageURL: "https://www.cookingclassy.com/wp-content/uploads/2014/07/pepperoni-pizza6+srgb..jpg",
    reservation: {
      reservedAt: "2021-12-15T19:36:54.803541",
      price: 0.78,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "aa93cd2c-c401-4d73-8dda-4c0003864051"
    },
    id: "aa93cd2c-c401-4d73-8dda-4c0003864051",
    name: "Pepperoni Pizza"
  },
  {
    startingPrice: 12.2,
    minimumPrice: 0.37,
    description: "Mmm... Sugar again",
    currentPrice: 0.37,
    createdAt: "2021-12-06T12:18:56.184011",
    idRestaurant: "8a46fbd8-8608-4bf1-8b82-d51684986cf4",
    typesOfFood: [
      {
        id: "cd7f6ca6-726c-4029-814e-cab89c6f7298",
        name: "Sweets"
      }
    ],
    startDecreasingAt: "2021-12-06T10:18:40",
    decreaseType: 0,
    intervalTimeInMinutes: 105,
    amountPerInterval: 0.56,
    percentPerInterval: 4.590163934426229,
    imageURL: "https://talutti.lt/wp-content/themes/talutti/images/kaunas0.jpg",
    reservation: {
      reservedAt: "2021-12-15T19:17:02.187355",
      price: 0.37,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "bd921985-6195-4421-be66-7825a147e7c7"
    },
    id: "bd921985-6195-4421-be66-7825a147e7c7",
    name: "Waffles"
  },
  {
    startingPrice: 8.5,
    minimumPrice: 5.13,
    description: "Mmm... Sugar",
    currentPrice: 5.13,
    createdAt: "2021-12-07T05:20:20.276472",
    idRestaurant: "8a46fbd8-8608-4bf1-8b82-d51684986cf4",
    typesOfFood: [
      {
        id: "cd7f6ca6-726c-4029-814e-cab89c6f7298",
        name: "Sweets"
      },
      {
        id: "df7f6ca6-726c-4029-814e-cab89c6f7298",
        name: "Drinks"
      }
    ],
    startDecreasingAt: "2021-12-07T10:20:00",
    decreaseType: 1,
    intervalTimeInMinutes: 80,
    amountPerInterval: 0.34,
    percentPerInterval: 4,
    imageURL:
      "https://www.vz.lt/apps/pbcsi.dll/bilde?Site=VZ&Date=20170711&Category=ARTICLE&ArtNo=711009997&Ref=PH&Item=10&NewTbl=1&maxW=1500&AlignV=center&lastupdate=515",
    reservation: {
      reservedAt: "2021-12-15T19:31:14.654862",
      price: 5.13,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "00cd0bad-ca89-4e89-9429-79a540ef2d83"
    },
    id: "00cd0bad-ca89-4e89-9429-79a540ef2d83",
    name: "Milkshake"
  },
  {
    startingPrice: 4.5,
    minimumPrice: 0.05,
    description: "The classic.",
    currentPrice: 0.05,
    createdAt: "2021-12-07T11:27:42.439118",
    idRestaurant: "ea26117f-3502-4b82-b4f4-a2a81daa5a9d",
    typesOfFood: [
      {
        id: "cf7f6ca6-726c-4029-814e-c4d89c6f7298",
        name: "Fast Food"
      }
    ],
    startDecreasingAt: "2021-12-06T11:27:26",
    decreaseType: 0,
    intervalTimeInMinutes: 30,
    amountPerInterval: 0.32,
    percentPerInterval: 7.111111111111111,
    imageURL: "https://s.abcnews.com/images/Lifestyle/gty_french_fries_mcdonalds_kb_150121_4x3_992.jpg",
    reservation: {
      reservedAt: "2021-12-15T19:40:26.84618",
      price: 0.05,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "e33f86ca-e211-4205-9fa1-af71cc69774f"
    },
    id: "e33f86ca-e211-4205-9fa1-af71cc69774f",
    name: "French Fries XL"
  },
  {
    startingPrice: 20,
    minimumPrice: 3.55,
    description: "5 bowls of Caesar’s Salad served with chicken, salad and dressing. No tomatoes!",
    currentPrice: 3.55,
    createdAt: "2021-12-06T11:30:16.240429",
    idRestaurant: "ea26117f-3502-4b82-b4f4-a2a81daa5a9d",
    typesOfFood: [
      {
        id: "cf7f6ca6-726c-4029-814e-c4d89c6f7298",
        name: "Fast Food"
      },
      {
        id: "cf7f6ca6-726c-4029-814e-cab89c6f7298",
        name: "Healthy Food"
      }
    ],
    startDecreasingAt: "2021-12-06T12:30:00",
    decreaseType: 0,
    intervalTimeInMinutes: 30,
    amountPerInterval: 2.77,
    percentPerInterval: 13.85,
    imageURL: "https://i.insider.com/56b4c0f12e52651a008b523a?width=1000&format=jpeg&auto=webp",
    reservation: {
      reservedAt: "2021-12-15T19:21:48.601288",
      price: 3.55,
      customerId: "7714624a-dc5b-4bbc-9290-53718129a962",
      foodId: "e43ed882-7fcb-4ae6-99e0-bf45debcdcac"
    },
    id: "e43ed882-7fcb-4ae6-99e0-bf45debcdcac",
    name: "Caesar’s Salad x5"
  }
]
