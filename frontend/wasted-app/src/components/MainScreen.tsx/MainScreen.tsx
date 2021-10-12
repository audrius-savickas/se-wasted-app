import React from "react"
import { View, Text, Button, Colors } from "react-native-ui-lib"

export const MainScreen = () => {
  const log = () => console.log("Hello world")

  return (
    <View flex center>
      <Text cyan10 text30BO marginB-s6>
        Wasted App
      </Text>
      <Button
        margin-s2
        black
        bg-white
        outlineColor={Colors.black}
        label={"User"}
        onPress={log}
      />
      <Button
        black
        bg-white
        outlineColor={Colors.black}
        label={"Restaurant"}
        onPress={log}
      />
    </View>
  )
}
