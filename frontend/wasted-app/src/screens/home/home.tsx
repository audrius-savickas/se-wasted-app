import React from "react"
import {Button, Colors, Image, Text, View} from "react-native-ui-lib"
import {navigateToRestaurantLogin, setUserRoot} from "../../services/navigation"
import {HomeProps} from "./interfaces"

export const Home = ({componentId}: HomeProps) => {
  return (
    <View center marginT-s10>
      <Image
        source={require("../../../assets/wasted-app-logo.png")}
        style={{width: 290, height: 300, resizeMode: "contain"}}
      />
      <Text marginT-s10 cyan10 text20BO marginB-s4>
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
