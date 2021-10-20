import React, {useEffect} from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {getAllRestaurants} from "../../api"
import {WASTED_SERVER_URL} from "../../api/urls"
import {RestaurantItem} from "../restaurant-item/"

export const RestaurantsList = () => {
  const restaurants = [
    {id: 0, name: "Etno dvaras", address: "Vilnius, Lithuania"},
    {id: 1, name: "KFC", address: "Kaunas, Lithuania"},
    {id: 2, name: "McDonalds", address: "Klaipeda, Lithuania"}
  ]

  const fetchRestaurants = async () => {
    await getAllRestaurants()
  }

  useEffect(() => {
    fetchRestaurants()
  }, [])

  const renderItem = ({item}: ListRenderItemInfo<{id: number; name: string; address: string}>) => {
    return <RestaurantItem name={item.name} id={item.id} address={item.address} />
  }

  return <FlatList renderItem={renderItem} data={restaurants} />
}
