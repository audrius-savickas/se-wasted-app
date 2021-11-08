import React from "react"
import {Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {navigateToRestaurantList} from "../../services/navigation"
import {RestaurantInfoProps} from "./interfaces"

export const RestaurantInfo = ({restaurant}: RestaurantInfoProps) => {
  const {name} = restaurant

  return (
    <View margin-s4>
      <View centerH>
        <Text text30M purple20 marginT-s2 marginB-s1>
          {name}
        </Text>
        <Image marginT-s2 source={{uri: restaurant.imageURL, height: 200, width: 330}} />
      </View>
      <View marginT-s6 marginH-s6>
        <View row centerV>
          <Text text60L purple20 style={{width: 120}}>
            Location
          </Text>
          <Text text60L>Vilnius</Text>
        </View>
        <View row centerV marginT-s4>
          <Text text60L purple20 style={{width: 120}}>
            Distance
          </Text>
          <Text>600 m</Text>
        </View>
      </View>
      <View marginT-s6 center>
        <TouchableOpacity bg-purple30 br60 paddingH-s4 paddingV-s2>
          <Text text60L white>
            See restaurant's list of food ↗️
          </Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}
