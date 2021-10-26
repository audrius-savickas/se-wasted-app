import {NativeStackScreenProps} from "@react-navigation/native-stack"
import React, {useEffect, useState} from "react"
import {Text, View} from "react-native-ui-lib"
import {getAllFoodByRestaurantId} from "../../api"
import {Food} from "../../api/interfaces"
import {FoodsList} from "../../components/foods-list"
import {RootStackParamList} from "../RootStackParamsList"

type foodListProps = NativeStackScreenProps<RootStackParamList, "FoodList">

export const FoodList = ({route}: foodListProps) => {
  const [foods, setFoods] = useState([] as Food[])

  const fetchFoods = async () => {
    const response = await getAllFoodByRestaurantId(route.params.id)
    setFoods(response)
  }

  useEffect(() => {
    fetchFoods()
  }, [])

  return (
    <>
      <View center margin-s3>
        <Text text40M>{route.params.restaurantName}</Text>
        <Text text60L>Foods</Text>
      </View>
      <View flex>
        <FoodsList foods={foods} />
      </View>
    </>
  )
}
