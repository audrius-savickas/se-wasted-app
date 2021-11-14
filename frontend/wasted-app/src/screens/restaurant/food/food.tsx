import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {LoaderScreen, Text} from "react-native-ui-lib"
import {Colors} from "react-native/Libraries/NewAppScreen"
import {getRestaurantById} from "../../../api"
import {Restaurant} from "../../../api/interfaces"
import {setHomeRoot} from "../../../services/navigation"
import {FoodList} from "../../user/restaurants/food-list"
import {FoodScreenProps} from "./interfaces"

export const Food = ({componentId, restaurantId}: FoodScreenProps) => {
  const [restaurant, setRestaurant] = useState({} as Restaurant)
  const [loading, setLoading] = useState(true)

  const fetchRestaurantById = async () => {
    setRestaurant(await getRestaurantById(restaurantId))
    setLoading(false)
  }

  useEffect(() => {
    fetchRestaurantById()
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <>
      {loading ? (
        <LoaderScreen collapsable={Colors.blue30} message="Loading..." />
      ) : (
        <FoodList componentId={componentId} restaurantId={restaurantId} restaurantName={"bla"} />
      )}
    </>
  )
}
