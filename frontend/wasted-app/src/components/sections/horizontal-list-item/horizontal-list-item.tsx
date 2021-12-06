import React from "react"
import {Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {HorizontalListItemProps} from "./interfaces"

export const HorizontalListItem = ({name, imageURL, tag, onPress}: HorizontalListItemProps) => {
  return (
    <TouchableOpacity margin-s1 centerH onPress={onPress}>
      <Image
        source={{
          uri: imageURL,
          width: 100,
          height: 100
        }}
        style={{width: 100, height: 100}}
      />
      <Text marginT-s1 center style={{width: 100}}>
        {name}
      </Text>
      {tag && (
        <View br20 bg-purple30 padding-s1 paddingH-s2 marginT-s1>
          <Text white text90M>
            {tag}
          </Text>
        </View>
      )}
    </TouchableOpacity>
  )
}
