export interface Restaurant {
  id: string
  name: string
  coords: {latitude: number; longitude: number}
  address: string
  imageURL: string
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

export enum DecreaseType {
  AMOUNT,
  PERCENT
}
