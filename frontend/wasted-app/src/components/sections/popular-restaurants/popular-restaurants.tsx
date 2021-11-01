import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Image, Text, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant} from "../../../api/interfaces"
import {HorizontalList} from "../../horizontal-list"

export const PopularRestaurants = () => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])

  const fetchRestaurants = async () => {
    setRestaurants(await getAllRestaurants())
  }

  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => (
    <View margin-s1 centerH>
      <Image
        source={{
          uri: `https://media.istockphoto.com/photos/closeup-mcdonalds-outdoor-sign-against-blue-sky-picture-id458546943?k=20&m=458546943&s=612x612&w=0&h=G7fU8lNJh50I-Ou_ocB8XE5s_jpphKO0wNPy_5OxOkc=`,
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
        ⭐️ Popular restaurants
      </Text>
      <HorizontalList items={restaurants} renderItem={renderItem} />
    </View>
  )
}
