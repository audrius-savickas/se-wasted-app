import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {LoaderScreen, Text, View} from "react-native-ui-lib"
import {Colors} from "react-native/Libraries/NewAppScreen"
import {getAllFoodByRestaurantId} from "../../../../api"
import {Food} from "../../../../api/interfaces"
import {EmptyList} from "../../../../components/empty-list"
import {SimpleFoodsList} from "../../../../components/simple-foods-list"
import {FoodListProps} from "./interfaces"

export const FoodList = ({componentId, restaurantId, restaurantName, isRestaurant = false}: FoodListProps) => {
  const [foods, setFoods] = useState([] as Food[])
  const [loading, setLoading] = useState(true)
  const [refreshing, setRefreshing] = useState(false)

  const fetchFoods = async () => {
    const response = await getAllFoodByRestaurantId(restaurantId)
    setFoods(response)
    setRefreshing(false)
    setLoading(false)
  }

  useEffect(() => {
    fetchFoods()
  }, [])

  useEffect(() => {
    if (refreshing) {
      fetchFoods()
    }
  }, [refreshing])

  return (
    <>
      {loading ? (
        <LoaderScreen color={Colors.blue30} message="Loading..." />
      ) : (
        <>
          <View center margin-s3>
            <Text text40M>{restaurantName}</Text>
            <Text text60L>Foods</Text>
          </View>
          <View flex>
            <SimpleFoodsList
              emptyListComponent={
                <EmptyList
                  title={
                    isRestaurant
                      ? `Uh oh :(\nSadly you haven't added any foods yet.`
                      : `Uh oh :(\nSadly ${restaurantName} doesn't have any foods added yet. `
                  }
                  subtitle={
                    isRestaurant
                      ? `Please feel free to add more foods by clicking "Add food" button`
                      : "Please feel free to check back later or order from other restaurants!"
                  }
                  buttonLabel={isRestaurant ? "Add food" : "Go back"}
                  onPress={isRestaurant ? () => {} : () => Navigation.pop(componentId)}
                />
              }
              refreshing={refreshing}
              foods={foods}
              componentId={componentId}
              onRefresh={() => setRefreshing(true)}
            />
          </View>
        </>
      )}
    </>
  )
}
