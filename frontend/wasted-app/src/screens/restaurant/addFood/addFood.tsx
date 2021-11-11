import React, { useEffect } from "react"
import { Navigation } from "react-native-navigation"
import { Text } from "react-native-ui-lib"
import { setHomeRoot } from "../../../services/navigation"
import { AddFoodScreenProps } from "./interfaces"

export const AddFood = ({} : AddFoodScreenProps) => {
  
  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <Text>Add food</Text>
  )
}
