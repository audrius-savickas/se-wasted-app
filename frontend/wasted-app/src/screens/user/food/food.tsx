import React, {useEffect} from "react"
import {Navigation} from "react-native-navigation"
import {Text, View} from "react-native-ui-lib"
import {setHomeRoot} from "../../../services/navigation"
import {FoodProps} from "./interfaces"

export const Food = ({componentId}: FoodProps) => {
  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "GO_BACK") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])
  return (
    <View>
      <Text>This is user's food view</Text>
    </View>
  )
}
