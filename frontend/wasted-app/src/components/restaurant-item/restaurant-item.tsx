import React from "react"
import {Colors, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {RestaurantItemProps} from "./interfaces"

export const RestaurantItem = ({restaurant, onPress}: RestaurantItemProps) => {
  const {name, address, imageURL} = restaurant

  return (
    <View br40 flex marginH-s8 marginV-s1 style={{borderColor: Colors.grey40, borderWidth: 1}}>
      <Text text50R purple30 marginV-s2 marginH-s3>
        {name}
      </Text>
      <Image source={{uri: imageURL}} style={{height: 130}} />
      <View row margin-s4 centerV>
        <Text flex>{address}</Text>
        <TouchableOpacity onPress={onPress}>
          <Text text60R purple30>
            SEE MORE
          </Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}
