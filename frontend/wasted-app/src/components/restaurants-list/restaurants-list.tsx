import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {Restaurant} from "../../api/interfaces"
import {navigateToFoodList} from "../../services/navigationService"
import {RestaurantItem} from "../restaurant-item/"
import {RestaurantsListProps} from "./interfaces"

export const RestaurantsList = ({componentId, restaurants}: RestaurantsListProps) => {
  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => {
    return (
      <RestaurantItem
        name={item.name}
        id={item.id}
        address={item.address}
        onPress={() => navigateToFoodList(componentId, {restaurantId: item.id, restaurantName: item.name})}
      />
    )
  }

  return <FlatList renderItem={renderItem} data={restaurants} keyExtractor={item => item.id} />
}
