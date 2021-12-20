import {NavigationComponentProps} from "react-native-navigation"
import {Food} from "../../../api/interfaces"

export interface ReservationInfoProps extends NavigationComponentProps {
  food: Food
}
