import {NativeStackScreenProps} from "@react-navigation/native-stack"
import React, {useEffect, useState} from "react"
import {LoaderScreen, View} from "react-native-ui-lib"
import {Colors} from "react-native/Libraries/NewAppScreen"
import {getAllRestaurants} from "../../api"
import {Restaurant} from "../../api/interfaces"
import {RestaurantsList} from "../../components/restaurants-list"
import {RootStackParamList} from "../RootStackParamsList"

type restaurantListProps = NativeStackScreenProps<RootStackParamList, "RestaurantList">

export const RestaurantList = ({navigation}: restaurantListProps) => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])
  const [loading, setLoading] = useState(true)

  const fetchRestaurants = async () => {
    const response = await getAllRestaurants()
    setRestaurants(response)
    setLoading(false)
  }

  useEffect(() => {
    fetchRestaurants()
  }, [])

  return (
    <View flex>
      {loading ? (
        <LoaderScreen color={Colors.blue30} message="Loading..." />
      ) : (
        <RestaurantsList navigation={navigation} restaurants={restaurants} />
      )}
    </View>
  )
}
