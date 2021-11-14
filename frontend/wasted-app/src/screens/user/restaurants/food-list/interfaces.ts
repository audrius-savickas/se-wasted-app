import {NavigationComponentProps} from "react-native-navigation"

export interface FoodListOwnProps {
  restaurantName: string
  restaurantId: string
  isRestaurant?: boolean
}

export interface FoodListProps extends FoodListOwnProps, NavigationComponentProps {}
