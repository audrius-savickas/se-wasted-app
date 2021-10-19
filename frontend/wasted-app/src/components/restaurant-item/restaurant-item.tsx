import React from "react"
import {StyleSheet} from "react-native"
import {Colors, ListItem, Text, View} from "react-native-ui-lib"
import {RestaurantItemProps} from "./interfaces"

export const RestaurantItem = ({name, id, address}: RestaurantItemProps) => {
  return (
    <ListItem height={100}>
      <View
        flex
        style={{
          borderBottomColor: Colors.black,
          borderBottomWidth: StyleSheet.hairlineWidth
        }}
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
      </View>
    </ListItem>
  )
}
