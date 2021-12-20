import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {Colors, LoaderScreen, Text, View} from "react-native-ui-lib"
import {getCustomerReservedFoods} from "../../../api/customer"
import {Food} from "../../../api/interfaces"
import {CustomerReservationsList} from "../../../components/customer-reservations-list"
import {useCustomer} from "../../../hooks/use-customer"
import {ReservationsProps} from "./interfaces"

export const Reservations = ({componentId}: ReservationsProps) => {
  const {customerId} = useCustomer()
  const [foods, setFoods] = useState([] as Food[])
  const [loading, setLoading] = useState(true)

  const fetchReservations = async () => {
    const response = await getCustomerReservedFoods({customerId})
    if (response) {
      console.log("success")
      setFoods(response)
    } else {
      console.log("fail")
    }

    setLoading(false)
  }

  useEffect(() => {
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

    fetchReservations()
  }, [])

  return (
    <View flex>
      <Text marginV-s2 text40L center>
        Your reservations
      </Text>
      {loading ? (
        <LoaderScreen loaderColor={Colors.blue30} message="Loading..." />
      ) : (
        <CustomerReservationsList foods={foods} componentId={componentId} />
      )}
    </View>
  )
}
