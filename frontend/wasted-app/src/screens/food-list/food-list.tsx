import React, {useEffect, useState} from "react"
import {Text, View} from "react-native-ui-lib"
import {getAllFoodByRestaurantId} from "../../api"
import {Food} from "../../api/interfaces"
import {FoodsList} from "../../components/foods-list"
import {FoodListProps} from "./interfaces"

export const FoodList = ({componentId, restaurantId, restaurantName}: FoodListProps) => {
  const [foods, setFoods] = useState([] as Food[])

  const fetchFoods = async () => {
    const response = await getAllFoodByRestaurantId(restaurantId)
    setFoods(response)
  }

  useEffect(() => {
    fetchFoods()
  }, [])

  return (
    <>
      <View center margin-s3>
        <Text text40M>{restaurantName}</Text>
        <Text text60L>Foods</Text>
      </View>
      <View flex>
        <FoodsList foods={foods} />
      </View>
    </>
  )
}
