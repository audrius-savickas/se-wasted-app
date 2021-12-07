import {NavigationComponentProps} from "react-native-navigation"

export interface FoodListOwnProps {
  restaurantName: string
  idRestaurant: string
  isRestaurant?: boolean
}

export interface FoodListProps extends FoodListOwnProps, NavigationComponentProps {}
