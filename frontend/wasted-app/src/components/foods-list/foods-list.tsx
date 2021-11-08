import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {Food} from "../../api/interfaces"
import {showFoodInfoModal} from "../../services/navigation"
import {FoodItem} from "../food-item"
import {FoodsListProps} from "./interfaces"

export const FoodsList = ({foods, componentId}: FoodsListProps) => {
  const renderItem = ({item}: ListRenderItemInfo<Food>) => {
    return (
      <FoodItem food={item} onPress={() => showFoodInfoModal({componentId, food: item, showRestaurantLink: false})} />
    )
  }

  return <FlatList data={foods} renderItem={renderItem} keyExtractor={item => item.id} />
}
