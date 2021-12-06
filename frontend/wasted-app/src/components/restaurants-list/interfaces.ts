import {NavigationComponentProps} from "react-native-navigation"
import {Restaurant} from "../../api/interfaces"

export interface RestaurantsListProps extends NavigationComponentProps {
  restaurants: Restaurant[]
  onEndReached: () => void
}
