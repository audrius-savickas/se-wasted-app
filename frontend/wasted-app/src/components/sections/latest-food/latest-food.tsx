import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getAllFood} from "../../../api/food"
import {Food, FoodSortType} from "../../../api/interfaces"
import {showFoodInfoModal} from "../../../services/navigation"
import {timeAgo} from "../../../utils/date"
import {HorizontalList} from "../../horizontal-list"

export const LatestFood = () => {
  const [food, setFood] = useState([] as Food[])

  const fetchFood = async () => {
    setFood(await getAllFood({sortType: FoodSortType.TIME}))
  }

  const renderItem = ({item}: ListRenderItemInfo<Food>) => (
    <TouchableOpacity margin-s1 centerH onPress={() => showFoodInfoModal({food: item})}>
      <Image
        source={{
          uri: item.imageURL,
          width: 100,
          height: 100
        }}
      />
      <Text marginT-s1>{item.name}</Text>
      <View br20 bg-purple30 padding-s1 paddingH-s2 marginT-s1>
        <Text white text90M>
          {timeAgo(item.createdAt)}
        </Text>
      </View>
    </TouchableOpacity>
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
