import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {Food} from "../../api/interfaces"
import {showFoodInfoModal} from "../../services/navigation"
import {SimpleFoodItem} from "../simple-food-item"
import {SimpleFoodsListProps} from "./interfaces"

export const SimpleFoodsList = ({
  foods,
  emptyListComponent,
  componentId,
  refreshing,
  onRefresh
}: SimpleFoodsListProps) => {
  const renderItem = ({item}: ListRenderItemInfo<Food>) => {
    return (
      <SimpleFoodItem food={item} onPress={() => showFoodInfoModal({componentId, food: item, isCustomer: false})} />
    )
  }

  return (
    <FlatList
      ListEmptyComponent={emptyListComponent}
      refreshing={refreshing}
      data={foods}
      keyExtractor={item => item.id}
      renderItem={renderItem}
      onRefresh={onRefresh}
    />
  )
}
