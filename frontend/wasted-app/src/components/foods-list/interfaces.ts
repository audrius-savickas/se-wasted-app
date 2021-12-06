import {NavigationComponentProps} from "react-native-navigation"
import {Food} from "../../api/interfaces"

export interface FoodsListProps extends NavigationComponentProps {
  foods: Food[]
  onEndReached: () => void
}
