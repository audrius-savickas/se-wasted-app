import {NavigationComponentProps} from "react-native-navigation"

export interface FoodListOwnProps {
  idRestaurant: string
  isRestaurant?: boolean
}

export interface FoodListProps extends FoodListOwnProps, NavigationComponentProps {}
