import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Text, View} from "react-native-ui-lib"
import {getAllFood} from "../../../api/food"
import {Food, FoodSortType} from "../../../api/interfaces"
import {navigateToFoodInfo} from "../../../services/navigation"
import {formatPrice} from "../../../utils/currency"
import {HorizontalList} from "../../horizontal-list"
import {HorizontalListItem} from "../horizontal-list-item"
import {CheapestFoodProps} from "./interfaces"

export const CheapestFood = ({componentId}: CheapestFoodProps) => {
  const [food, setFood] = useState([] as Food[])
  const [pageNumber, setPageNumber] = useState(1)

  const fetchFood = async () => {
    setFood(await getAllFood({sortObject: {sortType: FoodSortType.PRICE}}))
  }

  const renderItem = ({item}: ListRenderItemInfo<Food>) => (
    <HorizontalListItem
      name={item.name}
      imageURL={item.imageURL}
      tag={formatPrice(item.currentPrice)}
      onPress={() => navigateToFoodInfo(componentId, {componentId, food: item})}
    />
  )

  const onEndReached = async () => {
    const newFood = await getAllFood({
      sortObject: {sortType: FoodSortType.PRICE},
      pagination: {pageNumber: pageNumber + 1, pageSize: 10}
    })

    if (newFood.length) {
      setFood(food.concat(newFood))
      setPageNumber(pageNumber + 1)
    }
  }

  useEffect(() => {
    fetchFood()
  }, [])

  return (
    <View centerV margin-s4>
      <Text text50L marginB-s2>
        💵 Cheapest food
      </Text>
      <HorizontalList items={food} renderItem={renderItem} onEndReached={onEndReached} />
    </View>
  )
}
