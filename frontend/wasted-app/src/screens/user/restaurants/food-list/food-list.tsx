import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {LoaderScreen, Text, View} from "react-native-ui-lib"
import {Colors} from "react-native/Libraries/NewAppScreen"
import {getAllFoodByRestaurantId} from "../../../../api"
import {Food} from "../../../../api/interfaces"
import {EmptyList} from "../../../../components/empty-list"
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
      {loading ? (
        <LoaderScreen color={Colors.blue30} message="Loading..." />
      ) : (
        <>
          {foods.length ? (
            <>
              <View center margin-s3>
                <Text text40M>{restaurantName}</Text>
                <Text text60L>Foods</Text>
              </View>
              <View flex>
                <SimpleFoodsList foods={foods} componentId={componentId} />
              </View>
            </>
          ) : (
            <EmptyList
              title={`Uh oh :(\nSadly ${restaurantName} doesn't have any foods added yet. `}
              subtitle="Please feel free to check back later or order from other restaurants!"
              buttonLabel="Go back"
              onPress={() => Navigation.pop(componentId)}
            />
          )}
        </>
      )}
    </>
  )
}
