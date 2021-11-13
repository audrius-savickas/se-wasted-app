import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {Text, View} from "react-native-ui-lib"
import {Food} from "../../api/interfaces"
import {showFoodInfoModal} from "../../services/navigation"
import {FoodItem} from "../food-item"
import {FoodsListProps} from "./interfaces"

export const FoodsList = ({componentId, foods}: FoodsListProps) => {
  const renderItem = ({item}: ListRenderItemInfo<Food>) => {
    return (
      <FoodItem
        food={item}
        onPress={() => {
          showFoodInfoModal({food: item, showRestaurantLink: true, componentId})
        }}
      />
    )
  }

  return <FlatList style={{marginBottom: 10}} renderItem={renderItem} data={foods} keyExtractor={item => item.id} />
}
