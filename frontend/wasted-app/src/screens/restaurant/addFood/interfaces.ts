import {NavigationComponentProps} from "react-native-navigation"
import {Food} from "../../../api/interfaces"

export interface AddFoodScreenProps extends NavigationComponentProps {
  existingFood?: Food
}
