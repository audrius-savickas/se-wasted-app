import React, { useEffect } from "react"
import { Navigation } from "react-native-navigation"
import {Text, View} from "react-native-ui-lib"
import { setHomeRoot } from "../../../services/navigation"

export const Profile = () => {
  
  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <View>
      <Text>This is profile view</Text>
    </View>
  )
}
