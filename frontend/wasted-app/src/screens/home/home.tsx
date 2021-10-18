import {NativeStackScreenProps} from "@react-navigation/native-stack"
import React from "react"
import {Button, Colors, Text, View} from "react-native-ui-lib"
import {RootStackParamList} from "../RootStackParamsList"

type homeScreenProp = NativeStackScreenProps<RootStackParamList, "Home">

export const Home = ({navigation}: homeScreenProp) => {
  return (
    <View flex center>
      <Text cyan10 text30BO marginB-s6>
        Wasted App
      </Text>
      <Button margin-s2 black bg-white label={"User"} outlineColor={Colors.black} />
      <Button
        black
        bg-white
        outlineColor={Colors.black}
        label={"Restaurant"}
        onPress={() => navigation.navigate("RestaurantLoginRegistration")}
      />
    </View>
  )
}
