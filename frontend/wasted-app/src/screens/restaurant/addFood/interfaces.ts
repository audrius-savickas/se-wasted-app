import {NavigationComponentProps} from "react-native-navigation"

export interface AddFoodScreenOwnProps {
  idRestaurant: string
}

export interface AddFoodScreenProps extends NavigationComponentProps, AddFoodScreenOwnProps {}
