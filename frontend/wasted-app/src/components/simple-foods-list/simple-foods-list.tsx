import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {Food} from "../../api/interfaces"
import {showFoodInfoModal} from "../../services/navigation"
import {SimpleFoodItem} from "../simple-food-item"
import {SimpleFoodsListProps} from "./interfaces"

export const SimpleFoodsList = ({foods, componentId}: SimpleFoodsListProps) => {
  const renderItem = ({item}: ListRenderItemInfo<Food>) => {
    return (
      <SimpleFoodItem
        food={item}
        onPress={() => showFoodInfoModal({componentId, food: item, showRestaurantLink: false})}
      />
    )
  }

  return <FlatList data={foods} renderItem={renderItem} keyExtractor={item => item.id} />
}
