import {NativeStackScreenProps} from "@react-navigation/native-stack"
import React, {useEffect, useState} from "react"
import {View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../api"
import {Restaurant} from "../../api/interfaces"
import {RestaurantsList} from "../../components/restaurants-list"
import {RootStackParamList} from "../RootStackParamsList"

type restaurantListProps = NativeStackScreenProps<RootStackParamList, "RestaurantList">

export const RestaurantList = ({navigation}: restaurantListProps) => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])

  const fetchRestaurants = async () => {
    const response = await getAllRestaurants()
    setRestaurants(response)
  }

  useEffect(() => {
    fetchRestaurants()
  }, [])

  return (
    <View flex>
      <RestaurantsList navigation={navigation} restaurants={restaurants} />
    </View>
  )
}
