import React, {useEffect} from "react"
import {Navigation} from "react-native-navigation"
import {setHomeRoot} from "../../../services/navigation"
import {FoodList} from "../../user/restaurants/food-list"
import {FoodScreenProps} from "./interfaces"

export const Food = ({componentId, restaurant}: FoodScreenProps) => {
  const {id, name} = restaurant

  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  return <FoodList componentId={componentId} restaurantId={id} restaurantName={name} />
}
