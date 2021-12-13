import React from "react"
import {Button, Colors, Image, Text, View} from "react-native-ui-lib"
import {navigateToRestaurantLogin, navigateToUserLogin} from "../../services/navigation"
import {HomeProps} from "./interfaces"

export const Home = ({componentId}: HomeProps) => {
  return (
    <View flex center>
      <Image
        source={require("../../../assets/wasted-app-logo.png")}
        style={{width: 270, height: 290, resizeMode: "contain"}}
      />
      <Text text20M marginB-s4 color="#6D13F8">
        Wasted App
      </Text>
      <View marginB-s10>
        <Button
          center
          margin-s2
          black
          bg-white
          style={{borderColor: "#0482DF", height: 50, alignSelf: "center"}}
          iconSource={require("../../../assets/profile-30x30.png")}
          label={"User"}
          outlineColor={Colors.black}
          onPress={() => navigateToUserLogin(componentId)}
        />
        <Button
          marginB-s10
          black
          bg-white
          style={{borderColor: "#0482DF", width: 180, height: 50, alignSelf: "center"}}
          iconSource={require("../../../assets/restaurant-25x25.png")}
          outlineColor={Colors.black}
          label={"Restaurant"}
          onPress={() => navigateToRestaurantLogin(componentId, {})}
        />
      </View>
    </View>
  )
}
