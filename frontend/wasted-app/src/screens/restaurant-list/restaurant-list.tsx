import React from "react"
import {Text, View} from "react-native-ui-lib"
import {RestaurantsList} from "../../components/restaurants-list"

export const RestaurantList = () => {
  return (
    <View bg-red10>
      <RestaurantsList />
    </View>
  )
}
