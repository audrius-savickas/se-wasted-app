import React, {useEffect} from "react"
import {Navigation} from "react-native-navigation"
import {Text, View} from "react-native-ui-lib"
import {setHomeRoot} from "../../../services/navigation"
import {HomeProps} from "./interfaces"

export const Home = ({componentId}: HomeProps) => {
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
      <Text>This is user's home</Text>
    </View>
  )
}
