import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Text, View} from "react-native-ui-lib"
import {getAllFood} from "../../../api/food"
import {Food, FoodSortType} from "../../../api/interfaces"
import {navigateToFoodInfo} from "../../../services/navigation"
import {timeAgo} from "../../../utils/date"
import {HorizontalList} from "../../horizontal-list"
import {HorizontalListItem} from "../horizontal-list-item"
import {LatestFoodProps} from "./interfaces"

export const LatestFood = ({componentId}: LatestFoodProps) => {
  const [food, setFood] = useState([] as Food[])

  const fetchFood = async () => {
    setFood(await getAllFood({sortObject: {sortType: FoodSortType.TIME}}))
  }

  const renderItem = ({item}: ListRenderItemInfo<Food>) => (
    <HorizontalListItem
      name={item.name}
      imageURL={item.imageURL}
      tag={timeAgo(item.createdAt)}
      onPress={() => navigateToFoodInfo(componentId, {componentId, food: item})}
    />
  )

  useEffect(() => {
    fetchFood()
  }, [])

  return (
    <View centerV margin-s4>
      <Text text50L marginB-s2>
        ⏰ Latest food
      </Text>
      <HorizontalList items={food} renderItem={renderItem} />
    </View>
  )
}
