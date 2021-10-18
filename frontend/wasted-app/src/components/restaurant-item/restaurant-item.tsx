import React from "react"
import {ListItem, Text, View} from "react-native-ui-lib"
import {RestaurantItemProps} from "./interfaces"

export const RestaurantItem = ({name, id, address}: RestaurantItemProps) => {
  return (
    <ListItem>
      <ListItem.Part left>
        <Text>{id}</Text>
      </ListItem.Part>
      <ListItem.Part middle>
        <Text flex>{name}</Text>
      </ListItem.Part>
    </ListItem>
  )
}
