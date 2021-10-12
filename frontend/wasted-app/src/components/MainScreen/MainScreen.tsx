import React from "react"
import {Button, Colors, Text, View} from "react-native-ui-lib"

export const MainScreen = () => {
  const log = () => console.log("Hello world")

  return (
    <View flex center>
      <Text cyan10 text30BO marginB-s6>
        Wasted App
      </Text>
      <Button margin-s2 black bg-white label={"User"} outlineColor={Colors.black} onPress={log} />
      <Button black bg-white outlineColor={Colors.black} label={"Restaurant"} onPress={log} />
    </View>
  )
}
