import {NavigationComponentProps} from "react-native-navigation"
import {Food} from "../../api/interfaces"

export interface FoodInfoProps extends NavigationComponentProps {
  food: Food
}
