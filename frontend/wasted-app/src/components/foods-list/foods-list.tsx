import { Zocial } from "@expo/vector-icons"
import React from "react"
import {FlatList, ListRenderItemInfo} from "react-native"
import { TouchableOpacity } from "react-native-gesture-handler"
import {Food} from "../../api/interfaces"
import { showFoodInfoModal } from "../../services/navigation"
import {FoodItem} from "../food-item"
import {FoodsListProps} from "./interfaces"

export const FoodsList = ({foods}: FoodsListProps) => {
  const renderItem = ({item}: ListRenderItemInfo<Food>) => {
    return (
      <TouchableOpacity onPress={() => showFoodInfoModal({food: item})}>
        <FoodItem 
          id={item.id} 
          name={item.name} 
          price={item.currentPrice} 
          types={item.typesOfFood}
        />
      </TouchableOpacity>
    )
  }

  return <FlatList data={foods} renderItem={renderItem} keyExtractor={item => item.id} />
}
