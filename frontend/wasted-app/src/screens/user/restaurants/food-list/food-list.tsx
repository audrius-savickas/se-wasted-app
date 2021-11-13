import React, {useEffect, useState} from "react"
import {LoaderScreen, Text, View} from "react-native-ui-lib"
import {Colors} from "react-native/Libraries/NewAppScreen"
import {getAllFoodByRestaurantId} from "../../../../api"
import {Food} from "../../../../api/interfaces"
import {SimpleFoodsList} from "../../../../components/simple-foods-list"
import {FoodListProps} from "./interfaces"

export const FoodList = ({componentId, restaurantId, restaurantName}: FoodListProps) => {
  const [foods, setFoods] = useState([] as Food[])
  const [loading, setLoading] = useState(true)

  const fetchFoods = async () => {
    const response = await getAllFoodByRestaurantId(restaurantId)
    setFoods(response)
    setLoading(false)
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
      {loading ? (
        <LoaderScreen color={Colors.blue30} message="Loading..." />
      ) : (
        <View flex>
          <SimpleFoodsList foods={foods} componentId={componentId} />
        </View>
      )}
    </>
  )
}
