import {NavigationComponentProps} from "react-native-navigation"
import {Customer} from "../../../api/interfaces"

export interface CustomerInfoProps extends NavigationComponentProps {
  customer: Customer
}
