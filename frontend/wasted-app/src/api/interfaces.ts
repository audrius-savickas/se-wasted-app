export interface Restaurant {
  id: string
  name: string
  coords: {latitude: number; longitude: number}
  address: string
  imageURL: string
  distanceToUser: number
}

export interface Food {
  id: string
  name: string
  idRestaurant: string
  startingPrice: number
  currentPrice: number
  createdAt: string
  typesOfFood: FoodType[]
  startDecreasingAt: Date
  decreaseType: DecreaseType
  intervalTimeInMinutes: number
  amountPerInterval: number
  percentPerInterval: number
  imageURL: string
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
  sortType: RestaurantSortType
  coordinates?: Coordinates
}

export interface FoodSortObject {
  sortType: FoodSortType
}

export enum DecreaseType {
  AMOUNT = 0,
  PERCENT = 1
}

export enum RestaurantSortType {
  NAME = "name",
  NAME_DESC = "name_desc",
  DIST = "dist",
  DIST_DESC = "dist_desc"
}

export enum FoodSortType {
  PRICE = "price",
  PRICE_DESC = "price_desc",
  NAME = "name",
  NAME_DESC = "name_desc",
  TIME = "time",
  TIME_DESC = "time_desc"
}
