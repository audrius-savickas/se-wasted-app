import {NativeStackNavigationProp} from "@react-navigation/native-stack"
import {Restaurant} from "../../api/interfaces"
import {RootStackParamList} from "../../screens/RootStackParamsList"

export interface RestaurantsListProps {
  navigation: NativeStackNavigationProp<RootStackParamList, "RestaurantList">
  restaurants: Restaurant[]
}
