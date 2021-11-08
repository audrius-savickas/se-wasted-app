import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {NavigationComponentProps} from "react-native-navigation"
import {Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant} from "../../../api/interfaces"
import {navigateToRestaurantInfo} from "../../../services/navigation"
import {HorizontalList} from "../../horizontal-list"

export const NearRestaurants = ({componentId}: NavigationComponentProps) => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])

  const fetchRestaurants = async () => {
    setRestaurants(await getAllRestaurants())
  }

  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => (
    <TouchableOpacity
      margin-s1
      centerH
      onPress={() =>
        navigateToRestaurantInfo(componentId, {
          imageUrl: "https://g2.dcdn.lt/images/pix/kfc-siauliuose-87245359.jpg",
          restaurant: item
        })
      }
    >
      <Image
        source={{
          uri: item.imageURL,
          width: 100,
          height: 100
        }}
      />
      <Text marginT-s1>{item.name}</Text>
    </TouchableOpacity>
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
