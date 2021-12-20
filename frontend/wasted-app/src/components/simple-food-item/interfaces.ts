import {Food} from "../../api/interfaces"

export interface SimpleFoodItemProps {
  food: Food
  isRestaurant?: boolean
  onPress: () => void
}
