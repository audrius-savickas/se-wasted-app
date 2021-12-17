import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {Colors, LoaderScreen, Text, View} from "react-native-ui-lib"
import {getRestaurantReservedFoods} from "../../../api"
import {Food} from "../../../api/interfaces"
import {RestaurantReservationsList} from "../../../components/restaurant-reservations-list"
import {useRestaurant} from "../../../hooks/use-restaurant"
import {ReservationsProps} from "./interfaces"

export const Reservations = ({componentId}: ReservationsProps) => {
  const [loading, setLoading] = useState(true)
  const [foods, setFoods] = useState([] as Food[])
  const {restaurantId} = useRestaurant()

  const getReservations = async () => {
    const response = await getRestaurantReservedFoods({restaurantId})

    if (response) {
      console.log("success")
      setFoods(response)
    } else {
      console.log("fail")
    }

    setLoading(false)
  }

  useEffect(() => {
    getReservations()

    Navigation.mergeOptions(componentId, {
      sideMenu: {
        left: {
          visible: false,
          width: 260
        }
      },
      topBar: {
        leftButtons: [
          {
            icon: require("../../../../assets/menu-26x26.png"),
            disableIconTint: true,
            id: "SIDE_MENU"
          }
        ]
      }
    })
  }, [])

  return (
    <View flex>
      <Text marginV-s2 marginH-s8 text40L center>
        Your restaurant's reservations
      </Text>
      {loading ? (
        <LoaderScreen loaderColor={Colors.blue30} message="Loading..." />
      ) : (
        <RestaurantReservationsList componentId={componentId} foods={foods} />
      )}
    </View>
  )
}
