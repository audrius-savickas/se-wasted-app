import {NavigationComponentProps} from "react-native-navigation"
import {Restaurant} from "../../../api/interfaces"

export interface FoodScreenOwnProps {
  restaurant: Restaurant
}

export interface FoodScreenProps extends NavigationComponentProps, FoodScreenOwnProps {}
