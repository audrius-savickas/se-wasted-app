import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Image, Text, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant} from "../../../api/interfaces"
import {HorizontalList} from "../../horizontal-list"

export const NearRestaurants = () => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])

  const fetchRestaurants = async () => {
    setRestaurants(await getAllRestaurants())
  }

  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => (
    <View margin-s1 centerH>
      <Image
        source={{
          uri: item.imageURL,
          width: 100,
          height: 100
        }}
      />
      <Text marginT-s1>{item.name}</Text>
    </View>
  )

  useEffect(() => {
    fetchRestaurants()
  }, [])

  return (
    <View centerV margin-s4>
      <Text text50L marginB-s2>
        📍 Restaurants near you
      </Text>
      <HorizontalList items={restaurants} renderItem={renderItem} />
    </View>
  )
}
