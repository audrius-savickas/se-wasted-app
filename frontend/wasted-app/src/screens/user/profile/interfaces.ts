import {NavigationComponentProps} from "react-native-navigation"
import {Customer} from "../../../api/interfaces"

export interface CustomerProfileProps extends NavigationComponentProps {
  customer: Customer
}
