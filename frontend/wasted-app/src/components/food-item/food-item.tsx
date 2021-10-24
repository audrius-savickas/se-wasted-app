import React from "react"
import {StyleSheet} from "react-native"
import {Colors, ListItem, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {FoodItemProps} from "./interfaces"

export const FoodItem = ({id, name, price}: FoodItemProps) => {
  return (
    <ListItem height={100}>
      <TouchableOpacity
        flex
        style={{
          borderTopColor: Colors.black,
          borderTopWidth: StyleSheet.hairlineWidth
        }}
      >
        <View flex row marginH-s4>
          <ListItem.Part left marginR-s6>
            <Text text60L>{id}</Text>
          </ListItem.Part>
          <ListItem.Part middle>
            <Text text50L>{name}</Text>
          </ListItem.Part>
          <ListItem.Part right marginL-s4>
            <Text text60L>${price}</Text>
          </ListItem.Part>
        </View>
      </TouchableOpacity>
    </ListItem>
  )
}
