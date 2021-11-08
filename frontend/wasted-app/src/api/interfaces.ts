export interface Restaurant {
  id: string
  name: string
  coords: {latitude: number; longitude: number}
  address: string,
  imageURL: string
}

export interface Food {
  id: string
  name: string
  price: string
  idTypeOfFood: string
}

export interface FoodType {
  id: string
  name: string
}
