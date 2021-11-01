import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {LoaderScreen, View} from "react-native-ui-lib"
import {Colors} from "react-native/Libraries/NewAppScreen"
import {getAllRestaurants} from "../../../api"
import {Restaurant} from "../../../api/interfaces"
import {RestaurantsList} from "../../../components/restaurants-list"
import {setHomeRoot} from "../../../services/navigation"
import {RestaurantListProps} from "./interfaces"

export const RestaurantList = ({componentId}: RestaurantListProps) => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])
  const [loading, setLoading] = useState(true)

  const fetchRestaurants = async () => {
    const response = await getAllRestaurants()
    setRestaurants(response)
    setLoading(false)
  }

  useEffect(() => {
    fetchRestaurants()
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "GO_BACK") {
        setHomeRoot()
      }
    })

    return () => listener.remove()
  }, [])

  return (
    <View flex>
      {loading ? (
        <LoaderScreen color={Colors.blue30} message="Loading..." />
      ) : (
        <RestaurantsList componentId={componentId} restaurants={restaurants} />
      )}
    </View>
  )
}
