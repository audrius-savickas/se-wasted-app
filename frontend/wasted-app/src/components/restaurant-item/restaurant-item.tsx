import React from "react"
import {StyleSheet} from "react-native"
import {Colors, ListItem, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {RestaurantItemProps} from "./interfaces"

export const RestaurantItem = ({name, id, address, onPress}: RestaurantItemProps) => {
  return (
    <ListItem height={100}>
      <TouchableOpacity
        flex
        style={{
          borderBottomColor: Colors.black,
          borderBottomWidth: StyleSheet.hairlineWidth
        }}
        onPress={onPress}
      >
        <View flex row marginH-s4>
          <ListItem.Part left marginR-s4>
            <Text text60L>{id}</Text>
          </ListItem.Part>
          <ListItem.Part middle>
            <Text text50L>{name}</Text>
          </ListItem.Part>
          <ListItem.Part right>
            <Text text60L>{address}</Text>
          </ListItem.Part>
        </View>
      </TouchableOpacity>
    </ListItem>
  )
}
