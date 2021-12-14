import React, {useEffect} from "react"
import {Navigation} from "react-native-navigation"
import {Text, View} from "react-native-ui-lib"
import {CustomerProfileProps} from "./interfaces"

export const Profile = ({componentId}: CustomerProfileProps) => {
  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "DISMISS") {
        Navigation.dismissModal(componentId)
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <View>
      <Text>Profile</Text>
    </View>
  )
}
