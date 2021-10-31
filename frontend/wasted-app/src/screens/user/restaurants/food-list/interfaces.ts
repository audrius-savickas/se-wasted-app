import {NavigationComponentProps} from "react-native-navigation"

export interface FoodListOwnProps {
  restaurantName: string
  restaurantId: string
}

export interface FoodListProps extends FoodListOwnProps, NavigationComponentProps {}
