import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Image, Text, View} from "react-native-ui-lib"
import {getAllFood} from "../../../api/food"
import {Food, Restaurant} from "../../../api/interfaces"
import {HorizontalList} from "../../horizontal-list"

export const CheapestFood = () => {
  const [food, setFood] = useState([] as Food[])

  const fetchFood = async () => {
    setFood(await getAllFood())
  }

  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => (
    <View margin-s1 centerH>
      <Image
        source={{
          uri: `https://www.bbc.co.uk/staticarchive/4924b0772cad94008653b216980c869e1d4ec953.jpg`,
          width: 100,
          height: 100
        }}
      />
      <Text marginT-s1>{item.name}</Text>
    </View>
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
