import React, {useEffect, useState} from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {getAllRestaurants} from "../../api"
import {Restaurant} from "../../api/interfaces"
import {RestaurantItem} from "../restaurant-item/"
import {RestaurantsListProps} from "./interfaces"

export const RestaurantsList = ({navigation}: RestaurantsListProps) => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])

  const fetchRestaurants = async () => {
    const response = await getAllRestaurants()
    setRestaurants(response)
  }

  useEffect(() => {
    fetchRestaurants()
  }, [])

  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => {
    return (
      <RestaurantItem
        name={item.name}
        id={item.id}
        address={item.address}
        onPress={() => navigation.navigate("FoodList", {restaurantName: item.name, id: item.id})}
      />
    )
  }

  return <FlatList renderItem={renderItem} data={restaurants} keyExtractor={item => item.id} />
}
