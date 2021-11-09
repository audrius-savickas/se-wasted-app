import React from "react"
import {Colors, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {formatDistance} from "../../utils/coordinates"
import {RestaurantItemProps} from "./interfaces"

export const RestaurantItem = ({restaurant, onPress}: RestaurantItemProps) => {
  const {name, address, imageURL, distanceToUser} = restaurant

  return (
    <View br40 flex marginH-s8 marginV-s1 style={{borderColor: Colors.grey40, borderWidth: 1}}>
      <View row centerV>
        <View flex>
          <Text text50R purple30 marginV-s2 marginH-s3>
            {name}
          </Text>
        </View>
        <View>
          <Text text60L purple30 marginH-s3>
            {formatDistance(distanceToUser)} km
          </Text>
        </View>
      </View>
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
