import React from "react"
import {Text, View} from "react-native-ui-lib"
import {RestaurantItem} from "../restaurant-item/"

export const RestaurantsList = () => {
  const restaurants = [
    {id: 0, name: "Etno dvaras"},
    {id: 1, name: "KFC"},
    {id: 2, name: "McDonalds"}
  ]

  return (
    <View>
      {restaurants.map((restaurant, index) => (
        <RestaurantItem key={index} name={restaurant.name} id={restaurant.id} />
      ))}
    </View>
  )
}
