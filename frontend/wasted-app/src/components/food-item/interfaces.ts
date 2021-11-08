import {FoodType} from "../../api/interfaces"

export interface FoodItemProps {
  id: string
  name: string
  price: string
  types: FoodType[]
}
