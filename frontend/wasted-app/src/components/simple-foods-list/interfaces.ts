import {ListRenderItemInfo} from "react-native"
import {Food} from "../../api/interfaces"

export interface SimpleFoodsListProps {
  foods: Food[]
  emptyListComponent: JSX.Element
  refreshing: boolean
  onRefresh: () => void
  renderItem: ({item}: ListRenderItemInfo<any>) => JSX.Element
}
