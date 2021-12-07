import {NavigationComponentProps} from "react-native-navigation"

export interface FoodScreenOwnProps {
  idRestaurant: string
}

export interface FoodScreenProps extends NavigationComponentProps, FoodScreenOwnProps {}
