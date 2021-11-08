import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Image, Text, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant, RestaurantSortType} from "../../../api/interfaces"
import {HorizontalList} from "../../horizontal-list"

export const PopularRestaurants = () => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])

  const fetchRestaurants = async () => {
    setRestaurants(await getAllRestaurants({sortType: RestaurantSortType.NAME}))
  }

  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => (
    <View margin-s1 centerH>
      <Image
        source={{
          uri: item.imageURL,
          width: 100,
          height: 100
        }}
        style={{width: 100, height: 100}}
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
        ⭐️ Popular restaurants
      </Text>
      <HorizontalList items={restaurants} renderItem={renderItem} />
    </View>
  )
}
