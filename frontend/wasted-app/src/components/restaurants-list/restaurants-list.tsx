import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {RestaurantItem} from "../restaurant-item/"

export const RestaurantsList = () => {
  const restaurants = [
    {id: 0, name: "Etno dvaras", address: "Vilnius, Lithuania"},
    {id: 1, name: "KFC", address: "Kaunas, Lithuania"},
    {id: 2, name: "McDonalds", address: "Klaipeda, Lithuania"}
  ]

  const renderItem = ({item}: ListRenderItemInfo<{id: number; name: string; address: string}>) => {
    return <RestaurantItem name={item.name} id={item.id} address={item.address} />
  }

  return <FlatList renderItem={renderItem} data={restaurants} />
}
