import React from "react"
import {View} from "react-native-ui-lib"
import {RestaurantsList} from "../../components/restaurants-list"

export const RestaurantList = () => {
  return (
    <View flex>
      <RestaurantsList />
    </View>
  )
}
