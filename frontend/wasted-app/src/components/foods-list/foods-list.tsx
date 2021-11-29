import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {Food} from "../../api/interfaces"
import {navigateToFoodInfo} from "../../services/navigation"
import {FoodItem} from "../food-item"
import {FoodsListProps} from "./interfaces"

export const FoodsList = ({componentId, foods}: FoodsListProps) => {
  const renderItem = ({item}: ListRenderItemInfo<Food>) => {
    return (
      <FoodItem
        food={item}
        onPress={() => {
          navigateToFoodInfo(componentId, {food: item, componentId})
        }}
      />
    )
  }

  return <FlatList style={{marginBottom: 8}} renderItem={renderItem} data={foods} keyExtractor={item => item.id} />
}
