import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {Restaurant} from "../../api/interfaces"
import {navigateToRestaurantInfo} from "../../services/navigation"
import {RestaurantItem} from "../restaurant-item/"
import {RestaurantsListProps} from "./interfaces"

export const RestaurantsList = ({componentId, restaurants}: RestaurantsListProps) => {
  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => {
    return (
      <RestaurantItem
        restaurant={item}
        onPress={() => navigateToRestaurantInfo(componentId, {componentId, restaurant: item})}
      />
    )
  }

  return (
    <FlatList style={{marginBottom: 8}} renderItem={renderItem} data={restaurants} keyExtractor={item => item.id} />
  )
}
