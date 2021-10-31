import React from "react"
import {Button, Colors, Text, View} from "react-native-ui-lib"
import {navigateToRestaurantLogin, setUserRoot} from "../../services/navigation"
import {HomeProps} from "./interfaces"

export const Home = ({componentId}: HomeProps) => {
  return (
    <View flex center>
      <Text cyan10 text30BO marginB-s6>
        Wasted App
      </Text>
      <Button
        margin-s2
        black
        bg-white
        label={"User"}
        outlineColor={Colors.black}
        onPress={() => setUserRoot(componentId)}
      />
      <Button
        black
        bg-white
        outlineColor={Colors.black}
        label={"Restaurant"}
        onPress={() => navigateToRestaurantLogin(componentId, {})}
      />
    </View>
  )
}
