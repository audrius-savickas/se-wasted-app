export interface Restaurant {
  id: string
  name: string
  coords: {latitude: number; longitude: number}
  address: string
  imageURL: string
  distanceToUser: number
  description: string
  foodCount: number
}

export interface Food {
  id: string
  name: string
  description: string
  idRestaurant: string
  startingPrice: number
  minimumPrice: number
  currentPrice: number
  createdAt: string
  startDecreasingAt: string
  typesOfFood: FoodType[]
  decreaseType: DecreaseType
  intervalTimeInMinutes: number
  amountPerInterval: number
  percentPerInterval: number
  imageURL: string
  reservation: Reservation | null
}

export interface Customer {
  firstName: string
  lastName: string
  id: string
  mail: string
  phone: string
}

export interface Reservation {
  reservedAt: string
  foodId: string
  customerId: string
  price: number
}

export interface FoodType {
  id: string
  name: string
}

export interface Coordinates {
  latitude: number
  longitude: number
}

export interface RestaurantSortObject {
  sortType?: RestaurantSortType
  coordinates?: Coordinates
  ascending?: boolean
}

export interface FoodSortObject {
  sortType: FoodSortType
}

export interface Credentials {
  email: string
  password: string
}

export interface RestaurantRegisterRequest {
  credentials: Credentials
  name: string
  coords: Coordinates
  address: string
  imageUrl: string
  description?: string
}

export enum DecreaseType {
  AMOUNT,
  PERCENT
}

export enum RestaurantSortType {
  NAME = "name",
  NAME_DESC = "name_desc",
  DIST = "dist",
  DIST_DESC = "dist_desc",
  FOOD_COUNT = "foodCount",
  FOOD_COUNT_DESC = "foodCount_desc"
}

export enum FoodSortType {
  PRICE = "price",
  PRICE_DESC = "price_desc",
  NAME = "name",
  NAME_DESC = "name_desc",
  TIME = "time",
  TIME_DESC = "time_desc"
}
