import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getAllFood} from "../../../api/food"
import {Food} from "../../../api/interfaces"
import {showFoodInfoModal} from "../../../services/navigation"
import {timeAgo} from "../../../utils/date"
import {HorizontalList} from "../../horizontal-list"

export const LatestFood = () => {
  const [food, setFood] = useState([] as Food[])

  const fetchFood = async () => {
    setFood(await getAllFood())
  }

  const renderItem = ({item}: ListRenderItemInfo<Food>) => (
    <TouchableOpacity
      margin-s1
      centerH
      onPress={() =>
        showFoodInfoModal({
          food: item,
          imageUrl:
            "https://receptai.lidl.lt/var/site/storage/images/_aliases/960x540/0/7/8/5/895870-1-lit-LT/saltibarsciai-20210408-1440x495.jpg"
        })
      }
    >
      <Image
        source={{
          uri: `https://assets.epicurious.com/photos/57c5c6d9cf9e9ad43de2d96e/master/pass/the-ultimate-hamburger.jpg`,
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
