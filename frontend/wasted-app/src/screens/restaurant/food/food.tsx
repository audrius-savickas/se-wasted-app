import React, {useEffect, useRef, useState} from "react"
import {Navigation} from "react-native-navigation"
import {LoaderScreen} from "react-native-ui-lib"
import {Colors} from "react-native/Libraries/NewAppScreen"
import {useRestaurant} from "../../../hooks/use-restaurant"
import {FoodList} from "../../user/restaurants/food-list"
import {FoodScreenProps} from "./interfaces"

export const Food = ({componentId}: FoodScreenProps) => {
  const [loading, setLoading] = useState(true)
  const [sideMenuOpen, setSideMenuOpen] = useState(false)
  const {restaurantId, restaurant, getRestaurantFromId} = useRestaurant()
  const isMounted = useRef(false)

  const fetchRestaurantById = async () => {
    setLoading(true)
    console.log("restaurant Id")
    console.log(restaurantId)
    getRestaurantFromId({restaurantId})
  }

  useEffect(() => {
    fetchRestaurantById()

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

    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "SIDE_MENU") {
        setSideMenuOpen(open => !open)
      }
    })

    return () => listener.remove()
  }, [])

  useEffect(() => {
    if (isMounted.current) {
      Navigation.mergeOptions(componentId, {
        sideMenu: {
          left: {
            visible: sideMenuOpen
          }
        }
      })
    } else {
      isMounted.current = true
    }
  }, [sideMenuOpen])

  useEffect(() => {
    if (restaurant.name) {
      setLoading(false)
    }
  }, [restaurant])

  return (
    <>
      {loading ? (
        <LoaderScreen loaderColor={Colors.blue30} message="Loading..." />
      ) : (
        <FoodList isRestaurant componentId={componentId} idRestaurant={restaurantId} restaurantName={restaurant.name} />
      )}
    </>
  )
}
