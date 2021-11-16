import {NavigationComponentProps} from "react-native-navigation"

export interface AddFoodScreenOwnProps {
  restaurantId: string
}

export interface AddFoodScreenProps extends NavigationComponentProps, AddFoodScreenOwnProps {}
