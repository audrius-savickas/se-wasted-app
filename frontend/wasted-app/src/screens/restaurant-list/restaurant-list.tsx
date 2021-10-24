import {NativeStackScreenProps} from "@react-navigation/native-stack"
import React from "react"
import {View} from "react-native-ui-lib"
import {RestaurantsList} from "../../components/restaurants-list"
import {RootStackParamList} from "../RootStackParamsList"

type restaurantListProps = NativeStackScreenProps<RootStackParamList, "RestaurantList">

export const RestaurantList = ({navigation}: restaurantListProps) => {
  return (
    <View flex>
      <RestaurantsList navigation={navigation} />
    </View>
  )
}
