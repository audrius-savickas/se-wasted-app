import {NavigationComponentProps} from "react-native-navigation"
import {Restaurant} from "../../api/interfaces"

export interface RestaurantInfoProps extends NavigationComponentProps {
  restaurant: Restaurant
}
