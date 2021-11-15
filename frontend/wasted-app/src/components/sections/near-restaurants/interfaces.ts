import {Location} from "react-native-get-location"
import {NavigationComponentProps} from "react-native-navigation"

export interface NearRestaurantsProps extends NavigationComponentProps {
  location: Location
}
