import React from "react"
import {FlatList} from "react-native"
import {SimpleFoodsListProps} from "./interfaces"

export const SimpleFoodsList = ({
  foods,
  emptyListComponent,
  refreshing,
  onRefresh,
  renderItem
}: SimpleFoodsListProps) => {
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
