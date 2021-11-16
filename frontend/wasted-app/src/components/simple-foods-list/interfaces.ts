import {NavigationComponentProps} from "react-native-navigation"
import {Food} from "../../api/interfaces"

export interface SimpleFoodsListProps extends NavigationComponentProps {
  foods: Food[]
  emptyListComponent: JSX.Element
  refreshing: boolean
  onRefresh: () => void
}
