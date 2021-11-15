import {Location} from "react-native-get-location"
import {NavigationComponentProps} from "react-native-navigation"

export interface PopularRestaurantsProps extends NavigationComponentProps {
  location: Location
}
