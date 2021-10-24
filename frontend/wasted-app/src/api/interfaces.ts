export interface Restaurant {
  id: string
  name: string
  coords: {latitude: number; longitude: number}
  address: string
}

export interface Food {
  id: string
  name: string
  price: string
}
