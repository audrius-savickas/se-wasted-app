import {NavigationComponentProps} from "react-native-navigation"

export interface FoodScreenOwnProps {
    restaurantId: string,
    restaurantName: string
}

export interface FoodScreenProps extends NavigationComponentProps, FoodScreenOwnProps {}
