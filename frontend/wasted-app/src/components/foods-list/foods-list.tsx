import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import {Food} from "../../api/interfaces"
import {FoodItem} from "../food-item"
import {FoodsListProps} from "./interfaces"

export const FoodsList = ({foods}: FoodsListProps) => {
  const renderItem = ({item}: ListRenderItemInfo<Food>) => {
    return <FoodItem id={item.id} name={item.name} price={item.price} />
  }

  return <FlatList data={foods} renderItem={renderItem} keyExtractor={item => item.id} />
}
