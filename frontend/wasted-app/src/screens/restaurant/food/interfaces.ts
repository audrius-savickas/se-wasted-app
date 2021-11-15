import {NavigationComponentProps} from "react-native-navigation"

export interface FoodScreenOwnProps {
  restaurantId: string
}

export interface FoodScreenProps extends NavigationComponentProps, FoodScreenOwnProps {}
