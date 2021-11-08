import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getAllFood} from "../../../api/food"
import {Food} from "../../../api/interfaces"
import {showFoodInfoModal} from "../../../services/navigation"
import {formatPrice} from "../../../utils/currency"
import {HorizontalList} from "../../horizontal-list"

export const CheapestFood = () => {
  const [food, setFood] = useState([] as Food[])

  const fetchFood = async () => {
    setFood(await getAllFood())
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
      <View br20 bg-purple30 padding-s1 marginT-s1>
        <Text white text90M>
          {formatPrice(item.currentPrice)}
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
        💵 Cheapest food
      </Text>
      <HorizontalList items={food} renderItem={renderItem} />
    </View>
  )
}
